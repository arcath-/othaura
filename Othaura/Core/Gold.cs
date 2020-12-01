/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

using Console = SadConsole.Console;
using RogueSharp;
using Othaura.Interfaces;
using Microsoft.Xna.Framework;

namespace Othaura.Core {

    public class Gold : ITreasure, Interfaces.IDrawable {
        public int Amount { get; set; }

        public Gold(int amount) {
            Amount = amount;
            Symbol = '$';
            Color = Color.Yellow;
        }

        public bool PickUp(IActor actor) {
            actor.Gold += Amount;
            Game.MessageLog.Add($"{actor.Name} picked up {Amount} gold");
            return true;
        }

        public Color Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(Console console, IMap map) {
            if (!map.IsExplored(X, Y)) {
                return;
            }

            if (map.IsInFov(X, Y)) {
                console.SetGlyph(X, Y, Symbol, Color, Colors.FloorBackgroundFov);
            }
            else {
                console.SetGlyph(X, Y, Symbol, Color, Colors.FloorBackground);
            }
        }
    }
}
