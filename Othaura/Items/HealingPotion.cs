//v3 complete

using Othaura.Abilities;
using Othaura.Core;

namespace Othaura.Items {

    public class HealingPotion : Item {
        public HealingPotion() {
            Name = "Healing Potion";
            RemainingUses = 1;
        }

        protected override bool UseItem() {
            int healAmount = 15;
            Game.MessageLog.Add($"{Game.Player.Name} consumes a {Name} and recovers {healAmount} health");

            Heal heal = new Heal(healAmount);

            RemainingUses--;

            return heal.Perform();
        }
    }
}
