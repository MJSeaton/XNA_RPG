using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CultivarPre
{
    static class Tile
    {
        static public Texture2D TileSetTexture;
        static public int TileWidth = 64;
        static public int TileHeight = 64;
        static public int TileStepX = 64;
        static public int TileStepY = 16;
        static public int OddRowXOffset = 32;
        static public int HeightTileOffset = 32;


        /////This texture 2d is used to hold a texture image, and is loaded in the LoadContent() for later drawing
        ////
        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            int tileX = tileIndex % (TileSetTexture .Width / TileWidth);
            return new Rectangle(tileX * TileWidth, tileY *TileHeight, TileWidth, TileHeight);
        }
        /////////this function turns the tile index into a rectangle so multiple tile textures can be saved in 1pic
        /////////


        

    }
}
