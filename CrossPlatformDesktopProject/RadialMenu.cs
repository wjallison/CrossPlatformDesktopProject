﻿using System;
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
        public int followingType = 0;
        public bool isOn = false;
        public Vector2 center = new Vector2(0, 0);
        public Rectangle drawBox = new Rectangle(new Point(-50, -50), new Point(100, 100));
        public Rectangle[] buttonRectangles = new Rectangle[5];
        public RadialButton[] buttons = new RadialButton[5];
        public Texture2D texture;
        public double diam = 100;

        public bool switchToAvaliable = false;
        public RadialButton SwitchButton;

        public PhysEntity follow;
        
        public RadialMenu()
        {
            texture = _globals.textures[4,0];
            for (int i = 0; i < 5; i++)
            {
                buttonRectangles[i] = new Rectangle(
                    new Point((int)(40 * Math.Cos(90 - i * 18)), (int)(40 * Math.Sin(90 - i * 18))),
                    new Point(50, 50)
                    );
                buttons[i] = new RadialButton(buttonRectangles[i], _globals.textures[4,i + 1], _globals.textures[4, i + 7]);

                SwitchButton = new RadialButton(
                    new Rectangle(new Point((int)(center.X - 25), (int)(center.Y - 25)), new Point(50, 50)),
                    _globals.textures[4,6]
                    );
            }
        }

        public void Off()
        {
            isFollowing = false;
            isOn = false;
            follow.RadialMenuFollowingOnOff(false);
        }

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
            Center(selected.pos);
        }

        public void ChangeTarget(PhysEntity selected)
        {
            isOn = true;
            if (follow != null)
            {
                follow.UpdateEvent -= follow_updateEvent;
                follow.RadialMenuFollowingOnOff(false);
            }
            follow = selected;
            follow.UpdateEvent += follow_updateEvent;
            follow.RadialMenuFollowingOnOff(true);
            isFollowing = true;
        }

        void follow_updateEvent(object sender)
        {
            //s(PhysEntity)sender
            if (isFollowing)
            {
                center = follow.pos;
                drawBox.Location = new Point((int)center.X - (int)(diam / 2), (int)center.Y - (int)(diam / 2));
                ButtonsSpatial();
            }
            
        }

        public void Center(Vector2 pt)
        {
            center = pt;
            drawBox.Location = new Point((int)center.X - (int)(diam/2), (int)center.Y - (int)(diam / 2));
            ButtonsSpatial();
        }
        public void ButtonsSpatial()
        {
            for (int i = 0; i < 5; i++)
            {
                int xMod = (int)(60 * Math.Cos(3.14 / 180 * (90 - i * 18)));
                int yMod = -(int)(60 * Math.Sin(3.14 / 180 * (90 - i * 18)));


                buttonRectangles[i] = new Rectangle(
                    new Point(
                        (int)(50 * Math.Cos(3.14/180 * (90 - i * 18))) + (int)center.X, 
                    (int)center.Y - (int)(50 * Math.Sin(3.14 / 180 * (90 - i * 18)))),
                    new Point(20, 20)
                    );
                buttons[i].box = buttonRectangles[i];
            }
            SwitchButton.box = new Rectangle(new Point((int)(center.X - 25), (int)(center.Y - 25)), new Point(50, 50));
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

    public class SelectedAreaMenu
    {
        public Rectangle area;
        public bool active = false;
        public List<SelectedAreaButton> buttons = new List<SelectedAreaButton>();
        public Texture2D texture;

        public SelectedAreaMenu()
        {
            texture = _globals.textures[6, 2];
            buttons.Add(new SelectedAreaButton());
        }

        public void Activate(Rectangle r)
        {
            area = r;
            active = true;

            Rectangle buttonBox = new Rectangle(r.X + r.Width / 2 - 10, r.Y + r.Height - 20, 20, 20);
            buttons[0].box = buttonBox;
        }

        public void Deactivate()
        {
            active = false;
        }
        //public void Activate(MouseTracker mt)
        //{

        //}
    }

    public class SelectedAreaButton
    {
        public Rectangle box;
        public Texture2D textureEnabled;
        public Texture2D textureDisabled;
        public bool enabled = false;

        public SelectedAreaButton(Texture2D tex, Rectangle rect, Texture2D texD)
        {
            box = rect;
            textureEnabled = tex;
            textureDisabled = texD;
        }
        public SelectedAreaButton() { }
    }
}
