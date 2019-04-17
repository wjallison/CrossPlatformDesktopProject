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

        public int transactionStage;
        public List<string> transactionResources = new List<string>();
        public List<double> transactionResCost = new List<double>();
        public string transactionTargetType;
        public string transactionTarget;
        public int transactionTargetX, transactionTargetY;

        public delegate void ReduceResources(string resource, double qty);
        public event ReduceResources reduceResourcesEvent;

        public delegate void AddStationBlock(string type, int x, int y);
        public event AddStationBlock addStationBlockEvent;

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
                        (int)((i) * boxSize),
                        (int)((j) * boxSize),
                        (int)boxSize, (int)boxSize);
                    isStationBlockGrid[i, j] = false;

                }
            }
        }

        public void ProceedWithTransaction()
        {
            if(transactionStage < transactionResources.Count-1)
            {
                transactionStage++;
                reduceResourcesEvent(transactionResources[transactionStage], transactionResCost[transactionStage]);
            }
            else
            {

                switch (transactionTargetType)
                {
                    case "station":
                        addStationBlockEvent(transactionTarget, transactionTargetX, transactionTargetY);
                        break;
                    
                }

            }
        }

        public void MouseClick(Rectangle m)
        {
            if (buildOptionsMenu.enabled)
            {
                if (m.Intersects(buildOptionsMenu.baseRect))
                {
                    for(int i = 0; i < buildOptionsMenu.buttons.Count; i++)
                    {
                        if (buildOptionsMenu.buttons[i].enabled)
                        {
                            if (m.Intersects(buildOptionsMenu.buttons[i].rect))
                            {
                                transactionTarget = buildOptionsMenu.buttons[i].target;
                                transactionTargetType = buildOptionsMenu.buttons[i].targetType;
                                transactionResources = buildOptionsMenu.buttons[i].costString;
                                transactionResCost = buildOptionsMenu.buttons[i].costDouble;
                                transactionTargetX = buildOptionsMenu.buttons[i].targetX;
                                transactionTargetY = buildOptionsMenu.buttons[i].targetY;
                                transactionStage = 0;
                                reduceResourcesEvent(transactionResources[0], transactionResCost[0]);
                            }
                        }
                    }
                }
                else
                {
                    buildOptionsMenu.enabled = false;
                }
            }
            else
            {
                for(int i = 0; i < 11; i++)
                {
                    for(int j = 0; j < 11; j++)
                    {
                        if (m.Intersects(boxGrid[i, j]))
                        {
                            buildOptionsMenu.ShowBuildOptions(i, j);
                        }
                    }
                }
            }
        }

        public void Update(Station station)
        {
            for(int i = 0; i < station.blocks.Count; i++)
            {
                int x = (int)station.blocks[i].gridPos[0] + 5;
                int y = (int)station.blocks[i].gridPos[1] + 5;
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
        public List<Button> buttons = new List<Button>();
        public Button exitButton;
        public bool enabled = false;

        

        public BuildOptionsMenu(Rectangle rect)
        {
            baseRect = rect;
            texture = _globals.textures[6, 0];

        }

        public void ShowBuildOptions(int x, int y)
        {
            
            buttons.Clear();

            buttons.Add(new Button(_globals.textures[2, 1], baseRect, new Vector2(10, 40), 20, 20));
            buttons[0].AddText("Mining Drone Dock", new Vector2(30, 0),0,0);
            buttons.Add(new Button(_globals.textures[2, 1], baseRect, new Vector2(10, 70), 20, 20));
            buttons[1].AddText("Harvest Drone Dock", new Vector2(30, 0), 0, 0);
            //buttons[0].

            enabled = true;
        }

        public void ShowUpgradeOptions(StationBlock s)
        {
            buttons.Clear();



            enabled = true;
        }

    }




    public class Button
    {
        public Texture2D texture;
        public Rectangle rect;
        public string content;
        public Vector2 contentPos;
        public bool visible;
        public bool enabled;

        public string target;
        public string targetType;
        public List<string> costString = new List<string>();
        public List<double> costDouble = new List<double>();
        public int targetX = 0;
        public int targetY = 0;


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
        public void AddText(string txt, Vector2 relPos, int x = 0, int y = 0)
        {
            content = txt;
            contentPos = relPos + new Vector2((float)rect.X, (float)rect.Y);

            switch (txt)
            {
                case "Mining Drone Dock":
                    targetType = "station";
                    target = "stationDock";
                    costString.Add("Fe");
                    costDouble.Add(10);
                    targetX = x;
                    targetY = y;
                    break;
                case "Harvest Drone Dock":
                    targetType = "station";
                    target = "stationDockHarvester";
                    costString.Add("Fe");
                    costDouble.Add(10);
                    targetX = x;
                    targetY = y;
                    break;
            }
        }
        //public void AddCost()
    }
}
