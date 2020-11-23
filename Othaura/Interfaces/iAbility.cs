/************************************************************
23 November 2020 - Roguesharp V5 implementation  
  
************************************************************/

namespace Othaura.Interfaces {

    public interface IAbility {

        string Name { get; }
        int TurnsToRefresh { get; }
        int TurnsUntilRefreshed { get; }

        bool Perform();
        void Tick();
    }
}
