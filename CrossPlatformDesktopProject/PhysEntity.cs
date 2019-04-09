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
    //public class NonCollideEntity : Clickable
    //{
    //    public double diam;
    //    public Rectangle hitBox;
    //    //public Circle hitCircle;
    //    public bool radialMenuFollows = false;
    //    public bool playerControlled;
    //    public string idNo;

    //    public Texture2D texture;

    //    public Vector2 pos, posDot, posDotDot, drawPos;

    //    public double theta;
    //    public double thetaDot;

    //    public void Update(GameTime gameTime)
    //    {

    //    }

    //    public virtual void IndividualUpdate(GameTime gameTime) { }
    //}

    public class PhysEntity : Clickable
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
        public Circle hitCircle;
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
            hitCircle.center = pos;

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
            type = "asteroid";

            theta = 0;
            thetaDot = 0.01f * PlusMinusOne();
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
            hitCircle = new Circle(pos, (float)diam);

            texture = text;
            playerControled = false;
        }
        public override void TakeDamage(double damage)
        {
            damage = damage * damResist / 100;

            health = health - damage;
        }
    }

    //public class Debris : NonCollideEntity
    public class Debris : PhysEntity
    {
        public IDictionary<string, double> content = new Dictionary<string, double>();
        //public double diam;
        //public Rectangle hitBox;
        ////public Circle hitCircle;
        //public bool radialMenuFollows = false;
        //public bool playerControlled;
        //public string idNo;

        //public Texture2D texture;

        //public Vector2 pos, posDot, posDotDot, drawPos;

        public Debris(Asteroid source)
        {
            //Random rand = new Random();
            solid = false;

            pos = source.pos;
            posDot = source.posDot;
            posDot.X += (float)(5 * PlusMinusOne());
            posDot.Y += (float)(5 * PlusMinusOne());

            theta = 0;
            thetaDot = 0.01f * PlusMinusOne();

            diam = 25;
            drawPos = new Vector2((float)(pos.X - 12.5), (float)(pos.Y - 12.5));
            hitBox = new Rectangle(new Point((int)drawPos.X, (int)drawPos.Y),
                new Point(25, 25));
            playerControled = false;
            //idNo = 

            //texture = _globals.textures[1004];
            content = source.content;
            
            foreach(string k in content.Keys)
            {
                content[k] *= .1;                
            }
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

    public class Clickable
    {
        public string type;

        public double PlusMinusOne()
        {
            Random rand = new Random();
            double d = rand.Next() * 2 - 1;
            return d;
        }
    }
}
