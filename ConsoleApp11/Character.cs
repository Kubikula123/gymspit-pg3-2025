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

        // --- NEW BG3 / d20 COMBAT SYSTEM ---
        protected void AttackEnemy(TextWriter output, Character enemy, Die die)
        {
            // We use a local Random here to guarantee a 1-20 roll, 
            // regardless of how your existing 'Die' class is set up.
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

                // For a critical hit, we double the base attack damage
                int critDamage = attack * 2;
                enemy.ReceiveDamage(output, critDamage);
            }
            else
            {
                // Standard hit check: d20 roll + attack modifier vs enemy's static defense (Armor)
                int totalAttack = d20Roll + attack;

                if (totalAttack >= enemy.Defense) // "Meets it, beats it" rule
                {
                    output.WriteLine("Hit! (Roll: {0} + Mod: {1} = {2} vs Armor: {3})", d20Roll, attack, totalAttack, enemy.Defense);

                    // Normal damage based on the attack stat
                    enemy.ReceiveDamage(output, attack);
                }
                else
                {
                    output.WriteLine("Miss! (Roll: {0} + Mod: {1} = {2} vs Armor: {3})", d20Roll, attack, totalAttack, enemy.Defense);
                    output.WriteLine("{0}'s attack glances off {1}'s armor.", name, enemy.Name);
                }
            }
        }

        // Renamed from ReceiveAttack to ReceiveDamage, as the hit/miss logic is now fully handled above
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
        // ------------------------------------

        private void Wait(TextWriter output, Die die)
        {
            output.WriteLine("{0} waits and rolls a standard die...", name);
            output.WriteLine("They rolled a {0}!", die.Roll());
        }
    }
}
