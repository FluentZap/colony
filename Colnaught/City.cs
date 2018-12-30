﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{

    enum Listof_RoadSprites : byte
    {
        vertical,
        horizontal,
        TL,
        TR,
        BL,
        BR,
        SplitL,
        SplitR,
        SplitT,
        SplitB,
        cross,
        EndT,
        EndB,
        EndL,
        EndR
    }

    enum Listof_Structures
    {
        Grass,
        CityCenter,
        ZoneResidential,
        ZoneCommercial,
        ZoneIndustrial,
        RoadDirt,
        Residential_1,
        Residential_2,
        Residential_3,
        Residential_4,
        Residential_5,        
        Commercial_1,
        Commercial_2,
        Commercial_3,
        Commercial_4,
        Commercial_5,
        Industrial_1,
        Industrial_2,
        Industrial_3,
        Industrial_4,
        Industrial_5
    }
    

    class Tile_Traffic
    {
        public Tile_Traffic Parent = null;
        public int ID = 0;

        public int tier = 0;
        public int OriginJobs = 0;
        public int OriginCommerce = 0;
        public int OriginProducts = 0;

        public int DestJobs = 0;
        public int DestCommerce = 0;
        public int DestProducts = 0;

        //Road tiles only have Transfer traffic

        public int OriginJobs_Transfer = 0;
        public int OriginCommerce_Transfer = 0;
        public int OriginProducts_Transfer = 0;

        public int DestJobs_Transfer = 0;
        public int DestCommerce_Transfer = 0;
        public int DestProducts_Transfer = 0;        


        public void AddToTransfer(Tile_Traffic Source)
        {
            OriginJobs_Transfer += Source.OriginJobs;
            OriginCommerce_Transfer += Source.OriginCommerce;
            OriginProducts_Transfer += Source.OriginProducts;

            DestJobs_Transfer += Source.DestJobs;
            DestCommerce_Transfer += Source.DestCommerce;
            DestProducts_Transfer += Source.DestProducts;
        }


        public void AddToBoth(Tile_Traffic Source)
        {
            OriginJobs_Transfer += Source.OriginJobs + Source.OriginJobs_Transfer;
            OriginCommerce_Transfer += Source.OriginCommerce + Source.OriginCommerce_Transfer;
            OriginProducts_Transfer += Source.OriginProducts + Source.OriginProducts_Transfer;

            DestJobs_Transfer += Source.DestJobs + Source.DestJobs_Transfer;
            DestCommerce_Transfer += Source.DestCommerce + Source.DestCommerce_Transfer;
            DestProducts_Transfer += Source.DestProducts + Source.DestProducts_Transfer;
        }


        public void Clear()
        {
            OriginJobs = 0;
            OriginCommerce = 0;
            OriginProducts = 0;

            DestJobs = 0;
            DestCommerce = 0;
            DestProducts = 0;

            OriginJobs_Transfer = 0;
            OriginCommerce_Transfer = 0;
            OriginProducts_Transfer = 0;

            DestJobs_Transfer = 0;
            DestCommerce_Transfer = 0;
            DestProducts_Transfer = 0;
            Parent = null;
            tier = 0;
            ID = 0;
        }        


    }
    


    class City_Tyle
    {
        public Listof_Structures Type = 0;
        public Listof_Texture Texture = 0;
        public int SpriteIndex = 0;
        public int LandValue;
        public bool Buildable = false;
        public Tile_Traffic Traffic = new Tile_Traffic();
        public int ZoneID = 0;        

        public bool Connected = true;

        public City_Tyle()
        {

        }

    }



    class District
    {
        public string Name;
        public Rectangle Area;
        public SortedDictionary<int, HashSet<Point>> ValueList = new SortedDictionary<int, HashSet<Point>>();
        public int[] Population = new int[4];
        public int[] Jobs = new int[4];
        public int[] Comerce = new int[4];
        public int Products;

        public Dictionary<int, Tile_Traffic> ZoneTraffic = new Dictionary<int, Tile_Traffic>();


        SortedDictionary<int, HashSet<Point>> TrafficPoints_Jobs = new SortedDictionary<int, HashSet<Point>>();
        SortedDictionary<int, HashSet<Point>> TrafficPoints_Commerce = new SortedDictionary<int, HashSet<Point>>();
        SortedDictionary<int, HashSet<Point>> TrafficPoints_Products = new SortedDictionary<int, HashSet<Point>>();


        public void ClearPoints()
        {
            TrafficPoints_Jobs.Clear();
            TrafficPoints_Commerce.Clear();
            TrafficPoints_Products.Clear();
        }


        public void AddPoints_Jobs(int value, Point point)
        {
            if (!TrafficPoints_Jobs.ContainsKey(value))
                TrafficPoints_Jobs.Add(value, new HashSet<Point>());
            TrafficPoints_Jobs[value].Add(point);
        }

        public void AddPoints_Commerce(int value, Point point)
        {
            if (!TrafficPoints_Commerce.ContainsKey(value))
                TrafficPoints_Commerce.Add(value, new HashSet<Point>());
            TrafficPoints_Commerce[value].Add(point);
        }

        public void AddPoints_Products(int value, Point point)
        {
            if (!TrafficPoints_Products.ContainsKey(value))
                TrafficPoints_Products.Add(value, new HashSet<Point>());
            TrafficPoints_Products[value].Add(point);
        }



        public District(Rectangle Area)
        {
            this.Area = Area;
            for (int x = 0; x < 256; x++)
            {
                ValueList.Add(x, new HashSet<Point>());
            }
            
        }   
        

        public void ClearJPC()
        {
            for (int x = 0; x <= 4; x++)
            {
                Population[x] = 0;
                Jobs[x] = 0;
                Comerce[x] = 0;                
            }
            Products = 0;
        }



    }








    partial class City
    {
        public Rectangle CityArea;        
        public City_Tyle[,] TileMap;
        public HashSet<District> districts;
        Encyclopedia _e;

        public City(Point CitySize, Encyclopedia _e)
        {
            this.CityArea = new Rectangle(new Point(0, 0), CitySize);
            TileMap = new City_Tyle[CitySize.X, CitySize.Y];
            districts = new HashSet<District>();
            this._e = _e;

            for (int x = 0; x < CitySize.X; x++)
                for (int y = 0; y < CitySize.Y; y++)                
                    TileMap[x, y] = new City_Tyle();
            
        }

       



        public void Calculate_JPC()
        {
            foreach (var district in districts)
            {
                district.ClearJPC();

                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {

                        switch (TileMap[x, y].Type)
                        {
                            case Listof_Structures.Residential_1:
                                district.Population[0] += 6;
                                break;
                            case Listof_Structures.Residential_2:
                                district.Population[0] += 6;
                                break;
                            case Listof_Structures.Residential_3:
                                district.Population[0] += 6;
                                break;
                            case Listof_Structures.Residential_4:
                                district.Population[0] += 6;
                                break;
                            case Listof_Structures.Residential_5:
                                district.Population[0] += 6;
                                break;
                        }

                    }
            }
        }



        public void Calculate_Growth()
        {
            foreach (var district in districts)
            {
                for (int v = 255; v >= 0; v--)
                {
                    //One at a time
                    bool Built = false;
                    if (district.ValueList[v].Count > 0)
                    {
                        foreach (var t in district.ValueList[v])
                        {
                            //**********Residential**********
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.ZoneResidential)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Residential_1;
                                Built = true;
                                break;
                            }

                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Residential_1 && v > 8 && TileMap[t.X, t.Y].Traffic.tier < 4)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Residential_2;
                                TileMap[t.X, t.Y].SpriteIndex = 1;
                                Built = true;
                                break;
                            }

                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Residential_2 && v > 16)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Residential_3;
                                TileMap[t.X, t.Y].SpriteIndex = 2;
                                Built = true;
                                break;
                            }

                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Residential_3 && v > 30)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Residential_4;
                                TileMap[t.X, t.Y].SpriteIndex = 3;
                                Built = true;
                                break;
                            }

                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Residential_4 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Residential_5;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }

                            //**********Commercial**********
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.ZoneCommercial)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_1;
                                Built = true;
                                break;
                            }

                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Commercial_1 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_2;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Commercial_2 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_3;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Commercial_3 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_4;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Commercial_5 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_5;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }


                            //**********Industrial**********
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.ZoneIndustrial)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_1;
                                Built = true;
                                break;
                            }

                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Industrial_1 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_2;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Industrial_2 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_3;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Industrial_3 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_4;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.Industrial_4 && v > 40)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_5;
                                TileMap[t.X, t.Y].SpriteIndex = 4;
                                Built = true;
                                break;
                            }
                        }
                        if (Built) break;
                    }
                }
            }
        }



        public void Calculate_Land_Value()
        {

            Rectangle rect = Rectangle.Empty;

            for (int x = 0; x < CityArea.Width; x++)
                for (int y = 0; y < CityArea.Height; y++)
                    TileMap[x, y].LandValue = 0;

            for (int x = 0; x < CityArea.Width; x++)
                for (int y = 0; y < CityArea.Height; y++)
                {                    
                    if (TileMap[x, y].Type == Listof_Structures.RoadDirt)
                    {
                        rect = new Rectangle(x, y, 1, 1);
                        rect.Inflate(1, 1);
                        for (int x2 = rect.Left; x2 < rect.Right; x2++)
                            for (int y2 = rect.Top; y2 < rect.Bottom; y2++)
                            {
                                //Set to Land Value 1 if there is a road next to it
                                if (CityArea.Contains(new Point(x2, y2)))
                                    TileMap[x2, y2].LandValue = 1;
                            }
                    }                   
                }

            for (int x = 0; x < CityArea.Width; x++)
                for (int y = 0; y < CityArea.Height; y++)
                {
                    int ValueAdd = 0;
                    rect = new Rectangle(x, y, 1, 1);

                    switch (TileMap[x, y].Type)
                    {
                        case Listof_Structures.Residential_1:                            
                            rect.Inflate(1, 1);
                            ValueAdd = 1;
                            break;
                        case Listof_Structures.Residential_2:
                            rect.Inflate(1, 1);
                            ValueAdd = 1;
                            break;
                        case Listof_Structures.Residential_3:
                            rect.Inflate(1, 1);
                            ValueAdd = 1;
                            break;
                        case Listof_Structures.Residential_4:
                            rect.Inflate(1, 1);
                            ValueAdd = 1;
                            break;
                        case Listof_Structures.Residential_5:
                            rect.Inflate(1, 1);
                            ValueAdd = 1;
                            break;
                    }

                    if (ValueAdd > 0)
                    {
                        for (int x2 = rect.Left; x2 < rect.Right; x2++)
                            for (int y2 = rect.Top; y2 < rect.Bottom; y2++)
                            {
                                if (CityArea.Contains(new Point(x2, y2)))
                                    TileMap[x2, y2].LandValue += ValueAdd;
                            }
                    }
                }
                        
            foreach (var district in districts)
            {
                for (int x = 0; x < 256; x++)
                    district.ValueList[x].Clear();

                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        if (!district.ValueList[TileMap[x, y].LandValue].Contains(new Point(x, y)))
                            district.ValueList[TileMap[x, y].LandValue].Add(new Point(x, y));
                    }
            }

        }

    }
}