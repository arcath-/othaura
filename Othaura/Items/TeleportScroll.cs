/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

using RogueSharp;
using Othaura.Core;

namespace Othaura.Items {

    public class TeleportScroll : Item {

        public TeleportScroll() {
            Name = "Teleport Scroll";
            RemainingUses = 1;
        }

        protected override bool UseItem() {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;

            Game.MessageLog.Add($"{player.Name} uses a {Name} and reappears in another place");

            Point point = map.GetRandomLocation();

            map.SetActorPosition(player, point.X, point.Y);

            RemainingUses--;

            return true;
        }
    }
}
