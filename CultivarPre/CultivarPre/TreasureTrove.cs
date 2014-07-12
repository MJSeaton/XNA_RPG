using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CultivarPre
{
    class TreasureTrove
    {
        public int TileImageIndex;
        public int[] Location;
        public bool Opened;
        public TreasureTrove(int locX, int locY)
        {
            Opened = false;
            TileImageIndex = 70;
            Location = new int[2]{locX, locY };

        }


    }
}
