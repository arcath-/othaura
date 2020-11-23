/************************************************************
23 November 2020 - Roguesharp V5 implementation  
  
************************************************************/

using Othaura.Core;
using Othaura.Interfaces;
using Othaura.Systems;

namespace Othaura.Behaviors {

    public class FullyHeal : IBehavior {

        public bool Act(Monster monster, CommandSystem commandSystem) {

            if (monster.Health < monster.MaxHealth) {
                int healthToRecover = monster.MaxHealth - monster.Health;
                monster.Health = monster.MaxHealth;
                Game.MessageLog.Add($"{monster.Name} catches his breath and recovers {healthToRecover} health");
                return true;
            }
            return false;
        }
    }
}
