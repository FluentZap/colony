using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{    

    public enum Listof_TechItems : int
    {
        CityCenter,
        ResidentialZone,
        CommercialZone,
        IndustrialZone,
        Road1,
        PowerPlant
    }

    class TechItem
    {
        //BuildUnlock




    }   


    public enum Listof_BuildIconTexture : int
    {
        CityCenter,
        Road,
        Residentail,
        Commercial,
        Industrial,
        PowerPlant
    }

    class BuildButtonTech
    {
        public Listof_BuildTabCategories Category;
        public Listof_Structures BuildItem;
        public Listof_BuildIconTexture BuildIcon;
    }



    class Tech
    {
        public Dictionary<Listof_TechItems, BuildButtonTech> BuildingTech = new Dictionary<Listof_TechItems, BuildButtonTech>();
        





        public Tech()
        {
            BuildingTech.Add(Listof_TechItems.CityCenter, new BuildButtonTech() { Category = Listof_BuildTabCategories.Zones, BuildItem = Listof_Structures.CityCenter, BuildIcon = Listof_BuildIconTexture.CityCenter });
            BuildingTech.Add(Listof_TechItems.ResidentialZone, new BuildButtonTech() { Category = Listof_BuildTabCategories.Zones, BuildItem = Listof_Structures.ZoneResidential, BuildIcon = Listof_BuildIconTexture.Residentail });
            BuildingTech.Add(Listof_TechItems.CommercialZone, new BuildButtonTech() { Category = Listof_BuildTabCategories.Zones, BuildItem = Listof_Structures.ZoneCommercial, BuildIcon = Listof_BuildIconTexture.Commercial });
            BuildingTech.Add(Listof_TechItems.IndustrialZone, new BuildButtonTech() { Category = Listof_BuildTabCategories.Zones, BuildItem = Listof_Structures.ZoneIndustrial, BuildIcon = Listof_BuildIconTexture.Industrial });
            BuildingTech.Add(Listof_TechItems.Road1, new BuildButtonTech() { Category = Listof_BuildTabCategories.Roads, BuildItem = Listof_Structures.RoadDirt, BuildIcon = Listof_BuildIconTexture.Road });

            BuildingTech.Add(Listof_TechItems.PowerPlant, new BuildButtonTech() { Category = Listof_BuildTabCategories.Power, BuildItem = Listof_Structures.PowerPlant1, BuildIcon = Listof_BuildIconTexture.Road });
        }



        void Unlock_Tech(Listof_TechItems TechItem)
        {




        }





    }









    public partial class Game1
    {


        HashSet<Listof_TechItems> UnlockedTech = new HashSet<Listof_TechItems>();

        Dictionary<Listof_BuildTabCategories, HashSet<int>> BuildTech = new Dictionary<Listof_BuildTabCategories, HashSet<int>>();











    }
}
