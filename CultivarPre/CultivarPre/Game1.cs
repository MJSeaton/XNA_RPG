using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CultivarPre
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        /// <summary>
        /// /////size of displayed map, etc
        /// /////
        /// </summary>
        int baseOffsetX = -32;
        int baseOffsetY = -64;
        float heightRowDepthMod = 0.0000001f;
        int squaresAcross = 10;
        int squaresDown = 25;
        Texture2D highlight;
        TileMap myMap;
        public Game1()
        {

          
          

            //myMap.DrawLineSE(85, 5, 5, 10);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tile.TileSetTexture = Content.Load<Texture2D>("TILESET5");
            /////////
            //////////
            myMap = new TileMap(Content.Load<Texture2D>("mousemap"));

            myMap.FillRectangle(4, 7, 7, 15, 15);
            int[] Vektah = myMap.GetPointParallel(15, 15, 9, 1);

            myMap.FillRectangle(4, 7, 7, Vektah[0], Vektah[1]);
            Vektah = myMap.FindCenterOf(7, 7, Vektah[0], Vektah[1]);
            myMap.PlaceTileAt(Vektah[0], Vektah[1], 115);
            myMap.PlaceStackAt(Vektah[0], Vektah[1], 70, 3);
            MapGenerator MapGen = new MapGenerator();
            MapGen.GenerateNewNoiseMap();
            myMap.FillMapFromArray(MapGen.OutPutNoiseMap);
            
            ////////Code that follows initializes the Camera
            //////// 

            highlight = Content.Load<Texture2D>("hilight");
            Camera.location.X = 0;
            Camera.location.Y = myMap.MapHeight / 2;
            Camera.ViewWidth = this.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = this.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((myMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((myMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(baseOffsetX, baseOffsetY);
            
            //////
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // scalar is used to change scroll speed
            int scalar = 5;

            KeyboardState ks = Keyboard.GetState();
            //
            if (ks.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //
            if (ks.IsKeyDown(Keys.Left))
            {
                Camera.Move(new Vector2(-2 * scalar, 0));   
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                Camera.Move(new Vector2(2 * scalar, 0));
            }
            if (ks.IsKeyDown(Keys.Up))
            {
                Camera.Move(new Vector2(0, -2 * scalar));
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                Camera.Move(new Vector2(0, 2 * scalar));
            }
            if (ks.IsKeyDown(Keys.O))
            {
                Camera.Move(new Vector2((Camera.WorldHeight / 20) *scalar, (Camera.WorldWidth / 20)* scalar));
            }
            if (ks.IsKeyDown(Keys.P))
            {
                Camera.Move(new Vector2(-(Camera.WorldHeight / 20), -(Camera.WorldWidth / 20)));
            }
                base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;



            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;
            ///
            ///
            float maxdepth = ((myMap.MapWidth + 1) * ((myMap.MapHeight + 1) * Tile.TileWidth)) / 10;
            float depthOffset;

            for (int y= 0; y< squaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) %2 == 1){
                        rowOffset = Tile.OddRowXOffset;
                }
                for (int x = 0; x<squaresAcross; x++)
                {
                    ////
                    ////
                    int mapx = (firstX + x);
                    int mapy = (firstY + y);
                    depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);
                    //
                    //

                    if ((mapx >= myMap.MapWidth) || (mapy >= myMap.MapHeight))
                        continue;
                    

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                       spriteBatch.Draw(
                       Tile.TileSetTexture,
                       Camera.WorldToScreen(
                       
                          new Vector2(( mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                        Tile.GetSourceRectangle(tileID),
                        Color.White,
                        0.0f, 
                        Vector2.Zero,
                        1.0f,
                        SpriteEffects.None,
                        1.0f);

                    }

                    ///////////////
                    ////////////
                    /////////////
                    int heightRow = 0;
                    
                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2(
                                    (mapx * Tile.TileStepX) + rowOffset,
                                    mapy * Tile.TileStepY - (heightRow *Tile.HeightTileOffset))),
                                Tile.GetSourceRectangle(tileID),
                                Color.White,
                                0.0f,
                                Vector2.Zero,
                                1.0f,
                                SpriteEffects.None,
                                depthOffset - ((float)heightRow * heightRowDepthMod));
                        heightRow++;
                    }

                    ///////////
                    //////////
                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                                Tile.GetSourceRectangle(tileID),
                                Color.White,
                                0.0f,
                                Vector2.Zero,
                                1.0f,
                                SpriteEffects.None,
                                depthOffset - ((float)heightRow * heightRowDepthMod));
                    }

                }
            }

            Vector2 highlightLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Point highlightPoint = myMap.WorldToMapCell(new Point((int)highlightLoc.X, (int)highlightLoc.Y));

            int highlightrowOffset = 0;
            if ((highlightPoint.Y) % 2 == 1)
            {
                highlightrowOffset = Tile.OddRowXOffset;
            }
            spriteBatch.Draw(highlight, Camera.WorldToScreen(
                new Vector2(
                    (highlightPoint.X * Tile.TileStepX) + highlightrowOffset,
                    (highlightPoint.Y + 2) * Tile.TileStepY)),
                    new Rectangle(0, 0, 64, 32),
                    Color.White * 0.3f,
                    0.0f,
                    Vector2.Zero,
                    1.0f, SpriteEffects.None,
                    0.0f);
            spriteBatch.End();
        
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
