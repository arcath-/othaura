/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

using System.Collections.Generic;
using System.Linq;
//using Microsoft.Xna.Framework.Input;
using SadConsole.Input;
using Console = SadConsole.Console;
using RogueSharp;
using Othaura.Core;
using Othaura.Interfaces;




namespace Othaura.Systems {

    public class TargetingSystem {

        private enum SelectionType {
            None = 0,
            Target = 1,
            Area = 2,
            Line = 3
        }

        public bool IsPlayerTargeting { get; private set; }

        private Point _cursorPosition;
        private List<Point> _selectableTargets = new List<Point>();
        private int _currentTargetIndex;
        private ITargetable _targetable;
        private int _area;
        private SelectionType _selectionType;

        public bool SelectMonster(ITargetable targetable) {
            Initialize();
            _selectionType = SelectionType.Target;
            DungeonMap map = Game.DungeonMap;
            _selectableTargets = map.GetMonsterLocationsInFieldOfView().ToList();
            _targetable = targetable;
            _cursorPosition = _selectableTargets.FirstOrDefault();
            if (_cursorPosition == null) {
                StopTargeting();
                return false;
            }

            IsPlayerTargeting = true;
            return true;
        }

        public bool SelectArea(ITargetable targetable, int area = 0) {
            Initialize();
            _selectionType = SelectionType.Area;
            Player player = Game.Player;
            _cursorPosition = new Point { X = player.X, Y = player.Y };
            _targetable = targetable;
            _area = area;

            IsPlayerTargeting = true;
            return true;
        }

        public bool SelectLine(ITargetable targetable) {
            Initialize();
            _selectionType = SelectionType.Line;
            Player player = Game.Player;
            _cursorPosition = new Point { X = player.X, Y = player.Y };
            _targetable = targetable;

            IsPlayerTargeting = true;
            return true;
        }

        private void StopTargeting() {
            IsPlayerTargeting = false;
            Initialize();
        }

        private void Initialize() {
            _cursorPosition = null;
            _selectableTargets = new List<Point>();
            _currentTargetIndex = 0;
            _area = 0;
            _targetable = null;
            _selectionType = SelectionType.None;
        }

        public bool HandleKey(AsciiKey key) {
            if (_selectionType == SelectionType.Target) {
                HandleSelectableTargeting(key);
            }
            else if (_selectionType == SelectionType.Area) {
                HandleLocationTargeting(key);
            }
            else if (_selectionType == SelectionType.Line) {
                HandleLocationTargeting(key);
            }

            if (Microsoft.Xna.Framework.Input.Keys.Enter == key ) {
                _targetable.SelectTarget(_cursorPosition);
                StopTargeting();
                return true;
            }

            return false;
        }

        private void HandleSelectableTargeting(AsciiKey key) {
            if (Microsoft.Xna.Framework.Input.Keys.Right == key || Microsoft.Xna.Framework.Input.Keys.Down == key) {
                _currentTargetIndex++;
                if (_currentTargetIndex >= _selectableTargets.Count) {
                    _currentTargetIndex = 0;
                }
                _cursorPosition = _selectableTargets[_currentTargetIndex];
            }
            else if (Microsoft.Xna.Framework.Input.Keys.Left == key || Microsoft.Xna.Framework.Input.Keys.Up == key) {
                _currentTargetIndex--;
                if (_currentTargetIndex < 0) {
                    _currentTargetIndex = _selectableTargets.Count - 1;
                }
                _cursorPosition = _selectableTargets[_currentTargetIndex];
            }
        }

        private void HandleLocationTargeting(AsciiKey key) {
            int x = _cursorPosition.X;
            int y = _cursorPosition.Y;
            DungeonMap map = Game.DungeonMap;

            if (Microsoft.Xna.Framework.Input.Keys.Right == key) {
                x++;
            }
            else if (Microsoft.Xna.Framework.Input.Keys.Left == key) {
                x--;
            }
            else if (Microsoft.Xna.Framework.Input.Keys.Up == key) {
                y--;
            }
            else if (Microsoft.Xna.Framework.Input.Keys.Down == key) {
                y++;
            }

            if (map.IsInFov(x, y)) {
                _cursorPosition.X = x;
                _cursorPosition.Y = y;
            }
        }

        public void Draw(Console mapConsole) {
            if (IsPlayerTargeting) {
                DungeonMap map = Game.DungeonMap;
                Player player = Game.Player;
                if (_selectionType == SelectionType.Area) {
                    foreach (Cell cell in map.GetCellsInArea(_cursorPosition.X, _cursorPosition.Y, _area)) {
                        mapConsole.SetBackground(cell.X, cell.Y, Palette.DbSun);                        
                    }
                }
                else if (_selectionType == SelectionType.Line) {
                    foreach (Cell cell in map.GetCellsAlongLine(player.X, player.Y, _cursorPosition.X, _cursorPosition.Y)) {
                        mapConsole.SetBackground(cell.X, cell.Y, Palette.DbSun);
                    }
                }

                mapConsole.SetBackground(_cursorPosition.X, _cursorPosition.Y, Palette.DbLight);
            }
        }
    }
}
