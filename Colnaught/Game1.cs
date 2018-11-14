using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Colnaught
{
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
        Texture2D Tile;
        Vector3 Camera = new Vector3(0, 0, 0);
        float zoom = 100;

        Point MouseDragStart;
        bool MouseDraging;

        Point MouseDragScreenScrollStart;


        Point Screen_Scroll;
        public Point Screen_Size;

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

            Window.IsBorderless = true;
            this.IsMouseVisible = true;            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            Tile = Content.Load<Texture2D>("Tile");
            model = Content.Load<Model>("CityCenter");
            model2 = Content.Load<Model>("Tower1");

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
                zoom -= 1f;
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                zoom += 1f;

            if (zoom < 1) zoom = 1;

            UI_MouseDrag();

            base.Update(gameTime);
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


            Vector2 pos, sel_pos;
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            Rectangle ScreenR;
            //sel_pos = (Mouse.GetState().Position + Screen_Scroll) / new Point(128, 256);

            ScreenR.X = Screen_Scroll.X / 128;
            ScreenR.Y = Screen_Scroll.Y / 256;
            ScreenR.Width = 10; //(int)Math.Ceiling((double)Screen_Size.X / 128);
            ScreenR.Height = 10; //(int)Math.Ceiling((double)Screen_Size.Y / 256);
                                 //ScreenR.Inflate(10, 10);            

            Vector2 mPos = new Vector2(Mouse.GetState().Position.X + Screen_Scroll.X, Mouse.GetState().Position.Y + Screen_Scroll.Y);

            sel_pos.X = (float)Math.Floor((mPos.Y - 128) / 64F + (mPos.X - 128) / 128F);
            sel_pos.Y = (float)Math.Floor((-mPos.X - 128) / 128F + (mPos.Y - 128) / 64F);

            /*
            for (int x = ScreenR.Left; x < ScreenR.Right; x++)
                for (int y = ScreenR.Top; y < ScreenR.Bottom; y++)
                {

                    pos.Y = x * 32 + y * 32 + Camera.Y;
                    pos.X = x * 64 - y * 64 + Camera.X;
                    spriteBatch.Draw(Tile, pos, Color.White);
                    if (x == sel_pos.X && y == sel_pos.Y)
                        spriteBatch.Draw(Tile, pos, Color.Red);

                }            
            */
            for (int y = 0; y < 100; y++)
                for (int x = 0; x < 100; x++)
                {
                    //pos.X = x * 64 - y * 64 - (128 / 2) + Camera.X;
                    pos.X = x * 64 - y * 64 - 64 - Screen_Scroll.X;
                    pos.Y = y * 32 + x * 32 - Screen_Scroll.Y;                    
                    spriteBatch.Draw(Tile, pos, Color.White);
                    if (x == sel_pos.X && y == sel_pos.Y)
                        spriteBatch.Draw(Tile, pos, Color.Red);
                }


            //spriteBatch.Draw(Tile, new Rectangle(pos.X, pos.Y, 128, 256), new Rectangle(0, 0, 128, 256), Color.White);
            //spriteBatch.Draw(Tile, new Rectangle(pos.X + 128, pos.Y, 128, 256), new Rectangle(0, 0, 128, 256), Color.White);


            /*theta += 0.001f;
            for (int x = 0; x < 20; x++)
            {
                for (int y = -20; y < 0; y++)
                {
                    Matrix world = Matrix.Identity;

                    world = Matrix.Identity * Matrix.CreateTranslation(y * 2, x * 2, 0);
                    DrawModel(model2, world);
                }
            }
            */

            spriteBatch.End();
            base.Draw(gameTime);
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
