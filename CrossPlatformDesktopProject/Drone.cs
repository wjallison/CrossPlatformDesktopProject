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
        
        public double range = 75;

        

        public int physEntListTargetIndex;

        public StationBlock home;

        public double thetaDotDotMax = 10;

        

        //private double thrust;

        public int[] resources = new int[15];


        #region Orders

        public PhysEntity targetPhysEnt;
        public int targetIndex;
        public bool miningEnabled = false;
        public bool dockingEnabled = false;
        public bool approaching = false;
        public bool targetSet = false;
        public Vector2 target;
        public Vector2 relTarget = new Vector2(0, 0);

        public enum OrderState
        {
            none = 0,
            goTo = 1,
            mine = 2,
            gather = 3
        }
        public int orderState = 0;




        #endregion


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
                else
                {
                    double a = -Math.Pow(posDot.Length(), 2) / (2 * relTarget.Length());
                    if(Math.Abs(a) < thrustMax / mass)
                    {
                        posDotDot = new Vector2(
                            (float)(thrustMax / relTarget.Length() * relTarget.X / mass),
                            (float)(thrustMax / relTarget.Length() * relTarget.Y / mass));
                    }
                    else
                    {
                        posDotDot = new Vector2(
                            -(float)(thrustMax / relTarget.Length() * relTarget.X / mass),
                            -(float)(thrustMax / relTarget.Length() * relTarget.Y / mass));
                    }

                    Vector2 perp = PerpVel();
                    posDotDot.X += -(float).01 * perp.X;
                    posDotDot.Y += -(float).01 * perp.Y;
                }
                //if (ApproxEquals(Math.Atan2(relTarget.Y, relTarget.X), theta))
                //{
                //    thetaDot = 0;
                //    double a = -Math.Pow(posDot.Length(), 2) / (2 * relTarget.Length());
                //    if (Math.Abs(a) < negThrustMax)
                //    {
                //        posDotDot = new Vector2(
                //            (float)(thrustMax / relTarget.Length() * relTarget.X / mass),
                //            (float)(thrustMax / relTarget.Length() * relTarget.Y / mass));
                //    }
                //    else
                //    {
                //        posDotDot = new Vector2(
                //            (float)(-negThrustMax / relTarget.Length() * relTarget.X / mass),
                //            (float)(-negThrustMax / relTarget.Length() * relTarget.Y / mass));
                //    }
                //}
                //else
                //{
                //    posDotDot = new Vector2(0, 0);
                //    thetaDot = 10 * (Math.Atan2(relTarget.Y, relTarget.X) - theta);
                //}
            }

        }

        public Vector2 PerpVel()
        {
            //double dot = _globals.Dot(posDot, target);
            Vector2 perpToTarget = _globals.Rot(true, relTarget);
            double dot = _globals.Dot(posDot, perpToTarget) / perpToTarget.Length();
            return new Vector2((float)(dot * perpToTarget.X), (float)(dot * perpToTarget.Y));
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
        //    public enum OrderState
        //{
        //    none = 0,
        //    goTo = 1,
        //    mine = 2,
        //    gather = 3
        //}
            //switch (orderState)
            //{
            //    case 0:
            //        if(home != null)
            //        {
            //            ApproachPt(home.pos);
            //            //Approach(ref p);
            //        }
            //        else { UpdateTarget(pos); }
            //        break;
            //    case 1:

            //        break;
            //    case 2:

            //        break;
            //}

            if (approaching)
            {
                Approach(targetPhysEnt, targetIndex);
            }
            TargetUpdate();
        }

        public void Approach(PhysEntity target, int ind)
        {
            float x = (float)((target.pos.X - pos.X) / (target.pos - pos).Length() * (target.diam / 2 + 50));
            float y = (float)((target.pos.Y - pos.Y) / (target.pos - pos).Length() * (target.diam / 2 + 50));

            targetPhysEnt = target;
            targetIndex = ind;
            UpdateTarget(new Vector2(x, y));
        }
        public void ApproachPt(Vector2 pt)
        {
            float x = (float)((pt.X - pos.X) / (pt - pos).Length() * (10));
            float y = (float)((pt.Y - pos.Y) / (pt - pos).Length() * (10));

            UpdateTarget((new Vector2(x, y)) + pt);
        }

        public double DealDamage()
        {
            return baseDamage;
        }

        #region Orders
        public void Mine(PhysEntity target, int ind)
        {
            Approach(target, ind);
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
        //public void GoTo(PhysEntity a)
        //{
        //    Approach(a);

        //    miningEnabled = false;
        //    dockingEnabled = false;
        //    approaching = true;
        //}

        public void GoHome()
        {
            UpdateTarget(home.pos);
            dockingEnabled = true;
            miningEnabled = false;
            approaching = false;
        }

        public void ReceiveOrder(int order, Vector2 targetPt, PhysEntity targetEnt = null, int targetInd = 0)
        {
            //orderState 
            switch (order)
            {
                case 0:
                    if(targetEnt != null)
                    {
                        Approach(targetEnt, targetInd);
                        miningEnabled = false;
                        dockingEnabled = false;
                        approaching = true;
                    }
                    else
                    {
                        ApproachPt(targetPt);
                    }
                    break;
            }
        }

        #endregion
    }

    //public class MiningDrone : Drone
    //{

    //}
}
