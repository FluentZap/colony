using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{

    enum Listof_Structures
    {
        Grass,
        CityCenter,
        ZoneResidential,
        ZoneCommercial,
        ZoneIndustrial,
        RoadDirt

    }

    
    class City_Tyle
    {
        public Listof_Structures Type = 0;

        public City_Tyle()
        {

        }

    }


    class City
    {
        public City_Tyle[,] TileMap = new City_Tyle[200, 200];

        public City()
        {
            for (int x = 0; x < 200; x++)
                for (int y = 0; y < 200; y++)                
                    TileMap[x, y] = new City_Tyle();
            
        }


    }
}
