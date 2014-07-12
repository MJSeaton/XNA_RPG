﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CultivarPre
{
    static class Camera
    {
        public static int ViewWidth { get; set; }
        public static int ViewHeight { get; set; }
        public static int WorldWidth { get; set; }
        public static int WorldHeight { get; set; }
        public static Vector2 DisplayOffset { get; set; }
        /// <summary>
        /// /view variables represent the area covered by the camera
        /// </summary>
        static public Vector2 location = Vector2.Zero;

      
        public static Vector2 Location
        {
            get
            {
                return location;
            }
            set
            {
                location = new Vector2(MathHelper.Clamp(value.X, 0f, WorldWidth - ViewWidth),
                   MathHelper.Clamp(value.Y, 0f, WorldHeight - ViewHeight));
            }
        }
        /////////////
        ////////////Transform and move camera functions follow
        /////////////
        public static Vector2 WorldToScreen(Vector2 WorldPosition)
        {
            return WorldPosition - Location + DisplayOffset;
        }
        public static Vector2 ScreenToWorld(Vector2 ScreenPosition)
        {
            return ScreenPosition + Location - DisplayOffset;
        }
        public static void Move(Vector2 Offset)
        {
            Location += Offset;
        }
    }
}
