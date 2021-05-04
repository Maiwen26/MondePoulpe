using MondePoulpe.Core;
using MondePoulpe.Interfaces;
using MondePoulpe.Systems;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondePoulpe.Behaviors
{
    public class StandardMoveAndAttack : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            Ocean ocean = Game.Ocean;
            Player player = Game.Player;
            FieldOfView monsterFov = new FieldOfView(ocean);

            // Si le monstre n'a pas été alerté, calculez un champ de vision  
            //Utilisez la valeur vision du monstre pour la distance dans le contrôle du champ de vision.
            // Si le joueur se trouve dans le champ de vision du monstre, il doit l'alerter.
            // Ajouter un message au MessageLog concernant ce statut d'alerte
            if (!monster.TurnsAlerted.HasValue)
            {
                monsterFov.ComputeFov(monster.X, monster.Y, monster.Awareness, true);
                if (monsterFov.IsInFov(player.X, player.Y))
                {
                    Game.MessageLog.Add($"{monster.Name} est prêt à se battre contre le {player.Name}");
                    monster.TurnsAlerted = 1;
                }
            }

            if (monster.TurnsAlerted.HasValue)
            {
                // Avant de trouver un chemin, assurez-vous de rendre les cellules des monstres et des joueurs praticables à pied.
                ocean.SetIsWalkable(monster.X, monster.Y, true);
                ocean.SetIsWalkable(player.X, player.Y, true);

                PathFinder pathFinder = new PathFinder(ocean);
                Path path = null;

                try
                {
                    path = pathFinder.ShortestPath(
                    ocean.GetCell(monster.X, monster.Y),
                    ocean.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException)
                {
                    // Le monstre peut voir le joueur, mais ne peut pas trouver le chemin vers lui.
                    // Cela peut être dû à d'autres monstres qui bloquent le chemin.
                    // Ajoutez un message au journal des messages indiquant que le monstre attend.
                    Game.MessageLog.Add($"{monster.Name} attend un tour");
                }

                // N'oubliez pas de remettre le statut de walkable à false
                ocean.SetIsWalkable(monster.X, monster.Y, false);
                ocean.SetIsWalkable(player.X, player.Y, false);

                // Dans le cas où il y a un chemin, dites au système de commande de déplacer le monstre.
                if (path != null)
                {
                    try
                    {
                        // TODO: This should be path.StepForward() but there is a bug in RogueSharp V3
                        // The bug is that a Path returned from a PathFinder does not include the source Cell
                        commandSystem.MoveMonster(monster, (Cell)path.Steps.First());
                    }
                    catch (NoMoreStepsException)
                    {
                        Game.MessageLog.Add($"{monster.Name} est frustré");
                    }
                }

                monster.TurnsAlerted++;

                // Perte du statut d'alerte tous les 15 tours. 
                // Tant que le joueur est toujours dans le champ de vision, le monstre reste en alerte.
                // Sinon, le monstre cessera de poursuivre le joueur.
                if (monster.TurnsAlerted > 15)
                {
                    monster.TurnsAlerted = null;
                }
            }
            return true;
        }
    }
}
