using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Colnaught
{


    enum Listof_Texture : Int32
    {
        Grass = 0,
        CityCenter,
        Residential,
        Commercial,
        Industrial,
        Road,
        Residential_Structure,
        Commercial_Structure,
        Industrial_Structure,
        Construction,

        Panel1 = 300,
        Button1
    };

    enum Listof_MouseMode
    {
        Default,
        Building        
    }

    



    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public partial class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont basicfont;

        float theta;
        Model model;
        Model model2;
        Texture2D[] TileTexture = new Texture2D[500];

        Vector3 Camera = new Vector3(0, 0, 0);

        float zoom = 1.0f;
        bool Zoomed = false;

        Point MouseDragStart;
        bool MouseDraging;

        int OldMouseScrollWheelValue = Mouse.GetState().ScrollWheelValue;

        Point MouseDragScreenScrollStart;
        Listof_MouseMode MouseMode = Listof_MouseMode.Default;
        bool MouseLeftClicked, MouseRightClicked;

        Listof_Structures Building;

        Point BuildPoint1 = new Point(-1, -1);
        Point BuildPoint2 = new Point(-1, -1);
        Rectangle BuildRect;
        bool Buildable;

        int BuildCost = 0;

        Point Screen_Scroll = new Point(0, 0);
        public Point Screen_Size;

        Interface _interface;
        City _city;
        Encyclopedia _e;


        decimal Currency = 75000;
        decimal Income = 0;

        long Date = 0;

        double DayCounter = 0;
        double DaySpeed = 60f / 30f;

        Vector2 CenterTile;
        



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Screen_Size.X = 1920;
            Screen_Size.Y = 1080;
            
            graphics.ApplyChanges();

            //RasterizerState rs = new RasterizerState { MultiSampleAntiAlias = true };
            //GraphicsDevice.RasterizerState = rs;
            //graphics.PreferMultiSampling = true;

            base.Initialize();
            Window.IsBorderless = true;
            Window.Position = new Point(0, 0);
            this.IsMouseVisible = true;            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
            TileTexture[(int)Listof_Texture.Grass] = Content.Load<Texture2D>("Grass");
            TileTexture[(int)Listof_Texture.CityCenter] = Content.Load<Texture2D>("Tile");
            TileTexture[(int)Listof_Texture.Panel1] = Content.Load<Texture2D>("Panel1");
            TileTexture[(int)Listof_Texture.Button1] = Content.Load<Texture2D>("Button1");

            TileTexture[(int)Listof_Texture.Road] = Content.Load<Texture2D>("Road1");

            TileTexture[(int)Listof_Texture.Residential] = Content.Load<Texture2D>("Residential");
            TileTexture[(int)Listof_Texture.Commercial] = Content.Load<Texture2D>("Commercial");
            TileTexture[(int)Listof_Texture.Industrial] = Content.Load<Texture2D>("Industrial");

            TileTexture[(int)Listof_Texture.Residential_Structure] = Content.Load<Texture2D>("Residential_1");
            TileTexture[(int)Listof_Texture.Commercial_Structure] = Content.Load<Texture2D>("Commercial_1");
            TileTexture[(int)Listof_Texture.Industrial_Structure] = Content.Load<Texture2D>("Industrial_1");


            TileTexture[(int)Listof_Texture.Construction] = Content.Load<Texture2D>("Construction1");

            model = Content.Load<Model>("CityCenter");
            model2 = Content.Load<Model>("Tower1");
            basicfont = Content.Load<SpriteFont>("romulus");

            _interface = new Interface(Screen_Size);
            _e = new Encyclopedia();
            _city = new City(new Point(200, 200), _e);
            

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                Screen_Scroll.X -= (int)(128);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Screen_Scroll.X += (int)(128);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Screen_Scroll.Y -= (int)(256);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Screen_Scroll.Y += (int)(256);


            
            //CenterTile.Y = ((Screen_Scroll.Y + Screen_Size.Y / 2) / zoom - 128) / 64f;
            //CenterTile.X = ((Screen_Scroll.X + Screen_Size.X / 2) / zoom - 128) / 64f;

            CenterTile.Y = ((Mouse.GetState().Position.Y + Screen_Scroll.Y) / zoom - 128) / 64f;            
            CenterTile.X = ((Mouse.GetState().Position.X + Screen_Scroll.X) / zoom - 128) / 64f;            

            Zoomed = false;
            if (Zoomed == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.OemMinus) || Mouse.GetState().ScrollWheelValue < OldMouseScrollWheelValue)
                {
                    if (zoom > 0.25f)
                    {
                        zoom -= 0.25f;
                        Screen_Scroll.Y -= (int)(32 + CenterTile.Y * 16);
                        Screen_Scroll.X -= (int)(32 + CenterTile.X * 16);
                        Zoomed = true;                        
                    }
                }                    
                if (Keyboard.GetState().IsKeyDown(Keys.OemPlus) || Mouse.GetState().ScrollWheelValue > OldMouseScrollWheelValue)
                {                    
                    zoom += 0.25f;                    
                    Screen_Scroll.Y += (int)(32 + CenterTile.Y * 16);
                    Screen_Scroll.X += (int)(32 + CenterTile.X * 16);
                    Zoomed = true;                    
                }                    
            }

            OldMouseScrollWheelValue = Mouse.GetState().ScrollWheelValue;
            if (Keyboard.GetState().IsKeyUp(Keys.OemMinus) && Keyboard.GetState().IsKeyUp(Keys.OemPlus))
                Zoomed = false;


            UI_MouseDrag();
            Update_Interface();            


            //_city.Calculate_Growth();

            if (DayCounter >= 100)
            {
                _city.Calculate_Land_Value();
                _city.Calculate_Growth();
                
                _city.Calculate_Traffic();
                _city.Calculate_JPC();


                Income = _city.Calculate_Taxes();
                Currency += Income;
                DayCounter = 0;
                Date++;
            }

            DayCounter += DaySpeed;


            base.Update(gameTime);
        }
        

        void SetRoadSprites(HashSet<Point> RoadList)
        {
            bool L, R, T, B;
            HashSet<Point> Crawl = new HashSet<Point>();           

        }
        
        
    }
}
