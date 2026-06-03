using System;
using System.IO;

namespace Lecture18
{
    public enum HeroClass
    {
        Fighter,
        Ranger,
        Wizard
    }

    internal class Player : Character
    {
        private TextReader input;
        private TextWriter? prompt;

        public HeroClass CharacterClass { get; private set; }

        public Player(string name, int maxHitPoints, int attack, int defense, HeroClass characterClass, TextReader input, TextWriter? prompt = null) :
            base(name, maxHitPoints, attack, defense)
        {
            this.input = input;
            this.prompt = prompt;
            this.CharacterClass = characterClass;
        }

        protected override string ChooseAction()
        {
            while (true)
            {
                if (prompt != null)
                {
                    prompt.WriteLine("\n--- Your Turn ---");
                    prompt.WriteLine("Choose an action:");

                    // Show different flavor text and options based on class
                    if (CharacterClass == HeroClass.Fighter)
                    {
                        prompt.WriteLine("(A)ttack - Weapon Attack (+{0} to hit)", Attack);
                    }
                    else if (CharacterClass == HeroClass.Ranger)
                    {
                        prompt.WriteLine("(A)ttack - Arrow Shooting (+{0} to hit)", Attack);
                    }
                    else if (CharacterClass == HeroClass.Wizard)
                    {
                        prompt.WriteLine("(A)ttack - Basic Staff Strike (+{0} to hit)", Attack);
                        prompt.WriteLine("(F)ireball - High Damage, uses d20 check");
                        prompt.WriteLine("(M)agic Missile - 100% Hit Chance, Moderate Damage");
                    }
                    prompt.WriteLine("(W)ait");
                }

                string? choiceRaw = input.ReadLine();
                if (choiceRaw == null)
                {
                    return TURN_CHOICE_WAIT;
                }

                string choice = choiceRaw.Trim().ToLower();

                // Standard universal actions
                if (choice == "a" || choice == "attack") return TURN_CHOICE_ATTACK;
                if (choice == "w" || choice == "wait") return TURN_CHOICE_WAIT;

                // Wizard-specific actions
                if (CharacterClass == HeroClass.Wizard)
                {
                    if (choice == "f" || choice == "fireball") return "fireball";
                    if (choice == "m" || choice == "magic missile") return "magic_missile";
                }

                if (prompt != null)
                {
                    prompt.WriteLine("Invalid choice!");
                }
            }
        }

        public override void TakeTurn(TextWriter output, Character enemy, Die die)
        {
            string action = ChooseAction();

            switch (action)
            {
                case TURN_CHOICE_ATTACK:
                    if (CharacterClass == HeroClass.Fighter) output.WriteLine("\n{0} charges forward for a fierce Weapon Attack!", Name);
                    else if (CharacterClass == HeroClass.Ranger) output.WriteLine("\n{0} takes aim and unleashes an Arrow Shooting attack!", Name);
                    else if (CharacterClass == HeroClass.Wizard) output.WriteLine("\n{0} swings their magical staff!", Name);

                    AttackEnemy(output, enemy, die);
                    break;

                case TURN_CHOICE_WAIT:
                    output.WriteLine("\n{0} waits and catches their breath...", Name);
                    break;

                case "fireball":
                    CastFireball(output, enemy);
                    break;

                case "magic_missile":
                    CastMagicMissile(output, enemy);
                    break;

                default:
                    output.WriteLine("\n{0} does nothing...", Name);
                    break;
            }
        }

        private void CastFireball(TextWriter output, Character enemy)
        {
            output.WriteLine("\n{0} hurls a massive FIREBALL at {1}!", Name, enemy.Name);

            Random rng = new Random();
            int d20Roll = rng.Next(1, 21);

            // Fireball is a powerful spell: +2 bonus to hit and double base damage
            int totalAttack = d20Roll + Attack + 2;

            if (totalAttack >= enemy.Defense)
            {
                int damage = Attack * 2;
                output.WriteLine("BOOM! The fireball explodes! (Roll: {0} + Mod: {1} = {2} vs Armor: {3})", d20Roll, Attack + 2, totalAttack, enemy.Defense);
                enemy.ReceiveDamage(output, damage);
            }
            else
            {
                output.WriteLine("Miss! {0} dodged the blast! (Roll: {1} + Mod: {2} = {3} vs Armor: {4})", enemy.Name, d20Roll, Attack + 2, totalAttack, enemy.Defense);
            }
        }

        private void CastMagicMissile(TextWriter output, Character enemy)
        {
            output.WriteLine("\n{0} casts MAGIC MISSILE! Three glowing darts shoot toward {1}!", Name, enemy.Name);
            output.WriteLine("Magic Missile never misses!");

            // Magic Missile skips the d20 roll completely and deals fixed damage
            int damage = 12;
            enemy.ReceiveDamage(output, damage);
        }
    }
}
