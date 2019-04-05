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
    public class Circle
    {
        public Vector2 center;
        public float diam;

        public Circle()
        {

        }
        public Circle(Vector2 pos, float d)
        {
            center = pos;
            diam = d;
        }

        public bool Overlaps(Circle other)
        {
            Vector2 delta = other.center - center;
            return (delta.Length() <= other.diam / 2 + diam / 2);
        }
        public bool Contains(Vector2 pt)
        {
            Vector2 delta = pt - center;
            return (delta.Length() <= diam / 2);
        }
    }
}
