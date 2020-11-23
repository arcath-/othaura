/************************************************************
23 November 2020 - Roguesharp V5 implementation  
  
************************************************************/

using RLNET;
using Othaura.Core;

namespace Othaura.Interfaces {

    public interface IDrawable {

        RLColor Color { get; set; }
        char Symbol { get; set; }
        int X { get; set; }
        int Y { get; set; }

        void Draw(RLConsole console, DungeonMap map);
    }
}
