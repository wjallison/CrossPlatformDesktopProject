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
        public bool[,] isStationBlockGrid = new bool[11, 11];
        public double boxSize = 30;
        public bool displayBuildOptions = false;

        public Rectangle buildOptionsRect;

        public BuildOptionsMenu buildOptionsMenu;

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
                    isStationBlockGrid[i, j] = false;

                }
            }
        }
        public BuildScreen(GraphicsDeviceManager graphics)
        {
            buildOptionsRect = new Rectangle(
                (int)(graphics.PreferredBackBufferWidth / 4),
                (int)(graphics.PreferredBackBufferHeight / 8),
                (int)(graphics.PreferredBackBufferWidth / 2),
                (int)(graphics.PreferredBackBufferHeight / 8 * 6)
                );
            buildOptionsMenu = new BuildOptionsMenu(buildOptionsRect);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    textureGrid[i, j] = _globals.textures[6, 1];
                    boxGrid[i, j] = new Rectangle(
                        (int)((i - 5) * boxSize),
                        (int)((j - 5) * boxSize),
                        (int)boxSize, (int)boxSize);
                    isStationBlockGrid[i, j] = false;

                }
            }
        }

        public void Update(Station station)
        {
            for(int i = 0; i < station.blocks.Count; i++)
            {
                int x = (int)station.blocks[i].gridPos[0];
                int y = (int)station.blocks[i].gridPos[1];
                textureGrid[x, y] = station.blocks[i].texture;
                isStationBlockGrid[x, y] = true;
            }
        }

        public Texture2D linearIterTextures(int i)
        {
            int x = (int)(i / 11);
            int y = i % 11;
            return textureGrid[x, y];
        }
        public Rectangle linearIterRects(int i)
        {
            int x = (int)(i / 11);
            int y = i % 11;
            return boxGrid[x, y];
        }
    }

    public class BuildOptionsMenu
    {
        public Texture2D texture;
        public Rectangle baseRect;
        public List<Button> buttons;
        public Button exitButton;

        public BuildOptionsMenu(Rectangle rect)
        {
            baseRect = rect;
            texture = _globals.textures[6, 0];

        }

        public void ShowBuildOptions()
        {
            //buttons.Add(new Button(_globals.textures[2, 0], baseRect, new Vector2(10, 10), 20, 20));
            //buttons[0].AddText("Core")
            buttons.Add(new Button(_globals.textures[2, 1], baseRect, new Vector2(10, 40), 20, 20));
            buttons[0].AddText("Mining Drone Dock", new Vector2(30, 0));
        }

    }




    public class Button
    {
        public Texture2D texture;
        public Rectangle rect;
        public string content;
        public Vector2 contentPos;


        public Button(Texture2D text, int x, int y, int width, int height)
        {
            texture = text;
            rect = new Rectangle(x, y, width, height);
        }
        public Button(Texture2D text, Rectangle container, Vector2 relPos, int width, int height)
        {
            texture = text;
            rect = new Rectangle(
                (int)(container.X + relPos.X),
                (int)(container.Y + relPos.Y),
                width, height);
        }
        public void AddText(string txt, Vector2 relPos)
        {
            content = txt;
            contentPos = relPos + new Vector2((float)rect.X, (float)rect.Y);
        }
    }
}
