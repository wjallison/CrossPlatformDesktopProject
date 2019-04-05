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
    public class PhysEntity
    {

        //single doubles
        public double mass;
        public double maxHealth;
        public double health;
        public double theta;
        public double thetaDot;
        public double diam;
        public double damResist = 100; //% out of 100
        public Rectangle hitBox;
        public bool radialMenuFollows = false;
        public bool initialized = true;
        public bool playerControled;

        public Texture2D texture;

        //double doubles
        //public double[] pos = new double[] { 0, 0 };  //Note: pos is to center of mass.
        //public double[] posDot = new double[] { 0, 0 };
        //public double[] posDotDot = new double[] { 0, 0 };
        //public double[] drawPos = new double[] { 0, 0 }; //Note: drawPos is to upper left corner.
        public Vector2 pos, posDot, posDotDot, drawPos;

        //strings
        public string idNo;

        //bools
        public bool solid = true;

        public PhysEntity()
        {
            initialized = false;
        }

        public void Update(GameTime gameTime)
        {
            individualUpdate();
            posDot.X = (float)(posDot.X + posDotDot.X * gameTime.ElapsedGameTime.TotalSeconds);
            posDot.Y = (float)(posDot.Y + posDotDot.Y * gameTime.ElapsedGameTime.TotalSeconds);

            pos.X = (float)(pos.X + posDot.X * gameTime.ElapsedGameTime.TotalSeconds);
            pos.Y = (float)(pos.Y + posDot.Y * gameTime.ElapsedGameTime.TotalSeconds);


            drawPos.X = pos.X - (float)(diam * .5);
            drawPos.Y = pos.Y - (float)(diam * .5);

            hitBox.Location = new Point((int)drawPos.X, (int)drawPos.Y);

            theta = theta + thetaDot * gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void individualUpdate() { }

        public virtual void TakeDamage(double damage)
        {
            damage = damage * damResist / 100;

            health = health - damage;
        }

        public bool ApproxEquals(double a, double b)
        {
            if(Math.Abs(a) + Math.Abs(b) == 0) { return true; }
            if ((a-b) / (a+b) < .0001) { return true; }
            return false;
        }
    }

    public class Drone : PhysEntity
    {
        public double thrustMax;
        public double negThrustMax;
        public double maxShield;
        public double shield;

        public double thetaDotDotMax = 10;

        public bool targetSet = false;
        public Vector2 target;
        public Vector2 relTarget = new Vector2(0, 0);

        private double thrust;

        public int[] resources = new int[15];

        

        public Drone(Texture2D text,
            double thrust, double diameter, 
            string name, double m, double hp, 
            double x = 0, double y = 0, double xDot = 0, double yDot = 0)
        {
            idNo = name;

            theta = 0;
            thetaDot = 0;
            pos = new Vector2((float)x, (float)y);
            posDot = new Vector2((float)xDot, (float)yDot);
            posDotDot = new Vector2(0, 0);

            diam = diameter;

            thrustMax = thrust;
            negThrustMax = .5 * thrustMax;

            
            maxHealth = hp;
            health = maxHealth;

            maxShield = maxHealth * .1;
            shield = maxShield;

            mass = m;

            drawPos = new Vector2(
                pos.X - (float)(diam * .5),
                pos.Y - (float)(diam * .5)
                );

            hitBox = new Rectangle((int)drawPos.X, (int)drawPos.Y, (int)diam, (int)diam);

            for(int i = 0; i < resources.Count(); i++)
            {
                resources[i] = 0;
            }

            texture = text;
            playerControled = true;
        }



        public void UpdateTarget(Vector2 t)
        {
            target = t;
            relTarget = target - pos;
            targetSet = true;
        }

        public void TargetUpdate()
        {
            if (targetSet)
            {
                relTarget = target - pos;

                if(relTarget.Length() < 10)
                {
                    if(posDot.X > 0)
                    {
                        if(posDot.X < 5)
                        {
                            posDot.X = 0;
                        }
                        else { posDot.X = (float)(.7 * posDot.X); }
                    }
                    else
                    {
                        if (posDot.X > -5)
                        {
                            posDot.X = 0;
                        }
                        else { posDot.X = (float)(.7 * posDot.X); }
                    }

                    if (posDot.Y > 0)
                    {
                        if (posDot.Y < 5)
                        {
                            posDot.Y = 0;
                        }
                        else { posDot.Y = (float)(.7 * posDot.Y); }
                    }
                    else
                    {
                        if (posDot.Y > -5)
                        {
                            posDot.Y = 0;
                        }
                        else { posDot.Y = (float)(.7 * posDot.Y); }
                    }
                }

                if (ApproxEquals(Math.Atan2(relTarget.Y, relTarget.X), theta))
                {
                    thetaDot = 0;
                    double a = -Math.Pow(posDot.Length(), 2) / (2 * relTarget.Length());
                    if (Math.Abs(a) < negThrustMax)
                    {
                        posDotDot = new Vector2(
                            (float)(thrustMax / relTarget.Length() * relTarget.X / mass),
                            (float)(thrustMax / relTarget.Length() * relTarget.Y / mass));
                    }
                    else
                    {
                        posDotDot = new Vector2(
                            (float)(-negThrustMax / relTarget.Length() * relTarget.X / mass),
                            (float)(-negThrustMax / relTarget.Length() * relTarget.Y / mass));
                    }
                }
                else
                {
                    posDotDot = new Vector2(0, 0);
                    thetaDot = 10 * (Math.Atan2(relTarget.Y, relTarget.X) - theta);
                }
            }
            
        }

        public override void TakeDamage(double damage)
        {
            if(shield > damage)
            {
                shield = shield - damage;
                return;
            }
            else
            {
                damage -= shield;
                shield = 0;

                damage = damage * damResist / 100;

                health = health - damage;
            }

            //if(health < 0)
            //{

            //}
        }

        public override void individualUpdate()
        {
            TargetUpdate();
        }

    }

    public class Asteroid : PhysEntity
    {
        public IDictionary<string, double> content = new Dictionary<string, double>();


        public Asteroid(Texture2D text, int lane, double diameter, string name, double m, double x = 0, double y = 0, double xDot = 0, double yDot = 0)
        {

            #region content based on lane
            Random rand = new Random();
            switch (lane)
            {
                case 0:
                    for(int i = 0; i < _globals.materials.Count(); i++)
                    {
                        if(_globals.materials[i] == "Fe")
                        {
                            content.Add("Fe", 100 * 0.4 * rand.NextDouble());
                        }
                        else if(_globals.materials[i] == "H2O")
                        {
                            content.Add("H2O", 100 * 0.4 * rand.NextDouble());
                        }
                        else if(_globals.materials[i] == "Cu")
                        {
                            content.Add("Cu", 100 * 0.2 * rand.NextDouble());
                        }
                        else
                        {
                            content.Add(_globals.materials[i], 0);
                        }
                        content["Rock"] = 100 - content["Fe"] - content["H2O"] - content["Cu"];
                    }
                    break;
            }
            #endregion

            idNo = name;

            theta = 0;
            thetaDot = 0;
            pos = new Vector2((float)x, (float)y);
            posDot = new Vector2((float)xDot, (float)yDot);
            posDotDot = new Vector2(0, 0);

            diam = diameter;

            maxHealth = Math.Pow(diameter * .5, 2);
            health = maxHealth;

            mass = m;

            drawPos = new Vector2(
                pos.X - (float)(diam * .5),
                pos.Y - (float)(diam * .5)
                );

            hitBox = new Rectangle((int)drawPos.X, (int)drawPos.Y, (int)diam, (int)diam);

            texture = text;
            playerControled = false;
        }
        public override void TakeDamage(double damage)
        {
            damage = damage * damResist / 100;

            health = health - damage;
        }
    }

    
    public class Station
    {
        public List<StationBlock> blocks = new List<StationBlock>();

        public void AddBlock(StationBlock s)
        {
            blocks.Add(s);
        }
    }

    public class StationBlock : PhysEntity
    {

    }

    public struct _selectedPhysEnt
    {
        public int index;
        public PhysEntity entity;

        public _selectedPhysEnt(int ind, PhysEntity ent)
        {
            index = ind;
            entity = ent;
        }
    }
}
