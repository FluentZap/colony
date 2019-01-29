using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Colnaught
{
    partial class Game1
    {




        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            bool DrawMapTile = true;
            Color MapTileColor = Color.White;
            Vector2 pos;
            Point sel_pos;
            Vector2 Tile_Size = new Vector2(zoom, zoom);
            Rectangle distrect = Rectangle.Empty;


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



            for (int y = 0; y < _city.CityArea.Height; y++)
                for (int x = 0; x < _city.CityArea.Width; x++)
                {
                    pos.X = x * 64 * zoom - y * 64 * zoom - 64 * zoom - Screen_Scroll.X;
                    pos.Y = y * 32 * zoom + x * 32 * zoom - Screen_Scroll.Y;
                    Rectangle rec = new Rectangle((int)sel_pos.X, (int)sel_pos.Y, 1, 1);
                    rec.Inflate(20, 20);

                    Rectangle ScreenRect = new Rectangle(0, 0, Screen_Size.X, Screen_Size.Y);
                    ScreenRect.Y -= Convert.ToInt32(256 * zoom);
                    ScreenRect.X -= Convert.ToInt32(128 * zoom);
                    ScreenRect.Inflate(256 * zoom, 128 * zoom);
                    //if (rec.Contains(new Point(x, y)))
                    //{                        
                    //}
                    //else

                    //Need to make the tile loop perdicted
                    if (ScreenRect.Contains(pos))
                    {

                        DrawMapTile = true;
                        MapTileColor = Color.White;


                        //////////////Building Calculation///////////////

                        if (MouseMode == Listof_MouseMode.Building)
                        {

                            foreach (var district in _city.districts)
                                if (district.Area.Contains(sel_pos)) distrect = district.Area;

                            //Structures
                            if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Structure)
                            {
                                if (x == sel_pos.X && y == sel_pos.Y)
                                    spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                            }

                            //Zones
                            if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Zone)
                            {
                                if (x == sel_pos.X && y == sel_pos.Y)
                                {
                                    if (_city.TileMap[x, y].Buildable)                                    
                                        spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.White, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);                                                                            
                                    else                                    
                                        spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);                                                                            
                                    DrawMapTile = false;

                                }

                                if (BuildRect.Contains(new Point(x, y)))
                                {
                                    if (!_city.TileMap[x, y].Buildable && _e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].BuildingType == Listof_BuildTypes.Empty)
                                    {
                                        spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                                        DrawMapTile = false;
                                    }


                                    if (_city.TileMap[x, y].Buildable)
                                    {
                                        spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.White, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                                        DrawMapTile = false;
                                    }
                                }
                            }


                            //Roads
                            if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Road)
                            {

                                Point P1, P2, P3;
                                //Set building points X and Y
                                if (BuildPoint1.X <= BuildPoint2.X)
                                { P1.X = BuildPoint1.X; P2.X = BuildPoint2.X; }
                                else
                                { P1.X = BuildPoint2.X; P2.X = BuildPoint1.X; }

                                if (BuildPoint1.Y <= BuildPoint2.Y)
                                { P1.Y = BuildPoint1.Y; P2.Y = BuildPoint2.Y; }
                                else
                                { P1.Y = BuildPoint2.Y; P2.Y = BuildPoint1.Y; }


                                //Set Shortest Angle
                                int ShortX = Math.Abs(BuildPoint1.X - BuildPoint2.X), ShortY = Math.Abs(BuildPoint1.Y - BuildPoint2.Y), Temp;

                                if (Keyboard.GetState().IsKeyUp(Keys.LeftControl))
                                { Temp = ShortX; ShortX = ShortY; ShortY = Temp; }

                                if (ShortY >= ShortX)
                                { P3.X = BuildPoint2.X; P3.Y = BuildPoint1.Y; }
                                else
                                { P3.X = BuildPoint1.X; P3.Y = BuildPoint2.Y; }

                                if (y == P3.Y && x >= P1.X && x <= P2.X)
                                { spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), (Buildable == true ? Color.CornflowerBlue : Color.LightSalmon), 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1); DrawMapTile = false; }

                                if (x == P3.X && y >= P1.Y && y <= P2.Y)
                                { spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), (Buildable == true ? Color.CornflowerBlue : Color.LightSalmon), 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1); DrawMapTile = false; }

                            }


                            //CityCenter
                            if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.CityCenter)
                            {
                                Rectangle dist = new Rectangle(sel_pos.X, sel_pos.Y, 1, 1);
                                dist.Inflate(20, 20);

                                if (x == sel_pos.X && y == sel_pos.Y)
                                {
                                    spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                                    DrawMapTile = false;
                                }
                                if (dist.Contains(new Point(x, y)))
                                    MapTileColor = Color.CornflowerBlue;
                            }


                            //Hilight Districts
                            if (_city.districts.Count == 0)
                                if (_e.Dictionaryof_BuildItems[Building].BuildingType != Listof_BuildTypes.CityCenter)
                                    MapTileColor = Color.LightSalmon;

                            foreach (var district in _city.districts)
                            {
                                if (_e.Dictionaryof_BuildItems[Building].BuildingType != Listof_BuildTypes.CityCenter)
                                {
                                    if (district.Area.Contains(new Point(x, y)))
                                    {
                                        if (distrect.Contains(new Point(x, y)))
                                        {
                                            if (_city.TileMap[x, y].Buildable)
                                                MapTileColor = Color.LightGreen;
                                            else
                                                MapTileColor = Color.LightSalmon;
                                        }
                                        else
                                            MapTileColor = Color.CornflowerBlue;
                                    }
                                }
                                else
                                {
                                    if (district.Area.Contains(new Point(x, y)))
                                        MapTileColor = Color.CornflowerBlue;
                                }
                            }

                        }

                        if (DrawMapTile)
                            spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].Texture], pos, new Rectangle(0 + 128 * _city.TileMap[x, y].SpriteIndex, 0, 128, 256), MapTileColor, 0, new Vector2(), Tile_Size, SpriteEffects.None, 1);

                    }
                }

            //if (x == sel_pos.X && y == sel_pos.Y)
            //spriteBatch.Draw(TileTexture[(int)Listof_Texture.Grass], pos, null, Color.White, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);        


            DrawInterface(sel_pos);
            spriteBatch.End();
            base.Draw(gameTime);

        }




        void DrawInterface(Point sel_pos)
        {
            foreach (var item in _interface.Dictionaryof_CityScreenButtons)
            {
                if (item.Value.Type == Listof_ButtonType.Panel) spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], item.Value.Location, item.Value.color);
                if (item.Value.Type == Listof_ButtonType.Button) spriteBatch.Draw(TileTexture[(int)Listof_Texture.Button1], item.Value.Location, item.Value.color);
            }

            //RCI
            Vector2 RCI = new Vector2(Screen_Size.X - 400, Screen_Size.Y - 200);

            spriteBatch.DrawString(basicfont, "R", RCI + new Vector2(0, 0), Color.Green);
            spriteBatch.DrawString(basicfont, "C", RCI + new Vector2(30, 0), Color.Blue);
            spriteBatch.DrawString(basicfont, "I", RCI + new Vector2(60, 0), Color.Orange);

            spriteBatch.DrawString(basicfont, _city.ResidentialDemand.ToString(), RCI + new Vector2(0, 160), Color.Green);
            spriteBatch.DrawString(basicfont, _city.CommercialDemand.ToString(), RCI + new Vector2(30, 160), Color.Blue);
            spriteBatch.DrawString(basicfont, _city.IndustrialDemand.ToString(), RCI + new Vector2(60, 160), Color.Orange);

            spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], new Rectangle((int)RCI.X - 2, (int)RCI.Y + 30 - 2, 28, 104), Color.SteelBlue);
            spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], new Rectangle((int)RCI.X, (int)RCI.Y + 30, 24, Convert.ToInt32(_city.ResidentialDemand / 10)), Color.Silver);

            spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], new Rectangle((int)RCI.X + 30 - 2, (int)RCI.Y + 30 - 2, 28, 104), Color.SteelBlue);
            spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], new Rectangle((int)RCI.X + 30, (int)RCI.Y + 30, 24, Convert.ToInt32(_city.CommercialDemand / 10)), Color.Silver);

            spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], new Rectangle((int)RCI.X + 60 - 2, (int)RCI.Y + 30 - 2, 28, 104), Color.SteelBlue);
            spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], new Rectangle((int)RCI.X + 60, (int)RCI.Y + 30, 24, Convert.ToInt32(_city.IndustrialDemand / 10)), Color.Silver);


            //Day Timer
            spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], new Rectangle(18, Screen_Size.Y - 162, 104, 24), Color.SteelBlue);
            spriteBatch.Draw(TileTexture[(int)Listof_Texture.Panel1], new Rectangle(20, Screen_Size.Y - 160, Convert.ToInt32(DayCounter), 20), Color.Silver);

            spriteBatch.DrawString(basicfont, "Day " + Date.ToString(), new Vector2(20, Screen_Size.Y - 200), Color.White);


            spriteBatch.DrawString(basicfont, "+$" + Income.ToString(), new Vector2(Screen_Size.X - 300, Screen_Size.Y - 200), Color.LightGreen);
            spriteBatch.DrawString(basicfont, "$" + Currency.ToString(), new Vector2(Screen_Size.X - 300, Screen_Size.Y - 180), Color.White);

            if (BuildCost > 0)
                spriteBatch.DrawString(basicfont, "-$" + BuildCost.ToString(), Mouse.GetState().Position.ToVector2() + new Vector2(20, -20), Color.White);
            //spriteBatch.DrawString(basicfont, "-$" + BuildCost.ToString(), new Vector2(Screen_Size.X - 300, Screen_Size.Y - 180), Color.White);



            //DEBUG
            if (_city.CityArea.Contains(sel_pos))
            {
                spriteBatch.DrawString(basicfont, "Transfer Jobs: " + _city.TileMap[sel_pos.X, sel_pos.Y].Traffic.OriginJobs.ToString(), new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(basicfont, "Jobs: " + _city.TileMap[sel_pos.X, sel_pos.Y].Traffic.OriginJobs.ToString(), new Vector2(0, 20), Color.White);
                spriteBatch.DrawString(basicfont, "Tier: " + _city.TileMap[sel_pos.X, sel_pos.Y].Traffic.tier.ToString(), new Vector2(0, 40), Color.White);

                spriteBatch.DrawString(basicfont, "Type: " + _e.Dictionaryof_BuildItems[_city.TileMap[sel_pos.X, sel_pos.Y].Type].BuildingType.ToString(), new Vector2(0, 60), Color.White);

                spriteBatch.DrawString(basicfont, "Connections: " + _city.TileMap[sel_pos.X, sel_pos.Y].ConnectedItems.ToString(), new Vector2(0, 80), Color.White);

                spriteBatch.DrawString(basicfont, "Tile: X=" + sel_pos.X.ToString()
                                                 + " Y=" + sel_pos.Y.ToString(), new Vector2(0, 100), Color.White);

                spriteBatch.DrawString(basicfont, "Value: " + _city.TileMap[sel_pos.X, sel_pos.Y].LandValue.ToString(), new Vector2(0, 120), Color.White);
            }

            if (_city.WorkersDemand > 0)
                spriteBatch.DrawString(basicfont, "Worker Supply: " + _city.WorkersSupply.ToString() + "  Unemployemnt %" + ((double)(_city.WorkersSupply - _city.WorkersDemand) / _city.WorkersDemand) * 100, new Vector2(300, 0), Color.White);            
            spriteBatch.DrawString(basicfont, "Worker Demand: " + _city.WorkersDemand.ToString(), new Vector2(300, 20), Color.White);
            spriteBatch.DrawString(basicfont, "Worker Market: " + _city.WorkerMarket.ToString(), new Vector2(300, 40), Color.White);

            spriteBatch.DrawString(basicfont, "Commerce Supply: " + _city.CommerceSupply.ToString(), new Vector2(300, 60), Color.White);
            spriteBatch.DrawString(basicfont, "Commerce Demand: " + _city.CommerceDemand.ToString(), new Vector2(300, 80), Color.White);
            spriteBatch.DrawString(basicfont, "Commerce Market: " + _city.CommerceMarket.ToString(), new Vector2(300, 100), Color.White);

            spriteBatch.DrawString(basicfont, "Products Supply: " + _city.ProductsSupply.ToString(), new Vector2(300, 120), Color.White);
            spriteBatch.DrawString(basicfont, "Products Demand: " + _city.ProductsDemand.ToString(), new Vector2(300, 140), Color.White);
            spriteBatch.DrawString(basicfont, "Products Market: " + _city.ProductsMarket.ToString(), new Vector2(300, 160), Color.White);

            spriteBatch.DrawString(basicfont, "Excess Products: " + _city.ExcessProducts.ToString(), new Vector2(300, 180), Color.White);
            spriteBatch.DrawString(basicfont, "Excess Commerce: " + _city.ExcessCommerce.ToString(), new Vector2(300, 200), Color.White);



            foreach (var district in _city.districts)
            {               
                //spriteBatch.DrawString(basicfont, "Workers: " + district.Workers.ToString(), new Vector2(300, 0), Color.White);
                //spriteBatch.DrawString(basicfont, "Jobs: " + district.Jobs[0].ToString(), new Vector2(300, 20), Color.White);
                //spriteBatch.DrawString(basicfont, "JobMarket: " + district.JobMarket.ToString(), new Vector2(300, 40), Color.White);
                //spriteBatch.DrawString(basicfont, "Products: " + district.Products.ToString(), new Vector2(300, 60), Color.White);
                //spriteBatch.DrawString(basicfont, "Commerce: " + district.Commerce[0].ToString(), new Vector2(300, 80), Color.White);

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
