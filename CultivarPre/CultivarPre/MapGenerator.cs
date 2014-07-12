using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CultivarPre
{
    class MapGenerator
    {
        int MapWidth; //this variable stores the x dimension of the output noisemap size
        int MapHeight; //this variable stores the y dimension of the output noisemap size
        Random RandomGen; // this is the standard c# RNG, used to seed the data in the random pool variable
        public float[,] OutPutNoiseMap;
        
        float[] Random_Pool;
        


        /// <summary>
        /// ////////The constructor simply initializes RandomGen and Random_Pool and fills Random_Pool with values determined by RandomGen
        /// </summary>
        public MapGenerator()
        {
            Random_Pool = new float[20000]; // the Random_Pool contains the random numbers necessary for coherent value noise. It is initialized to contain 1024 floats
            RandomGen = new Random();
            MapWidth = 1025;//init 1025
            MapHeight = 1025;//init 1025
            OutPutNoiseMap = new float[1025, 1025];

            for (int i = 0; i < 20000; i += 1)
            { // loop through each cell in the array, 
                Random_Pool[i] = (float)(2 * RandomGen.NextDouble() - 1); //Since Random.NextDouble() generates a  random double between zero and 1, we can shift the range by multiplying it by two (extending it from range 1 to range 2) and shifting the range to the left by 1 (values that would have been 0 are now negative 1, etc).
            }
        }

        float GetRandom(float x, float y)
        {
          //  if (y % 2 != 0)
          //  {
          //      return Random_Pool[(int)(y + 255 * x) % 1024];
          //  }
            return Random_Pool[(int)(x + 256 * y) % 20000];
        }
        /////////////This function uses the cubic s-curve described in Perlin's original paper, the hemite blending function (3t^2 -2t^3) to interpolate a value for a point inbetween two others
        /////////////
        float InterpolateCubicSCurve(float LeftSample, float RightSample, float position)
        {
            float F = position * position * (3 - 2 * (position));
            return (LeftSample * (1 - F) + RightSample * F);
        }

        ////////////
        ////////////To interpolate multidimensional noise, specifically 2d noise, we pass in two floats that are the x and y indexes of the point being interpolated.
        ////////////We then split these floats into two parts- their integer components to one part and any fractional remainder left when that float is cast to an integer to another
        ////////////We then use the getRandom function to lookup the noise value mapped to the x,y integer in the random_pool and then use the InterpolateCubicSCurve function to 
        ///////////get an interpolated noise value for the position on each axis, then interpolate the results of the previous 2 interpolations at the location fractional Y in order to get the final noise value
        //////////for the point in question 

        float Interpolated2DNoise(float x, float y)
        {
            int IntegerX = (int)x;
            float FractionalX = x - IntegerX;
            int IntegerY = (int)y;
            float FractionalY = y - IntegerY;

            float v1 = GetRandom(IntegerX, IntegerY);
            float v2 = GetRandom(IntegerX + 1, IntegerY);
            float v3 = GetRandom(IntegerX, IntegerY + 1);
            float v4 = GetRandom(IntegerX + 1, IntegerY + 1);

            float i1 = InterpolateCubicSCurve(v1, v2, FractionalX);
            float i2 = InterpolateCubicSCurve(v3, v4, FractionalX);
            return (InterpolateCubicSCurve(i1, i2, FractionalY));
        }
        /////////
        ////////FractalOctaverNoise Takes in an x and y and determines values in a fractal nature by layering waves of noise upon one another. At low frequencies, it produces large features, since the same data is interpolated to 
        ////////a smaller number of points, whereas at high frequencies, it creates local since the resolution is scaled up.   
        ////////
        float FractalOctaverNoise(float x, float y)
        {
            float Total = 0; // this variable holds the total sum of the noise generated in all octaves
            float NumberOfOctaves = 5; //this variable defines the number of octaves used to generate the noise
            float Persistence = .04f; //this variable is used to determine the amplitude at each octave
            for (int i = 0; i < NumberOfOctaves; i++)
            {
                float Frequency = (float) Math.Pow(2 + i , i);
                float Amplitude = (float) Math.Pow(Persistence, i);
                float UnscaledOctaveNoise = Interpolated2DNoise(x * Frequency, y * Frequency);
                Total += UnscaledOctaveNoise * Amplitude;
            }
            return (Math.Min(Math.Max(Total, -1), 1));
        }

        public void GenerateNewNoiseMap()
        {
            OutPutNoiseMap = new float[MapHeight, MapWidth];
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    OutPutNoiseMap[y, x] = FractalOctaverNoise(x, y);
                }
            }
        }





    }
}

