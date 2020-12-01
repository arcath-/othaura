/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
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
