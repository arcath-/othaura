/************************************************************
28 November 2020 - Started refactor using Sadconsole
  
************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othaura.Core;
using SadConsole.Input;

using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace Othaura.Systems {

    public class CommandSystem {

        // Return value is true if the player was able to move
        // false when the player couldn't move, such as trying to move into a wall
        public bool MovePlayer(Direction direction) {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch (direction) {
                case Direction.DownLeft: {
                        x = Game.Player.X - 1;
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Direction.Down: {
                        x = Game.Player.X;
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Direction.DownRight: {
                        x = Game.Player.X + 1;
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Direction.Left: {
                        x = Game.Player.X - 1;
                        y = Game.Player.Y;
                        break;
                    }
                case Direction.Right: {
                        x = Game.Player.X + 1;
                        y = Game.Player.Y;
                        break;
                    }
                case Direction.UpLeft: {
                        x = Game.Player.X - 1;
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Direction.Up: {
                        x = Game.Player.X;
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Direction.UpRight: {
                        x = Game.Player.X + 1;
                        y = Game.Player.Y - 1;
                        break;
                    }
                default: {
                        return false;
                    }
            }

            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y)) {
                return true;
            }

            return false;
        }

        
    }
}
