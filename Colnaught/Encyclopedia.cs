using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{


    enum Listof_BuildTypes
    {
        Empty,
        Zone,
        Road,
        Structure,
        CityCenter
    }


    enum Listof_ZoneType
    {
        None,
        Residential,
        Commercial,
        Industrial
    }


    class Typeof_BuildItems
    {
        public int Cost;
        public Point Size = new Point(1, 1);
        public Listof_BuildTypes BuildingType;
        public Listof_Texture Texture;
        public Listof_ZoneType ZoneType = Listof_ZoneType.None;
        public Tile_Traffic Traffic = new Tile_Traffic();
        public int PowerSupply;
        public int PowerDrain;

        public int[] Education = new int[4];

    }


    class Encyclopedia
    {


        public Dictionary<Listof_Structures, Typeof_BuildItems> Dictionaryof_BuildItems = new Dictionary<Listof_Structures, Typeof_BuildItems>();


        public Encyclopedia()
        {
            Dictionaryof_BuildItems.Add(Listof_Structures.Grass, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Empty, Cost = 0, Texture = Listof_Texture.Grass });
            Dictionaryof_BuildItems.Add(Listof_Structures.CityCenter, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.CityCenter, Cost = 50000, Texture = Listof_Texture.CityCenter });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneResidential, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Cost = 2, Texture = Listof_Texture.Residential, ZoneType = Listof_ZoneType.Residential });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneCommercial, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Cost = 2, Texture = Listof_Texture.Commercial, ZoneType = Listof_ZoneType.Commercial });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneIndustrial, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Cost = 2, Texture = Listof_Texture.Industrial, ZoneType = Listof_ZoneType.Industrial });

            Dictionaryof_BuildItems.Add(Listof_Structures.RoadDirt, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Road, Cost = 50, Texture = Listof_Texture.Road });

            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential, PowerDrain = 1 });            
            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_2, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_3, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_4, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_5, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential, PowerDrain = 1 });

            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial, PowerDrain = 2 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_2, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_3, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_4, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_5, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial, PowerDrain = 1 });

            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial, PowerDrain = 4 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_2, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_3, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_4, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial, PowerDrain = 1 });
            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_5, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial, PowerDrain = 1 });


            Dictionaryof_BuildItems.Add(Listof_Structures.PowerPlant1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Cost = 2500, Texture = Listof_Texture.PowerPlant1, Size = new Point(2, 2), PowerSupply = 200 });

            Dictionaryof_BuildItems.Add(Listof_Structures.School1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Cost = 2500, Texture = Listof_Texture.School1, Size = new Point(2, 2), Education = new int[4] { 10, 0, 0, 0 } });

            //Traffic and Production
            Dictionaryof_BuildItems[Listof_Structures.PowerPlant1].Traffic = new Tile_Traffic() { DestJobsWorker = 50, DestProducts = 10 };

            Dictionaryof_BuildItems[Listof_Structures.Residential_1].Traffic = new Tile_Traffic() { Housing = 4, OriginJobsWorker = 4, DestCommerceWorker = 2 };
            Dictionaryof_BuildItems[Listof_Structures.Residential_2].Traffic = new Tile_Traffic() { Housing = 40, OriginJobsWorker = 40, DestCommerceWorker = 20 };
            Dictionaryof_BuildItems[Listof_Structures.Residential_3].Traffic = new Tile_Traffic() { Housing = 400, OriginJobsWorker = 400, DestCommerceWorker = 200 };
            Dictionaryof_BuildItems[Listof_Structures.Residential_4].Traffic = new Tile_Traffic() { Housing = 32, OriginJobsWorker = 32, DestCommerceWorker = 16 };
            Dictionaryof_BuildItems[Listof_Structures.Residential_5].Traffic = new Tile_Traffic() { Housing = 64, OriginJobsWorker = 64, DestCommerceWorker = 32 };


            Dictionaryof_BuildItems[Listof_Structures.Commercial_1].Traffic = new Tile_Traffic() { DestJobsWorker = 6, DestProducts = 5, OriginCommerceWorker = 8 };
            Dictionaryof_BuildItems[Listof_Structures.Commercial_2].Traffic = new Tile_Traffic() { DestJobsWorker = 60, DestProducts = 50, OriginCommerceWorker = 80 };

            Dictionaryof_BuildItems[Listof_Structures.Industrial_1].Traffic = new Tile_Traffic() { DestJobsWorker = 20, OriginProducts = 10};            
            Dictionaryof_BuildItems[Listof_Structures.Industrial_2].Traffic = new Tile_Traffic() { DestJobsWorker = 40, DestJobsCommoner = 20, OriginProducts = 40 };
        }



    }

    
}