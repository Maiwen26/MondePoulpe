using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MondePoulpe.Core
{
    public class Ocean : Map
    {
        
        public List<Rectangle> Rooms;

        public List<Rectangle> Rooms;


        public List<Rectangle> Rooms { get; set; }       
        public List<Door> Doors { get; set; }
        public Stairs StairsUp { get; set; }
        public Stairs StairsDown { get; set; }

        //Constructeur
        public Ocean()
        {
            Game.SchedulingSystem.Clear();
            // Initialiser la liste des salles lorsque nous créons un nouvel Océan
            _monsters = new List<Monster>();
            _pnj = new List<PNJ>();

            Rooms = new List<Rectangle>(); 
            Doors = new List<Door>();
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
<<<<<<< HEAD

        // Retourne vrai si l'acteur peut être placé sur la cellule ou faux sinon.
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            // N'autoriser le placement d'un acteur que si la cellule est praticable
            if (GetCell(x, y).IsWalkable)
            {
                //La cellule sur laquelle se trouvait l'acteur est maintenant praticable.
                SetIsWalkable(actor.X, actor.Y, true);
                //  Mise à jour de la position de l'acteur
                actor.X = x;
                actor.Y = y;
                // La nouvelle cellule sur laquelle se trouve l'acteur n'est pas accessible
                SetIsWalkable(actor.X, actor.Y, false);
                // Essayez d'ouvrir une porte s'il y en a une ici
                OpenDoor(actor, x, y);
                // N'oubliez pas de mettre à jour le champ de vision si nous venons de repositionner le joueur.
                if (actor is Player)
                {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }

        // Renvoie la porte à la position x,y ou null si elle n'est pas trouvée.
        public Door GetDoor(int x, int y)
        {
            return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        // L'acteur ouvre la porte située à la position x,y
        private void OpenDoor(Actor actor, int x, int y)
        {
            Door door = GetDoor(x, y);
            if (door != null && !door.IsOpen)
            {
                door.IsOpen = true;
                var cell = GetCell(x, y);
                // Une fois que la porte est ouverte, elle doit être marquée comme transparente et ne plus bloquer le champ de vision.
                SetCellProperties(x, y, true, cell.IsWalkable, cell.IsExplored);

                Game.MessageLog.Add($"{actor.Name} a ouvert une porte");
            }
        }


        // Appelé par MapGenerator après la génération d'une nouvelle carte pour ajouter le joueur à la carte.
=======
        
        // Called by MapGenerator after we generate a new map to add the player to the map
>>>>>>> 0f031e3... Modif pnj
        public void AddPlayer(Player player)
        {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
            Game.SchedulingSystem.Add((Interfaces.IScheduleable)player);
        }


        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster);
            // Après avoir ajouté le monstre à la carte, assurez-vous que la cellule n'est pas praticable.
            SetIsWalkable(monster.X, monster.Y, false);
            Game.SchedulingSystem.Add((Interfaces.IScheduleable)monster);
        }

        public void RemoveMonster(Monster monster)
        {
            _monsters.Remove(monster);
            //Après avoir retiré le monstre de la carte, assurez-vous que la cellule est de nouveau praticable.
            SetIsWalkable(monster.X, monster.Y, true);
            Game.SchedulingSystem.Remove((Interfaces.IScheduleable)monster);
        }

        public Monster GetMonsterAt(int x, int y)
        {
            return _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }


        public bool CanMoveDownToNextLevel()
        {
            Player player = Game.Player;
            return StairsDown.X == player.X && StairsDown.Y == player.Y;
        }


        // Une méthode d'aide pour définir la propriété IsWalkable d'une cellule.
        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = (Cell)GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }


        // The Draw method will be called each time the map is updated
        // It will render all of the symbols/colors for each cell to the map sub console
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
            // Keep an index so we know which position to draw monster stats at
            int i = 0;

            // Iterate through each monster on the map and draw it after drawing the Cells
            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);
                // When the monster is in the field-of-view also draw their stats
                if (IsInFov(monster.X, monster.Y))
                {
                    // Pass in the index to DrawStats and increment it afterwards
                    monster.DrawStats(statConsole, i);
                    i++;
                }
            }
            foreach (Door door in Doors)
            {
                door.Draw(mapConsole, this);
            }
            // Add the following code after we finish drawing doors.
            StairsUp.Draw(mapConsole, this);
            StairsDown.Draw(mapConsole, this);

            foreach (PNJ pnj in _pnj)
            {
                pnj.Draw(mapConsole, this);
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
        

        
        

        

        

       

        // Look for a random location in the room that is walkable.
        public Point GetRandomWalkableLocationInRoom(Rectangle room)
        {
            if (DoesRoomHaveWalkableSpace(room))
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    if (IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }

            // If we didn't find a walkable location in the room return null
            return default;
        }

        // Iterate through each Cell in the room and return true if any are walkable
        public bool DoesRoomHaveWalkableSpace(Rectangle room)
        {
            for (int x = 1; x <= room.Width - 2; x++)
            {
                for (int y = 1; y <= room.Height - 2; y++)
                {
                    if (IsWalkable(x + room.X, y + room.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
<<<<<<< HEAD
        

       
=======

        public void AddPNJ(PNJ pnj)
        {
            _pnj.Add(pnj);
            // Ajout d'un pnj, le poulpe ne pourra alors pas marcher sur cette case
            SetIsWalkable(pnj.X, pnj.Y, false);
        }

        public void RemovePNJ(PNJ pnj)
        {
            _pnj.Remove(pnj);
            // Retirer le PNJ du jeu et vérifier que le poulpe peut bien marcher dessus
            SetIsWalkable(pnj.X, pnj.Y, true);
        }

        public PNJ GetPNJAt(int x, int y)
        {
            return _pnj.FirstOrDefault(m => m.X == x && m.Y == y);
        }
>>>>>>> 0f031e3... Modif pnj
    }
   
}
