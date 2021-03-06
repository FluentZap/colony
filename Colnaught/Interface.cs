﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{



    public enum Listof_BuildTabCategories
    {
        Zones,
        Roads,
        Power
    }


    public enum Listof_BuildItems
    {
        Residential,
        Commercial,
        Industrial
    }

    public enum Listof_BuildTabRoads
    {
        DirtRoad
    }

    public enum Listof_BuildTabPower
    {
        PowerPlant
    }


    enum Listof_ViewInterfaceButtons : int
    {
        BuildButton1,
        BuildButton2,
        BuildButton3,
        BuildButton4,
        BuildButton5,
        BuildPanel,
        BuildCategory1,
        BuildCategory2,
        BuildCategory3,
        BuildCategory4,
        BuildCategory5,
        BuildCategory6
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
        public int SpriteIndex = 0;
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
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildPanel, new Interface_Item() { Location = new Rectangle(0, h - 200, w, 200) });

            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton1, new Interface_Item() { Location = new Rectangle(256 + 128 * 0, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton2, new Interface_Item() { Location = new Rectangle(256 + 128 * 1, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton3, new Interface_Item() { Location = new Rectangle(256 + 128 * 2, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton4, new Interface_Item() { Location = new Rectangle(256 + 128 * 3, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton5, new Interface_Item() { Location = new Rectangle(256 + 128 * 4, h - 200, 128, 128), Type = Listof_ButtonType.Button });

            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildCategory1, new Interface_Item() { Location = new Rectangle(256 + 128 * 0, h - 232, 128, 32), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildCategory2, new Interface_Item() { Location = new Rectangle(256 + 128 * 1, h - 232, 128, 32), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildCategory3, new Interface_Item() { Location = new Rectangle(256 + 128 * 2, h - 232, 128, 32), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildCategory4, new Interface_Item() { Location = new Rectangle(256 + 128 * 3, h - 232, 128, 32), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildCategory5, new Interface_Item() { Location = new Rectangle(256 + 128 * 4, h - 232, 128, 32), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildCategory6, new Interface_Item() { Location = new Rectangle(256 + 128 * 5, h - 232, 128, 32), Type = Listof_ButtonType.Button });

        }




    }









    public partial class Game1
    {
        public Listof_BuildTabCategories BuildCategorySelected;
        public int BuildButtonScroll;





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

                        //Build Catagory Building
                        int buttonID = 0;
                        foreach (var tech in _t.BuildingTech)
                        {
                            if (tech.Value.Category == BuildCategorySelected)
                            {
                                if (buttonID >= BuildButtonScroll)
                                {
                                    int id = buttonID - BuildButtonScroll;
                                    if (id >= 0 && id <= 4 && id == (int)item.Key)
                                    {
                                        ClearBuild();
                                        MouseMode = Listof_MouseMode.Building;
                                        Building = tech.Value.BuildItem;
                                    }
                                }
                                buttonID++;
                            }

                            if (buttonID - BuildButtonScroll >= 4)
                                break;
                        }



                        if (item.Key >= Listof_ViewInterfaceButtons.BuildCategory1) BuildCategorySelected = Listof_BuildTabCategories.Zones;
                        if (item.Key >= Listof_ViewInterfaceButtons.BuildCategory2) BuildCategorySelected = Listof_BuildTabCategories.Roads;
                        if (item.Key >= Listof_ViewInterfaceButtons.BuildCategory3) BuildCategorySelected = Listof_BuildTabCategories.Power;


                    }


                    if (item.Value.Type == Listof_ButtonType.Button) item.Value.color = new Color(200, 200, 200);
                }
            }

            BuildInterface(onPanel);

        }




        void BuildInterface(bool onPanel)
        {
            Vector2 mPos = new Vector2((Mouse.GetState().Position.X + Screen_Scroll.X) / zoom, (Mouse.GetState().Position.Y + Screen_Scroll.Y) / zoom);
            Point sel_pos;
            sel_pos.X = (int)Math.Floor((mPos.Y - 128) / 64 + (mPos.X - 128) / 128);
            sel_pos.Y = (int)Math.Floor((-mPos.X - 128) / 128 + (mPos.Y - 128) / 64);
            bool Build = false;
            bool StartDrag = false;


            if (MouseMode == Listof_MouseMode.Building)
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed) MouseRightClicked = true;
                //Check for right click cancel
                if (MouseRightClicked && Mouse.GetState().RightButton == ButtonState.Released)
                    if (Math.Abs(MouseDragScreenScrollStart.X - Screen_Scroll.X) < 4 && Math.Abs(MouseDragScreenScrollStart.Y - Screen_Scroll.Y) < 4)
                    {
                        MouseRightClicked = false;
                        ClearBuild();
                    }

                if (!onPanel)
                {
                    if (!MouseLeftClicked && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        MouseLeftClicked = true;
                        StartDrag = true;
                    }

                    if (MouseLeftClicked && Mouse.GetState().LeftButton == ButtonState.Released) { MouseLeftClicked = false; Build = true; }
                }
                else
                    MouseLeftClicked = false;

                Typeof_BuildItems BuildData = _e.Dictionaryof_BuildItems[Building];

                //Placed Buildings
                if (BuildData.BuildingType == Listof_BuildTypes.Structure)
                {
                    BuildPoint1 = sel_pos;
                    if (Build)
                        if (Buildable)
                        {
                            for (int y = 0; y < BuildData.Size.Y; y++)
                                for (int x = 0; x < BuildData.Size.X; x++)
                                {
                                    _city.TileMap[x, y].ClearConnections();
                                    _city.TileMap[BuildPoint1.X - x, BuildPoint1.Y - y].Type = Building;
                                    _city.TileMap[BuildPoint1.X - x, BuildPoint1.Y - y].SpriteIndex = x + (y * BuildData.Size.X);                                    
                                    ConnectTile(new Point(BuildPoint1.X - x, BuildPoint1.Y - y));
                                }

                            Currency -= BuildCost;

                            MouseLeftClicked = false;
                            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                                MouseMode = Listof_MouseMode.Default;

                            ClearBuild();
                        }
                }

                //City Center
                if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.CityCenter)
                {
                    BuildPoint1 = sel_pos;

                    if (Build == true)
                        if (Buildable)
                        {
                            Rectangle DistrictArea = new Rectangle(sel_pos, new Point(1, 1));
                            DistrictArea.Inflate(20, 20);
                            _city.districts.Add(new District(DistrictArea));
                            _city.TileMap[sel_pos.X, sel_pos.Y].Type = Building;

                            Currency -= BuildCost;

                            MouseLeftClicked = false;
                            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                                MouseMode = Listof_MouseMode.Default;

                            ClearBuild();
                        }
                        else
                            ClearBuild();
                }

                //Zone
                if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Zone)
                {
                    //Draging
                    if (!Build && !MouseLeftClicked && Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        BuildPoint1 = sel_pos;
                        BuildPoint2 = sel_pos;
                    }


                    //Assign Build Rectangle
                    if (!onPanel && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        BuildPoint2 = sel_pos;
                        Point P1, P2;
                        //Set building points X and Y
                        if (BuildPoint1.X <= BuildPoint2.X)
                        { P1.X = BuildPoint1.X; P2.X = BuildPoint2.X; }
                        else
                        { P1.X = BuildPoint2.X; P2.X = BuildPoint1.X; }

                        if (BuildPoint1.Y <= BuildPoint2.Y)
                        { P1.Y = BuildPoint1.Y; P2.Y = BuildPoint2.Y; }
                        else
                        { P1.Y = BuildPoint2.Y; P2.Y = BuildPoint1.Y; }

                        BuildRect = new Rectangle(P1.X, P1.Y, (P2.X - P1.X) + 1, (P2.Y - P1.Y) + 1);
                    }

                    //Build Item
                    if (Build)
                    {
                        if (InsideDistrict(BuildRect) && Buildable)
                        {
                            for (int x = BuildRect.Left; x < BuildRect.Right; x++)
                                for (int y = BuildRect.Top; y < BuildRect.Bottom; y++)
                                    if (_city.CityArea.Contains(new Point(x, y)) && _city.TileMap[x, y].Buildable)
                                    {
                                        //tile is the zone being created
                                        _city.TileMap[x, y].ClearConnections();
                                        _city.TileMap[x, y].Type = Building;
                                        ConnectTile(new Point(x, y));
                                    }

                            Currency -= BuildCost;
                            ClearBuild();
                        }
                        else
                            ClearBuild();

                    }
                }

                //Roads
                if (_e.Dictionaryof_BuildItems[Building].BuildingType == Listof_BuildTypes.Road)
                {

                    if (!Build && !MouseLeftClicked && Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        BuildPoint1 = sel_pos;
                        BuildPoint2 = sel_pos;
                    }

                    if (!onPanel && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        BuildPoint2 = sel_pos;


                    //Build roads
                    if (Build == true)
                        if (Buildable)
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


                            int ShortX = Math.Abs(BuildPoint1.X - BuildPoint2.X), ShortY = Math.Abs(BuildPoint1.Y - BuildPoint2.Y), Temp;

                            if (Keyboard.GetState().IsKeyUp(Keys.LeftControl))
                            { Temp = ShortX; ShortX = ShortY; ShortY = Temp; }

                            if (ShortY >= ShortX)
                            { P3.X = BuildPoint2.X; P3.Y = BuildPoint1.Y; }
                            else
                            { P3.X = BuildPoint1.X; P3.Y = BuildPoint2.Y; }

                            for (int x = P1.X; x <= P2.X; x++)
                                BuildRoadTile(new Point(x, P3.Y));
                            for (int y = P1.Y; y <= P2.Y; y++)
                                BuildRoadTile(new Point(P3.X, y));

                            //seccond pass to connect all the roads to the tiles

                            for (int x = P1.X; x <= P2.X - 1; x++)
                                ReconnectTile(new Point(x, P3.Y));
                            for (int y = P1.Y; y <= P2.Y; y++)
                                ReconnectTile(new Point(P3.X, y));


                            //Set Road Sprites
                            for (int x = P1.X; x <= P2.X - 1; x++)
                                SetRoadTileSprite(new Point(x, P3.Y));
                            for (int y = P1.Y; y <= P2.Y; y++)
                                SetRoadTileSprite(new Point(P3.X, y));


                            Currency -= BuildCost;

                            ClearBuild();
                        }
                        else
                            ClearBuild();

                }

                if (MouseMode == Listof_MouseMode.Building)
                    CheckBuildable(_e.Dictionaryof_BuildItems[Building].BuildingType);

            }
        }

        void BuildRoadTile(Point P)
        {
            _city.TileMap[P.X, P.Y].ClearConnections();

            _city.TileMap[P.X, P.Y].Type = Building;
            _city.TileMap[P.X, P.Y].SpriteIndex = 0;
            _city.TileMap[P.X, P.Y].Buildable = false;

            Rectangle BuildArea = new Rectangle(-4, -4, 8, 8);
            //set buildable area
            for (int x = P.X + BuildArea.Left; x <= P.X + BuildArea.Right; x += 1)
                if (_city.CityArea.Contains(new Point(x, P.Y)))
                    if (_city.TileMap[x, P.Y].Type == Listof_Structures.Grass)
                        _city.TileMap[x, P.Y].Buildable = true;

            for (int y = P.Y + BuildArea.Top; y <= P.Y + BuildArea.Bottom; y += 1)
                if (_city.CityArea.Contains(new Point(P.X, y)))
                    if (_city.TileMap[P.X, y].Type == Listof_Structures.Grass)
                        _city.TileMap[P.X, y].Buildable = true;
        }

        void ClearBuild()
        {
            BuildCost = 0;
            MouseLeftClicked = false;
            BuildRect = new Rectangle(0, 0, 0, 0);
            BuildPoint1 = new Point(-1, -1);
            BuildPoint2 = new Point(-1, -1);
            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Keyboard.GetState().IsKeyUp(Keys.RightShift))
                MouseMode = Listof_MouseMode.Default;
        }

        void CheckBuildable(Listof_BuildTypes B)
        {
            BuildCost = 0;

            Buildable = true;
            Typeof_BuildItems BuildData = _e.Dictionaryof_BuildItems[Building];

            if (B == Listof_BuildTypes.Road)
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
                //Set shortest angle

                int ShortX = Math.Abs(BuildPoint1.X - BuildPoint2.X), ShortY = Math.Abs(BuildPoint1.Y - BuildPoint2.Y), Temp;

                if (Keyboard.GetState().IsKeyUp(Keys.LeftControl))
                { Temp = ShortX; ShortX = ShortY; ShortY = Temp; }

                if (ShortY >= ShortX)
                { P3.X = BuildPoint2.X; P3.Y = BuildPoint1.Y; }
                else
                { P3.X = BuildPoint1.X; P3.Y = BuildPoint2.Y; }

                District district = _city.GetDistrictByPoint(P1);
                if (district != null)
                {
                    for (int x = P1.X; x <= P2.X - 1; x++)
                        if (!district.Area.Contains(new Point(x, P3.Y))) { Buildable = false; return; }

                    for (int y = P1.Y; y <= P2.Y; y++)
                        if (!district.Area.Contains(new Point(P3.X, y))) { Buildable = false; return; }
                }
                else
                {
                    Buildable = false;
                    return;
                }

                //Check cost
                for (int x = P1.X; x <= P2.X - 1; x++)
                    if (_city.TileMap[x, P3.Y].Type != Building)
                        BuildCost += _e.Dictionaryof_BuildItems[Building].Cost;

                for (int y = P1.Y; y <= P2.Y; y++)
                    if (_city.TileMap[P3.X, y].Type != Building)
                        BuildCost += _e.Dictionaryof_BuildItems[Building].Cost;
            }


            if (B == Listof_BuildTypes.Zone)
            {
                Point P1, P2;
                //Set building points X and Y
                if (BuildPoint1.X <= BuildPoint2.X)
                { P1.X = BuildPoint1.X; P2.X = BuildPoint2.X; }
                else
                { P1.X = BuildPoint2.X; P2.X = BuildPoint1.X; }

                if (BuildPoint1.Y <= BuildPoint2.Y)
                { P1.Y = BuildPoint1.Y; P2.Y = BuildPoint2.Y; }
                else
                { P1.Y = BuildPoint2.Y; P2.Y = BuildPoint1.Y; }

                Rectangle buildzone = new Rectangle(P1.X, P1.Y, (P2.X - P1.X) + 1, (P2.Y - P1.Y) + 1);

                District district1 = _city.GetDistrictByPoint(P1);
                District district2 = _city.GetDistrictByPoint(P2);


                if (district1 != null && district2 != null && district1 == district2)
                {

                }
                else
                {
                    Buildable = false;
                    return;
                }

                //Add Cost
                for (int x = buildzone.Left; x < buildzone.Right; x++)
                    for (int y = buildzone.Top; y < buildzone.Bottom; y++)
                        if (_city.TileMap[x, y].Buildable == true && (_city.TileMap[x, y].Type != Building))
                            BuildCost += _e.Dictionaryof_BuildItems[Building].Cost;


            }


            if (B == Listof_BuildTypes.CityCenter)
            {
                Rectangle area = new Rectangle(BuildPoint1.X, BuildPoint1.Y, 1, 1);
                area.Inflate(20, 20);
                if (IntersectsDistrict(area))
                    Buildable = false;

                if (!_city.CityArea.Contains(area))
                    Buildable = false;

                BuildCost = BuildData.Cost;

            }



            if (B == Listof_BuildTypes.Structure)
            {
                for (int y = 0; y < BuildData.Size.Y; y++)
                    for (int x = 0; x < BuildData.Size.X; x++)
                        if (!_city.TileMap[BuildPoint1.X - x, BuildPoint1.Y - y].Buildable) Buildable = false;

                BuildCost = BuildData.Cost;
            }



            //No more Money :(
            if (BuildCost > Currency)
                Buildable = false;

        }




        void SetRoadTileSprite(Point P)
        {
            //Check built tile and surrounding tiles
            Rectangle rect = new Rectangle(P.X, P.Y, 1, 1);
            rect.Inflate(1, 1);

            for (int x = rect.Left; x <= rect.Right; x++)
                for (int y = rect.Top; y <= rect.Bottom; y++)
                    if (_city.CityArea.Contains(x, y) && _e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].BuildingType == Listof_BuildTypes.Road)
                    {
                        bool left = false, right = false, top = false, bottom = false;
                        if (_city.CityArea.Contains(x - 1, y) && _e.Dictionaryof_BuildItems[_city.TileMap[x - 1, y].Type].BuildingType == Listof_BuildTypes.Road) left = true;
                        if (_city.CityArea.Contains(x + 1, y) && _e.Dictionaryof_BuildItems[_city.TileMap[x + 1, y].Type].BuildingType == Listof_BuildTypes.Road) right = true;
                        if (_city.CityArea.Contains(x, y - 1) && _e.Dictionaryof_BuildItems[_city.TileMap[x, y - 1].Type].BuildingType == Listof_BuildTypes.Road) top = true;
                        if (_city.CityArea.Contains(x, y + 1) && _e.Dictionaryof_BuildItems[_city.TileMap[x, y + 1].Type].BuildingType == Listof_BuildTypes.Road) bottom = true;


                        if (left && right && !top && !bottom) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.LeftRightHorizontal;
                        if (top && bottom && !left && !right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.TopBottomHorizontal;

                        if (top && left && right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.ThreeWayRight;
                        if (bottom && left && right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.ThreeWayLeft;
                        if (top && bottom && left) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.ThreeWayTop;
                        if (top && bottom && right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.ThreeWayBottom;

                        if (top && !bottom && left && !right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.CornerTopRight;
                        if (!top && bottom && !left && right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.CornerBottomLeft;


                        if (!top && bottom && left && !right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.CornerTopLeft;
                        if (top && !bottom && !left && right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.CornerBottomRight;


                        //if (top && !bottom && left && !right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.CornerBottomLeft;


                        if (top && bottom && left && right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.FourWay;

                        if (top && !bottom && !left && !right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.LeftEnd;
                        if (!top && bottom && !left && !right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.RightEnd;
                        if (!top && !bottom && left && !right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.BottomEnd;
                        if (!top && !bottom && !left && right) _city.TileMap[x, y].SpriteIndex = (int)Listof_RoadSprites.TopEnd;

                    }

        }





        void ReconnectTile(Point P)
        {
            for (int x = P.X - 4; x <= P.X + 4; x++)
                for (int y = P.Y - 4; y <= P.Y + 4; y++)
                    if (_city.CityArea.Contains(new Point(x, y)))
                        if (x == P.X || y == P.Y)
                            _city.TileMap[x, y].ClearConnections();

            for (int x = P.X - 4; x <= P.X + 4; x++)
                for (int y = P.Y - 4; y <= P.Y + 4; y++)
                    if (_city.CityArea.Contains(new Point(x, y)))
                        if (x == P.X || y == P.Y)
                            ConnectTile(new Point(x, y));
        }

        void ConnectTile(Point P)
        {
            //Check all tiles in range of 4
            if (_city.CityArea.Contains(P))
            {
                Listof_BuildTypes type = _e.Dictionaryof_BuildItems[_city.TileMap[P.X, P.Y].Type].BuildingType;

                for (int x = P.X + 1; x <= P.X + 4; x++) //x>
                    if (CheckTile(x, P.Y, P.X, P.Y, type)) break;

                for (int x = P.X - 1; x >= P.X - 4; x--) //x<
                    if (CheckTile(x, P.Y, P.X, P.Y, type)) break;

                for (int y = P.Y + 1; y <= P.Y + 4; y++) //y>
                    if (CheckTile(P.X, y, P.X, P.Y, type)) break;

                for (int y = P.Y - 1; y >= P.Y - 4; y--) //y<
                    if (CheckTile(P.X, y, P.X, P.Y, type)) break;
            }


            bool CheckTile(int x, int y, int Sx, int Sy, Listof_BuildTypes SType)
            {
                City_Tyle tile = _city.TileMap[Sx, Sy];

                if (_city.CityArea.Contains(new Point(x, y)))
                {
                    if (SType == Listof_BuildTypes.Zone || SType == Listof_BuildTypes.Structure)
                    {
                        if (_e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].BuildingType == Listof_BuildTypes.Road)
                        { _city.TileMap[x, y].ConnectTile(tile); return true; }
                    }

                    if (SType == Listof_BuildTypes.Road)
                    {
                        if (_e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].BuildingType == Listof_BuildTypes.Structure ||
                        _e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].BuildingType == Listof_BuildTypes.Zone)
                            tile.ConnectTile(_city.TileMap[x, y]);
                        else
                        if (_e.Dictionaryof_BuildItems[_city.TileMap[x, y].Type].BuildingType == Listof_BuildTypes.Road)
                            return true;
                    }
                }
                return false;
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
