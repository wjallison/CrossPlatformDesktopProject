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
    public class Drone : PhysEntity
    {
        public double thrustMax;
        public double negThrustMax;
        public double maxShield;
        public double shield;
        public double baseDamage;
        public bool miningEnabled = false;
        public bool dockingEnabled = false;
        public bool approaching = false;
        public double range = 75;

        public PhysEntity targetPhysEnt;

        public int physEntListTargetIndex;

        public StationBlock home;

        public double thetaDotDotMax = 10;

        public bool targetSet = false;
        public Vector2 target;
        public Vector2 relTarget = new Vector2(0, 0);

        private double thrust;

        public int[] resources = new int[15];


        //public Drone() { }
        public Drone(Texture2D text,
            double thrust, double diameter,
            string name, double m, double hp,
            double x = 0, double y = 0, double xDot = 0, double yDot = 0, StationBlock homeBlock = null)
        {
            type = "drone";
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
            hitCircle = new Circle(pos, (float)diam);

            for (int i = 0; i < resources.Count(); i++)
            {
                resources[i] = 0;
            }

            texture = text;
            playerControled = true;

            if(homeBlock != null)
            {
                home = homeBlock;
            }
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

                if (relTarget.Length() < 10)
                {
                    if (posDot.X > 0)
                    {
                        if (posDot.X < 5)
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
            if (shield > damage)
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
            if (approaching)
            {
                Approach(ref targetPhysEnt);
            }
            TargetUpdate();
        }

        public void Approach(ref PhysEntity target)
        {
            float x = (float)((target.pos.X - pos.X) / (target.pos - pos).Length() * (target.diam / 2 + 50));
            float y = (float)((target.pos.Y - pos.Y) / (target.pos - pos).Length() * (target.diam / 2 + 50));

            targetPhysEnt = target;
            UpdateTarget(new Vector2(x, y));
        }

        public double DealDamage()
        {
            return baseDamage;
        }




        #region Orders
        public void Mine(PhysEntity target)
        {
            Approach(ref target);
            approaching = true;
            miningEnabled = true;
            dockingEnabled = false;
        }

        public void GoTo(Vector2 posTarget)
        {
            UpdateTarget(posTarget);
            miningEnabled = false;
            dockingEnabled = false;
            approaching = false;
        }
        public void GoTo(PhysEntity a)
        {
            Approach(ref a);

            miningEnabled = false;
            dockingEnabled = false;
            approaching = true;
        }

        public void GoHome()
        {
            UpdateTarget(home.pos);
            dockingEnabled = true;
            miningEnabled = false;
            approaching = false;
        }

        #endregion
    }

    //public class MiningDrone : Drone
    //{

    //}
}
