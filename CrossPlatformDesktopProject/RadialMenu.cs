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
    public class RadialMenu
    {
        public bool isFollowing = false;
        public int followingIndex = 0;
        public bool isOn = false;
        public Vector2 center = new Vector2(0, 0);
        public Rectangle drawBox = new Rectangle(new Point(-50, -50), new Point(100, 100));
        public Rectangle[] buttonRectangles = new Rectangle[5];
        public RadialButton[] buttons = new RadialButton[5];
        public Texture2D texture;

        public bool switchToAvaliable = false;
        public RadialButton SwitchButton;

        //public Enum buttons { }

        //RadialMenu Items
        /*
         * GoTo
         * Attack / Mine
         * Gather
         * Attach
         * Dock
         * 
         * 
         */
        //public RadialMenu(Texture2D[] textures)
        //{
        //    for(int i = 0; i < 5; i++)
        //    {
        //        buttonRectangles[i] = new Rectangle(
        //            new Point((int)(60 * Math.Cos(90 - i * 18)), (int)(60 * Math.Sin(90 - i * 18))),
        //            new Point(50, 50)
        //            );
        //        buttons[0] = new RadialButton(buttonRectangles[i], textures[i]);
        //    }
        //}
        public RadialMenu()
        {
            texture = _globals.textures[4];
            for (int i = 0; i < 5; i++)
            {
                buttonRectangles[i] = new Rectangle(
                    new Point((int)(60 * Math.Cos(90 - i * 18)), (int)(60 * Math.Sin(90 - i * 18))),
                    new Point(50, 50)
                    );
                buttons[i] = new RadialButton(buttonRectangles[i], _globals.textures[i + 5], _globals.textures[i + 12]);

                SwitchButton = new RadialButton(
                    new Rectangle(new Point((int)(center.X - 25), (int)(center.Y - 25)), new Point(50, 50)),
                    _globals.textures[10],
                    _globals.textures[17]
                    );
            }
        }

        public void Off()
        {
            isFollowing = false;
            isOn = false;
        }

        //public void Update
        //public void UpdateToSpace
        public void UpdateOff()
        {
            isFollowing = false;
            isOn = false;
        }

        public void UpdateSpace(Vector2 pt)
        {
            isFollowing = false;
            isOn = true;

            Center(pt);
            //ButtonsSpatial();
        }

        public void SetState(bool[] states)
        {
            for(int i = 0; i < 5; i++)
            {
                buttons[i].enabled = states[i];
            }
            SwitchButton.enabled = states[5];
        }

        public void UpdateAsteroid(Asteroid a, int ind)
        {
            isFollowing = true;
            isOn = true;
            followingIndex = ind;

            Center(a.pos);
            //ButtonsSpatial();
        }

        public void Follow(PhysEntity phys, int ind)
        {
            isFollowing = true;
            isOn = true;
            followingIndex = ind;

            Center(phys.pos);
        }
        public void Update(PhysEntity selected, Clickable focus = null)
        {
            center = selected.pos;
        }

        public void Center(Vector2 pt)
        {
            center = pt;
            drawBox.Location = new Point((int)center.X, (int)center.Y);
            ButtonsSpatial();
        }
        public void ButtonsSpatial()
        {
            for (int i = 0; i < 5; i++)
            {
                buttonRectangles[i] = new Rectangle(
                    new Point((int)(60 * Math.Cos(90 - i * 18)) + (int)center.X, 
                    (int)center.Y + (int)(60 * Math.Sin(90 - i * 18))),
                    new Point(50, 50)
                    );
                buttons[0].box = buttonRectangles[i];
            }
        }
    }

    public class RadialButton
    {
        public Rectangle box;
        public Texture2D textureEnabled;
        public Texture2D textureDisabled;
        public bool enabled = false;
        
        public RadialButton()
        {

        }
        public RadialButton(Rectangle r, Texture2D te, Texture2D td = null)
        {
            box = r;
            textureEnabled = te;
            textureDisabled = td;
        }
    }
}
