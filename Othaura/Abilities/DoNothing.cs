/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

using Othaura.Core;

namespace Othaura.Abilities {

    public class DoNothing : Ability {
        public DoNothing() {
            Name = "None";
            TurnsToRefresh = 0;
            TurnsUntilRefreshed = 0;
        }

        protected override bool PerformAbility() {
            Game.MessageLog.Add("No ability in that slot");
            return false;
        }
    }
}
