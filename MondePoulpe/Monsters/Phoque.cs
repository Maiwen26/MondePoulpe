using MondePoulpe.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondePoulpe.Monsters
{
    public class Phoque : Monster
    {
        public static Phoque Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Phoque
            {
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.PhoqueColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Food = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Phoque",
                Speed = 14,
                Symbol = 'P'
            };
        }
    }
}
