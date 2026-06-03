namespace Lecture18
{
    internal class Die
    {
        Random random;

        int sides;


        public Die(Random random, int sides)
        {
            if (sides < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.random = random;
            this.sides = sides;
        }


        public int Roll()
        {
            return random.Next(sides) + 1;
        }
    }
}

       