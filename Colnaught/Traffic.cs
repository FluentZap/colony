using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{
    partial class City
    {

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
                HashSet<Point> AddList = new HashSet<Point>();

                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                        TileMap[x, y].Traffic.Clear();

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


                        //Check for Sourounding tiles for a road to add traffic to
                        //If no road is found add traffic to Bottom/Right or both.
                        if (Traffic)
                        {
                            bool RoadConnected = false;

                            for (int x2 = x - 1; x2 < x + 1; x2++)
                            {
                                for (int y2 = y - 1; y2 < y + 1; y2++)
                                {
                                    if (x2 >= 0 && x2 <= CityArea.Width && y2 >= 0 && y2 <= CityArea.Height)
                                    {
                                        //Check if the tile is a road
                                        if (_e.Dictionaryof_BuildItems[TileMap[x2, y2].Type].BuildingType == Listof_BuildTypes.Road)
                                            RoadConnected = true;
                                    }
                                    if (RoadConnected) break;
                                }
                                if (RoadConnected) break;
                            }

                            //AddTransfurTraffic(T, TileMap[x2, y2]);

                            //If the traffic is not connected to a road add it to it's neighbors evenly.
                            if (!RoadConnected)
                            {
                                //Check Top, Bottom, Left, Right                                
                                City_Tyle Top = null, Bottom = null, Left = null, Right = null;

                                if (y - 1 >= 0)
                                    if (_e.Dictionaryof_BuildItems[TileMap[x, y - 1].Type].ZoneType == _e.Dictionaryof_BuildItems[T.Type].ZoneType)
                                        Top = TileMap[x, y - 1];

                                if (y + 1 <= CityArea.Height)
                                    if (_e.Dictionaryof_BuildItems[TileMap[x, y + 1].Type].ZoneType == _e.Dictionaryof_BuildItems[T.Type].ZoneType)
                                        Bottom = TileMap[x, y + 1];

                                if (x - 1 >= 0)
                                    if (_e.Dictionaryof_BuildItems[TileMap[x - 1, y].Type].ZoneType == _e.Dictionaryof_BuildItems[T.Type].ZoneType)
                                        Left = TileMap[x - 1, y];

                                if (x + 1 <= CityArea.Width)
                                    if (_e.Dictionaryof_BuildItems[TileMap[x + 1, y].Type].ZoneType == _e.Dictionaryof_BuildItems[T.Type].ZoneType)
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




                        }
                    }

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


            }
        }
    }
}