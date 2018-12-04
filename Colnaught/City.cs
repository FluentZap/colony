using Microsoft.Xna.Framework;
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
        Residential_1_1

    }
    
    class City_Tyle
    {
        public Listof_Structures Type = 0;
        public byte SpriteIndex = 0;
        public byte LandValue;

        public City_Tyle()
        {

        }

    }



    class District
    {
        public string Name;
        public Rectangle Area;
        public SortedDictionary<int, HashSet<Point>> ValueList = new SortedDictionary<int, HashSet<Point>>();

        public District(Rectangle Area)
        {
            this.Area = Area;
            for (int x = 0; x < 256; x++)
            {
                ValueList.Add(x, new HashSet<Point>());
            }
            
        }


    }








    class City
    {
        public Rectangle CityArea;        
        public City_Tyle[,] TileMap;
        public HashSet<District> districts;

        public City(Point CitySize)
        {
            this.CityArea = new Rectangle(new Point(0, 0), CitySize);
            TileMap = new City_Tyle[CitySize.X, CitySize.Y];
            districts = new HashSet<District>();

            for (int x = 0; x < CitySize.X; x++)
                for (int y = 0; y < CitySize.Y; y++)                
                    TileMap[x, y] = new City_Tyle();
            
        }




        public void Calculate_Growth()
        {
            foreach (var district in districts)
            {
                for (int v = 255; v >= 0; v--)
                {
                    bool Built = false;
                    if (district.ValueList[v].Count > 0)
                    {
                        foreach (var t in district.ValueList[v])
                        {
                            if (TileMap[t.X, t.Y].Type == Listof_Structures.ZoneResidential)
                            {
                                TileMap[t.X, t.Y].Type = Listof_Structures.Residential_1_1;
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
            for (int x = 0; x < CityArea.Width; x++)
                for (int y = 0; y < CityArea.Height; y++)
                    TileMap[x, y].LandValue = 0;

            for (int x = 0; x < CityArea.Width; x++)
                for (int y = 0; y < CityArea.Height; y++)
                {
                    
                    if (TileMap[x, y].Type == Listof_Structures.RoadDirt)
                    {
                        Rectangle rect = new Rectangle(x, y, 1, 1);
                        rect.Inflate(1, 1);
                        for (int x2 = rect.Left; x2 < rect.Right; x2++)
                            for (int y2 = rect.Top; y2 < rect.Bottom; y2++)
                            {
                                if (CityArea.Contains(new Point(x2, y2)))
                                    TileMap[x2, y2].LandValue += 1;
                            }
                    }


                    if (TileMap[x, y].Type == Listof_Structures.Residential_1_1)
                    {
                        Rectangle rect = new Rectangle(x, y, 1, 1);
                        rect.Inflate(1, 1);
                        for (int x2 = rect.Left; x2 < rect.Right; x2++)
                            for (int y2 = rect.Top; y2 < rect.Bottom; y2++)
                            {
                                if (CityArea.Contains(new Point(x2, y2)))
                                    TileMap[x2, y2].LandValue += 3;
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
