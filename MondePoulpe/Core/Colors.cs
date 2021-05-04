using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace MondePoulpe.Core
{
    public class Colors
    {
        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = Swatch.AlternateDarkest;
        public static RLColor FloorBackgroundFov = Swatch.DbDark;
        public static RLColor FloorFov = Swatch.Alternate;

        public static RLColor WallBackground = Swatch.SecondaryDarkest;
        public static RLColor Wall = Swatch.Secondary;
        public static RLColor WallBackgroundFov = Swatch.SecondaryDarker;
        public static RLColor WallFov = Swatch.SecondaryLighter;


        //couleur du joueur :
        public static RLColor Player = Swatch.DbLight;

        public static RLColor PhoqueColor = Swatch.DbBrightWood;

        public static RLColor PNJ = Swatch.DbSun;


        public static RLColor TextHeading = RLColor.White;
        public static RLColor Text = Swatch.DbLight;
        public static RLColor Gold = Swatch.DbSun;
    }
}
