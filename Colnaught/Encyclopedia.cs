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
        public Listof_BuildTypes BuildingType;
        public Listof_Texture Texture;
        public Listof_ZoneType ZoneType = Listof_ZoneType.None;
        public Tile_Traffic Traffic = new Tile_Traffic();

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

            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential });            
            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_2, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential });
            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_3, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential });
            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_4, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential });
            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_5, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_Structure, ZoneType = Listof_ZoneType.Residential });

            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial });
            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_2, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial });
            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_3, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial });
            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_4, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial });
            Dictionaryof_BuildItems.Add(Listof_Structures.Commercial_5, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Commercial_Structure, ZoneType = Listof_ZoneType.Commercial });

            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial });
            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_2, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial });
            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_3, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial });
            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_4, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial });
            Dictionaryof_BuildItems.Add(Listof_Structures.Industrial_5, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Industrial_Structure, ZoneType = Listof_ZoneType.Industrial });

            //Traffic and Production
            Dictionaryof_BuildItems[Listof_Structures.Residential_1].Traffic.OriginJobs = 4;
            Dictionaryof_BuildItems[Listof_Structures.Residential_1].Traffic.DestCommerce = 2;

            Dictionaryof_BuildItems[Listof_Structures.Residential_2].Traffic.OriginJobs = 8;
            Dictionaryof_BuildItems[Listof_Structures.Residential_2].Traffic.DestCommerce = 4;

            Dictionaryof_BuildItems[Listof_Structures.Residential_3].Traffic.OriginJobs = 16;
            Dictionaryof_BuildItems[Listof_Structures.Residential_3].Traffic.DestCommerce = 8;

            Dictionaryof_BuildItems[Listof_Structures.Residential_4].Traffic.OriginJobs = 32;
            Dictionaryof_BuildItems[Listof_Structures.Residential_4].Traffic.DestCommerce = 16;

            Dictionaryof_BuildItems[Listof_Structures.Residential_5].Traffic.OriginJobs = 64;
            Dictionaryof_BuildItems[Listof_Structures.Residential_5].Traffic.DestCommerce = 32;

            Dictionaryof_BuildItems[Listof_Structures.Commercial_1].Traffic.DestJobs = 6;
            Dictionaryof_BuildItems[Listof_Structures.Commercial_1].Traffic.DestProducts = 5;
            Dictionaryof_BuildItems[Listof_Structures.Commercial_1].Traffic.OriginCommerce = 8;

            Dictionaryof_BuildItems[Listof_Structures.Industrial_1].Traffic.DestJobs = 20;
            Dictionaryof_BuildItems[Listof_Structures.Industrial_1].Traffic.OriginProducts = 10;
                            

        }



    }

    
}