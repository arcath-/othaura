//V3 Complete

using System;
using System.Collections.Generic;
using System.Linq;
using RogueSharp;
using Othaura.Core;
using RogueSharp.DiceNotation;


namespace Othaura.Systems {

    public class MapGenerator {

        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;
        private readonly int _level;

        private readonly DungeonMap _map;

        private readonly EquipmentGenerator _equipmentGenerator;

        // Constructing a new MapGenerator requires the dimensions of the maps it will create
        // as well as the sizes and maximum number of rooms
        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, int level) {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _level = level;
            _map = new DungeonMap();
            _equipmentGenerator = new EquipmentGenerator(level);
        }

        // Generate a new map that places rooms randomly
        public DungeonMap CreateMap() {

            // Set the properties of all cells to false
            _map.Initialize(_width, _height);

            // Try to place as many rooms as the specified maxRooms
            // Note: Only using decrementing loop because of WordPress formatting
            for (int r = 0; r < _maxRooms; r++) {

                // Determine the size and position of the room randomly
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, _height - roomHeight - 1);

                // All of our rooms can be represented as Rectangles
                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);

                // Check to see if the room rectangle intersects with any other rooms
                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                // As long as it doesn't intersect add it to the list of rooms
                if (!newRoomIntersects) {
                    _map.Rooms.Add(newRoom);
                }
            }
            // Iterate through each room that we wanted placed 
            // call CreateRoom to make it
            foreach (Rectangle room in _map.Rooms) {
                CreateRoom(room);
            }

            // Iterate through each room that was generated. Don't do anything with the first room though.
            for (int r = 0; r < _map.Rooms.Count; r++) {

                if (r == 0) {
                    continue;
                }

                // For all remaing rooms get the center of the room and the previous room
                int previousRoomCenterX = _map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = _map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = _map.Rooms[r].Center.X;
                int currentRoomCenterY = _map.Rooms[r].Center.Y;

                // Give a 50/50 chance of which 'L' shaped connecting hallway to tunnel out
                if (Game.Random.Next(0, 2) == 1) {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                }
                else {
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }

            // Iterate through each room that we wanted placed and create doors for it.
            foreach (Rectangle room in _map.Rooms) {
                CreateDoors(room);
            }

            // Needs to be before PlacePlayer()
            CreateStairs();

            // Places player before the map is returned.
            PlacePlayer();

            // After the existing PlacePlayer() call, add another call to PlaceMonsters()
            PlaceMonsters();

            PlaceEquipment();

            PlaceItems();

            PlaceAbility();

            return _map;
        }

        // Given a rectangular area on the map
        // set the cell properties for that area to true
        private void CreateRoom(Rectangle room) {
            for (int x = room.Left + 1; x < room.Right; x++) {
                for (int y = room.Top + 1; y < room.Bottom; y++) {
                    _map.SetCellProperties(x, y, true, true);
                }
            }
        }

        // Carve a tunnel out of the map parallel to the x-axis
        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition) {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++) {
                _map.SetCellProperties(x, yPosition, true, true);
            }
        }

        // Carve a tunnel out of the map parallel to the y-axis
        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition) {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++) {
                _map.SetCellProperties(xPosition, y, true, true);
            }
        }        

        // Method to place monsters on the map.
        private void PlaceMonsters() {

            foreach (var room in _map.Rooms) {

                // Each room has a 60% chance of having monsters
                if (Dice.Roll("1D10") < 7) {

                    // Generate between 1 and 4 monsters
                    var numberOfMonsters = Dice.Roll("1D4");

                    for (int i = 0; i < numberOfMonsters; i++) {

                        if (_map.DoesRoomHaveWalkableSpace(room)) {

                            // Find a random walkable location in the room to place the monster
                            Point randomRoomLocation = _map.GetRandomLocationInRoom(room);

                            // It's possible that the room doesn't have space to place a monster
                            // In that case skip creating the monster
                            if (randomRoomLocation != null) {

                                _map.AddMonster(ActorGenerator.CreateMonster(_level, _map.GetRandomLocationInRoom(room)));
                            }
                        }
                    }
                }
            }
        }

        //
        private void PlaceEquipment() {

            foreach (var room in _map.Rooms) {

                if (Dice.Roll("1D10") < 3) {

                    if (_map.DoesRoomHaveWalkableSpace(room)) {

                        Point randomRoomLocation = _map.GetRandomLocationInRoom(room);

                        if (randomRoomLocation != null) {

                            Core.Equipment equipment;
                            try {
                                equipment = _equipmentGenerator.CreateEquipment();
                            }
                            catch (InvalidOperationException) {
                                // no more equipment to generate so just quit adding to this level
                                return;
                            }

                            Point location = _map.GetRandomLocationInRoom(room);
                            _map.AddTreasure(location.X, location.Y, equipment);
                        }
                    }
                }
            }
        }

        //
        private void PlaceItems() {

            foreach (var room in _map.Rooms) {

                if (Dice.Roll("1D10") < 3) {

                    if (_map.DoesRoomHaveWalkableSpace(room)) {

                        Point randomRoomLocation = _map.GetRandomLocationInRoom(room);

                        if (randomRoomLocation != null) {
                            Item item = ItemGenerator.CreateItem();
                            Point location = _map.GetRandomLocationInRoom(room);
                            _map.AddTreasure(location.X, location.Y, item);
                        }
                    }
                }
            }
        }

        // Find the center of the first room that we created and place the Player there
        private void PlacePlayer() {
            Player player = ActorGenerator.CreatePlayer();

            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;

            _map.AddPlayer(player);
        }

        //
        private void PlaceAbility() {

            if (_level == 1 || _level % 3 == 0) {

                try {
                    var ability = AbilityGenerator.CreateAbility();
                    int roomIndex = Game.Random.Next(0, _map.Rooms.Count - 1);
                    Point location = _map.GetRandomLocationInRoom(_map.Rooms[roomIndex]);
                    _map.AddTreasure(location.X, location.Y, ability);
                }

                catch (InvalidOperationException) {
                }
            }
        }

        // Method to create doors.
        private void CreateDoors(Rectangle room) {
            // The the boundries of the room
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            // Put the rooms border cells into a list
            List<DungeonCell> borderCells = _map.GetCellsAlongLine(xMin, yMin, xMax, yMin).ToList();
            borderCells.AddRange(_map.GetCellsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange(_map.GetCellsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange(_map.GetCellsAlongLine(xMax, yMin, xMax, yMax));

            // Go through each of the rooms border cells and look for locations to place doors.
            foreach (Cell cell in borderCells) {
                if (IsPotentialDoor(cell)) {
                    // A door must block field-of-view when it is closed.
                    _map.SetCellProperties(cell.X, cell.Y, false, true);
                    _map.Doors.Add(new Door {
                        X = cell.X,
                        Y = cell.Y,
                        IsOpen = false
                    });
                }
            }
        }

        // Checks to see if a cell is a good candidate for placement of a door
        private bool IsPotentialDoor(Cell cell) {

            // If the cell is not walkable
            // then it is a wall and not a good place for a door
            if (!cell.IsWalkable) {
                return false;
            }

            // Store references to all of the neighboring cells 
            Cell right = _map.GetCell(cell.X + 1, cell.Y);
            Cell left = _map.GetCell(cell.X - 1, cell.Y);
            Cell top = _map.GetCell(cell.X, cell.Y - 1);
            Cell bottom = _map.GetCell(cell.X, cell.Y + 1);

            // Make sure there is not already a door here
            if (_map.GetDoor(cell.X, cell.Y) != null ||
                _map.GetDoor(right.X, right.Y) != null ||
                _map.GetDoor(left.X, left.Y) != null ||
                _map.GetDoor(top.X, top.Y) != null ||
                _map.GetDoor(bottom.X, bottom.Y) != null) {
                return false;
            }

            // This is a good place for a door on the left or right side of the room
            if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable) {
                return true;
            }

            // This is a good place for a door on the top or bottom of the room
            if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable) {
                return true;
            }
            return false;
        }

        // Method to create stairs.
        private void CreateStairs() {
            _map.StairsUp = new Stairs {
                X = _map.Rooms.First().Center.X + 1,
                Y = _map.Rooms.First().Center.Y,
                IsUp = true
            };
            _map.StairsDown = new Stairs {
                X = _map.Rooms.Last().Center.X,
                Y = _map.Rooms.Last().Center.Y,
                IsUp = false
            };
        }
    }

}