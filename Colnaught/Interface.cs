using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colnaught
{

    enum Listof_ViewInterfaceButtons
    {        
        BuildPanel,
        BuildButton1,
        BuildButton2,
        BuildButton3,
        BuildButton4,
        BuildButton5
    }

    enum Listof_ButtonType
    {
        Panel,
        Button
    }




    class Interface_Item
    {
        public string Label;
        public Listof_ButtonType Type;
        public Rectangle Location;
        public Color color = Color.White;
        /*
        public Interface_Item(Rectangle Location, Listof_ButtonType Type = Listof_ButtonType.Panel, string Label = "")
        {
            this.Label = Label;
            this.Type = Type;
            this.Location = Location;
        }
        */

    }


    class Interface
    {
        public Dictionary<Listof_ViewInterfaceButtons, Interface_Item> Dictionaryof_CityScreenButtons = new Dictionary<Listof_ViewInterfaceButtons, Interface_Item>();

        public Interface(Point Screen_Size)
        {
            int w = Screen_Size.X;
            int h = Screen_Size.Y;
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildPanel, new Interface_Item() { Location = new Rectangle(0, h - 200, w, 200)});

            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton1, new Interface_Item() { Location = new Rectangle(256 + 128 * 0, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton2, new Interface_Item() { Location = new Rectangle(256 + 128 * 1, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton3, new Interface_Item() { Location = new Rectangle(256 + 128 * 2, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton4, new Interface_Item() { Location = new Rectangle(256 + 128 * 3, h - 200, 128, 128), Type = Listof_ButtonType.Button });
            Dictionaryof_CityScreenButtons.Add(Listof_ViewInterfaceButtons.BuildButton5, new Interface_Item() { Location = new Rectangle(256 + 128 * 4, h - 200, 128, 128), Type = Listof_ButtonType.Button });
        }               




    }
}
