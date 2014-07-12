using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CultivarPre
{
    class MapCell
    {
        public List<int> BaseTiles = new List<int>();
        public List<int> HeightTiles = new List<int>();
        public List<int> TopperTiles = new List<int>();
        public void AddTopperTile(int tileID)
        {
            TopperTiles.Add(tileID);
        }

        public void AddHeightTile(int tileID)
        {
            HeightTiles.Add(tileID);
        }

        public int TileID
        {
            get { return BaseTiles.Count > 0 ? BaseTiles[0] : 0; }
            set
            {
                if (BaseTiles.Count > 0)
                    BaseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }
        /// <summary>
        /// /
        /// ///
        public void AddBaseTile(int tileID)
        {
            BaseTiles.Add(tileID);
        }

        /// </summary>
        /// <param name="tileID"></param>
        public MapCell(int tileID)
        {
            TileID = tileID;
        }

    }
}
