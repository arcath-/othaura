/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

namespace Othaura.Interfaces {

    public interface IItem {
        string Name { get; }
        int RemainingUses { get; }

        bool Use();
    }
}
