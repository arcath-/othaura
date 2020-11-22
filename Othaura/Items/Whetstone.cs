//v3 complete

using RogueSharp.DiceNotation;
using Othaura.Core;
using Othaura.Equipment;

namespace Othaura.Items {

    public class Whetstone : Item {

        public Whetstone() {
            Name = "Whetstone";
            RemainingUses = 5;
        }

        protected override bool UseItem() {
            Player player = Game.Player;

            if (player.Hand == HandEquipment.None()) {
                Game.MessageLog.Add($"{player.Name} is not holding anything they can sharpen");
            }
            else if (player.AttackChance >= 80) {
                Game.MessageLog.Add($"{player.Name} cannot make their {player.Hand.Name} any sharper");
            }
            else {
                Game.MessageLog.Add($"{player.Name} uses a {Name} to sharper their {player.Hand.Name}");
                player.Hand.AttackChance += Dice.Roll("1D3");
                RemainingUses--;
            }

            return true;
        }
    }
}
