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
    public class UIItem
    {
        public Rectangle box;
        public Texture2D texture;
        public List<UIControl> controls;

        public UIItem(Rectangle rect, Texture2D _texture)
        {
            box = rect;
            texture = _texture;
        }
        public UIItem(Point loc, Point size, Texture2D _texture)
        {
            box = new Rectangle(loc, size);
            texture = _texture;
        }
        public UIItem(Vector2 loc, Vector2 size, Texture2D _texture)
        {
            box = new Rectangle(new Point((int)loc.X, (int)loc.Y), new Point((int)size.X, (int)size.Y));
            texture = _texture;
        }

        public void AddControl(UIControl uIControl)
        {
            controls.Add(uIControl);
        }

        public void AddRelControl(UIControl uIControl)
        {
            uIControl.box.Location = new Point(box.X + uIControl.box.X, box.Y + uIControl.box.Y);
            controls.Add(uIControl);            
        }
    }

    public class UIControl
    {
        public Rectangle box;
        public Texture2D texture;

        public UIControl()
        {

        }
        public UIControl(Point loc, Point size, Texture2D _texture)
        {
            box = new Rectangle(loc, size);
            texture = _texture;
        }
        public UIControl(Vector2 loc, Vector2 size, Texture2D _texture)
        {
            box = new Rectangle(new Point((int)loc.X, (int)loc.Y), new Point((int)size.X, (int)size.Y));
            texture = _texture;
        }
    }
}
