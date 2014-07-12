using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CultivarPre
{
    class DungeonFloor
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int DungeonWidth = 128;
        public int DungeonHeight = 128;
        public int NumRooms = 30;
        public bool WrapMap = true;
        private MapGenerator DFloorHieghtMap;

        DungeonFloor()
        {
            DFloorHieghtMap = new MapGenerator();
            DFloorHieghtMap.GenerateNewNoiseMap();
            for (int y = 0; y < DungeonHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < DungeonWidth; x++)
                {
                    if (DFloorHieghtMap.OutPutNoiseMap[y, x] > .7)
                    {

                        thisRow.Columns.Add(new MapCell(34));
                    }
                    else
                    {
                        thisRow.Columns.Add(new MapCell(111));
                    }
                }
                Rows.Add(thisRow);
            }
        }

        public int[] MakeRoom(int y, int x, int width, int height)
        {
            int[] finalcoords = new int[2];
            if (!((y + height >= DungeonHeight) || (x + width >= DungeonWidth)))
            {
                for (int Y = y; Y <= y + height; Y++)
                {
                    for (int X = x; X <= x + width; x++)
                    {
                        Rows[Y].Columns[X].TileID = 0;
                        finalcoords[0] = Y;
                        finalcoords[1] = X;
                    }
                }
            }
            return (finalcoords);

        }
   }
}
