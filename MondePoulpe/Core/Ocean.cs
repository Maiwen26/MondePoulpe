using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondePoulpe.Core
{
    public class Ocean : Map
    {
        // le méthode dessin sera appelé à chaque fois que la map est mise à jour
        // Il permet de mettre des symboles à chaque cellule de la map.
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            // quand une cellule n'est pas encore exploré, nous avons pas besoin de dessiné quelques choses
            if (!cell.IsExplored)
            {
                return;
            }

            //quand une cellule est dans son champ de vision alors elle sera dessiné avec des couleurs plus claires
            if (IsInFov(cell.X, cell.Y))
            {
                //les murs sont représentés par des "#" et le sol par des "."
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            // quand une cellule n'est pas dans son champ de vision, elle est dessiné avec des couleurs foncées
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }
        //Cette méthode sera appelé à chaque fois mouvement du joueur pour mettre à jour son champ de vision
        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            //le calcul du champ de vision est basé sur la localisation du joueur et des alentours
            ComputeFov(player.X, player.Y, player.Awareness, true);
            //Toutes les cellules du champs de vision qui ont déjà été explorées sont marquées.
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
    }
}
