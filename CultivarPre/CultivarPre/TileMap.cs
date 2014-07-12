using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CultivarPre
{
    class MapRow
    {
        public List<MapCell> Columns = new List<MapCell>();
    }
    /// <summary>
    /// Maprow uses a list collection to string together a row of cells. 
    /// Add rows to another list to make a 2d array
    /// </summary>
    class TileMap
    {
        public bool[,] BooleanTreasure = new bool[512, 512];
        public List<TreasureTrove> TreasureMap = new List<TreasureTrove>();
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 1024;
        public int MapHeight = 1024;
        private Random RNDGEN= new Random();
        private Texture2D mouseMap;
        /// <summary>
        /// /FillRectangle takes in an int for tile type, width, an int for height, an int for x.start and an int for y.start
        /// / Rows[y].Columns[x].TileID = rnd.Next(84, 90);
        /// </summary>
        /// 
        public void FillMapFromArray(float[,] FillingArray)
        {
            int xstart = 0;
            int ystart = MapHeight/2;
            int y = MapHeight / 2;
            int x = 0;
            //////////
            //////////Underneath this is the code that determines how to turn the noisemaparray into a tilemap
            //////////first, generate the overarching fractal landscape you be applied by imposing a cutoff and filling the rest with tiles
            for (int i = 0; i < MapHeight / 2; i++)
            {
                for (int j = 0; j < MapWidth / 2; j++)
                {
                    if (Rows[y].Columns[x].TileID == 0)
                    { /// new code to prevent overwrites
                        if (FillingArray[i, j] >= .9)
                        {
                            Rows[y].Columns[x].HeightTiles.Add(4);
                        }
                        if (FillingArray[i, j] >= 0.8)
                        {
                            Rows[y].Columns[x].TileID = 3;

                        }
                        else if (FillingArray[i, j] >= .3)
                        {
                            Rows[y].Columns[x].TileID = 2;
                        }
                        else
                        {
                            Rows[y].Columns[x].TileID = 0;
                        }
                        if (BooleanTreasure[i, j] != false)
                        {
                            if (RNDGEN.Next(0, 4) < 3)
                            {
                                int w = RNDGEN.Next(3, 6);
                                int h = RNDGEN.Next(3, 6);
                                FillRectangle(115,w, h, x, y);
                                int[] Center = new int[2];
                                Center = FindCenterOf(w, h, x, y);
                                PlaceTileAt(Center[0], Center[1], 17);
                            }
                            else
                            {
                                Rows[y].Columns[x].HeightTiles.Add(116);
                            }

                        }
                    }
                            
                        
                
                    if (y % 2 != 0)
                    {
                        x++;
                        y++;
                    }
                    else
                    y++;
                }
                if (ystart % 2 != 0)
                {
                    xstart++;
                    ystart--;
                }
                else
                {
                    ystart--;
                }
                x = xstart;
                y = ystart;
            }
        }
        /// 
        ///
        ////

    



        /// <summary>
        /// //
        /// fills in treasure and POI's based on the treasure map;
        /// </summary>
        /// <param name="TileType"></param>
        /// <param name="width"></param>
        /// <param name="startx"></param>
        /// <param name="starty"></param>
        public void FillLine(int TileType, int width, int startx, int starty)
        {
            int counter = 0;
            for (int x = startx; counter < width; x = x)
            {
                for (int y = starty; counter < width; y++)
                {

                    Rows[y].Columns[x].TileID = TileType;

                    if (y % 2 != 0)
                    {
                        x++;
                    }
                    counter++;
                }
           }
        }

        /////////
        //////////
        public void FillRectangle(int TileType, int width, int height, int startx, int starty)
        {
            int tempx = startx;
            int tempy = starty;
            for (int i = 0; i < height; i++)
            {
                FillLine(TileType, width, tempx, tempy);
                if (tempy % 2 != 0)
                {
                    tempx++;
                }
                tempy--;
            }
        }        


        ////////
        ////////
        public int[] FindCenterOf(int width, int height, int startx, int starty)
        {
            int tempx = startx;
            int tempy = starty;
            for (int i = 0; i < (height/2); i++)
            {
                    if (tempy % 2 != 0)
                    {
                        tempx++;
                    }
                    tempy--;
            }
               for (int j = 0; j< (width/2);j++){
                    if( tempy % 2 !=0)
                    {
                       tempx++;
                   }
                    tempy++;
                
                }
            
            return (new int[2] { tempx, tempy });
        }        

        /////////
        /////////
        public void PlaceTileAt(int x, int y, int TileType)
        {
            Rows[y].Columns[x].TileID = TileType;
        }
        /// <summary>
        /// 
        public void PlaceStackAt(int x, int y, int TileStart, int StackWidth)
        {
            for (int i = TileStart; i < TileStart + StackWidth; i++)
            {
                Rows[y].Columns[x].HeightTiles.Add(i);
            }
        }
        /// <returns></returns>
        public int[] GetPointParallel(int initx, int inity, int width, int direction)
        {
            int tempx=0;
            int tempy=0;
            int counter = 0;
            int[] ArrayToReturn = new int[2];



            if (direction == 1)
            {
                for (tempx = initx; counter < width; tempx = tempx)
                {
                    for (int y = inity; counter < width; y++)
                    {

                        if (y % 2 != 0)
                        {
                            if (y != inity)
                            {
                                tempx++;
                            }
                        }
                        counter++;
                        tempy = y;
                    }

                 
                }
            
            }
            ArrayToReturn[0] = tempx;
            ArrayToReturn[1] = tempy;
            return(ArrayToReturn);

        }
        ////////
        ////////
        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            Point mapCell = new Point(
                (int)(worldPoint.X / mouseMap.Width),
                ((int)(worldPoint.Y / mouseMap.Height)) * 2
                );
            int localPointX = worldPoint.X % mouseMap.Width;
            int localPointY = worldPoint.Y % mouseMap.Height;

            int dx = 0;
            int dy = 0;

            uint[] myUint = new uint[1];

            if (new Rectangle(0, 0, mouseMap.Width, mouseMap.Height).Contains(localPointX, localPointY))
            {
                mouseMap.GetData(0, new Rectangle(localPointX, localPointY, 1, 1), myUint, 0, 1);

                if (myUint[0] == 0xFF0000FF) // Red
                {
                    dx = -1;
                    dy = -1;
                    localPointX = localPointX + (mouseMap.Width / 2);
                    localPointY = localPointY + (mouseMap.Height / 2);
                }
                if (myUint[0] == 0xFF00FF00) //Green
                {
                    dx = -1;
                    dy = 1;
                    localPointX = localPointX + (mouseMap.Width / 2);
                    localPointY = localPointY - (mouseMap.Height / 2);
                }
                if (myUint[0] == 0xFF00FFFF) // Yellow
                {
                    dy = -1;
                    localPointX = localPointX - (mouseMap.Width / 2);
                    localPointY = localPointY + (mouseMap.Height / 2);
                }
                if (myUint[0] == 0xFFFF0000) // Blue
                {
                    dy = +1;
                    localPointX = localPointX - (mouseMap.Width / 2);
                    localPointY = localPointY - (mouseMap.Height / 2);
                }
            }
            mapCell.X += dx;
            mapCell.Y += dy - 2;

            localPoint = new Point(localPointX, localPointY);

            return mapCell;
        }

        public Point WorldToMapCell(Point worldPoint)
        {
            Point dummy;
            return WorldToMapCell(worldPoint, out dummy);
        }


        /////////
        ////////Constructor follows
        public TileMap(Texture2D MouseMap)
        {
            this.mouseMap = MouseMap;
            int treasurefreq = 200;
            Random rnd = new Random();
            ////////put 200 pieces of treasure in each quardrant
            //////// 
            for (int i = 0; i < treasurefreq; i++)
            {
                BooleanTreasure[(rnd.Next(6,(512 / 2))), (rnd.Next(6,(512 / 2)))] = true;
                TreasureMap.Add(new TreasureTrove(rnd.Next(6,(512 / 2)), rnd.Next(6,(MapWidth / 2))));
            }
            //////////
            ///////////
            for (int i = 0; i < treasurefreq; i++)
            {
                BooleanTreasure[(rnd.Next((512 / 2), 512-6)), rnd.Next((512 / 2), 512 -6)] = true;
                TreasureMap.Add(new TreasureTrove(rnd.Next((512 / 2), 512-6), rnd.Next((512 / 2), 512 -6)));
            }
            /////
            //////////
            for (int i = 0; i < treasurefreq; i++)
            {
                BooleanTreasure[(rnd.Next(6, (512 / 2))), (rnd.Next((512 / 2) + 6,512) - 6)] = true;
                TreasureMap.Add(new TreasureTrove(rnd.Next(6, (512 / 2)), rnd.Next((512/ 2) + 6,512) - 6));
            }
            /////////////
            ///////////
            for (int i = 0; i < treasurefreq; i++)
            {
                BooleanTreasure[(rnd.Next((512 / 2), 512 -6)), (rnd.Next(6, (512 / 2) -6))] = true;
                TreasureMap.Add(new TreasureTrove(rnd.Next((512 / 2), 512 -6), rnd.Next(6, (512 / 2) -6)));
            }
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    
                    thisRow.Columns.Add(new MapCell(0));
                }
                Rows.Add(thisRow);
            }
           
                    // Create Sample Map Data
           

            // End Create Sample Map Data
        }
    }
}
