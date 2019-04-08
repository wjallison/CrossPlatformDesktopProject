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
        public bool isOn = false;
        public Vector2 center = new Vector2(0, 0);
        public Rectangle drawBox = new Rectangle(new Point(-50, -50), new Point(100, 100));
        public Rectangle[] buttonRectangles = new Rectangle[5];
        public RadialButton[] buttons = new RadialButton[5];

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
        public RadialMenu(Texture2D[] textures)
        {
            for(int i = 0; i < 5; i++)
            {
                buttonRectangles[i] = new Rectangle(
                    new Point((int)(100 * Math.Cos(90 - i * 18)), (int)(100 * Math.Sin(90 - i * 18))),
                    new Point(50, 50)
                    );
                buttons[0] = new RadialButton(buttonRectangles[i], textures[i]);
            }
        }

        //public void Update

        public void Update(PhysEntity selected, Clickable focus = null)
        {
            switch (focus.type)
            {
                case "asteroid":

                    break;

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
            //textureDisabled = td;
        }
    }
}
