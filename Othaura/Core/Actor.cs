//v3 complete

using RogueSharp;
using RLNET;
using Othaura.Interfaces;
using Othaura.Equipment;

namespace Othaura.Core {

    public class Actor : IActor, IDrawable, IScheduleable {

        public Actor() {
            Head = HeadEquipment.None();
            Body = BodyEquipment.None();
            Hand = HandEquipment.None();
            Feet = FeetEquipment.None();
        }

        // IActor
        public HeadEquipment Head { get; set; }
        public BodyEquipment Body { get; set; }
        public HandEquipment Hand { get; set; }
        public FeetEquipment Feet { get; set; }

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
                return _attack + Head.Attack + Body.Attack + Hand.Attack + Feet.Attack;
            }
            set {
                _attack = value;
            }
        }

        //% chance that each dice roll is a success. A success for the 
        //dice means that 1 point of dmg was inflicted. 
        public int AttackChance {
            get {
                return _attackChance + Head.AttackChance + Body.AttackChance + Hand.AttackChance + Feet.AttackChance;
            }
            set {
                _attackChance = value;
            }
        }

        //FOV range
        public int Awareness {
            get {
                return _awareness + Head.Awareness + Body.Awareness + Hand.Awareness + Feet.Awareness;
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
                return _defense + Head.Defense + Body.Defense + Hand.Defense + Feet.Defense;
            }
            set {
                _defense = value;
            }
        }

        //Percentage chance that each die rolled is a success. A 
        //success for a die means that 1 point of damage was blocked.
        public int DefenseChance {
            get {
                return _defenseChance + Head.DefenseChance + Body.DefenseChance + Hand.DefenseChance + Feet.DefenseChance;
            }
            set {
                _defenseChance = value;
            }
        }

        //How much money the actor has. Most monsters will drop 
        //gold upon death.
        public int Gold {
            get {
                return _gold + Head.Gold + Body.Gold + Hand.Gold + Feet.Gold;
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
                return _maxHealth + Head.MaxHealth + Body.MaxHealth + Hand.MaxHealth + Feet.MaxHealth;
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
                return _speed + Head.Speed + Body.Speed + Hand.Speed + Feet.Speed;
            }
            set {
                _speed = value;
            }
        }


        // IDrawable
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, DungeonMap map) {
            // Don't draw actors in cells that haven't been explored
            if (!map.GetCell(X, Y).IsExplored) {
                return;
            }

            // Only draw the actor with the color and symbol when they are in field-of-view
            if (map.IsInFov(X, Y)) {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else {
                // When not in field-of-view just draw a normal floor
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
            }
        }

        // IScheduleable
        public int Time {
            get {
                return Speed;
            }
        }
    }
}
