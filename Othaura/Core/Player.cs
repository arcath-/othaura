/************************************************************
28 November 2020 - Started refactor using Sadconsole
  
************************************************************/

namespace Othaura.Core {

    public class Player : Actor {
        public Player() {
            Awareness = 15;
            Name = "Rogue";
            Color = Colors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
        }
    }
}
