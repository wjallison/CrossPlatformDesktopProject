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

        public RadialMenu()
        {

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
        public Texture2D texture;
        
        public RadialButton()
        {

        }
        public RadialButton(Rectangle r, Texture2D t)
        {
            box = r;
            texture = t;
        }
    }
}
