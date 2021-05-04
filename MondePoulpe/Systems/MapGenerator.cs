using MondePoulpe.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondePoulpe.Systems
{
    public class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;

        private readonly Ocean _map;

        // la construction d'une nouvelle map necessite ses dimensions
        public MapGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new Ocean();
        }

        // Generer une nouvelle map avec des murs autours
        public Ocean CreateMap()
        {
            // Chaque cellule de la map est initialisée à True quand on peut y marcher, quand elle est transparente ou déjà explorée.
            
            _map.Initialize(_width, _height);
            foreach (Cell cell in _map.GetAllCells())
            {
                _map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }
            // Mettre la première et la dernière ligne de la map pas transparente ou alors sur laquelle on ne peut pas marcher
            foreach (Cell cell in _map.GetCellsInRows(0, _height - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            // Mettre la première et la dernière colonne de la map pas transparente ou alors sur laquelle on ne peut pas marcher
            foreach (Cell cell in _map.GetCellsInColumns(0, _width - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            return _map;
        }
    }
}
