/************************************************************
28 November 2020 - Started refactor using Sadconsole
  
************************************************************/

using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using RogueSharp;
using Othaura.Interfaces;

namespace Othaura.Core {

    public class Actor : IActor, Othaura.Interfaces.IDrawable {

        //Notes in IActor.cs about stats
        private int _attack;
        private int _attackChance;
        private int _awareness;
        private int _defense;
        private int _defenseChance;
        private int _gold;
        private int _health;
        private int _maxHealth;
        private string _name;
        private int _speed;

        //Number of dice to roll when performing an attack. Also max
        //dmg in one attack.
        public int Attack {
            get {
                return _attack;
            }
            set {
                _attack = value;
            }
        }

        //% chance that each dice roll is a success. A success for the 
        //dice means that 1 point of dmg was inflicted. 
        public int AttackChance {
            get {
                return _attackChance;
            }
            set {
                _attackChance = value;
            }
        }

        //FOV range
        public int Awareness {
            get {
                return _awareness;
            }
            set {
                _awareness = value;
            }
        }

        //Number of dice to roll when defending against an attack. Also 
        //represents the maximum amount of damage that can blocked or 
        //dodged from an incoming attack.
        public int Defense {
            get {
                return _defense;
            }
            set {
                _defense = value;
            }
        }

        //Percentage chance that each die rolled is a success. A 
        //success for a die means that 1 point of damage was blocked.
        public int DefenseChance {
            get {
                return _defenseChance;
            }
            set {
                _defenseChance = value;
            }
        }

        //How much money the actor has. Most monsters will drop 
        //gold upon death.
        public int Gold {
            get {
                return _gold;
            }
            set {
                _gold = value;
            }
        }

        //The current health total of the actor. If this value is 0 
        //or less then the actor is killed.
        public int Health {
            get {
                return _health;
            }
            set {
                _health = value;
            }
        }

        //How much health the actor has when fully healed.
        public int MaxHealth {
            get {
                return _maxHealth;
            }
            set {
                _maxHealth = value;
            }
        }

        //Name of actor
        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }

        //How fast the actor is. This determines the rate at which 
        //they perform actions. A lower number is faster. An actor 
        //with a speed of 10 will perform twice as many actions in 
        //the same time as an actor with a speed of 20.
        public int Speed {
            get {
                return _speed;
            }
            set {
                _speed = value;
            }
        }


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
