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
            Projected_Traffic.Clear();
            foreach (var district in districts)
            {
                /*
                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                        TileMap[x, y].Traffic.Clear();
                 */

                //Add traffic and get ConnectedList
                for (int x = district.Area.Left; x < district.Area.Right; x++)
                    for (int y = district.Area.Top; y < district.Area.Bottom; y++)
                    {
                        City_Tyle T = TileMap[x, y];
                        //Check and add Traffic Value to Tile
                        
                        //T.Traffic.AddTrafficFrom(T.Traffic);
                        
                        if (T.Constructing)
                        {
                            Projected_Traffic.AddTrafficFrom(_e.Dictionaryof_BuildItems[T.Type].Traffic);
                            //T.Traffic.Clear();
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