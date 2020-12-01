/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

using Microsoft.Xna.Framework;
using RogueSharp;
using Console = SadConsole.Console;
using Othaura.Interfaces;

namespace Othaura.Core {

    public class Stairs : Interfaces.IDrawable {
        public Color Color {
            get; set;
        }
        public char Symbol {
            get; set;
        }
        public int X {
            get; set;
        }
        public int Y {
            get; set;
        }
        public bool IsUp {
            get; set;
        }

        public void Draw(Console console, IMap map) {
            if (!map.GetCell(X, Y).IsExplored) {
                return;
            }

            Symbol = IsUp ? '<' : '>';

            if (map.IsInFov(X, Y)) {
                Color = Colors.Player;
            }
            else {
                Color = Colors.Floor;
            }
                        
            console.SetGlyph(X, Y, Symbol, Color);
        }
    }
}
