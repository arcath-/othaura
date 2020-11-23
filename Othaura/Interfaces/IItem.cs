/************************************************************
23 November 2020 - Roguesharp V5 implementation  
  
************************************************************/

namespace Othaura.Interfaces {

    public interface IItem {

        string Name { get; }
        int RemainingUses { get; }

        bool Use();
    }
}
