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
    public class LayerTracker
    {
        //public List<List<Rectangle>> layers = new List<List<Rectangle>>();
        //public List<List<Texture2D>> textures = new List<List<Texture2D>>();
        //Need a way to return the identity of the item selected.  
        //public List<Rectangle> layer2 = new List<Rectangle>();
        //public List<Rectangle> layer3 = new List<Rectangle>();
        public List<List<Clickable>> layers = new List<List<Clickable>>();

        public delegate void ContainedClicked(object sender, int i, int j);
        public event ContainedClicked ContainedClickedEvent;

        //void ContainedClicked_Continuation(object sender)
        //{

        //}

        public LayerTracker()
        {
            for(int i = 0; i < 10; i++)
            {
                //layers.Add(new List<Rectangle>());
                //textures.Add(new List<Texture2D>());
                layers.Add(new List<Clickable>());
            }
        }

        public void ClearAll()
        {
            for(int i = 0; i < layers.Count; i++)
            {
                layers[i].Clear();
            }
        }

        public void AddClickable(Clickable c, int layer)
        {
            layers[layer].Add(c);

        }

        //public int[] 
        public void Clicked(Rectangle click)
        {
            for(int i = 0; i < layers.Count; i++)
            {
                for(int j = 0; j < layers[i].Count; j++)
                {
                    if (click.Intersects(layers[i][j].hitBox))
                    {
                        //layers[i][j].Click();
                        ContainedClickedEvent(layers[i][j], i, j);
                    }
                }
            }
        }
    }
}
