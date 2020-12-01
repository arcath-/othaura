/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

using Console = SadConsole.Console;
using RogueSharp;
using Othaura.Interfaces;
using Microsoft.Xna.Framework;

namespace Othaura.Core {

    public class Item : IItem, ITreasure, Interfaces.IDrawable {
        public Item() {
            Symbol = '!';
            Color = Color.Yellow;
        }

        public string Name { get; protected set; }
        public int RemainingUses { get; protected set; }

        public bool Use() {
            return UseItem();
        }

        protected virtual bool UseItem() {
            return false;
        }

        public bool PickUp(IActor actor) {
            Player player = actor as Player;

            if (player != null) {
                if (player.AddItem(this)) {
                    Game.MessageLog.Add($"{actor.Name} picked up {Name}");
                    return true;
                }
            }

            return false;
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
