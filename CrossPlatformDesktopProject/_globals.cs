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
    public static class _globals
    {
        /*
         * NEW:
         * [0,x]: asteroid
         * 
         * [1,0]: drone
         * [1,1]: miningDrone
         * [1,2]: harvestDrone
         * [1,3]: harpoonDrone
         * 
         * [2,0]: Core (station core)
         * [2,1]: StationDock
         * 
         * [3,0]: debris
         * 
         * [4,0]: radial menu
         * [4,1]: rb 1
         * [4,...]
         * [4,5]: rb 5
         * [4,6]: rb switch
         * [4,7]: rb1 disabled
         * [4,...]:
         * [4,11]: rb5 disabled
         * 
         * [5] Effects
         * [5,0] laser effect
         * 
         * [6] UI
         * [6,0] bounding box 
         * [6,1] building menu blank space     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
         */
        public static Texture2D[,] textures = new Texture2D[30,15];

        

        public static string[] materials = new string[]
        {
            "Waste",
            "Fe",
            "Rock",
            "Au",
            "Ag",
            "Pt",
            "U",
            "Cu",
            "H2O",
            "H2",
            "O2",
            "CO2",
            "Money",
            "Fuel"
        };

        public static double Dot(Vector2 a, Vector2 b)
        {
            double ret = a.X * b.X + a.Y * b.Y;
            return ret;
        }
        public static Vector2 Rot(bool up, Vector2 vect)
        {
            Vector2 ret = new Vector2(1, 1);
            if (up)
            {
                ret.X = -1 * vect.Y;
                ret.Y = vect.X;
            }
            else
            {
                ret.X = vect.Y;
                ret.Y = -1 * vect.X;
            }

            return ret;
        }
        public static Vector2 VectFromRect(Rectangle r)
        {
            return new Vector2((float)r.X, (float)r.Y);
        }
        //public static 
    }

    public struct DroneDefs
    {
        string type;
        double maxThrust;
        double maxHealth;
        double mass;
        double diameter;
        
        public DroneDefs(string t, double mT, double mH, double m, double d)
        {
            type = t;
            maxThrust = mT;
            maxHealth = mH;
            mass = m;
            diameter = d;
        }
    }
}
