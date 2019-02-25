using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Colnaught
{

    enum Listof_RoadSprites : int
    {
        TopBottomHorizontal,
        LeftRightHorizontal,
        ThreeWayTop,
        ThreeWayRight,
        ThreeWayBottom,
        ThreeWayLeft,
        FourWay,
        TopEnd,
        BottomEnd,
        RightEnd,
        LeftEnd,
        CornerTopLeft,
        CornerTopRight,
        CornerBottomLeft,
        CornerBottomRight
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
        Industrial_5,
        PowerPlant1
    }


    class Tile_Traffic
    {
        public Tile_Traffic Parent = null;
        public int ID = 0;

        public int tier = 0;
        public float OriginJobs = 0;
        public float OriginCommerce = 0;
        public float OriginProducts = 0;

        public float DestJobs = 0;
        public float DestCommerce = 0;
        public float DestProducts = 0;
        



        public void AddTrafficToRoad(Tile_Traffic Source, int Split)
        {
            OriginJobs += Source.OriginJobs / Split;
            OriginCommerce += Source.OriginCommerce / Split;
            OriginProducts += Source.OriginProducts / Split;

            DestJobs += Source.DestJobs / Split;
            DestCommerce += Source.DestCommerce / Split;
            DestProducts += Source.DestProducts / Split;
        }


        public void AddTrafficFrom(Tile_Traffic Source)
        {
            OriginJobs += Source.OriginJobs;
            OriginCommerce += Source.OriginCommerce;
            OriginProducts += Source.OriginProducts;

            DestJobs += Source.DestJobs;
            DestCommerce += Source.DestCommerce;
            DestProducts += Source.DestProducts;
        }


        public void Clear()
        {
            OriginJobs = 0;
            OriginCommerce = 0;
            OriginProducts = 0;

            //DestJobs = 0;
            //DestCommerce = 0;
            //DestProducts = 0;

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
        public double LandValue;
        public bool Buildable = false;
        public int Growth = 0;
        public bool Constructing = false;

        public Tile_Traffic Traffic = new Tile_Traffic();


        public City_Tyle[] RoadContacts = new City_Tyle[16];
        public int ConnectedItems = 0;


        void AddRoadConnection(City_Tyle tile)
        {
            bool AlreadyAdded = false;

            foreach (var item in RoadContacts)
                if (item == tile) AlreadyAdded = true;

            if (ConnectedItems < 16 && !AlreadyAdded)
                for (int n = 0; n <= 15; n++)
                    if (RoadContacts[n] == null)
                    {
                        RoadContacts[n] = tile;
                        ConnectedItems++;
                        break;
                    }
        }

        void RemoveRoadConnection(City_Tyle tile)
        {
            for (int n = 0; n <= 15; n++)
                if (RoadContacts[n] != null)
                    if (RoadContacts[n] == tile)
                    {
                        RoadContacts[n] = null;
                        ConnectedItems--;
                    }
        }

        void ClearRoadConnection()
        {
            City_Tyle[] RoadContacts = new City_Tyle[16];
            ConnectedItems = 0;
        }

        //public int ConnectedZones = 0; //ConnectedRoads when it's a Zone        
        //public City_Tyle[] RoadContacts = new City_Tyle[16];

        //public bool Connected = true;

        public City_Tyle()
        {

        }



        public void ConnectTile(City_Tyle tile)
        {
            //Add tile to our list
            AddRoadConnection(tile);
            //Add us to there list
            tile.AddRoadConnection(this);
        }

        public void RemoveTile(City_Tyle tile)
        {
            //Remove us from there list
            tile.RemoveRoadConnection(this);
            //Remove them from our list
            RemoveRoadConnection(tile);
        }


        public void ClearConnections()
        {
            for (int n = 0; n <= 15; n++)
                if (RoadContacts[n] != null)
                {
                    RoadContacts[n].RemoveRoadConnection(this);
                    RemoveRoadConnection(RoadContacts[n]);
                }
        }



        public void TrafficByConnection()
        {
            Traffic.Clear();

            foreach (var item in RoadContacts)
                if (item != null)
                    Traffic.AddTrafficToRoad(item.Traffic, item.ConnectedItems);
        }



        //Might not need the overhead yet
        //Connection.AddConnectedRoad(this);        









    }



    class District
    {
        public string Name;
        public Rectangle Area;
        public SortedDictionary<int, HashSet<Point>> ValueList = new SortedDictionary<int, HashSet<Point>>();
        public int[] Population = new int[4];
        public int[] Jobs = new int[4];
        public int[] Commerce = new int[4];
        public int Products;

        public int PowerSupply;
        public int PowerDrain;


        public int Workers = 0;
        public double JobMarket = 0;

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
            for (int x = 0; x < 4; x++)
            {
                Population[x] = 0;
                Jobs[x] = 0;
                Commerce[x] = 0;
            }
            Products = 0;
        }



    }








    partial class City
    {
        public Rectangle CityArea;
        public City_Tyle[,] TileMap;
        public HashSet<District> districts;
        public double ResidentialDemand = 0, CommercialDemand = 0, IndustrialDemand = 0;

        public double ResidentialDemandCap = 0, CommercialDemandCap = 0, IndustrialDemandCap = 0;

        public Tile_Traffic Projected_Traffic = new Tile_Traffic();

        public int WorkersSupply = 0, ProductsSupply = 0, CommerceSupply = 0;
        public int WorkersDemand = 0, ProductsDemand = 0, CommerceDemand = 0;

        public int ExcessProducts = 0;
        public int ExcessCommerce = 0;

        public float MaxGrowthRate = 5;

        public double WorkerMarket = 0, ProductsMarket = 0, CommerceMarket = 0;


        Encyclopedia _e;

        public City(Point CitySize, Encyclopedia _e)
        {
            CityArea = new Rectangle(new Point(0, 0), CitySize);
            TileMap = new City_Tyle[CitySize.X, CitySize.Y];
            districts = new HashSet<District>();
            this._e = _e;

            for (int x = 0; x < CitySize.X; x++)
                for (int y = 0; y < CitySize.Y; y++)
                    TileMap[x, y] = new City_Tyle();

        }


        public District GetDistrictByPoint(Point P)
        {
            foreach (var district in districts)
                if (district.Area.Contains(P)) return district;
            return null;
        }





        public void Calculate_Power()
        {
            foreach (var district in districts)
            {
                district.PowerDrain = 0;
                district.PowerSupply = 0;

                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        //Temp setup
                        City_Tyle T = TileMap[x, y];
                        district.PowerDrain += _e.Dictionaryof_BuildItems[T.Type].PowerDrain;
                        district.PowerSupply += _e.Dictionaryof_BuildItems[T.Type].PowerSupply;
                    }

            }

        }

        public void Calculate_RCI()
        {


        }


        public decimal Calculate_Taxes()
        {
            decimal Taxes = 0;


            Taxes += Math.Round(WorkersSupply * 0.0005m, 2);
            Taxes += Math.Round(ProductsSupply * 0.0001m, 2);

            if (CommerceSupply > CommerceDemand)
                Taxes += Math.Round((CommerceSupply - CommerceDemand) * 0.01m, 2);
            Taxes += Math.Round(CommerceSupply * 0.01m, 2);

            return Taxes;
        }


        public void Calculate_JPC()
        {
            //ResidentialDemand = 0;
            //CommercialDemand = 0;
            //IndustrialDemand = 0;

            ResidentialDemandCap = 0;
            CommercialDemandCap = 0;
            IndustrialDemandCap = 0;

            foreach (var district in districts)
            {
                district.ClearJPC();
                Tile_Traffic Traff = new Tile_Traffic();
                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        //Temp setup
                        City_Tyle T = TileMap[x, y];

                        if (_e.Dictionaryof_BuildItems[T.Type].BuildingType == Listof_BuildTypes.Road)
                            Traff.AddTrafficFrom(T.Traffic);
                    }

                //int WorkersSupply = 0, ProductsSupply = 0, CommerceSupply = 0;
                //int WorkersDemand = 0, ProductsDemand = 0, CommerceDemand = 0;
                //int ProductsAfterCommerce = 0;               

                //float MaxGrowthRate = 5;
                //double WorkerMarket = 0, ProductsMarket = 0, CommerceMarket = 0;

                WorkersSupply = Convert.ToInt32(Traff.OriginJobs);
                WorkersDemand = Convert.ToInt32(Traff.DestJobs);

                //Worker to Job Ratio
                if (WorkersDemand > 0)
                    WorkerMarket = (double)WorkersSupply / WorkersDemand;
                else
                    WorkerMarket = 0;
                //Job market ratio cap
                if (WorkerMarket > 1) WorkerMarket = 1;

                ProductsSupply = Convert.ToInt32(Math.Floor(Traff.OriginProducts * WorkerMarket));
                ProductsDemand = Convert.ToInt32(Math.Floor(Traff.DestProducts * WorkerMarket));


                ExcessProducts = ProductsSupply - ProductsDemand;
                ExcessCommerce = CommerceSupply - CommerceDemand;

                double CommerceBoost = 0;
                double IndustryBoost = 0;

                //Industry To Commerce
                if (ProductsDemand > 0)
                {
                    ProductsMarket = (double)ProductsSupply / ProductsDemand;
                    if (ProductsMarket > 1) ProductsMarket = 1;
                    CommerceSupply = Convert.ToInt32(Math.Floor(Traff.OriginCommerce * ProductsMarket));

                    IndustryBoost = WorkerMarket;
                }
                else
                    CommerceSupply = 0;

                CommerceDemand = Convert.ToInt32(Math.Floor(Traff.DestCommerce));


                //Commerce To Residential
                if (CommerceDemand > 0)
                {
                    CommerceMarket = (double)CommerceSupply / CommerceDemand;
                    if (CommerceMarket > 1) CommerceMarket = 1;
                    CommerceBoost = 0.5 + (0.5 * CommerceMarket);
                }
                else
                    CommerceMarket = 0;


                district.Workers = WorkersSupply;
                district.Jobs[0] = WorkersDemand;
                district.JobMarket = WorkerMarket;
                district.Products = ExcessProducts;
                district.Commerce[0] = CommerceSupply;


                //ResidentialDemand = WorkersDemand - (WorkersSupply * CommerceBoost);

                //ResidentialDemand += (Traff.DestJobs * 1.15) - Traff.OriginJobs - Projected_Traffic.OriginJobs;
                //CommercialDemand += (Traff.DestCommerce * 1.10) - Traff.OriginCommerce - Projected_Traffic.OriginCommerce;
                //IndustrialDemand += (Traff.DestProducts * 1.10) - Traff.OriginProducts - Projected_Traffic.OriginProducts;


                ResidentialDemand = 1000;
                //CommercialDemand += 20;
                //IndustrialDemand += 20;

                //if (ResidentialDemand > ResidentialDemandCap) ResidentialDemand = ResidentialDemandCap;
                //if (CommercialDemand > CommercialDemandCap) CommercialDemand = CommercialDemandCap;
                //if (IndustrialDemand > IndustrialDemandCap) IndustrialDemand = IndustrialDemandCap;

            }
        }



        public void Calculate_Growth()
        {
            foreach (var district in districts)
            {
                bool Built = false;
                int RGrowthLimit = 0, CGrowthLimit = 0, IGrowthLimit = 0;
                for (int v = 255; v >= 0; v--)
                {
                    //One at a time                    
                    if (district.ValueList[v].Count > 0)
                    {
                        foreach (var t in district.ValueList[v])
                        {
                            City_Tyle Tile = TileMap[t.X, t.Y];

                            if (ResidentialDemand > 0 && RGrowthLimit <= MaxGrowthRate)
                                if (Grow_Residential()) { ResidentialDemand -= _e.Dictionaryof_BuildItems[TileMap[t.X, t.Y].Type].Traffic.OriginJobs; RGrowthLimit++; }

                            if (CommercialDemand > 0 && CGrowthLimit <= MaxGrowthRate)
                                if (Grow_Commercial()) { CommercialDemand -= _e.Dictionaryof_BuildItems[TileMap[t.X, t.Y].Type].Traffic.OriginCommerce; CGrowthLimit++; }

                            if (IndustrialDemand > 0 && IGrowthLimit <= MaxGrowthRate)
                                if (Grow_Industrial()) { IndustrialDemand -= _e.Dictionaryof_BuildItems[TileMap[t.X, t.Y].Type].Traffic.OriginProducts; IGrowthLimit++; }


                            bool Grow_Residential()
                            {
                                //**********Residential**********

                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Residential_4 && v > 16 && ResidentialDemand > 64)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Residential_5;
                                    TileMap[t.X, t.Y].SpriteIndex = 8;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }

                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Residential_3 && v > 14 && ResidentialDemand > 32)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Residential_4;
                                    TileMap[t.X, t.Y].SpriteIndex = 6;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }

                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Residential_2 && v > 12 && ResidentialDemand > 16)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Residential_3;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }

                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Residential_1 && v > 9 && ResidentialDemand > 8)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Residential_2;
                                    TileMap[t.X, t.Y].SpriteIndex = 2;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }

                                if (TileMap[t.X, t.Y].Type == Listof_Structures.ZoneResidential && ResidentialDemand >= 4)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Residential_1;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }

                                return false;
                            }
                            //TODO Reverse flow
                            bool Grow_Commercial()
                            {
                                //**********Commercial**********
                                if (TileMap[t.X, t.Y].Type == Listof_Structures.ZoneCommercial && CommercialDemand >= 10)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_1;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }

                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Commercial_1 && v > 40)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_2;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }
                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Commercial_2 && v > 40)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_3;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }
                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Commercial_3 && v > 40)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_4;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }
                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Commercial_5 && v > 40)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Commercial_5;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }
                                return false;
                            }

                            bool Grow_Industrial()
                            {
                                //**********Industrial**********
                                if (TileMap[t.X, t.Y].Type == Listof_Structures.ZoneIndustrial && IndustrialDemand >= 10)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_1;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }

                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Industrial_1 && v > 40)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_2;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }
                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Industrial_2 && v > 40)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_3;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }
                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Industrial_3 && v > 40)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_4;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }
                                if (TileMap[t.X, t.Y].Type == Listof_Structures.Industrial_4 && v > 40)
                                {
                                    TileMap[t.X, t.Y].Type = Listof_Structures.Industrial_5;
                                    TileMap[t.X, t.Y].SpriteIndex = 4;
                                    TileMap[t.X, t.Y].Constructing = true;
                                    Built = true;
                                    return true;
                                }
                                return false;

                            }

                        }
                    }
                }
            }

            //Grow constructed buildings based on time.
            foreach (var district in districts)
                for (int v = 255; v >= 0; v--)
                    if (district.ValueList[v].Count > 0)
                        foreach (var t in district.ValueList[v])
                        {
                            City_Tyle tile = TileMap[t.X, t.Y];
                            if (_e.Dictionaryof_BuildItems[tile.Type].BuildingType == Listof_BuildTypes.Structure &&
                                _e.Dictionaryof_BuildItems[tile.Type].ZoneType == Listof_ZoneType.Residential)
                            {
                                if (tile.Constructing)
                                    tile.Growth++;

                                if (tile.Growth == 4 && tile.SpriteIndex == 0) { tile.SpriteIndex = 1; tile.Constructing = false; }
                                if (tile.Growth == 8 && tile.SpriteIndex == 2) { tile.SpriteIndex = 3; tile.Constructing = false; }
                                if (tile.Growth == 12 && tile.SpriteIndex == 4) { tile.SpriteIndex = 5; tile.Constructing = false; }
                                if (tile.Growth == 16 && tile.SpriteIndex == 6) { tile.SpriteIndex = 7; tile.Constructing = false; }
                                if (tile.Growth == 20 && tile.SpriteIndex == 8) { tile.SpriteIndex = 9; tile.Constructing = false; }
                            }

                            if (_e.Dictionaryof_BuildItems[tile.Type].BuildingType == Listof_BuildTypes.Structure &&
                                _e.Dictionaryof_BuildItems[tile.Type].ZoneType == Listof_ZoneType.Commercial)
                            {
                                if (tile.Constructing)
                                    tile.Growth++;

                                if (tile.Growth == 8 && tile.SpriteIndex == 0) { tile.SpriteIndex = 1; tile.Constructing = false; }
                                if (tile.Growth == 16 && tile.SpriteIndex == 2) { tile.SpriteIndex = 3; tile.Constructing = false; }
                                if (tile.Growth == 20 && tile.SpriteIndex == 4) { tile.SpriteIndex = 5; tile.Constructing = false; }
                                if (tile.Growth == 24 && tile.SpriteIndex == 6) { tile.SpriteIndex = 7; tile.Constructing = false; }
                                if (tile.Growth == 30 && tile.SpriteIndex == 8) { tile.SpriteIndex = 9; tile.Constructing = false; }
                            }

                            if (_e.Dictionaryof_BuildItems[tile.Type].BuildingType == Listof_BuildTypes.Structure &&
                                _e.Dictionaryof_BuildItems[tile.Type].ZoneType == Listof_ZoneType.Industrial)
                            {
                                if (tile.Constructing)
                                    tile.Growth++;

                                if (tile.Growth == 12 && tile.SpriteIndex == 0) { tile.SpriteIndex = 1; tile.Constructing = false; }
                                if (tile.Growth == 20 && tile.SpriteIndex == 2) { tile.SpriteIndex = 3; tile.Constructing = false; }
                                if (tile.Growth == 30 && tile.SpriteIndex == 4) { tile.SpriteIndex = 5; tile.Constructing = false; }
                                if (tile.Growth == 40 && tile.SpriteIndex == 6) { tile.SpriteIndex = 7; tile.Constructing = false; }
                                if (tile.Growth == 50 && tile.SpriteIndex == 8) { tile.SpriteIndex = 9; tile.Constructing = false; }
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
                    double ValueAdd = 0;
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
                                double distance;

                                if (Math.Abs(x - x2) > Math.Abs(y - y2))
                                    distance = Math.Abs(x - x2) * 2;
                                else
                                    distance = Math.Abs(y - y2) * 2;
                                if (distance == 0)
                                    distance = 1;

                                if (CityArea.Contains(new Point(x2, y2)))
                                    TileMap[x2, y2].LandValue += ValueAdd / distance;
                            }
                    }
                }
            //Add items to value list
            foreach (var district in districts)
            {
                for (int x = 0; x < 256; x++)
                    district.ValueList[x].Clear();

                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        int value = Convert.ToInt32(Math.Floor(TileMap[x, y].LandValue));
                        if (!district.ValueList[value].Contains(new Point(x, y)))
                            district.ValueList[value].Add(new Point(x, y));
                    }
            }

        }

    }
}
