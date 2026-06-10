using System;
using System.IO;

namespace Lecture18
{
    internal abstract class Character
    {
        protected const string TURN_CHOICE_ATTACK = "attack";
        protected const string TURN_CHOICE_WAIT = "wait";

        private string name;
        private int hitPoints;
        private int maxHitPoints;
        private int attack;
        private int defense;

        public string Name => name;
        public int HitPoints => hitPoints;
        public int MaxHitPoints => maxHitPoints;
        public bool Alive => hitPoints > 0;
        public int Attack => attack;
        public int Defense => defense;

        public Character(string name, int maxHitPoints, int attack, int defense)
        {
            this.name = name;
            this.maxHitPoints = maxHitPoints;
            this.attack = attack;
            this.defense = defense;
            Reset();
        }

        public void Reset()
        {
            hitPoints = maxHitPoints;
        }

        protected abstract string ChooseAction();

        public virtual void TakeTurn(TextWriter output, Character enemy, Die die)
        {
            string action = ChooseAction();

            switch (action)
            {
                case TURN_CHOICE_ATTACK:
                    AttackEnemy(output, enemy, die);
                    break;

                case TURN_CHOICE_WAIT:
                    Wait(output, die);
                    break;

                default:
                    output.WriteLine("{0} does nothing...", name);
                    break;
            }
        }

        protected void AttackEnemy(TextWriter output, Character enemy, Die die)
        {
            Random rng = new Random();
            int d20Roll = rng.Next(1, 21);

            output.WriteLine("{0} swings at {1} and rolls a {2} on the d20...", name, enemy.Name, d20Roll);

            if (d20Roll == 1)
            {
                output.WriteLine("CRITICAL MISS! {0} makes a huge mistake and completely whiffs the attack!", name);
            }
            else if (d20Roll == 20)
            {
                output.WriteLine("CRITICAL HIT! {0} strikes a devastating blow!", name);

                int critDamage = attack * 2;
                enemy.ReceiveDamage(output, critDamage);
            }
            else
            {
                int totalAttack = d20Roll + attack;

                if (totalAttack >= enemy.Defense) 
                {
                    output.WriteLine("Hit! (Roll: {0} + Mod: {1} = {2} vs Armor: {3})", d20Roll, attack, totalAttack, enemy.Defense);

                    enemy.ReceiveDamage(output, attack);
                }
                else
                {
                    output.WriteLine("Miss! (Roll: {0} + Mod: {1} = {2} vs Armor: {3})", d20Roll, attack, totalAttack, enemy.Defense);
                    output.WriteLine("{0}'s attack glances off {1}'s armor.", name, enemy.Name);
                }
            }
        }

        public void ReceiveDamage(TextWriter output, int damage)
        {
            if (damage > 0)
            {
                hitPoints -= damage;
                output.WriteLine("{0} takes {1} damage!", name, damage);
            }
            else
            {
                output.WriteLine("{0} takes no damage!", name);
            }
        }

        private void Wait(TextWriter output, Die die)
        {
            output.WriteLine("{0} waits and rolls a standard die...", name);
            output.WriteLine("They rolled a {0}!", die.Roll());
        }
    }
}
