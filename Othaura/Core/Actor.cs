/************************************************************
28 November 2020 - Started refactor using Sadconsole
  
************************************************************/

using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using RogueSharp;
using Othaura.Interfaces;

namespace Othaura.Core {

    public class Actor : IActor, Othaura.Interfaces.IDrawable {

        // IActor
        public string Name { get; set; }
        public int Awareness { get; set; }

        // IDrawable
        public Color Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(Console console, IMap map) {
            // Don't draw actors in cells that haven't been explored
            if (!map.GetCell(X, Y).IsExplored) {
                return;
            }

            // Only draw the actor with the color and symbol when they are in field-of-view
            if (map.IsInFov(X, Y)) {
                console.SetGlyph(X, Y, Symbol, Color, Colors.FloorBackgroundFov);
            }
            else {
                // When not in field-of-view just draw a normal floor
                console.SetGlyph(X, Y, '.', Colors.Floor, Colors.FloorBackground);
            }
        }
    }
}
