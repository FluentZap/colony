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

            bool DrawMapTile;
            Color MapTileColor;
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
                    //if (rec.Contains(new Point(x, y)))
                    //{                        
                    //}
                    //else

                    DrawMapTile = true;
                    MapTileColor = Color.White;

                    if (MouseMode == Listof_MouseMode.Building)
                    {

                        foreach (var district in _city.districts)
                            if (district.Area.Contains(sel_pos)) distrect = district.Area;

                            if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Structure)
                        {
                            if (x == sel_pos.X && y == sel_pos.Y)
                                spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                        }

                        if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Zone || _e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Road)
                        {
                            if (x == sel_pos.X && y == sel_pos.Y)
                            {
                                spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                                DrawMapTile = false;
                            }
                            if (BuildRect.Contains(new Point(x, y)))
                            {
                                spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[Building].Texture], pos, new Rectangle(0, 0, 128, 256), Color.CornflowerBlue, 0f, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                                DrawMapTile = false;
                            }                                
                        }


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
                                        MapTileColor = Color.LightGreen;
                                    else
                                        MapTileColor = Color.CornflowerBlue;
                                }                                    
                            }
                            else
                            {
                                if (district.Area.Contains(new Point(x, y)))
                                    MapTileColor = Color.LightSalmon;
                            }
                        }
                        
                    }

                    if (DrawMapTile)
                        spriteBatch.Draw(TileTexture[(int)_e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].Texture], pos, new Rectangle(0 + 128 * _city.TileMap[x, y].SpriteIndex, 0, 128, 256), MapTileColor, 0, new Vector2(), Tile_Size, SpriteEffects.None, 1);
                    
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
