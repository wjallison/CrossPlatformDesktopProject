using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CrossPlatformDesktopProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Texture2D textureBall, textureRadialMenu, textureBoundingBox;

        UIItem resourcesPanel, selectedEntityPanel, menuButtonPanel, groupButtonsPanel;

        Vector2 ballPos;
        float ballSpeed;

        double lane1SpawnCounter = 0;

        bool firstInitUpdate = true;
        bool firstInitDraw = true;

        bool temp1 = true;
        bool temp2 = true;

        bool mouseClicked = false;
        bool radialMenuOn = false;
        bool radialMenuFollowing = false;
        Vector2 radialMenuPos;
        Rectangle radialMenuRect;

        public int[] playerResources = new int[15];
        //"Waste",
        //    "Fe",
        //    "Rock",
        //    "Au",
        //    "Ag",
        //    "Pt",
        //    "U",
        //    "Cu",
        //    "H2O",
        //    "H2",
        //    "O2",
        //    "Cu",
        //    "CO2",
        //    "Money",
        //    "Fuel"

        public Rectangle resourcesPanelRect,
            selectedEntityPanelRect,
            MenuButtonRect,
            GroupButtonsRect;

        //List<Rectangle> uiElements = new List<Rectangle>();

        //public List<>

        public SpriteFont font;
        public double dV1 = 0, dV2 = 0, dV3 = 0, dV4 = 0, dV5 = 0, dV6 = 0;

        public List<PhysEntity> physEntList = new List<PhysEntity>();
        public List<Object> asteroidList = new List<object>();
        public _selectedPhysEnt selectedPhysEnt = new _selectedPhysEnt(0, new PhysEntity());
        public bool PhysEntSelected = false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //public List<Content.>
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            resourcesPanelRect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, 50);
            selectedEntityPanelRect = new Rectangle(50, graphics.PreferredBackBufferHeight - 60, graphics.PreferredBackBufferWidth - 100, 60);
            GroupButtonsRect = new Rectangle(graphics.PreferredBackBufferWidth - 30, 20, 30, graphics.PreferredBackBufferHeight - 50);
            MenuButtonRect = new Rectangle(graphics.PreferredBackBufferWidth - 50, graphics.PreferredBackBufferHeight - 50, 50, 50);

            ballPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            //Add UI elements to uiElements
            //Resources
            //uiElements.Add(new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            //uiElements.Add(new Rectangle())
            //uiElements.Add(resourcesPanelRect);
            //uiElements.Add(selectedEntityPanelRect);
            //uiElements.Add(GroupButtonsRect);
            //uiElements.Add(MenuButtonRect);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            textureBall = Content.Load<Texture2D>("ball");

            #region UI Elements
            textureRadialMenu = Content.Load<Texture2D>("asteroidProject_touchMenu");
            textureBoundingBox = Content.Load<Texture2D>("BoundingBox");

            #endregion

            font = Content.Load<SpriteFont>("Font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (firstInitUpdate)
            {
                resourcesPanel = new UIItem(resourcesPanelRect, textureBoundingBox);
                selectedEntityPanel = new UIItem(selectedEntityPanelRect, textureBoundingBox);

                firstInitUpdate = false;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var kstate = Keyboard.GetState();

            #region tutorial move ball
            if (kstate.IsKeyDown(Keys.Up))
                ballPos.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                ballPos.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                ballPos.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                ballPos.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            #endregion

            #region testing collisions
            if (kstate.IsKeyDown(Keys.NumPad0))
            {
                //SpawnAsteroid();
                if (temp1)
                {
                    physEntList.Add(new Asteroid(textureBall, 1, 50, "01", 10, 50, 50, 50, 0));
                    temp1 = false;
                }
            }
            //if (kstate.IsKeyDown(Keys.NumPad1))
            //{
            //    if (temp2)
            //    {
            //        physEntList.Add(new Asteroid(1, 50, "02", 10, 300, 50, 0, 0));
            //        temp2 = false;
            //    }
            //}
            #endregion

            #region testing drone line pathing
            if (kstate.IsKeyDown(Keys.NumPad4))
            {
                if (temp1)
                {
                    //Drone spawns at 300,50,0deg
                    physEntList.Add(new Drone(textureBall, 100, 50, "d1", 10, 10, 300, 50));
                    temp1 = false;
                }
            }
            if (kstate.IsKeyDown(Keys.NumPad5))
            {
                if(!temp1 && temp2)
                {
                    //Drone's target is updated to 500,50
                    //Drone d = (Drone)physEntList[0];
                    //d.UpdateTarget(new Vector2(500, 50));
                    //physEntList[0] = d;

                    //Drone's target is updated to 500,100
                    Drone d = (Drone)physEntList[0];
                    d.UpdateTarget(new Vector2(300, 150));
                    //physEntList.Add(new Asteroid(1, 50, "a1", 10, 300, 150, 0, 0));
                    physEntList[0] = d;
                    temp2 = false;
                }
            }
            #endregion

            #region click actions
            var mState = Mouse.GetState();

            for(int i = 0; i < physEntList.Count; i++)
            {
                if (physEntList[i].radialMenuFollows)
                {
                    SetRadialMenuPos(physEntList[i].pos.X, physEntList[i].pos.Y);
                }
            }

            if(mouseClicked == false)
            {
                if(mState.LeftButton == ButtonState.Pressed)
                {
                    mouseClicked = true;

                    /*
                     * 
                     * Order:
                     * Menu
                     * PC Entity
                     * NPC Entity
                     * 
                     * 
                     * 
                     * 
                     */
                    Rectangle r = new Rectangle(mState.Position.X, mState.Y, 1, 1);

                    if (r.Intersects(resourcesPanelRect))
                    {
                        radialMenuOn = false;
                    }
                    else if (r.Intersects(selectedEntityPanelRect))
                    {
                        radialMenuOn = false;
                    }
                    else if (r.Intersects(GroupButtonsRect))
                    {
                        radialMenuOn = false;
                    }
                    else if (r.Intersects(MenuButtonRect))
                    {

                    }
                    
                    else
                    {
                        SetRadialMenuPos(mState.Position.X, mState.Position.Y);
                        bool ent = false;
                        for (int i = 0; i < physEntList.Count; i++)
                        {
                            if (!physEntList[i].playerControled) { continue; }
                            physEntList[i].radialMenuFollows = false;
                            if (r.Intersects(physEntList[i].hitBox))
                            {
                                SetRadialMenuPos(physEntList[i].pos.X, physEntList[i].pos.Y);
                                radialMenuOn = true;
                                radialMenuFollowing = true;
                                physEntList[i].radialMenuFollows = true;

                                selectedPhysEnt.index = i;
                                selectedPhysEnt.entity = physEntList[i];
                                PhysEntSelected = true;
                                ent = true;
                            }
                        }
                        if (!ent) { PhysEntSelected = false; }
                        radialMenuOn = true;
                    }

                }
            }
            else
            {
                if(mState.LeftButton == ButtonState.Released)
                {
                    mouseClicked = false;
                }
            }

            #endregion

            #region spawn asteroids

            lane1SpawnCounter += gameTime.ElapsedGameTime.TotalSeconds;
            //roughly every 3 seconds
            if(lane1SpawnCounter > 3)
            {
                lane1SpawnCounter = 0;
                SpawnAsteroid(1);
            }

            #endregion

            #region remove entities outside box

            for(int i = 0; i < physEntList.Count; i++)
            {
                if(physEntList[i].pos.Y < -75)
                {
                    physEntList.RemoveAt(i);
                    i--;
                }
            }

            #endregion

            if (physEntList.Count > 0)
            {
                dV1 = physEntList[0].theta;
                dV2 = physEntList[0].posDot.X;
                dV3 = physEntList[0].posDotDot.X;
                dV4 = physEntList[0].health;
            }

            //mState.



            ballPos.X = Math.Min(Math.Max(textureBall.Width / 2, ballPos.X), graphics.PreferredBackBufferWidth - textureBall.Width / 2);
            ballPos.Y = Math.Min(Math.Max(textureBall.Height / 2, ballPos.Y), graphics.PreferredBackBufferHeight - textureBall.Height / 2);

            

            
            ScanForCollisions(gameTime);

            for(int i = 0; i < physEntList.Count; i++)
            {
                physEntList[i].Update(gameTime);
            }

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //spriteBatch.DrawString(font, displayValue.ToString(), new Vector2(200, -200), Color.Black);
            //spriteBatch.DrawString(font, "test", new Vector2(200, 100), Color.Black);
            spriteBatch.DrawString(font, "Theta: " + dV1.ToString(), new Vector2(200, 100), Color.Black);
            spriteBatch.DrawString(font, dV2.ToString(), new Vector2(200, 120), Color.Black);
            spriteBatch.DrawString(font, dV3.ToString(), new Vector2(200, 140), Color.Black);
            spriteBatch.DrawString(font, dV4.ToString(), new Vector2(200, 160), Color.Black);
            //spriteBatch.Draw(textureBall, ballPos, Color.White);
            spriteBatch.Draw(textureBall,
                ballPos,
                null,
                Color.White,
                0f,
                new Vector2(textureBall.Width / 2, textureBall.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );

            

            if (physEntList.Count > 0)
            {
                for (int i = 0; i < physEntList.Count; i++)
                {
                    //physEntList[i]
                    spriteBatch.Draw(
                        textureBall,
                        physEntList[i].hitBox,
                        Color.White
                        );
                }
            }

            if (radialMenuOn)
            {
                spriteBatch.Draw(
                    textureRadialMenu,
                    radialMenuRect,
                    Color.White
                    );
            }


            #region UI

            spriteBatch.Draw(
                resourcesPanel.texture,
                resourcesPanel.box,
                Color.White
                );
            spriteBatch.Draw(selectedEntityPanel.texture, selectedEntityPanel.box, Color.White);
            //if
            if (firstInitDraw)
            {
                selectedEntityPanel.AddRelControl(
                new UIControl(
                    new Vector2((float)25, 10), new Vector2(25, 25), selectedPhysEnt.entity.texture));
            }
            
            if (PhysEntSelected)
            {
                selectedEntityPanel.controls[0].texture = selectedPhysEnt.entity.texture;
                spriteBatch.Draw(selectedEntityPanel.controls[0].texture,
                            selectedEntityPanel.controls[0].box,
                            Color.White);
            }
            
            //spriteBatch.Draw(textureBall, new Rectangle(new Point(selectedEntityPanel.box.X, selectedEntityPanel.box.Y), new Point(10, 10)), Color.White);

            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Vector2 PosFromVelocity(double[] pos, double[] posDot, GameTime gametime)
        {
            //double x = pos[0] + posDot[0];
            Vector2 res = new Vector2((float)(pos[0] + posDot[0] * gametime.ElapsedGameTime.TotalSeconds), 
                (float)(pos[1] + posDot[1] * gametime.ElapsedGameTime.TotalSeconds));
            return res;
        }
        public Vector2 PosFromVelocity(Vector2 pos, Vector2 posDot, GameTime gametime)
        {
            Vector2 res = new Vector2((float)(pos.X + posDot.X * gametime.ElapsedGameTime.TotalSeconds),
                (float)(pos.Y + posDot.Y * gametime.ElapsedGameTime.TotalSeconds));
            return res;
        }

        public Vector2[] Collision(PhysEntity ent1, PhysEntity ent2)
        {
            double elasticity;
            //elasticity = .9;
            elasticity = 1;
            
            Vector2 res1, res2;
            float r1x, r1y, r2x, r2y;

            double p1, p2, p3, p4, p5;
            p1 = ent1.posDot.X;
            p2 = 2 * ent2.mass / (ent1.mass + ent2.mass);
            p3 = VectDot(ent1.posDot - ent2.posDot, ent1.pos - ent2.pos);
            p4 = Math.Pow((ent1.pos - ent2.pos).Length(), 2);
            p5 = (ent1.pos.X - ent2.pos.X);
            r1x = (float)(
                ent1.posDot.X 
                
                - 

                2 * ent2.mass / (ent1.mass + ent2.mass) 
                * 
                VectDot(ent1.posDot - ent2.posDot, ent1.pos - ent2.pos) 
                /
                Math.Pow((ent1.pos - ent2.pos).Length(), 2) 
                *
                (ent1.pos.X - ent2.pos.X));
            r1y = (float)(ent1.posDot.Y - 2 * ent2.mass / (ent1.mass + ent2.mass) *
                VectDot(ent1.posDot - ent2.posDot, ent1.pos - ent2.pos) /
                Math.Pow((ent1.pos - ent2.pos).Length(), 2) *
                (ent1.pos.Y - ent2.pos.Y));
            r2x = (float)(ent2.posDot.X - 2 * ent2.mass / (ent1.mass + ent2.mass) *
                VectDot(ent2.posDot - ent1.posDot, ent2.pos - ent1.pos) /
                Math.Pow((ent2.pos - ent1.pos).Length(), 2) *
                (ent2.pos.X - ent1.pos.X));
            r2y = (float)(ent2.posDot.Y - 2 * ent2.mass / (ent1.mass + ent2.mass) *
                VectDot(ent2.posDot - ent1.posDot, ent2.pos - ent1.pos) /
                Math.Pow((ent2.pos - ent1.pos).Length(), 2) *
                (ent2.pos.Y - ent1.pos.Y));

            res1 = new Vector2(r1x, r1y);
            res2 = new Vector2(r2x, r2y);
            //res1 = 5 * res2;

            return new Vector2[] { res1, res2 };

        }

        public float VectDot(Vector2 v1, Vector2 v2)
        {
            float res = v1.X * v2.X + v1.Y * v2.Y;
            return res;
        }
        public float VectDot(float v1x, float v1y, float v2x, float v2y)
        {
            float res = v1x * v2x + v1y * v2y;
            return res;
        }

        public void ScanForCollisions(GameTime gameTime)
        {
            for(int i = 0; i < physEntList.Count; i++)
            {
                for(int j = i+1; j < physEntList.Count; j++)
                {
                    //if(())
                    if (physEntList[i].hitCircle.Overlaps(physEntList[j].hitCircle))
                    {
                        Vector2[] newVects = Collision(physEntList[i], physEntList[j]);
                        double[] dams = DetermineDamage(new PhysEntity[] { physEntList[i], physEntList[j] }, newVects);
                        physEntList[i].posDot = newVects[0];
                        physEntList[j].posDot = newVects[1];

                        physEntList[i].TakeDamage(dams[0]);
                        physEntList[j].TakeDamage(dams[1]);
                    }
                    //if (physEntList[i].hitBox.Intersects(physEntList[j].hitBox))
                    //{
                    //    Vector2[] newVects = Collision(physEntList[i], physEntList[j]);
                    //    double[] dams = DetermineDamage(new PhysEntity[] { physEntList[i], physEntList[j] }, newVects);
                    //    physEntList[i].posDot = newVects[0];
                    //    physEntList[j].posDot = newVects[1];

                    //    physEntList[i].TakeDamage(dams[0]);
                    //    physEntList[j].TakeDamage(dams[1]);

                    //    //physEntList[i].Update(gameTime);
                    //    //physEntList[j].Update(gameTime);
                    //}
                }
            }
        }

        public double[] DetermineDamage(PhysEntity[] start, Vector2[] end)
        {
            double scalar = 1;

            double[] res = new double[2];
            res[0] = .5 * start[0].mass * Math.Pow(start[0].posDot.Length(), 2)
                -
                .5 * start[0].mass * Math.Pow(end[0].Length(), 2);
            res[1] = .5 * start[1].mass * Math.Pow(start[1].posDot.Length(), 2)
                -
                .5 * start[1].mass * Math.Pow(end[1].Length(), 2);
            res[0] *= scalar;
            res[1] *= scalar;

            return res;
            //double en2 = 
        }

        //public void SetUpUI

        public Vector2 MirrorAbout(Vector2 v, Vector2 m)
        {
            double th = Math.Atan2(m.Y-v.Y, m.X-v.X);
            v.X = (float)(v.X * Math.Cos(th) + v.Y * Math.Sin(th));
            v.Y = (float)(-v.X * Math.Sin(th) + v.Y * Math.Cos(th));
            return v;
        }

        public Vector2 VectRot(Vector2 v, double theta)
        {
            Vector2 ret = new Vector2(
                (float)(v.X * Math.Cos(theta) - v.Y * Math.Sin(theta)),
            (float)(v.X * Math.Sin(theta) + v.Y * Math.Cos(theta)));
            return ret;
        }

        public float VectDotNorm(Vector2 v1, Vector2 v2)
        {
            //if(v1.Length == 0 || v2.Length == 0)
            //{
            //    return 0;
            //}
            if((v1.X == 0 && v1.Y == 0) || (v2.X == 0 && v2.Y == 0))
            {
                return 0;
            }

            float ret = (v1.X * v2.X + v1.Y * v2.Y);
            ret = ret / v2.Length();
            return ret;
        }

        public void SpawnAsteroid()
        {
            //physEntList.Add(new Asteroid(1, 50, "01", 50,50, 1,1));
        }

        public void SetRadialMenuPos(double x, double y)
        {
            x -= 50;
            y -= 50;
            radialMenuPos = new Vector2((float)x, (float)y);
            radialMenuRect = new Rectangle(new Point((int)x, (int)y), new Point(100, 100));
        }

        public void SpawnAsteroid(int lane)
        {
            /* Lane definitions:
             * Lane 1:
             * 0 - 25 (% from right edge)
             * Asteroids can be aimed as far as -10 to 27.5
             * Speed: 10-50
             * Frequency:
             * 
             * Lane 2:
             * 40 - 75
             * Aim: 37.5, 80
             * Speed: 50 - 100
             * 
             * Lane 3:
             * 85 - 100
             * Aim: 80, 110
             * Speed: 100 - 200
             * */
            Asteroid newAsteroid;
            Random r = new Random();
            float xSpawn = 0;
            float xTarget;
            double diam = 0;
            int speed = 0;
            Vector2 direction = new Vector2(0,0);

            switch (lane)
            {
                case 1:
                    xSpawn = (float)(25 * r.NextDouble() / 100 * graphics.PreferredBackBufferWidth);
                    xTarget = (float)((37.5 * r.NextDouble() / 100 - .1) * graphics.PreferredBackBufferWidth);
                    speed = (int)(40 * r.NextDouble() + 10);
                    direction = new Vector2(xSpawn - xTarget, -graphics.PreferredBackBufferHeight);
                    //(int lane, double diameter, string name, double m, double x = 0, double y = 0, double xDot = 0, double yDot = 0)
                    diam = (50 * r.NextDouble() + 25);
                    
                    break;
            }

            newAsteroid = new Asteroid(textureBall,
                        1,
                        diam,
                        "a" + physEntList.Count.ToString(),
                        Math.Pow(diam / 2, 2),
                        graphics.PreferredBackBufferWidth - xSpawn,
                        graphics.PreferredBackBufferHeight + 100,
                        direction.X / direction.Length() * speed,
                        direction.Y / direction.Length() * speed
                        );

            physEntList.Add(newAsteroid);
        }
    }
}
