/************************************************************
27 November 2020 - Started refactor using Sadconsole
  
************************************************************/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Console = SadConsole.Console;
using RogueSharp;




namespace Othaura.Core {

    // Our custom DungeonMap class extends the base RogueSharp Map class
    public class DungeonMap : Map {

        // The Draw method will be called each time the map is updated
        // It will render all of the symbols/colors for each cell to the map sub console
        public void Draw(Console mapConsole) {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells()) {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
        }

        private void SetConsoleSymbolForCell(Console console, Cell cell) {
            // When we haven't explored a cell yet, we don't want to draw anything
            if (!cell.IsExplored) {
                return;
            }

            // When a cell is currently in the field-of-view it should be drawn with ligher colors
            if (IsInFov(cell.X, cell.Y)) {

                // Choose the symbol to draw based on if the cell is walkable or not
                // '.' for floor and '#' for walls
                if (cell.IsWalkable) {
                    console.SetGlyph(cell.X, cell.Y, '.', Colors.FloorFov, Colors.FloorBackgroundFov);
                }
                else {
                    console.SetGlyph(cell.X, cell.Y, '#', Colors.WallFov, Colors.WallBackgroundFov);
                }
            }

            // When a cell is outside of the field of view draw it with darker colors
            else {
                if (cell.IsWalkable) {
                    console.SetGlyph(cell.X, cell.Y, '.', Colors.Floor, Colors.FloorBackground);
                }
                else {
                    console.SetGlyph(cell.X, cell.Y, '#', Colors.Wall, Colors.WallBackground);
                }
            }
        }
    }
}
