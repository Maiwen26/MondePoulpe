using System;
namespace MondePoulpe.Core
{
    public class Raie:PNJ
    {
        public Raie()
        {
        }

        public static Raie Create(int level)
        {
            return new Raie
            {
                Awareness = 10,
                Color = Colors.PNJ,
                Name = "Raies",
                Speed = 0,
                Symbol = 'R',
            };
        }
    }
}
