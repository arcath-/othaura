/************************************************************
23 November 2020 - Roguesharp V5 implementation  
  
************************************************************/

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
