using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CultivarPre
{
    class CampFireRoom : RoomConstruct
    {
       public CampFireRoom(int W, int D, int X, int Y) : base(W, D, X ,Y)
       {
           
            for(int i=0; i< W + y ; i++){
                for (int j = 0; j < D + x; j++)
                {
                    ROOM[i, j].TileID = 0;
                }

            
            }
            ROOM[y + W/2, x + D/2].TileID = 20;

        }
    }
}
