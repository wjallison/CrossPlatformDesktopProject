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

        LayerTracker mainLayers = new LayerTracker();
        LayerTracker buildMenuLayers = new LayerTracker();

        BuildScreen buildScreen;

        public IDictionary<string, double> playerResources = new Dictionary<string, double>();

        Vector2 ballPos;
        float ballSpeed;
        float ballAngle = 0;

        double lane1SpawnCounter = 0;

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

        //public int[] playerResources = new int[15];
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
            GroupButtonsRect,
            PauseButtonRect;

        //List<Rectangle> uiElements = new List<Rectangle>();

        //public List<>

        public SpriteFont font;
        public double dV1 = 0, dV2 = 0, dV3 = 0, dV4 = 0, dV5 = 0, dV6 = 0;

        public List<PhysEntity> physEntList = new List<PhysEntity>();
        public List<Debris> debrisList = new List<Debris>();
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
            PauseButtonRect = new Rectangle(0, graphics.PreferredBackBufferHeight - 50, 50, 50);

            ballPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            for(int i = 0; i < _globals.materials.Length; i++)
            {
                playerResources.Add(_globals.materials[i], 0);
            }

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

            #region Textures
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
            _globals.textures[6, 1] = Content.Load<Texture2D>("buildBox");

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

            _globals.textures[0, 0] = Content.Load<Texture2D>("asteroid");

            _globals.textures[3, 0] = Content.Load<Texture2D>("debris");

            #endregion

            textureBlackLine = new Texture2D(GraphicsDevice, 1, 1);
            textureBlackLine.SetData<Color>(new Color[] { Color.Black });

            font = Content.Load<SpriteFont>("Font");

            resourcesPanel = new UIItem(resourcesPanelRect, _globals.textures[6,0]);
            selectedEntityPanel = new UIItem(selectedEntityPanelRect, _globals.textures[6, 0]);


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
            station.AddBlock(0, 1, "stationDockHarvester");

            buildScreen = new BuildScreen(graphics);
            buildScreen.reduceResourcesEvent += buildScreen_reduceResourcesEvent;
            buildScreen.addStationBlockEvent += buildScreen_addStationBlockEvent;
        }

        void buildScreen_reduceResourcesEvent(string resource, double qty)
        {
            if(playerResources[resource] > qty)
            {
                playerResources[resource] -= qty;
            }

            //buildScreen.ProceedWithTransaction();
        }
        void buildScreen_addStationBlockEvent(string type, int x, int y)
        {
            station.AddBlock(x - 5, y - 5, type);
            buildScreen.Update(station);
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

            if (gameState == (int)GameState.MainMenu)
            {

            }
            else if (gameState == (int)GameState.Loading)
            {

            }
            else if (gameState == (int)GameState.MainState)
            {
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
                //if (kstate.IsKeyDown(Keys.NumPad0))
                //{
                //    //SpawnAsteroid();
                //    if (temp1)
                //    {
                //        physEntList.Add(new Asteroid(textureBall, 1, 50, "01", 10, 50, 50, 50, 0));
                //        temp1 = false;
                //    }
                //}
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
                //if (kstate.IsKeyDown(Keys.NumPad4))
                //{
                //    if (temp1)
                //    {
                //        //Drone spawns at 300,50,0deg
                //        physEntList.Add(new Drone(textureBall, 100, 50, "d1", 300, 250, 300, 50));
                //        temp1 = false;
                //    }
                //}
                //if (kstate.IsKeyDown(Keys.NumPad5))
                //{
                //    if (!temp1 && temp2)
                //    {
                //        //Drone's target is updated to 500,50
                //        //Drone d = (Drone)physEntList[0];
                //        //d.UpdateTarget(new Vector2(500, 50));
                //        //physEntList[0] = d;

                //        //Drone's target is updated to 500,100
                //        Drone d = (Drone)physEntList[0];
                //        d.UpdateTarget(new Vector2(300, 150));
                //        //physEntList.Add(new Asteroid(1, 50, "a1", 10, 300, 150, 0, 0));
                //        physEntList[0] = d;
                //        temp2 = false;
                //    }
                //}
                #endregion

                #region testing drone following

                //if (kstate.IsKeyDown(Keys.NumPad7))
                //{
                //    if (temp1)
                //    {
                //        physEntList.Add(new Asteroid(textureBall, 1, 50, "a1", 10, 300, 150, 10, 0));
                //        physEntList.Add(new Drone(textureBall, 100, 50, "d1", 10, 300, 200, 250, 0, 0));
                //        temp1 = false;
                //    }
                //}
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

                #region testing drone asteroid interactions

                if (kstate.IsKeyDown(Keys.NumPad0))
                {
                    if (temp1)
                    {
                        SpawnAsteroid(0);
                        temp1 = false;
                    }
                }

                #endregion

                #endregion


                //if (radial.isFollowing)
                //{
                //    if(radial.followingType == 1)
                //    {
                //        radial.Update(physEntList[radial.followingIndex]);
                //    }
                //    else if(radial.followingType == 2)
                //    {
                //        radial.Update(debrisList[radial.followingIndex]);
                //    }
                    
                //}


                #region click actions
                var mState = Mouse.GetState();
                DealWithMouseEvent(mState);

                #endregion

                #region spawn asteroids

                lane1SpawnCounter += gameTime.ElapsedGameTime.TotalSeconds;
                //roughly every 3 seconds
                if (lane1SpawnCounter > 3)
                {
                    lane1SpawnCounter = 0;
                    SpawnAsteroid(1);
                }

                #endregion

                #region remove entities outside box

                for (int i = 0; i < physEntList.Count; i++)
                {
                    if (physEntList[i].pos.Y < -75 || physEntList[i].pos.X > graphics.PreferredBackBufferWidth + 75
                        || physEntList[i].pos.Y > graphics.PreferredBackBufferHeight + 200 || physEntList[i].pos.X < -75)
                    {
                        if(physEntList[i].GetType() == (new Drone()).GetType())
                        {
                            GoHomeYoureDrunk(i);
                        }
                        else
                        {
                            DestroyAt(i);
                            i--;
                        }
                        
                    }
                }

                #endregion

                if (physEntList.Count > 0)
                {
                    Drone d = (Drone)physEntList[0];
                    dV1 = physEntList[0].theta;
                    dV2 = physEntList[0].posDot.X;
                    dV3 = physEntList[0].posDotDot.X;
                    //dV4 = physEntList[0].health;
                    dV4 = physEntList[d.targetIndex].health;
                }


                ballPos.X = Math.Min(Math.Max(textureBall.Width / 2, ballPos.X), graphics.PreferredBackBufferWidth - textureBall.Width / 2);
                ballPos.Y = Math.Min(Math.Max(textureBall.Height / 2, ballPos.Y), graphics.PreferredBackBufferHeight - textureBall.Height / 2);

                for(int i = 0; i < station.blocks.Count; i++)
                {
                    //int counter = 0;
                    //TODO: Fix
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
                                Drone d = (Drone)physEntList[physEntList.Count - 1];
                                d.AttackEvent += drone_attackEvent;
                                d.HarvestEvent += drone_harvestEvent;
                                station.blocks[i].countUp = 0;
                            }

                            
                        }
                    }
                    
                }

                ScanForCollisions(gameTime);

                //mining
                for (int i = 0; i < physEntList.Count; i++)
                {


                    //if(physEntList[i].type == "miningDrone")
                    //{
                    //    //d.test += _Event;
                    //    Drone d = (Drone)physEntList[i];
                    //    if(d.targetPhysEnt != null)
                    //    {
                    //        if (d.miningProx)
                    //        {
                    //            d.targetPhysEnt.TakeDamage(d.DealDamage());

                    //            if(d.targetPhysEnt.health < 0)
                    //            {
                    //                SpawnDebris((Asteroid)d.targetPhysEnt);
                    //                DestroyAt(physEntList.IndexOf(d.targetPhysEnt));
                    //                d.ReceiveOrder(0, d.home.pos);
                    //            }
                    //        }

                    //    }
                    //    //d.test += _Event;
                    //    //d.test += _Event;


                    //    physEntList[i] = d;
                    //}
                    physEntList[i].Update(gameTime);

                    if(physEntList[i].type == "asteroid")
                    {
                        if(physEntList[i].health < 0)
                        {
                            SpawnDebris((Asteroid)physEntList[i]);



                            DestroyAt(i);
                            i--;



                        }
                    }
                }
                
                //debris
                for(int i = 0; i < debrisList.Count; i++)
                {
                    debrisList[i].Update(gameTime);
                }

                //Harvesting
                for(int i = 0; i < physEntList.Count; i++)
                {
                    if(physEntList[i].type == "harvestDrone")
                    {
                        Drone d = (Drone)physEntList[i];
                        if (d.harvestingEnabled)
                        {
                            if(d.relTarget.Length() < 75)
                            {
                                Debris deb = (Debris)d.targetPhysEnt;
                                Harvest(d, deb);
                                

                                if (deb.kill)
                                {
                                    d.GoHome();
                                    debrisList.Remove(deb);
                                    radial.followingIndex = selectedPhysEnt.index;
                                    radial.followingType = 1;
                                }

                                physEntList[i] = d;
                            }
                        }


                    }
                }

                //Docking
                for(int i = 0; i < physEntList.Count; i++)
                {
                    if(physEntList[i].GetType() == (new Drone()).GetType())
                    {
                        Drone d = (Drone)physEntList[i];
                        if (d.dockingEnabled)
                        {
                            if(d.relTarget.Length() < 50)
                            {
                                ReceiveResources(d);
                                d.DonateResources();
                            }
                        }
                    }
                }

                base.Update(gameTime);
            }
            else if (gameState == (int)GameState.Paused)
            {
                var mState = Mouse.GetState();
                DealWithMouseEvent(mState);
            }
            else if (gameState == (int)GameState.BuildMenuPaused)
            {
                buildScreen.Update(station);
                var mState = Mouse.GetState();

                if (mouseClicked == false)
                {
                    if (mState.LeftButton == ButtonState.Pressed)
                    {
                        mouseClicked = true;
                        Rectangle r = new Rectangle(mState.Position.X, mState.Y, 1, 1);
                        buildScreen.MouseClick(r);


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

        void drone_harvestEvent(object sender)
        {
            Drone d = (Drone)sender;
            Debris deb = (Debris)d.targetPhysEnt;
            //d.ReceiveResources(deb);
            //deb.RemoveResources();
            Harvest(d, deb);
            if (deb.kill)
            {
                d.GoHome();
                if (radial.follow == deb)
                {
                    radial.Off();
                }
                debrisList.Remove(deb);
                
            }
        }

        void drone_attackEvent(object sender)
        {
            Drone d = (Drone)sender;
            d.targetPhysEnt.TakeDamage(d.DealDamage());
        }

        public void DealWithMouseEvent(MouseState mState)
        {
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
                        if (radial.SwitchButton.enabled)
                        {
                            if (r.Intersects(radial.SwitchButton.box))
                            {
                                //selectedPhysEnt.entity = physEntList[radial.followingIndex];
                                //selectedPhysEnt.index = radial.followingIndex;
                                selectedPhysEnt.entity = radial.follow;
                                selectedPhysEnt.index = physEntList.IndexOf(radial.follow);

                            }
                        }
                    }
                    else if (r.Intersects(resourcesPanelRect))
                    {
                        radial.Off();
                    }
                    else if (r.Intersects(selectedEntityPanelRect))
                    {
                        radial.Off();
                    }
                    else if (r.Intersects(GroupButtonsRect))
                    {
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
                    else if (r.Intersects(PauseButtonRect))
                    {
                        if(gameState == (int)GameState.Paused)
                        {
                            gameState = (int)GameState.MainState;
                        }
                        else
                        {
                            gameState = (int)GameState.Paused;
                        }
                        //gameState = (int)GameState.Paused;
                        return;
                    }
                    else
                    {
                        radial.isFollowing = false;
                        for (int i = 0; i < physEntList.Count; i++)
                        {
                            if (r.Intersects(physEntList[i].hitBox))
                            {
                                //radial.Follow(physEntList[i], i);
                                //radial.follow.radialMenuFollows = false;
                                radial.ChangeTarget(physEntList[i]);
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
                                            //s[2] = true;
                                            break;
                                    }
                                }
                                //else if(physEntList[i].type = "dockingStation")


                                if (physEntList[i].playerControled)
                                {
                                    s[5] = true;
                                }

                                radial.SetState(s);
                                radial.followingType = 1;
                                //physEntList[i].radialMenuFollows = true;
                                break;
                            }
                        }
                        for (int i = 0; i < debrisList.Count; i++)
                        {
                            if (r.Intersects(debrisList[i].hitBox))
                            {
                                //radial.Follow(debrisList[i], i);
                                radial.ChangeTarget(debrisList[i]);
                                bool[] s = new bool[] { true, false, false, false, false, false };

                                if (physEntList[selectedPhysEnt.index].type == "harvestDrone")
                                {
                                    s[2] = true;
                                }
                                radial.SetState(s);
                            }
                        }
                        if (!radial.isFollowing)
                        {
                            radial.UpdateSpace(new Vector2((float)mState.Position.X, (float)mState.Position.Y));
                            bool[] s = new bool[] { true, false, false, false, false, false };
                            radial.SetState(s);
                            radial.followingType = 2;
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
        }

        public void SpawnDebris(Asteroid source)
        {
            Random r = new Random();
            for(int i = 0; i < r.Next(1, 3); i++)
            {
                Debris d = new Debris(source);
                debrisList.Add(d);
            }
            if(source.diam > 50)
            {
                if(1 == 1)
                {
                    Asteroid a = new Asteroid(source);
                    a.idNo = "HHHHHHHHHHHHHHHHHHH";
                    physEntList.Add(a);
                }
            }
        }

        public void Harvest(Drone d, Debris debris)
        {
            d.ReceiveResources(debris);
            debris.RemoveResources();
        }

        public void DestroyAt(int i)
        {
            
            if (radial.isFollowing)
            {
                if(radial.followingType == 1)
                {
                    if (radial.followingIndex > i)
                    {
                        radial.followingIndex--;
                    }
                    else if(radial.followingIndex == i)
                    {
                        radial.followingIndex = 0;
                        radial.followingType = 0;
                        radial.Off();
                    }
                }
                
                //else if(radial.followingIndex)
            }
            for (int j = 0; j < physEntList.Count; j++)
            {
                if (physEntList[j].GetType() == (new Drone()).GetType())
                {
                    Drone d = (Drone)physEntList[j];

                    //if (d.targetIndex >= i)
                    //{
                    //    if(d.targetPhysEnt.GetType() != (new Debris()).GetType())
                    //    {
                    //        d.targetIndex--;
                    //        d.targetPhysEnt = physEntList[d.targetIndex];
                    //        physEntList[j] = d;
                    //    }

                    //}
                    //else if(d.targetIndex == i)
                    //{
                    //    d.ReceiveOrder(0, d.home.pos);
                    //}
                    if (d.targetPhysEnt == physEntList[i])
                    {
                        d.ReceiveOrder(0, d.home.pos);
                    }
                }
            }
            physEntList.RemoveAt(i);
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
            

            if (gameState == (int)GameState.MainMenu)
            {

            }
            else if (gameState == (int)GameState.Loading)
            {

            }
            else if(gameState == (int)GameState.MainState)
            {
                DrawStation();
                DrawRegular();


                base.Draw(gameTime);
            }
            
            else if (gameState == (int)GameState.Paused)
            {
                DrawStation();
                DrawRegular();


                base.Draw(gameTime);
            }
            else if (gameState == (int)GameState.BuildMenuPaused)
            {
                DrawBuildScreen();
            }

            spriteBatch.End();
        }

        //public void Draw

        public void DrawRegular()
        {
            spriteBatch.DrawString(font, "Theta: " + dV1.ToString(), new Vector2(200, 100), Color.Black);
            spriteBatch.DrawString(font, dV2.ToString(), new Vector2(200, 120), Color.Black);
            spriteBatch.DrawString(font, dV3.ToString(), new Vector2(200, 140), Color.Black);
            spriteBatch.DrawString(font, dV4.ToString(), new Vector2(200, 160), Color.Black);
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
                    if (physEntList[i].posDotDot.Length() != 0)
                    {
                        DrawLine(physEntList[i].pos, physEntList[i].pos + physEntList[i].posDotDot, Color.White);
                        DrawLinearEffect(physEntList[i].pos, physEntList[i].pos + physEntList[i].posDot);
                    }
                }
            }
            if (debrisList.Count > 0)
            {
                for (int i = 0; i < debrisList.Count; i++)
                {
                    spriteBatch.Draw(
                        debrisList[i].texture,
                        debrisList[i].hitBox,
                        Color.White
                        );
                }
            }

            DrawSelectedDisplay();
            if (radial.isOn)
            {
                DrawRadialMenu();
            }

            #region UI

            spriteBatch.Draw(
                resourcesPanel.texture,
                resourcesPanel.box,
                Color.White
                );
            DrawResources();
            #endregion
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
            if(selectedPhysEnt.entity.texture != null)
            {
                selectedEntityPanel.controls[0].texture = selectedPhysEnt.entity.texture;
                spriteBatch.Draw(selectedEntityPanel.controls[0].texture, selectedEntityPanel.controls[0].box, Color.White);


                Vector2 offset = new Vector2(55, 5);
                Vector2 boxPos = new Vector2((float)selectedEntityPanel.box.Location.X, (float)selectedEntityPanel.box.Location.Y);
                //idNo
                spriteBatch.DrawString(font, selectedPhysEnt.entity.idNo, 
                    offset + boxPos,
                    Color.White);
                //type
                offset = new Vector2(55, 25);
                spriteBatch.DrawString(font, selectedPhysEnt.entity.type,
                    boxPos + offset,
                    Color.White);
            }
        }

        public void DrawResources()
        {
            int i = 0;
            int j = 0;
            foreach(string s in playerResources.Keys)
            {
                spriteBatch.DrawString(font, s,
                    new Vector2(50 + j * 100, 5 + 20 * i),
                    Color.White);
                spriteBatch.DrawString(font,
                    playerResources[s].ToString(),
                    new Vector2(105 + j * 100, 5 + 20 * i),
                    Color.White);
                if(i == 0) { i++; }
                else { i--; j++; }
            }
        }

        public void DrawBuildScreen()
        {
            //DrawBuildGrid();
            for(int i = 0; i < 11; i++)
            {
                for(int j = 0; j < 11; j++)
                {
                    spriteBatch.Draw(buildScreen.textureGrid[i, j], buildScreen.boxGrid[i, j], Color.White);
                }
            }
            if (buildScreen.buildOptionsMenu.enabled)
            {
                spriteBatch.Draw(buildScreen.buildOptionsMenu.texture, buildScreen.buildOptionsMenu.baseRect, Color.White);
                for(int i = 0; i < buildScreen.buildOptionsMenu.buttons.Count; i++)
                {
                    spriteBatch.Draw(buildScreen.buildOptionsMenu.buttons[i].texture,
                        buildScreen.buildOptionsMenu.buttons[i].rect,
                        Color.White);
                    spriteBatch.DrawString(font, 
                        buildScreen.buildOptionsMenu.buttons[i].content,
                        buildScreen.buildOptionsMenu.buttons[i].contentPos,
                        Color.White);
                    for(int j = 0; j < buildScreen.buildOptionsMenu.buttons[i].costString.Count; j++)
                    {
                        spriteBatch.DrawString(font,
                        buildScreen.buildOptionsMenu.buttons[i].costString[j],
                        buildScreen.buildOptionsMenu.buttons[i].costPos[j],
                        Color.White);
                        spriteBatch.DrawString(font,
                        buildScreen.buildOptionsMenu.buttons[i].costDouble[j].ToString(),
                        buildScreen.buildOptionsMenu.buttons[i].costPos[j] + new Vector2(0,14),
                        Color.White);
                    }
                    
                }
            }
        }

        public void DrawBuildGrid()
        {
            Texture2D tex = _globals.textures[6, 1];
            Texture2D[,] grid = new Texture2D[11, 11]; 
            for (int i = -5; i < 6; i++)
            {
                for(int j = -5; j < 6; j++)
                {
                    grid[i, j] = tex;
                }
            }
            for(int i = 0; i < station.blocks.Count; i++)
            {
                grid[(int)station.blocks[i].gridPos[0], (int)station.blocks[i].gridPos[1]] = station.blocks[i].texture;
            }

        }
        
        public void GoHomeYoureDrunk(int subjInd)
        {
            Drone d = (Drone)physEntList[subjInd];
            d.GoHome();
            physEntList[subjInd] = d;
        }

        public void IssueCommand(int command)
        {
            Drone d = (Drone)physEntList[selectedPhysEnt.index];

            switch (command)
            {
                case 0:
                    if (radial.isFollowing)
                    {
                        d.ReceiveOrder(0, new Vector2(0, 0), radial.follow);
                    }
                    else
                    {
                        d.ReceiveOrder(0, radial.center);
                    }
                    break;
                case 1:
                    d.ReceiveOrder(1, new Vector2(0, 0), radial.follow);
                    break;
                case 2:
                    d.ReceiveOrder(2, new Vector2(0, 0), radial.follow);
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

        public void ReceiveResources(Drone drone)
        {
            for(int i = 0; i < _globals.materials.Length; i++)
            {
                if(drone.content[_globals.materials[i]] > 0)
                {
                    playerResources[_globals.materials[i]] += 1;
                }
            }
        }

        public Vector2 PosFromVelocity(double[] pos, double[] posDot, GameTime gametime)
        {
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
            float elasticity;
            elasticity = 0.9f;
            //elasticity = 1;
            
            Vector2 res1, res2;
            float r1x, r1y, r2x, r2y;

            double p1, p2, p3, p4, p5;
            //p1 = ent1.posDot.X;
            //p2 = 2 * ent2.mass / (ent1.mass + ent2.mass);
            //p3 = VectDot(ent1.posDot - ent2.posDot, ent1.pos - ent2.pos);
            //p4 = Math.Pow((ent1.pos - ent2.pos).Length(), 2);
            //p5 = (ent1.pos.X - ent2.pos.X);
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
            r2x = (float)(ent2.posDot.X - 2 * ent1.mass / (ent1.mass + ent2.mass) *
                VectDot(ent2.posDot - ent1.posDot, ent2.pos - ent1.pos) /
                Math.Pow((ent2.pos - ent1.pos).Length(), 2) *
                (ent2.pos.X - ent1.pos.X));
            r2y = (float)(ent2.posDot.Y - 2 * ent1.mass / (ent1.mass + ent2.mass) *
                VectDot(ent2.posDot - ent1.posDot, ent2.pos - ent1.pos) /
                Math.Pow((ent2.pos - ent1.pos).Length(), 2) *
                (ent2.pos.Y - ent1.pos.Y));

            res1 = new Vector2(r1x * elasticity, r1y * elasticity);
            res2 = new Vector2(r2x * elasticity, r2y * elasticity);
            //res1 = 5 * res2;
            double KE10 = 0.5 * ent1.mass * Math.Pow(ent1.posDot.Length(), 2);
            double KE20 = 0.5 * ent2.mass * Math.Pow(ent2.posDot.Length(), 2);
            double KE11 = 0.5 * ent1.mass * Math.Pow(res1.Length(), 2);
            double KE21 = 0.5 * ent2.mass * Math.Pow(res2.Length(), 2);
            double d1 = Math.Abs(KE10 - KE11);
            double d2 = Math.Abs(KE20 - KE21);

            ent1.TakeDamage(d1);
            ent2.TakeDamage(d2);
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
                        physEntList[i].posDot = newVects[0];
                        physEntList[j].posDot = newVects[1];
                    }
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
        }

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
            if((v1.X == 0 && v1.Y == 0) || (v2.X == 0 && v2.Y == 0))
            {
                return 0;
            }

            float ret = (v1.X * v2.X + v1.Y * v2.Y);
            ret = ret / v2.Length();
            return ret;
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
            double speedRandMult = 20;
            double speedMin = 5;
            Vector2 direction = new Vector2(0,0);

            switch (lane)
            {
                case 0:
                    newAsteroid = new Asteroid(
                        1,
                        51,
                        "a0",
                        Math.Pow(diam / 2, 2),
                        station.blocks[0].pos.X - 200,
                        station.blocks[0].pos.Y,
                        0, 0
                        );
                    physEntList.Add(newAsteroid);
                    return;
                    break;
                case 1:
                    xSpawn = (float)(25 * r.NextDouble() / 100 * graphics.PreferredBackBufferWidth);
                    xTarget = (float)((37.5 * r.NextDouble() / 100 - .1) * graphics.PreferredBackBufferWidth);
                    speed = (int)(speedRandMult * r.NextDouble() + speedMin);
                    direction = new Vector2(xSpawn - xTarget, -graphics.PreferredBackBufferHeight);
                    //(int lane, double diameter, string name, double m, double x = 0, double y = 0, double xDot = 0, double yDot = 0)
                    diam = (50 * r.NextDouble() + 40);
                    
                    break;
            }

            newAsteroid = new Asteroid(
                        1,
                        diam,
                        "a" + physEntList.Count.ToString(),
                        Math.Pow(diam / 2, 2),
                        graphics.PreferredBackBufferWidth - xSpawn,
                        graphics.PreferredBackBufferHeight + 100,
                        direction.X / direction.Length() * speed,
                        direction.Y / direction.Length() * speed
                        );
            for(int i = 0; i < physEntList.Count; i++)
            {
                if (newAsteroid.hitCircle.Overlaps(physEntList[i].hitCircle))
                {
                    return;
                }
            }
            physEntList.Add(newAsteroid);
        }


        public void DrawLine(Vector2 start, Vector2 end, Color c)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            spriteBatch.Draw(textureBlackLine,
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
