//v3 complete 

using Othaura.Core;

namespace Othaura.Items {

    public class NoItem : Item {

        public NoItem() {
            Name = "None";
            RemainingUses = 1;
        }

        protected override bool UseItem() {
            return false;
        }
    }
}
