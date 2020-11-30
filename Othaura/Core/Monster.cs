﻿/************************************************************
30 November 2020 - Started refactor using Sadconsole
  
************************************************************/

using System;
using Console = SadConsole.Console;

using Microsoft.Xna.Framework;

namespace Othaura.Core {

    public class Monster : Actor {

        

        public void DrawStats(Console statConsole, int position) {
            // Start at Y=13 which is below the player stats.
            // Multiply the position by 2 to leave a space between each stat
            int yPosition = 13 + (position * 2);

            // Begin the line by printing the symbol of the monster in the appropriate color
            statConsole.Print(1, yPosition, Symbol.ToString(), Color);

            // Figure out the width of the health bar by dividing current health by max health
            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            // Set the background colors of the health bar to show how damaged the monster is
            // See new function logic imported from RLNET below.
            //(int x, int y, int width, int height, RLColor color)
            SetBackColor(statConsole, 3, yPosition, width, 1, Palette.Primary);
            SetBackColor(statConsole, 3 + width, yPosition, remainingWidth, 1, Palette.PrimaryDarkest);

            ////_statConsole.Fill(Colors.TextHeading, ColorAnsi.Blue, 0);

            // Print the monsters name over top of the health bar
            statConsole.Print(2, yPosition, $": {Name}", Palette.DbLight);
        }

        

        /// <summary>
        /// Sets the background color in the specified rectangle.
        /// </summary>
        /// <param name="x">X position to set.</param>
        /// <param name="y">Y position to set.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <param name="color">Color to set.</param>
        public void SetBackColor(Console statConsole, int x, int y, int width, int height, Color color) {
            if (width > 0 && height > 0) {
                for (int iy = y; iy < height + y; iy++)
                    for (int ix = x; ix < width + x; ix++) {
                        statConsole.SetBackground(ix, iy, color);
                    }
            }
        }
    }
}
