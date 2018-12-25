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

    }


    class Encyclopedia
    {


        public Dictionary<Listof_Structures, Typeof_BuildItems> Dictionaryof_BuildItems = new Dictionary<Listof_Structures, Typeof_BuildItems>();


        public Encyclopedia()
        {
            Dictionaryof_BuildItems.Add(Listof_Structures.Grass, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Empty, Texture = Listof_Texture.Grass });
            Dictionaryof_BuildItems.Add(Listof_Structures.CityCenter, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.CityCenter, Texture = Listof_Texture.CityCenter });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneResidential, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Texture = Listof_Texture.Residential, ZoneType = Listof_ZoneType.Residential });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneCommercial, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Texture = Listof_Texture.Commercial, ZoneType = Listof_ZoneType.Commercial });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneIndustrial, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Texture = Listof_Texture.Industrial, ZoneType = Listof_ZoneType.Industrial });

            Dictionaryof_BuildItems.Add(Listof_Structures.RoadDirt, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Road, Texture = Listof_Texture.Road });

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
        }



    }

    
}