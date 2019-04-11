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
         * OBSOLETE:
         * 0: asteroid
         * 1: drone
         * 2: testing station block
         * 3: debris
         * 4: radial menu
         * 5: radial button 1
         * 6: rb 2
         * 7: rb 3
         * 8: rb 4
         * 9: rb 5
         * 10: rb switch
         * 11: bounding box
         * 12: rb1 disabled
         * 13: rb2 dis
         * 14: rb3 dis
         * 15: rb4 dis
         * 16: rb5 dis
         * 17: rb switch dis
         * 18: laser effect
         * 19: miningDrone
         * 20: harvestDrone
         * 21: harpoonDrone
         * 22: Core (station core)
         * 23: StationDock
         * 
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
         * [3,x]: debris
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
         * 
         * 
         * 
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
            "Cu",
            "CO2",
            "Money",
            "Fuel"
        };
    }
}
