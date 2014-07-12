using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CultivarPre
{
    class RoomConstruct
    {

        public MapCell[,] ROOM;
        public int y;
        public int x;
        public RoomConstruct(int W, int D, int X, int Y)
        {
            y = Y;
            x = X;
            ROOM = new MapCell[W, D];
            
        }
    }
}
