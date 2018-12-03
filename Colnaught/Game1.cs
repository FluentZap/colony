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

        Panel1 = 300,
        Button1
    };

    enum Listof_MouseMode
    {
        Default,
        Building,
        DragBuilding
    }

    



    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        float theta;
        Model model;
        Model model2;
        Texture2D[] TileTexture = new Texture2D[500];

        Vector3 Camera = new Vector3(0, 0, 0);

        float zoom = 1.0f;

        Point MouseDragStart;
        bool MouseDraging;

        Point MouseDragScreenScrollStart;
        Listof_MouseMode MouseMode = Listof_MouseMode.Default;
        bool MouseLeftClicked, MouseRightClicked;

        Listof_Structures Building;

        Point BuildRectPoint;
        Rectangle BuildRect;

        Point Screen_Scroll;
        public Point Screen_Size;

        Interface _interface;
        City _city;
        Encyclopedia _e;

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

            RasterizerState rs = new RasterizerState { MultiSampleAntiAlias = true };
            GraphicsDevice.RasterizerState = rs;
            graphics.PreferMultiSampling = true;

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


            model = Content.Load<Model>("CityCenter");
            model2 = Content.Load<Model>("Tower1");


            _interface = new Interface(Screen_Size);
            _city = new City();
            _e = new Encyclopedia();

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
                Camera.X += 1000f * delta;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Camera.X -= 1000f * delta;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Camera.Y += 1000f * delta;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Camera.Y -= 1000f * delta;

            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                zoom -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                zoom += 0.01f;

            

            UI_MouseDrag();
            Update_Interface();

            base.Update(gameTime);
        }



        void Update_Interface()
        {
            bool onPanel = false;
            Vector2 mPos = new Vector2((Mouse.GetState().Position.X + Screen_Scroll.X) / zoom, (Mouse.GetState().Position.Y + Screen_Scroll.Y) / zoom);
            Point sel_pos;
            sel_pos.X = (int)Math.Floor((mPos.Y - 128) / 64 + (mPos.X - 128) / 128);
            sel_pos.Y = (int)Math.Floor((-mPos.X - 128) / 128 + (mPos.Y - 128) / 64);

            foreach (var item in _interface.Dictionaryof_CityScreenButtons)
            {
                item.Value.color = Color.White;
                if (item.Value.Location.Contains( Mouse.GetState().Position) == true)
                {
                    onPanel = true;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (item.Key == Listof_ViewInterfaceButtons.BuildButton1)
                        {
                            MouseMode = Listof_MouseMode.Building;
                            Building = Listof_Structures.CityCenter;
                        }

                        if (item.Key == Listof_ViewInterfaceButtons.BuildButton2)
                        {
                            MouseMode = Listof_MouseMode.DragBuilding;
                            BuildRect = Rectangle.Empty;
                            Building = Listof_Structures.ZoneResidential;
                        }

                        if (item.Key == Listof_ViewInterfaceButtons.BuildButton3)
                        {
                            MouseMode = Listof_MouseMode.DragBuilding;
                            BuildRect = Rectangle.Empty;
                            Building = Listof_Structures.ZoneCommercial;
                        }

                        if (item.Key == Listof_ViewInterfaceButtons.BuildButton4)
                        {
                            MouseMode = Listof_MouseMode.DragBuilding;
                            BuildRect = Rectangle.Empty;
                            Building = Listof_Structures.ZoneIndustrial;
                        }

                        if (item.Key == Listof_ViewInterfaceButtons.BuildButton5)
                        {
                            MouseMode = Listof_MouseMode.DragBuilding;
                            BuildRect = Rectangle.Empty;
                            Building = Listof_Structures.RoadDirt;
                        }
                    }
                        
                         


                    if (item.Value.Type == Listof_ButtonType.Button) item.Value.color = new Color(200, 200, 200);
                }
            }


            //Placed Buildings
            if (MouseMode == Listof_MouseMode.Building)
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed) MouseRightClicked = true;

                if (MouseRightClicked && Mouse.GetState().RightButton == ButtonState.Released)
                {
                    if (Math.Abs(MouseDragScreenScrollStart.X - Screen_Scroll.X) < 4 && Math.Abs(MouseDragScreenScrollStart.Y - Screen_Scroll.Y) < 4)
                    {
                        MouseRightClicked = false;
                        MouseLeftClicked = false;
                        MouseMode = Listof_MouseMode.Default;
                    }
                }


                if (!onPanel && Mouse.GetState().LeftButton == ButtonState.Pressed) MouseLeftClicked = true;
                if (MouseLeftClicked && Mouse.GetState().LeftButton == ButtonState.Released)
                {                    
                    _city.TileMap[sel_pos.X, sel_pos.Y].Type = Building;
                    MouseLeftClicked = false;
                    if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                        MouseMode = Listof_MouseMode.Default;
                }
            }

            //Drag Building
            if (MouseMode == Listof_MouseMode.DragBuilding)
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed) MouseRightClicked = true;

                if (MouseRightClicked && Mouse.GetState().RightButton == ButtonState.Released)
                {
                    if (Math.Abs(MouseDragScreenScrollStart.X - Screen_Scroll.X) < 4 && Math.Abs(MouseDragScreenScrollStart.Y - Screen_Scroll.Y) < 4)
                    {
                        MouseRightClicked = false;
                        MouseLeftClicked = false;
                        MouseMode = Listof_MouseMode.Default;
                    }
                }


                if (!onPanel && MouseLeftClicked == false && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    MouseLeftClicked = true;
                    BuildRect = new Rectangle(sel_pos, new Point(1, 1));
                    BuildRectPoint = sel_pos;
                }

                if (!onPanel && MouseLeftClicked == true && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {

                    if (BuildRectPoint.X <= sel_pos.X)
                    {
                        BuildRect.X = BuildRectPoint.X;
                        BuildRect.Width = sel_pos.X - BuildRect.X + 1;
                    }
                    else
                    {
                        BuildRect.X = sel_pos.X;
                        BuildRect.Width = BuildRectPoint.X + 1 - sel_pos.X;
                    }

                    if (BuildRectPoint.Y <= sel_pos.Y)
                    {
                        BuildRect.Y = BuildRectPoint.Y;
                        BuildRect.Height = sel_pos.Y - BuildRect.Y + 1;
                    }
                    else
                    {
                        BuildRect.Y = sel_pos.Y;
                        BuildRect.Height = BuildRectPoint.Y + 1 - sel_pos.Y;
                    }


                    //BuildRect.Width = sel_pos.X - BuildRect.X + 1;
                    //BuildRect.Height = sel_pos.Y - BuildRect.Y + 1;
                }



                if (MouseLeftClicked && Mouse.GetState().LeftButton == ButtonState.Released)
                {                                     

                    for (int x = BuildRect.Left; x < BuildRect.Right; x++)
                        for (int y = BuildRect.Top; y < BuildRect.Bottom; y++)
                        {
                            _city.TileMap[x, y].Type = Building;
                        }

                    MouseLeftClicked = false;
                    if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                        MouseMode = Listof_MouseMode.Default;
                    else
                        BuildRect = Rectangle.Empty;
                }
            }

        }

        void UI_MouseDrag()
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (!MouseDraging)
                {
                    MouseDraging = true;
                    MouseDragStart = Screen_Scroll + Mouse.GetState().Position;
                    MouseDragScreenScrollStart = Screen_Scroll;
                }
            }

            if (MouseDraging)
            {
                Screen_Scroll = (MouseDragStart - Mouse.GetState().Position);
                if (Mouse.GetState().RightButton != ButtonState.Pressed) MouseDraging = false;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            Vector2 pos;
            Point sel_pos;
            Vector2 Tile_Size = new Vector2(zoom, zoom);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            Rectangle ScreenR;
            //sel_pos = (Mouse.GetState().Position + Screen_Scroll) / new Point(128, 256);

            ScreenR.X = Screen_Scroll.X / 128;
            ScreenR.Y = Screen_Scroll.Y / 256;
            ScreenR.Width = 10; //(int)Math.Ceiling((double)Screen_Size.X / 128);
            ScreenR.Height = 10; //(int)Math.Ceiling((double)Screen_Size.Y / 256);
                                 //ScreenR.Inflate(10, 10);

            Vector2 mPos = new Vector2((Mouse.GetState().Position.X + Screen_Scroll.X) / zoom, (Mouse.GetState().Position.Y + Screen_Scroll.Y) / zoom);

            sel_pos.X = (int)Math.Floor((mPos.Y - 128) / 64 + (mPos.X - 128) / 128);
            sel_pos.Y = (int)Math.Floor((-mPos.X - 128) / 128 + (mPos.Y - 128) / 64);
            
            for (int y = 0; y < 200; y++)
                for (int x = 0; x < 200; x++)
                {
                    pos.X = x * 64 * zoom - y * 64 * zoom - 64 * zoom - Screen_Scroll.X;
                    pos.Y = y * 32 * zoom + x * 32 * zoom - Screen_Scroll.Y;
                    Rectangle rec = new Rectangle((int)sel_pos.X, (int)sel_pos.Y, 1, 1);
                    rec.Inflate(20, 20);
                    //if (rec.Contains(new Point(x, y)))
                    //{                        
                    //}
                    //else
                    
                    
                    spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].Texture], pos, null, Color.White, 0, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                    
                    if (MouseMode == Listof_MouseMode.Building)
                    {
                        if (x == sel_pos.X && y == sel_pos.Y)
                            spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, null, Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                    }

                    if (MouseMode == Listof_MouseMode.DragBuilding)
                    {
                        if (x == sel_pos.X && y == sel_pos.Y) spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, null, Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                        if (BuildRect.Contains(new Point(x, y)))
                            spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, null, Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);                                                
                    }

                    //if (x == sel_pos.X && y == sel_pos.Y)
                    //spriteBatch.Draw(TileTexture[(int)Listof_Texture.Grass], pos, null, Color.White, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                }


            DrawInterface();
            spriteBatch.End();
            base.Draw(gameTime);
        }



        void DrawInterface()
        {
            foreach (var item in _interface.Dictionaryof_CityScreenButtons)
            {
                if (item.Value.Type == Listof_ButtonType.Panel) spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], item.Value.Location, item.Value.color);
                if (item.Value.Type == Listof_ButtonType.Button) spriteBatch.Draw(TileTexture[(int)Listof_Texture.Button1], item.Value.Location, item.Value.color);
            }



        }



        void DrawModel(Model mod, Matrix world)
        {

            foreach (var mesh in mod.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    //Matrix viewmatrix = Matrix.CreateRotationX(0) * Matrix.CreateTranslation(new Vector3(0, 0, -2));
                    theta += 0.001f;

                    effect.World = world;
                    effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 8), new Vector3(0, 0, 0), new Vector3(0, 1, 0)) * Matrix.CreateFromYawPitchRoll(0, -MathHelper.PiOver4, -MathHelper.PiOver4) * Matrix.CreateTranslation(Camera);                   
                    float ratio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
                    effect.Projection = Matrix.CreateOrthographic(zoom * ratio, zoom, 0.0f, 1000.0f);
                    //effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight, 1, 200);
                }
                mesh.Draw();
            }
        }
    }
}
