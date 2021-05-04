using MondePoulpe.Behaviors;
using MondePoulpe.Systems;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondePoulpe.Core
{
    public class Monster : Actor
    {
        public int? TurnsAlerted { get; set; }
        public void DrawStats(RLConsole statConsole, int position)
        {
            // Commencez à Y=13 qui est en dessous des statistiques du joueur.
            // Multipliez la position par 2 pour laisser un espace entre chaque statistique.
            int yPosition = 13 + (position * 2);

            // Commencez la ligne en imprimant le symbole du monstre dans la couleur appropriée
            statConsole.Print(1, yPosition, Symbol.ToString(), Color);

            // Déterminez la largeur de la barre de santé en divisant la santé actuelle par la santé maximale.
            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            // Définir les couleurs d'arrière-plan de la barre de santé pour indiquer le degré d'endommagement du monstre.
            statConsole.SetBackColor(3, yPosition, width, 1, Swatch.Primary);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Swatch.PrimaryDarkest);

            // Imprimer le nom des monstres au-dessus de la barre de santé
            statConsole.Print(2, yPosition, $": {Name}", Swatch.DbLight);
        }

       

        public virtual void PerformAction(CommandSystem commandSystem)
        {
            var behavior = new StandardMoveAndAttack();
            behavior.Act(this, commandSystem);
        }
    }
}
