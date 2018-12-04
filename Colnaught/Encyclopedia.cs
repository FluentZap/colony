using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{

    enum Listof_BuildTypes
    {
        Zone,
        Road,
        Structure,
        CityCenter
    }

    class Typeof_BuildItems
    {
        public int Cost;
        public Listof_BuildTypes BuildingType;
        public Listof_Texture Texture;
    }


    class Encyclopedia
    {


        public Dictionary<Listof_Structures, Typeof_BuildItems> Dictionaryof_BuildItems = new Dictionary<Listof_Structures, Typeof_BuildItems>();


        public Encyclopedia()
        {
            Dictionaryof_BuildItems.Add(Listof_Structures.Grass, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Texture = Listof_Texture.Grass });
            Dictionaryof_BuildItems.Add(Listof_Structures.CityCenter, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.CityCenter, Texture = Listof_Texture.CityCenter });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneResidential, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Texture = Listof_Texture.Residential });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneCommercial, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Texture = Listof_Texture.Commercial });
            Dictionaryof_BuildItems.Add(Listof_Structures.ZoneIndustrial, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Zone, Texture = Listof_Texture.Industrial });

            Dictionaryof_BuildItems.Add(Listof_Structures.RoadDirt, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Road, Texture = Listof_Texture.Road });

            Dictionaryof_BuildItems.Add(Listof_Structures.Residential_1_1, new Typeof_BuildItems() { BuildingType = Listof_BuildTypes.Structure, Texture = Listof_Texture.Residential_1 });

        }



    }

    
}