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
        Texture2D textureBall, textureBlackLine;
        //Texture2D textureRadialMenu, textureBoundingBox, textureGoToButton, textureAttackButton, textureHarpoonButton, textureDockButton, textureGatherButton;

        UIItem resourcesPanel, selectedEntityPanel, menuButtonPanel, groupButtonsPanel;

        Vector2 ballPos;
        float ballSpeed;
        float ballAngle = 0;

        //double lane1SpawnCounter = 0;

        //bool firstInitUpdate = true;
        //bool firstInitDraw = true;

        bool temp1 = true;
        bool temp2 = true;

        bool mouseClicked = false;
        //bool radialMenuOn = false;
        //bool radialMenuFollowing = false;
        Vector2 radialMenuPos;
        Rectangle radialMenuRect;

        LinearEffect linear;

        RadialMenu radial;

        enum GameState
        {
            MainMenu = 0,
            Loading = 1,
            MainState = 2,
            Paused = 3,
            BuildMenuPaused = 4,
            SideMenuPaused = 5
        }



        int gameState = 2;

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
        //public List<Object> asteroidList = new List<object>();
        public _selectedPhysEnt selectedPhysEnt = new _selectedPhysEnt(0, new PhysEntity());
        //public int selectedEntIndex;
        //public List<int> selectedEntIndices = new List<int>();
        public bool PhysEntSelected = false;

        public Station station;

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
            //textureRadialMenu = Content.Load<Texture2D>("asteroidProject_touchMenu");
            _globals.textures[4,0] = Content.Load<Texture2D>("asteroidProject_touchMenu");
            _globals.textures[4,1] = Content.Load<Texture2D>("Goto");
            _globals.textures[4,2] = Content.Load<Texture2D>("Attack");
            _globals.textures[4,3] = Content.Load<Texture2D>("Harvest");
            _globals.textures[4,4] = Content.Load<Texture2D>("harpoon");
            _globals.textures[4,5] = Content.Load<Texture2D>("dock");

            _globals.textures[4,6] = Content.Load<Texture2D>("switch");

            _globals.textures[4,7] = Content.Load<Texture2D>("GotoDis");
            _globals.textures[4,8] = Content.Load<Texture2D>("AttackDis");
            _globals.textures[4,9] = Content.Load<Texture2D>("HarvestDis");
            _globals.textures[4,10] = Content.Load<Texture2D>("harpoonDis");
            _globals.textures[4,11] = Content.Load<Texture2D>("dockDis");
            //_globals.textures[17] = Content.Load<Texture2D>("switchDis");

            //textureBoundingBox = Content.Load<Texture2D>("BoundingBox");
            _globals.textures[6,0] = Content.Load<Texture2D>("BoundingBox");

            //textureGoToButton = Content.Load<Texture2D>("Goto");
            //textureAttackButton = Content.Load<Texture2D>("Attack");
            //textureGatherButton = Content.Load<Texture2D>("Harvest");
            //textureHarpoonButton = Content.Load<Texture2D>("harpoon");
            //textureDockButton = Content.Load<Texture2D>("dock");

            

            _globals.textures[5,0] = Content.Load<Texture2D>("laserHomogeneous");

            _globals.textures[1,1] = Content.Load<Texture2D>("miningDrone");
            _globals.textures[1,2] = Content.Load<Texture2D>("harvestingDrone");
            _globals.textures[1,3] = Content.Load<Texture2D>("harpoonDrone");

            _globals.textures[2,0] = Content.Load<Texture2D>("Core");
            _globals.textures[2,1] = Content.Load<Texture2D>("StationDock");

            #endregion

            textureBlackLine = new Texture2D(GraphicsDevice, 1, 1);
            textureBlackLine.SetData<Color>(new Color[] { Color.Black });

            font = Content.Load<SpriteFont>("Font");

            resourcesPanel = new UIItem(resourcesPanelRect, _globals.textures[6,0]);
            selectedEntityPanel = new UIItem(selectedEntityPanelRect, _globals.textures[6, 0]);

            //Load Radial Menu
            //Texture2D[] textures = new Texture2D[] {textureGoToButton,textureAttackButton,
            //textureGatherButton,textureHarpoonButton,textureDockButton};
            //radial = new RadialMenu(textures);

            selectedEntityPanel.AddRelControl(
                    new UIControl(
                        new Vector2((float)25, 10), new Vector2(25, 25), 
                        //selectedPhysEnt.entity.texture
                        _globals.textures[6,0]
                        ));


            radial = new RadialMenu();
            Vector2 a, b;
            a = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            b = new Vector2(a.X + 200, a.Y);
            Rectangle r = new Rectangle(new Point((int)a.X, (int)a.Y), new Point(200, 100));
            linear = new LinearEffect(a, b);
            linear.rect = r;

            station = new Station(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 3 * 2, 
                graphics.PreferredBackBufferHeight / 2));
            station.AddBlock(1, 0, "stationDock");
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

            //    enum GameState
            //{
            //    MainMenu = 0,
            //    Loading = 1,
            //    MainState = 2,
            //    Paused = 3,
            //    BuildMenuPaused = 4
            //}

            if (gameState == (int)GameState.MainMenu)
            {

            }
            else if (gameState == (int)GameState.Loading)
            {

            }
            else if (gameState == (int)GameState.MainState)
            {
                //if (firstInitUpdate)
                //{
                //    //resourcesPanel = new UIItem(resourcesPanelRect, textureBoundingBox);
                //    //selectedEntityPanel = new UIItem(selectedEntityPanelRect, textureBoundingBox);

                //    firstInitUpdate = false;
                //}
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here

                #region Debug commands

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
                        physEntList.Add(new Drone(textureBall, 100, 50, "d1", 300, 250, 300, 50));
                        temp1 = false;
                    }
                }
                if (kstate.IsKeyDown(Keys.NumPad5))
                {
                    if (!temp1 && temp2)
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

                #region testing drone following

                if (kstate.IsKeyDown(Keys.NumPad7))
                {
                    if (temp1)
                    {
                        physEntList.Add(new Asteroid(textureBall, 1, 50, "a1", 10, 300, 150, 10, 0));
                        physEntList.Add(new Drone(textureBall, 100, 50, "d1", 10, 300, 200, 250, 0, 0));
                        temp1 = false;
                    }
                }
                //if (kstate.IsKeyDown(Keys.NumPad8))
                //{
                //    if (!temp1 && temp2)
                //    {

                //        Drone d = (Drone)physEntList[1];
                //        //d.UpdateTarget(new Vector2(300, 150));
                //        d.GoTo(physEntList[0]);
                //        physEntList[1] = d;
                //        temp2 = false;
                //    }
                //}

                ballAngle += 0.01f;
                #endregion

                #endregion


                if (radial.isFollowing)
                {
                    radial.Update(physEntList[radial.followingIndex]);
                }


                #region click actions
                var mState = Mouse.GetState();

                if (mouseClicked == false)
                {
                    if (mState.LeftButton == ButtonState.Pressed)
                    {
                        mouseClicked = true;

                        Rectangle r = new Rectangle(mState.Position.X, mState.Y, 1, 1);

                        if (r.Intersects(radial.drawBox))
                        {
                            for (int i = 0; i < radial.buttons.Length; i++)
                            {
                                if (radial.buttons[i].enabled)
                                {
                                    if (r.Intersects(radial.buttons[i].box))
                                    {
                                        IssueCommand(i);
                                    }
                                }
                            }
                            if (radial.switchToAvaliable)
                            {
                                if (r.Intersects(radial.SwitchButton.box))
                                {
                                    selectedPhysEnt.entity = physEntList[radial.followingIndex];
                                    selectedPhysEnt.index = radial.followingIndex;

                                }
                            }
                        }
                        else if (r.Intersects(resourcesPanelRect))
                        {
                            //radialMenuOn = false;
                            //radial.isOn 
                            radial.Off();
                        }
                        else if (r.Intersects(selectedEntityPanelRect))
                        {
                            //radialMenuOn = false;
                            radial.Off();
                        }
                        else if (r.Intersects(GroupButtonsRect))
                        {
                            //radialMenuOn = false;
                            radial.Off();
                        }
                        else if (r.Intersects(MenuButtonRect))
                        {
                            radial.Off();
                        }
                        else if (r.Intersects(station.blocks[0].hitBox))
                        {
                            gameState = (int)GameState.BuildMenuPaused;
                            return;
                        }

                        else
                        {
                            //for(int i = 0; i < station.blocks.Count; i++)
                            //{
                            //    if(r.Intersects(station.blocks))
                            //}


                            //radial.isOn = true;
                            radial.isFollowing = false;
                            for (int i = 0; i < physEntList.Count; i++)
                            {
                                if (r.Intersects(physEntList[i].hitBox))
                                {
                                    radial.Follow(physEntList[i], i);



                                    bool[] s = new bool[] { true, false, false, false, false, false };

                                    //if(physEntList[selectedEntIndex].type == "miningDrone")
                                    //{
                                    //    s[1] = true;
                                    //}
                                    if (physEntList[i].type == "asteroid")
                                    {
                                        switch (physEntList[selectedPhysEnt.index].type)
                                        {
                                            case "miningDrone":
                                                s[1] = true;
                                                break;
                                            case "harvestingDrone":
                                                s[2] = true;
                                                break;
                                        }
                                    }
                                    //else if(physEntList[i].type = "dockingStation")


                                    if (physEntList[i].playerControled)
                                    {
                                        s[5] = true;
                                    }

                                    radial.SetState(s);
                                    //physEntList[i].radialMenuFollows = true;
                                    break;
                                }
                            }
                            if (!radial.isFollowing)
                            {
                                radial.UpdateSpace(new Vector2((float)mState.Position.X, (float)mState.Position.Y));
                                bool[] s = new bool[] { true, false, false, false, false, false };
                                radial.SetState(s);
                            }
                        }

                    }
                }
                else
                {
                    if (mState.LeftButton == ButtonState.Released)
                    {
                        mouseClicked = false;
                    }
                }

                #endregion

                #region spawn asteroids

                //lane1SpawnCounter += gameTime.ElapsedGameTime.TotalSeconds;
                ////roughly every 3 seconds
                //if(lane1SpawnCounter > 3)
                //{
                //    lane1SpawnCounter = 0;
                //    SpawnAsteroid(1);
                //}

                #endregion

                #region remove entities outside box

                for (int i = 0; i < physEntList.Count; i++)
                {
                    if (physEntList[i].pos.Y < -75)
                    {
                        physEntList.RemoveAt(i);
                        if (radial.isFollowing)
                        {
                            if (radial.followingIndex > i)
                            {
                                radial.followingIndex--;
                            }
                        }
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


                //Mining
                for(int i = 0; i < station.blocks.Count; i++)
                {
                    //int counter = 0;
                    //Not the right way, but I feel lazy
                    if (station.blocks[i].spawnsDrones)
                    {
                        if (station.blocks[i].droneIndList.Count < station.blocks[i].numDrones)
                        {
                            if(station.blocks[i].countUp < 10)
                            {
                                station.blocks[i].countUp++;
                            }
                            else
                            {
                                station.blocks[i].droneIndList.Add(physEntList.Count);
                                physEntList.Add(station.blocks[i].SpawnDrone());
                                station.blocks[i].countUp = 0;
                            }

                            
                        }
                    }
                    
                }

                ScanForCollisions(gameTime);

                for (int i = 0; i < physEntList.Count; i++)
                {
                    physEntList[i].Update(gameTime);
                }

                base.Update(gameTime);
            }
            else if (gameState == (int)GameState.Paused)
            {

            }
            else if (gameState == (int)GameState.BuildMenuPaused)
            {
                var mState = Mouse.GetState();

                if (mouseClicked == false)
                {
                    if (mState.LeftButton == ButtonState.Pressed)
                    {
                        mouseClicked = true;








                    }
                }
                else
                {
                    if (mState.LeftButton == ButtonState.Released)
                    {
                        mouseClicked = false;
                    }
                }
            }
            else if(gameState == (int)GameState.SideMenuPaused)
            {

            }
        }

        public Vector2 ScaleAbout(double scalar, Vector2 pt0, Vector2 center)
        {
            Vector2 ptPrime = center;
            ptPrime.X = ptPrime.X + (float)(scalar * (pt0.X - center.X));
            ptPrime.Y = ptPrime.Y + (float)(scalar * (pt0.Y - center.Y));
            return ptPrime;
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
            DrawStation();

            if (gameState == 0)
            {

            }
            else if (gameState == 1)
            {

            }
            else if(gameState == 2)
            {
                

                
                

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
                    ballAngle,
                    new Vector2(textureBall.Width / 2, textureBall.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                    );

                Vector2 offset = new Vector2(0, 0);
                //Vector2 ob = new Vector2(textureBall.Width / 2, textureBall.Height / 2);
                Vector2 ob = new Vector2(0, 0);
                spriteBatch.Draw(linear.texture,
                    linear.start + offset,
                    //new Rectangle((int)linear.start.X, (int)linear.start.Y, 200, 100),
                    new Rectangle(0, 0, 200, 5),
                    //null,
                    Color.White,
                    (Single)(3.14/6),
                    ob,
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
                //Vector2 offset = new Vector2(0, 0);
                spriteBatch.Draw(linear.texture,
                    linear.start + offset,
                    //new Rectangle((int)linear.start.X, (int)linear.start.Y, 200, 100),
                    new Rectangle(0, 0, 200, 5),
                    //null,
                    Color.White,
                    0f,
                    ob,
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
                spriteBatch.Draw(linear.texture,
                    linear.start + offset,
                    //new Rectangle((int)linear.start.X, (int)linear.start.Y, 200, 100),
                    new Rectangle(0, 0, 200, 5),
                    //null,
                    Color.White,
                    (Single)(3.14 / 3),
                    ob,
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
                spriteBatch.Draw(linear.texture,
                    linear.start + offset,
                    //new Rectangle((int)linear.start.X, (int)linear.start.Y, 200, 100),
                    new Rectangle(0, 0, 200, 5),
                    //null,
                    Color.White,
                    (Single)(3.14 / 2),
                    ob,
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
                //Rectangle r = new Rectangle()
                //spriteBatch.Draw(linear.texture, linear.rect, Color.White);
                //spriteBatch.

                if (physEntList.Count > 0)
                {
                    for (int i = 0; i < physEntList.Count; i++)
                    {
                        //physEntList[i]
                        spriteBatch.Draw(
                            physEntList[i].texture,
                            physEntList[i].hitBox,
                            Color.White
                            );
                    }
                }

                DrawSelectedDisplay();
                if (radial.isOn)
                {
                    //spriteBatch.Draw(
                    //    textureRadialMenu,
                    //    radialMenuRect,
                    //    Color.White
                    //    );
                    DrawRadialMenu();

                }


                #region UI

                spriteBatch.Draw(
                    resourcesPanel.texture,
                    resourcesPanel.box,
                    Color.White
                    );
                //spriteBatch.Draw(selectedEntityPanel.texture, selectedEntityPanel.box, Color.White);
                //if
                //if (firstInitDraw)
                //{
                //    selectedEntityPanel.AddRelControl(
                //    new UIControl(
                //        new Vector2((float)25, 10), new Vector2(25, 25), selectedPhysEnt.entity.texture));
                //}

                //if (PhysEntSelected)
                //{
                //    selectedEntityPanel.controls[0].texture = selectedPhysEnt.entity.texture;
                //    spriteBatch.Draw(selectedEntityPanel.controls[0].texture,
                //                selectedEntityPanel.controls[0].box,
                //                Color.White);
                //}

                //spriteBatch.Draw(textureBall, new Rectangle(new Point(selectedEntityPanel.box.X, selectedEntityPanel.box.Y), new Point(10, 10)), Color.White);

                #endregion

                //spriteBatch.Draw(textureBall,)
                


                

                base.Draw(gameTime);
            }
            
            else if (gameState == 3)
            {

            }
            else if (gameState == 4)
            {
                
            }

            spriteBatch.End();
        }

        public void DrawRadialMenu()
        {
            if (radial.isOn)
            {
                spriteBatch.Draw(radial.texture, radial.drawBox, Color.White);
                for(int i = 0; i < 5; i++)
                {
                    if (radial.buttons[i].enabled)
                    {
                        spriteBatch.Draw(radial.buttons[i].textureEnabled, radial.buttons[i].box, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(radial.buttons[i].textureDisabled, radial.buttons[i].box, Color.White);
                    }
                }
                if (radial.SwitchButton.enabled)
                {
                    spriteBatch.Draw(radial.SwitchButton.textureEnabled, radial.SwitchButton.box, Color.White);
                }
            }
        }

        public void DrawStation()
        {
            //spriteBatch.Draw(station.)
            if(gameState == (int)GameState.MainState)
            {
                for (int i = 0; i < station.blocks.Count; i++)
                {
                    spriteBatch.Draw(station.blocks[i].texture, station.blocks[i].hitBox, Color.White);
                }
            }
            if(gameState == (int)GameState.BuildMenuPaused)
            {
                station.ScaleAllAbout(2, station.blocks[0].pos);
                for (int i = 0; i < station.blocks.Count; i++)
                {
                    spriteBatch.Draw(station.blocks[i].texture, station.blocks[i].expRect, Color.White);
                }
            }
            
        }

        public void DrawSelectedDisplay()
        {
            spriteBatch.Draw(selectedEntityPanel.texture, selectedEntityPanel.box, Color.White);
            //spriteBatch.DrawString(font, dV4.ToString(), new Vector2(200, 160), Color.Black);
            if (PhysEntSelected)
            {
                selectedEntityPanel.controls[0].texture = selectedPhysEnt.entity.texture;
                spriteBatch.Draw(selectedEntityPanel.controls[0].texture,
                            selectedEntityPanel.controls[0].box,
                            Color.White);
                //located at 25, 10, and box is 25,25
                //-> extends to 50,35

                spriteBatch.DrawString(font, selectedPhysEnt.entity.idNo, new Vector2((float)selectedEntityPanel.box.Location.X, (float)selectedEntityPanel.box.Location.Y), Color.Black);
            }

        }

        public void IssueCommand(int command)
        {
            Drone d = (Drone)physEntList[selectedPhysEnt.index];
            //d.ReceiveOrder(command)

            switch (command)
            {
                case 0:
                    if (radial.isFollowing)
                    {
                        d.ReceiveOrder(0, new Vector2(0, 0), physEntList[radial.followingIndex], radial.followingIndex);
                    }
                    else
                    {
                        d.ReceiveOrder(0, radial.center);
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
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
                if (!physEntList[i].solid) { continue; }
                for (int j = i+1; j < physEntList.Count; j++)
                {
                    if (!physEntList[j].solid) { continue; }
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

        public void ScanForMining(GameTime gametime)
        {
            for(int i = 0; i < physEntList.Count; i++)
            {
                if(physEntList[i].type == "drone")
                {
                    Drone d = (Drone)physEntList[i];
                    if (d.miningEnabled)
                    {
                        for (int j = 0; j < physEntList.Count; j++)
                        {
                            if (physEntList[j].type == "asteroid")
                            {
                                if((physEntList[j].pos - d.pos).Length() < physEntList[j].diam / 2 + d.range)
                                {
                                    physEntList[j].TakeDamage(d.DealDamage());
                                }
                            }
                        }
                    }
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

        //public void SpawnAsteroid()
        //{
        //    //physEntList.Add(new Asteroid(1, 50, "01", 50,50, 1,1));
        //}

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


        public void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(textureBlackLine,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }

        public void DrawLinearEffect(Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(_globals.textures[5, 0],
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.White, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }
    }
}
