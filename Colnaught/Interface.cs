using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{

    enum Listof_ViewInterfaceButtons
    {        
        BuildPanel,
        BuildButton1,
        BuildButton2,
        BuildButton3,
        BuildButton4,
        BuildButton5
    }

    enum Listof_ButtonType
    {
        Panel,
        Button
    }




    class Interface_Item
    {
        public string Label;
        public Listof_ButtonType Type;
        public Rectangle Location;
        public Color color = Color.White;
        /*
        public Interface_Item(Rectangle Location, Listof_ButtonType Type = Listof_ButtonType.Panel, string Label = "")
        {
            this.Label = Label;
            this.Type = Type;
            this.Location = Location;
        }
        */

    }


    class Interface
    {
        public Dictionary<Listof_ViewInterfaceButtons, Interface_Item> Dictionaryof_CityScreenButtons = new Dictionary<Listof_ViewInterfaceButtons, Interface_Item>();

        public Interface(Point Screen_Size)
        {
            int w = Screen_Size.X;
            int h = Screen_Size.Y;
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildPanel, new Interface_Item() { Location = new Rectangle(0, h - 200, w, 200)});

            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton1, new Interface_Item() { Location = new Rectangle(256 + 128 * 0, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton2, new Interface_Item() { Location = new Rectangle(256 + 128 * 1, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton3, new Interface_Item() { Location = new Rectangle(256 + 128 * 2, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton4, new Interface_Item() { Location = new Rectangle(256 + 128 * 3, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton5, new Interface_Item() { Location = new Rectangle(256 + 128 * 4, h - 200, 128, 128), Type = Listof_ButtonType.Button });
        }               




    }









    public partial class Game1
    {









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
                if (item.Value.Location.Contains(Mouse.GetState().Position) == true)
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
                            MouseMode = Listof_MouseMode.Building;
                            BuildRect = Rectangle.Empty;
                            Building = Listof_Structures.ZoneResidential;
                        }

                        if (item.Key == Listof_ViewInterfaceButtons.BuildButton3)
                        {
                            MouseMode = Listof_MouseMode.Building;
                            BuildRect = Rectangle.Empty;
                            Building = Listof_Structures.ZoneCommercial;
                        }

                        if (item.Key == Listof_ViewInterfaceButtons.BuildButton4)
                        {
                            MouseMode = Listof_MouseMode.Building;
                            BuildRect = Rectangle.Empty;
                            Building = Listof_Structures.ZoneIndustrial;
                        }

                        if (item.Key == Listof_ViewInterfaceButtons.BuildButton5)
                        {
                            MouseMode = Listof_MouseMode.Building;
                            BuildRect = Rectangle.Empty;
                            Building = Listof_Structures.RoadDirt;
                        }
                    }


                    if (item.Value.Type == Listof_ButtonType.Button) item.Value.color = new Color(200, 200, 200);
                }
            }

            //Placed Buildings
            if (MouseMode == Listof_MouseMode.Building && _e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Structure)
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
                    if (InsideDistrict(BuildRect))
                    {
                        _city.TileMap[sel_pos.X, sel_pos.Y].Type = Building;
                        MouseLeftClicked = false;
                        if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                            MouseMode = Listof_MouseMode.Default;
                    }
                    else
                        MouseLeftClicked = false;
            }


            //City Center
            if (MouseMode == Listof_MouseMode.Building && _e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.CityCenter)
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

                //Build
                if (!onPanel && Mouse.GetState().LeftButton == ButtonState.Pressed) MouseLeftClicked = true;

                Rectangle area = new Rectangle(sel_pos.X, sel_pos.Y, 1, 1);
                area.Inflate(20, 20);
                if (MouseLeftClicked && Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    if (!IntersectsDistrict(area))
                    {
                        Rectangle DistrictArea = new Rectangle(sel_pos, new Point(1, 1));
                        DistrictArea.Inflate(20, 20);
                        _city.districts.Add(new District(DistrictArea));

                        _city.TileMap[sel_pos.X, sel_pos.Y].Type = Building;

                        MouseLeftClicked = false;
                        if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                            MouseMode = Listof_MouseMode.Default;
                    }
                    else
                        MouseLeftClicked = false;
                }
            }


            //Drag Building
            if (MouseMode == Listof_MouseMode.Building && _e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Zone)
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
                    if (InsideDistrict(BuildRect))
                    {
                        for (int x = BuildRect.Left; x < BuildRect.Right; x++)
                            for (int y = BuildRect.Top; y < BuildRect.Bottom; y++)
                            {
                                if (_city.CityArea.Contains(new Point(x, y)))
                                {
                                    _city.TileMap[x, y].Type = Building;
                                }
                            }

                        MouseLeftClicked = false;
                        if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                            MouseMode = Listof_MouseMode.Default;
                        else
                            BuildRect = Rectangle.Empty;
                    }
                    else
                    {
                        MouseLeftClicked = false;
                        BuildRect = Rectangle.Empty;
                    }
                        
                }
            }










            //Draw Roads
            if (MouseMode == Listof_MouseMode.Building && _e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Road)
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
                    BuildRect.Height = 1;
                    BuildRect.Width = 1;
                    BuildRect.X = BuildRectPoint.X;
                    BuildRect.Y = BuildRectPoint.Y;

                    if (Math.Abs(BuildRectPoint.X - Math.Abs(sel_pos.X)) > Math.Abs(BuildRectPoint.Y - Math.Abs(sel_pos.Y)))
                    {
                        //X
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

                    }
                    else
                    {
                        //Y
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
                    }
                }



                if (MouseLeftClicked && Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    if (InsideDistrict(BuildRect))
                    {
                        for (int x = BuildRect.Left; x < BuildRect.Right; x++)
                            for (int y = BuildRect.Top; y < BuildRect.Bottom; y++)
                            {
                                if (_city.CityArea.Contains(new Point(x, y)))
                                {
                                    _city.TileMap[x, y].Type = Building;
                                    _city.TileMap[x, y].SpriteIndex = 0;
                                }
                            }

                        BuildRect.Inflate(4, 4);
                        for (int x = BuildRect.Left; x < BuildRect.Right; x++)
                            for (int y = BuildRect.Top; y < BuildRect.Bottom; y++)
                            {
                                if (_city.CityArea.Contains(new Point(x, y)))
                                {
                                    if (_city.TileMap[x, y].Type == Listof_Structures.Grass)
                                        _city.TileMap[x, y].Buildable = true;
                                    else
                                        _city.TileMap[x, y].Buildable = false;
                                }
                            }
                        MouseLeftClicked = false;
                        if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                            MouseMode = Listof_MouseMode.Default;
                        else
                            BuildRect = Rectangle.Empty;
                    }
                    else
                    {
                        MouseLeftClicked = false;
                        BuildRect = Rectangle.Empty;
                    }
                        
                }
            }


            
        }


        bool InsideDistrict(Rectangle Area)
        {
            bool Inside = false;

            foreach (var district in _city.districts)
            {
                if (district.Area.Contains(Area)) Inside = true;
            }

            return Inside;
        }

        bool IntersectsDistrict(Rectangle Area)
        {
            bool Intersects = false;

            foreach (var district in _city.districts)
            {
                if (district.Area.Intersects(Area)) Intersects = true;
            }

            return Intersects;
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
    }

}
