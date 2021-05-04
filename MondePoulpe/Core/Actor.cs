using MondePoulpe.Interfaces;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondePoulpe.Core
{
    public class Actor : IActor, IDrawable
    {
        // IActor
        public string Name { get; set; }
        public int Awareness { get; set; }
        public int Attack { get; set; }
        public int AttackChance { get; set; }
        public int Defense { get; set; }
        public int DefenseChance { get; set; }
        public int Food { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Speed { get; set; }
        // IScheduleable
        public int Time
        {
            get
            {
                return Speed;
            }
        }


        // IDrawable
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole console, IMap map)
        {
            // Don't draw actors in cells that haven't been explored
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }

            // Only draw the actor with the color and symbol when they are in field-of-view
            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                // quand on est pas dans le champ de vision, on dessine juste un sol normal avec des "."
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
            }
        }
    }
}
