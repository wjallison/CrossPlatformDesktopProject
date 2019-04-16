using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrossPlatformDesktopProject
{
    public class BuildScreen
    {
        public Texture2D[,] textureGrid = new Texture2D[11, 11];
        public Rectangle[,] boxGrid = new Rectangle[11, 11];
        public double boxSize = 50;

        public BuildScreen()
        {
            for(int i = 0; i < 11; i++)
            {
                for(int j = 0; j < 11; j++)
                {
                    textureGrid[i, j] = _globals.textures[6, 1];
                    boxGrid[i, j] = new Rectangle(
                        (int)((i - 5) * boxSize), 
                        (int)((j - 5) * boxSize), 
                        (int)boxSize, (int)boxSize);

                }
            }
        }


    }
}
