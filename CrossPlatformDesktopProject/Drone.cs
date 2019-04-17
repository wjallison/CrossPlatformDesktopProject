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
        public double maxResources;
        
        public double range = 100;        
        public StationBlock home;

        public double thetaDotDotMax = 10;

        public PhysEntity subjectEntity;
        public IDictionary<string, double> content = new Dictionary<string, double>();


        #region Orders

        public PhysEntity targetPhysEnt;
        public int targetIndex = 0;
        public bool miningEnabled = false;
        public bool dockingEnabled = false;
        public bool approaching = false;
        public bool targetSet = false;
        public bool miningProx = false;
        public bool harvestingEnabled = false;
        public Vector2 target;
        public Vector2 relTarget = new Vector2(0, 0);
        //public int orderState = 0;

        public enum OrderState
        {
            none = 0,
            goTo = 1,
            mine = 2,
            gather = 3
        }
        public int orderState = 0;




        #endregion

        public Drone() { }
        public Drone(string typeOfDrone,
            string name, 
            double x = 0, double y = 0, double xDot = 0, double yDot = 0, StationBlock homeBlock = null)
        {
            type = typeOfDrone;
            pos = new Vector2((float)x, (float)y);
            posDot = new Vector2(0, 0);
            home = homeBlock;
            idNo = name;
            playerControled = true;
            switch (typeOfDrone)
            {
                case "miningDrone":
                    baseDamage = 10;
                    thrustMax = 1000;
                    maxHealth = 250;
                    health = maxHealth;
                    maxShield = maxHealth * .1;
                    shield = maxShield;
                    mass = 10;
                    diam = 20;
                    maxResources = 0;
                    texture = _globals.textures[1, 1];
                    break;
                case "harvestDrone":
                    baseDamage = 10;
                    thrustMax = 1000;
                    maxHealth = 250;
                    health = maxHealth;
                    maxShield = maxHealth * .1;
                    shield = maxShield;
                    mass = 10;
                    diam = 20;
                    maxResources = 100;
                    texture = _globals.textures[1, 2];
                    break;
            }
            drawPos = new Vector2(
                        pos.X - (float)(diam * .5),
                        pos.Y - (float)(diam * .5)
                        );
            hitBox = new Rectangle((int)drawPos.X, (int)drawPos.Y, (int)diam, (int)diam);
            hitCircle = new Circle(pos, (float)diam);
            foreach(string k in _globals.materials)
            {
                content.Add(k, 0);
            }
        }
        public Drone(Texture2D text,
            double thrust, double diameter,
            string name, double m, double hp,
            double x = 0, double y = 0, double xDot = 0, double yDot = 0, StationBlock homeBlock = null)
        {
            type = "miningDrone";
            baseDamage = 10;
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

            //for (int i = 0; i < resources.Count(); i++)
            //{
            //    resources[i] = 0;
            //}

            foreach (string k in _globals.materials)
            {
                content.Add(k, 0);
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

                if (relTarget.Length() < 5)
                //if(false)
                {
                    if (posDot.X > 0)
                    {
                        if (posDot.X < 5)
                        {
                            posDot.X = 0;
                        }
                        //else { posDot.X = (float)(.7 * posDot.X); }
                    }
                    else
                    {
                        if (posDot.X > -5)
                        {
                            posDot.X = 0;
                        }
                        //else { posDot.X = (float)(.7 * posDot.X); }
                    }

                    if (posDot.Y > 0)
                    {
                        if (posDot.Y < 5)
                        {
                            posDot.Y = 0;
                        }
                        //else { posDot.Y = (float)(.7 * posDot.Y); }
                    }
                    else
                    {
                        if (posDot.Y > -5)
                        {
                            posDot.Y = 0;
                        }
                        //else { posDot.Y = (float)(.7 * posDot.Y); }
                    }
                }
                else
                {
                    //double a = -Math.Pow(posDot.Length(), 2) / (2 * relTarget.Length());
                    if(posDot.X * relTarget.X + posDot.Y * relTarget.Y > 0)
                    {
                        double a = -Math.Pow((posDot.X * relTarget.X + posDot.Y * relTarget.Y) / relTarget.Length(), 2) / (2 * relTarget.Length());
                        if (Math.Abs(a) < thrustMax / mass / 2)
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
                    }
                    else
                    {
                        posDotDot = new Vector2(
                                (float)(thrustMax / relTarget.Length() * relTarget.X / mass),
                                (float)(thrustMax / relTarget.Length() * relTarget.Y / mass));
                    }

                    Vector2 perp = PerpVel();
                    posDotDot.X += -(float).01 * perp.X;
                    posDotDot.Y += -(float).01 * perp.Y;
                }
            }

        }

        public Vector2 PerpVel()
        {
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
            //test(this,new EventArgs());

            if (approaching)
            {
                Approach(targetPhysEnt, targetIndex);
            }
            if (miningEnabled)
            {
                if((pos - targetPhysEnt.pos).Length() < 100)
                {
                    //MineResources
                    //test(this, new EventArgs());
                    miningProx = true;
                }
                else { miningProx = false; }
            }
            TargetUpdate();
        }

        public event EventHandler test;

        public void Approach(PhysEntity target, int ind)
        {
            float x = (float)((target.pos.X - pos.X) / (target.pos - pos).Length() * (target.diam / 2 + 25));
            float y = (float)((target.pos.Y - pos.Y) / (target.pos - pos).Length() * (target.diam / 2 + 25));

            targetPhysEnt = target;
            targetIndex = ind;
            UpdateTarget(-(new Vector2(x, y)) + target.pos);
        }
        public void ApproachPt(Vector2 pt)
        {
            float x = (float)((pt.X - pos.X) / (pt - pos).Length() * (10));
            float y = (float)((pt.Y - pos.Y) / (pt - pos).Length() * (10));

            UpdateTarget(-(new Vector2(x, y)) + pt);
        }

        public double DealDamage()
        {
            return baseDamage;
        }

        #region Orders
        public void Mine(PhysEntity targetEnt, int ind)
        {
            Approach(targetEnt, ind);
            subjectEntity = targetEnt;
            approaching = true;
            miningEnabled = true;
            dockingEnabled = false;
            harvestingEnabled = false;
        }

        public void Harvest(PhysEntity targetEnt, int ind)
        {
            Approach(targetEnt, ind);
            subjectEntity = targetEnt;
            approaching = true;
            miningEnabled = false;
            dockingEnabled = false;
            harvestingEnabled = true;
        }

        public void GoTo(Vector2 posTarget)
        {
            UpdateTarget(posTarget);
            miningEnabled = false;
            dockingEnabled = false;
            approaching = false;
            harvestingEnabled = false;
        }

        public void GoHome()
        {
            UpdateTarget(home.pos);
            dockingEnabled = true;
            miningEnabled = false;
            approaching = false;
            harvestingEnabled = false;
        }

        public void ReceiveResources(Debris debris)
        {
            foreach(string k in debris.content.Keys)
            {
                if(debris.content[k] > 0)
                {
                    content[k] += 1;
                }
                
            }
        }

        public void DonateResources()
        {
            for(int i = 0; i < _globals.materials.Length; i++)
            {
                if(content[_globals.materials[i]] > 0)
                {
                    content[_globals.materials[i]] -= 1;
                }
            }
        }

        public void ReceiveOrder(int order, Vector2 targetPt, PhysEntity targetEnt = null, int targetInd = 0)
        {
            //orderState 
            switch (order)
            {
                case 0:
                    orderState = 0;
                    if (targetEnt != null)
                    {
                        Approach(targetEnt, targetInd);
                        miningEnabled = false;
                        dockingEnabled = false;
                        approaching = true;
                    }
                    else
                    {
                        ApproachPt(targetPt);
                        targetIndex = 0;
                        miningEnabled = false;
                        dockingEnabled = false;
                        approaching = false;
                    }
                    break;
                case 1:
                    orderState = 1;
                    Mine(targetEnt, targetInd);
                    break;
                case 2:
                    orderState = 2;
                    Harvest(targetEnt, targetInd);
                    break;
            }
        }

        #endregion
    }

}
