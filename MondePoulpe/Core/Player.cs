using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondePoulpe.Core
{
    public class Player : Actor
    {
        public Player()
        {
            Awareness = 15;
            Name = "Poulpe";
            Color = Colors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
            Attack = 2;
            AttackChance = 50;
            Defense = 2;
            DefenseChance = 40;
            Food = 0;
            Health = 100;
            MaxHealth = 100;
            Speed = 10;
        }
        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {Health}/{MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {Attack} ({AttackChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {Defense} ({DefenseChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Gold:    {Food}", Colors.Gold);
        }
    }
}

