﻿//v3 complete

using System;
using RLNET;
using Othaura.Behaviors;
using Othaura.Monsters;
using Othaura.Systems;

namespace Othaura.Core {

    public class Monster : Actor {

        //should be nullable if needed.
        public int? TurnsAlerted { get; set; }

        //
        public virtual void PerformAction(CommandSystem commandSystem) {
            var behavior = new StandardMoveAndAttack();
            behavior.Act(this, commandSystem);
        }

        //
        public void DrawStats(RLConsole statConsole, int position) {

            // Start at Y=13 which is below the player stats.
            // Multiply the position by 2 to leave a space between each stat
            int yPosition = 13 + (position * 2);

            // Begin the line by printing the symbol of the monster in the appropriate color
            statConsole.Print(1, yPosition, Symbol.ToString(), Color);

            // Figure out the width of the health bar by dividing current health by max health
            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            // Set the background colors of the health bar to show how damaged the monster is
            statConsole.SetBackColor(3, yPosition, width, 1, Palette.Primary);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Palette.PrimaryDarkest);

            // Print the monsters name over top of the health bar
            statConsole.Print(2, yPosition, $": {Name}", Palette.DbLight);
        }

        public static Monster Clone(Monster anotherMonster) {
            return new Ooze {
                Attack = anotherMonster.Attack,
                AttackChance = anotherMonster.AttackChance,
                Awareness = anotherMonster.Awareness,
                Color = anotherMonster.Color,
                Defense = anotherMonster.Defense,
                DefenseChance = anotherMonster.DefenseChance,
                Gold = anotherMonster.Gold,
                Health = anotherMonster.Health,
                MaxHealth = anotherMonster.MaxHealth,
                Name = anotherMonster.Name,
                Speed = anotherMonster.Speed,
                Symbol = anotherMonster.Symbol
            };
        }
    }
}
