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

        public void Calculate_Traffic()
        {
            Traffic_IO();
        }
        

        public void Traffic_IO()
        {
            //First Pass set's all the traffic second pass adds traffic to roads           
            foreach (var district in districts)
            {
                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                        TileMap[x, y].Traffic.Clear();


                //Add traffic and get ConnectedList
                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        City_Tyle T = TileMap[x, y];                        
                        //Check and add Traffic Value to Tile
                        switch (T.Type)
                        {
                            case Listof_Structures.Residential_1:
                                T.Traffic.OriginJobs += 4;
                                T.Traffic.DestCommerce += 2;
                                break;
                            case Listof_Structures.Residential_2:
                                T.Traffic.OriginJobs += 8;
                                T.Traffic.DestCommerce += 4;
                                break;
                            case Listof_Structures.Residential_3:
                                T.Traffic.OriginJobs += 16;
                                T.Traffic.DestCommerce += 8;
                                break;
                            case Listof_Structures.Residential_4:
                                T.Traffic.OriginJobs += 32;
                                T.Traffic.DestCommerce += 16;                                 
                                break;
                            case Listof_Structures.Residential_5:
                                T.Traffic.OriginJobs += 64;
                                T.Traffic.DestCommerce += 32;                                 
                                break;
                            case Listof_Structures.Commercial_1:
                                T.Traffic.DestJobs += 6;
                                T.Traffic.DestProducts += 4;
                                T.Traffic.OriginCommerce += 10;
                                break;
                            case Listof_Structures.Commercial_2:
                                T.Traffic.DestJobs += 8;
                                T.Traffic.DestProducts += 8;
                                T.Traffic.OriginCommerce += 8;                                 
                                break;
                            case Listof_Structures.Commercial_3:
                                T.Traffic.DestJobs += 16;
                                T.Traffic.DestProducts += 16;
                                T.Traffic.OriginCommerce += 16;                                 
                                break;
                            case Listof_Structures.Commercial_4:
                                T.Traffic.DestJobs += 32;
                                T.Traffic.DestProducts += 32;
                                T.Traffic.OriginCommerce += 32;                                 
                                break;
                            case Listof_Structures.Commercial_5:
                                T.Traffic.DestJobs += 64;
                                T.Traffic.DestProducts += 64;
                                T.Traffic.OriginCommerce += 64;                                 
                                break;
                            case Listof_Structures.Industrial_1:
                                T.Traffic.DestJobs += 20;
                                T.Traffic.OriginProducts += 10;
                                break;
                            case Listof_Structures.Industrial_2:
                                T.Traffic.DestJobs += 64;
                                T.Traffic.OriginProducts += 64;                                 
                                break;
                            case Listof_Structures.Industrial_3:
                                T.Traffic.DestJobs += 128;
                                T.Traffic.OriginProducts += 128;                                 
                                break;
                            case Listof_Structures.Industrial_4:
                                T.Traffic.DestJobs +=256;
                                T.Traffic.OriginProducts +=256;                                 
                                break;
                            case Listof_Structures.Industrial_5:
                                T.Traffic.DestJobs += 512;
                                T.Traffic.OriginProducts += 512;                                 
                                break;                            
                        }
                    }



                //Add all traffic to roads
                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        City_Tyle T = TileMap[x, y];

                        if (_e.Dictionaryof_BuildItems[T.Type].BuildingType == Listof_BuildTypes.Road)
                            T.TrafficByConnection();
                    }                                     
            }
        }
    }
}