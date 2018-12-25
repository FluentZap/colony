﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{
    partial class City
    {



        public void ProcessToParent(Tile_Traffic tile)
        {
            Tile_Traffic parent = tile.Parent;
            while (parent != null)
            {                
                parent.AddToTransfer(tile);
                tile = parent;
                parent = tile.Parent;
            }

        }

        public void AddTransfurTraffic(City_Tyle Source, City_Tyle Top = null, City_Tyle Bottom = null, City_Tyle Left = null, City_Tyle Right = null)
        {
            int split = 0;
            if (Top != null) split += 1;
            if (Bottom != null) split += 1;
            if (Left != null) split += 1;
            if (Right != null) split += 1;

            Tile_Traffic TrafficAdd = new Tile_Traffic();
            TrafficAdd.OriginJobs_Transfer = (Source.Traffic.OriginJobs + Source.Traffic.OriginJobs_Transfer) / split;
            TrafficAdd.OriginCommerce_Transfer = (Source.Traffic.OriginCommerce + Source.Traffic.OriginCommerce_Transfer) / split;
            TrafficAdd.OriginProducts_Transfer = (Source.Traffic.OriginProducts + Source.Traffic.OriginProducts_Transfer) / split;

            TrafficAdd.DestJobs_Transfer = (Source.Traffic.DestJobs + Source.Traffic.DestJobs_Transfer) / split;
            TrafficAdd.DestCommerce_Transfer = (Source.Traffic.DestCommerce + Source.Traffic.DestCommerce_Transfer) / split;
            TrafficAdd.DestProducts_Transfer = (Source.Traffic.DestProducts + Source.Traffic.DestProducts_Transfer) / split;

            if (Top != null) Top.Traffic.AddToTransfer(TrafficAdd);
            if (Bottom != null) Bottom.Traffic.AddToTransfer(TrafficAdd);
            if (Left != null) Left.Traffic.AddToTransfer(TrafficAdd);
            if (Right != null) Right.Traffic.AddToTransfer(TrafficAdd);

        }


        public void Calculate_Traffic()
        {
            Traffic_IO();
        }



        public void Traffic_IO()
        {
            //First Pass set's all the traffic second pass adds traffic to roads           
            foreach (var district in districts)
            {
                Queue<Point> SearchList = new Queue<Point>();
                
                HashSet<Point> Finished = new HashSet<Point>();


                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                        TileMap[x, y].Traffic.Clear();


                //Add traffic and get ConnectedList
                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        City_Tyle T = TileMap[x, y];
                        bool Traffic = false;
                        //Check and add Traffic Value to Tile
                        switch (T.Type)
                        {
                            case Listof_Structures.Residential_1:
                                T.Traffic.OriginJobs += 4;
                                T.Traffic.OriginCommerce += 4;
                                Traffic = true;
                                break;
                            case Listof_Structures.Residential_2:
                                T.Traffic.OriginJobs += 8;
                                T.Traffic.OriginCommerce += 8;
                                Traffic = true;
                                break;
                            case Listof_Structures.Residential_3:
                                T.Traffic.OriginJobs += 16;
                                T.Traffic.OriginCommerce += 16;
                                Traffic = true;
                                break;
                            case Listof_Structures.Residential_4:
                                T.Traffic.OriginJobs += 32;
                                T.Traffic.OriginCommerce += 32;
                                Traffic = true;
                                break;
                            case Listof_Structures.Residential_5:
                                T.Traffic.OriginJobs += 64;
                                T.Traffic.OriginCommerce += 64;
                                Traffic = true;
                                break;

                            case Listof_Structures.Commercial_1:
                                T.Traffic.DestJobs += 1;
                                T.Traffic.DestProducts += 1;
                                T.Traffic.DestCommerce += 1;
                                Traffic = true;
                                break;
                            case Listof_Structures.Commercial_2:
                                T.Traffic.DestJobs += 2;
                                T.Traffic.DestProducts += 2;
                                T.Traffic.DestCommerce += 2;
                                Traffic = true;
                                break;
                            case Listof_Structures.Commercial_3:
                                T.Traffic.DestJobs += 3;
                                T.Traffic.DestProducts += 3;
                                T.Traffic.DestCommerce += 3;
                                Traffic = true;
                                break;
                            case Listof_Structures.Commercial_4:
                                T.Traffic.DestJobs += 4;
                                T.Traffic.DestProducts += 4;
                                T.Traffic.DestCommerce += 4;
                                Traffic = true;
                                break;
                            case Listof_Structures.Commercial_5:
                                T.Traffic.DestJobs += 5;
                                T.Traffic.DestProducts += 5;
                                T.Traffic.DestCommerce += 5;
                                Traffic = true;
                                break;

                            case Listof_Structures.Industrial_1:
                                T.Traffic.DestJobs += 1;
                                T.Traffic.OriginProducts += 1;
                                Traffic = true;
                                break;
                            case Listof_Structures.Industrial_2:
                                T.Traffic.DestJobs += 2;
                                T.Traffic.OriginProducts += 2;
                                Traffic = true;
                                break;
                            case Listof_Structures.Industrial_3:
                                T.Traffic.DestJobs += 3;
                                T.Traffic.OriginProducts += 3;
                                Traffic = true;
                                break;
                            case Listof_Structures.Industrial_4:
                                T.Traffic.DestJobs += 4;
                                T.Traffic.OriginProducts += 4;
                                Traffic = true;
                                break;
                            case Listof_Structures.Industrial_5:
                                T.Traffic.DestJobs += 5;
                                T.Traffic.OriginProducts += 5;
                                Traffic = true;
                                break;

                        }


                        if (Traffic)
                        {
                            bool connected = false;
                            //Check Top, Bottom, Left, Right                                
                            //City_Tyle Top = null, Bottom = null, Left = null, Right = null;

                            if (y - 1 >= district.Area.Top)
                                if (_e.Dictionaryof_BuildItems[TileMap[x, y - 1].Type].BuildingType == Listof_BuildTypes.Road)
                                    connected = true;

                            if (y + 1 <= district.Area.Bottom)
                                if (_e.Dictionaryof_BuildItems[TileMap[x, y + 1].Type].BuildingType == Listof_BuildTypes.Road)
                                    connected = true;

                            if (x - 1 >= district.Area.Left)
                                if (_e.Dictionaryof_BuildItems[TileMap[x - 1, y].Type].BuildingType == Listof_BuildTypes.Road)
                                    connected = true;

                            if (x + 1 <= district.Area.Right)
                                if (_e.Dictionaryof_BuildItems[TileMap[x + 1, y].Type].BuildingType == Listof_BuildTypes.Road)
                                    connected = true;

                            //If there is a road conenction add to connected list
                            if (connected)
                                SearchList.Enqueue(new Point(x, y));
                        }
                    }

                int tier = 0;
                while (SearchList.Count() > 0)
                {
                    Point P = SearchList.Dequeue();
                    Finished.Add(P);

                    if (P.Y - 1 >= district.Area.Top)
                        if (_e.Dictionaryof_BuildItems[TileMap[P.X, P.Y - 1].Type].ZoneType == _e.Dictionaryof_BuildItems[TileMap[P.X, P.Y].Type].ZoneType)
                            if (!SearchList.Contains(new Point(P.X, P.Y - 1)) && !Finished.Contains(new Point(P.X, P.Y - 1)))
                            {
                                SearchList.Enqueue(new Point(P.X, P.Y - 1));
                                TileMap[P.X, P.Y - 1].Traffic.Parent = TileMap[P.X, P.Y].Traffic;
                                TileMap[P.X, P.Y - 1].Traffic.tier = TileMap[P.X, P.Y].Traffic.tier + 1;
                                ProcessToParent(TileMap[P.X, P.Y - 1].Traffic);
                            }

                    if (P.Y + 1 <= district.Area.Bottom)
                        if (_e.Dictionaryof_BuildItems[TileMap[P.X, P.Y + 1].Type].ZoneType == _e.Dictionaryof_BuildItems[TileMap[P.X, P.Y].Type].ZoneType)
                            if (!SearchList.Contains(new Point(P.X, P.Y + 1)) && !Finished.Contains(new Point(P.X, P.Y + 1)))
                            {
                                SearchList.Enqueue(new Point(P.X, P.Y + 1));
                                TileMap[P.X, P.Y + 1].Traffic.Parent = TileMap[P.X, P.Y].Traffic;
                                TileMap[P.X, P.Y + 1].Traffic.tier = TileMap[P.X, P.Y].Traffic.tier + 1;
                                ProcessToParent(TileMap[P.X, P.Y + 1].Traffic);
                            }

                    if (P.X - 1 >= district.Area.Left)
                        if (_e.Dictionaryof_BuildItems[TileMap[P.X - 1, P.Y].Type].ZoneType == _e.Dictionaryof_BuildItems[TileMap[P.X, P.Y].Type].ZoneType)
                            if (!SearchList.Contains(new Point(P.X - 1, P.Y)) && !Finished.Contains(new Point(P.X - 1, P.Y)))
                            {
                                SearchList.Enqueue(new Point(P.X - 1, P.Y));
                                TileMap[P.X - 1, P.Y].Traffic.Parent = TileMap[P.X, P.Y].Traffic;
                                TileMap[P.X - 1, P.Y].Traffic.tier = TileMap[P.X, P.Y].Traffic.tier + 1;
                                ProcessToParent(TileMap[P.X - 1, P.Y].Traffic);
                            }

                    if (P.X + 1 <= district.Area.Right)
                        if (_e.Dictionaryof_BuildItems[TileMap[P.X + 1, P.Y].Type].ZoneType == _e.Dictionaryof_BuildItems[TileMap[P.X, P.Y].Type].ZoneType)
                            if (!SearchList.Contains(new Point(P.X + 1, P.Y)) && !Finished.Contains(new Point(P.X + 1, P.Y)))
                            {
                                SearchList.Enqueue(new Point(P.X + 1, P.Y));
                                TileMap[P.X + 1, P.Y].Traffic.Parent = TileMap[P.X, P.Y].Traffic;
                                TileMap[P.X + 1, P.Y].Traffic.tier = TileMap[P.X, P.Y].Traffic.tier + 1;
                                ProcessToParent(TileMap[P.X + 1, P.Y].Traffic);
                            }
                    


                }

/*
                //Seccond Pass, This adds the calculated traffic value to the road that connects to it
                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        City_Tyle T = TileMap[x, y];

                        //Check Top, Bottom, Left, Right                                
                        City_Tyle Top = null, Bottom = null, Left = null, Right = null;

                        if (y - 1 >= 0)
                            if (_e.Dictionaryof_BuildItems[TileMap[x, y - 1].Type].BuildingType == Listof_BuildTypes.Road)
                                Top = TileMap[x, y - 1];

                        if (y + 1 <= CityArea.Height)
                            if (_e.Dictionaryof_BuildItems[TileMap[x, y + 1].Type].BuildingType == Listof_BuildTypes.Road)
                                Bottom = TileMap[x, y + 1];

                        if (x - 1 >= 0)
                            if (_e.Dictionaryof_BuildItems[TileMap[x - 1, y].Type].BuildingType == Listof_BuildTypes.Road)
                                Left = TileMap[x - 1, y];

                        if (x + 1 <= CityArea.Width)
                            if (_e.Dictionaryof_BuildItems[TileMap[x + 1, y].Type].BuildingType == Listof_BuildTypes.Road)
                                Right = TileMap[x + 1, y];

                        //If there is a conenction add trafic to them
                        if (Top == null && Bottom == null && Left == null && Right == null)
                            T.Connected = false;
                        else
                        {
                            AddTransfurTraffic(T, Top, Bottom, Left, Right);
                            T.Traffic.Processed = true;
                        }




                    }

*/
            }
        }
    }
}