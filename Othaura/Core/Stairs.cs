/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

using RogueSharp;
using Console = SadConsole.Console;

namespace Othaura.Core {

    public class Stairs {

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsUp { get; set; }

        public void Draw(Console console, IMap map) {
            if (!map.GetCell(X, Y).IsExplored) {
                return;
            }

            if (map.IsInFov(X, Y)) {
                if (IsUp) {
                    console.SetGlyph(X, Y, '<', Colors.Player);
                }
                else {
                    console.SetGlyph(X, Y, '>', Colors.Player);
                }
            }
            else {
                if (IsUp) {
                    console.SetGlyph(X, Y, '<', Colors.Floor);
                }
                else {
                    console.SetGlyph(X, Y, '>', Colors.Floor);
                }
            }
        }
    }
}
