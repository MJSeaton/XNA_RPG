using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CultivarPre
{
    class TemplateConstruct
    {
        TileMap theMap;
        int RoomX;
        int RoomY;
        int Height;
        int Width;

        
        
        
        public TemplateConstruct(TileMap THEMap)
        {
            theMap = THEMap;
            RoomX = 20;
            RoomY = 20;
            Height = 10;
            Width = 20;
        }

        public void PlaceInCenter(int ObjectTileType)
        {

            int toplacex = RoomX + (Width / 2);
            int toplacey = RoomY + (Height / 2);

            theMap.Rows[toplacey].Columns[toplacex].HeightTiles.Add(ObjectTileType);
        }
        public void Hollow(int TileTypeHollowTile, int TileTypeWall)
        {
            for (int x = RoomX; (x < (RoomX + Width)); x++)
            {
                for (int y = RoomY; (y < (RoomY + Height)); y++)
                {
                    theMap.Rows[y].Columns[x].TileID = TileTypeHollowTile;
                }
            }
            for (int x = RoomX ; x < (RoomX + Width) ; x++){
                theMap.Rows[RoomY].Columns[x].TileID= TileTypeWall;
                theMap.Rows[(RoomY + Height)].Columns[x].TileID = TileTypeWall;
            }
            for (int y = RoomY; y < (RoomY + Height); y++)
            {
                theMap.Rows[y].Columns[RoomX].TileID = TileTypeWall;
                theMap.Rows[y].Columns[(RoomX + Height)].TileID = TileTypeWall;
            }
        }

    }
}
