/************************************************************
25 November 2020 - Started refactor using Sadconsole
  
************************************************************/

using System;
using SadConsole;
//using RogueSharp;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Othaura.Core;
using Othaura.Systems;

namespace Othaura {

    public static class Game {

        // The screen height and width are in number of tiles 
        // 100x70 at 8x8 font (default) for tutorial.
        // My math for sadconsole is slightly different due to font 16x16

        //var mapConsoleWidth = (int)((Global.RenderWidth / Global.FontDefault.Size.X) * 1.0);
        //var mapConsoleHeight = (int)((Global.RenderHeight / Global.FontDefault.Size.Y) * 1.0);

        private static readonly int _screenWidth = 160;
        private static readonly int _screenHeight = 51;
        private static Console _rootConsole;
        //_rootConsole = new Console(_screenWidth, _screenHeight);

        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int _mapWidth = 128;
        private static readonly int _mapHeight = 35;
        private static Console _mapConsole;

        // Below the map console is the message console which displays attack rolls and other information
        private static readonly int _messageWidth = 128;
        private static readonly int _messageHeight = 8;
        private static Console _messageConsole;

        // The stat console is to the right of the map and display player and monster stats
        private static readonly int _statWidth = 32;
        private static readonly int _statHeight = 51;
        private static Console _statConsole;

        // Above the map is the inventory console which shows the players equipment, abilities, and items
        private static readonly int _inventoryWidth = 128;
        private static readonly int _inventoryHeight = 8;
        private static Console _inventoryConsole;

        // 
        public static DungeonMap DungeonMap { get; private set; }




        //
        public static void Main() {


            // This must be the exact name of the bitmap font file we are using or it will error.
            string fontFileName = "Assets/terminal16x16.png";

            // The title will appear at the top of the console window
            string consoleTitle = "Othaura - Level 1";
            //Window.Title = "Othaura - Level 1";

            

            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight);
            DungeonMap = mapGenerator.CreateMap();

            // Set up a handler for RLNET's Update event
            //_rootConsole.Update += OnRootConsoleUpdate;
            //OnRootConsoleUpdate();

            // Set up a handler for RLNET's Render event
            //_rootConsole.Render += OnRootConsoleRender;
            //OnRootConsoleRender();



            // Setup the engine and create the main window.
            SadConsole.Game.Create(_screenWidth, _screenHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();

            // blow away shit
            SadConsole.Game.Instance.Dispose();

        }

        //
        private static void Init() {

            // Any startup code for your game. We will use an example console for now  
            // main console var
            _rootConsole = new Console(_screenWidth, _screenHeight);

            // children console vars
            _statConsole = new Console(_statWidth, _statHeight);
            _statConsole.Position = new Point(128, 0);
            _statConsole.Fill(Colors.TextHeading, ColorAnsi.Blue, 0);
            _statConsole.Print(1, 1, "Stats");

            _mapConsole = new Console(_mapWidth, _mapHeight);
            _mapConsole.Position = new Point(0, 8);
            _mapConsole.Fill(Colors.TextHeading, ColorAnsi.Black, 0);
            _mapConsole.Print(1, 1, "Map");

            _inventoryConsole = new Console(_inventoryWidth, _inventoryHeight);
            _inventoryConsole.Position = new Point(0, 0);
            _inventoryConsole.Fill(Colors.TextHeading, ColorAnsi.Red, 0);
            _inventoryConsole.Print(1, 1, "Inventory");
            
            _messageConsole = new Console(_messageWidth, _messageHeight);
            _messageConsole.Position = new Point(0, 43);
            _messageConsole.Fill(Colors.TextHeading, ColorAnsi.White, 0);
            _messageConsole.Print(1, 1, "Messages");

            // assigning children console to main console
            _rootConsole.Children.Add(_statConsole);
            _rootConsole.Children.Add(_mapConsole);
            _rootConsole.Children.Add(_inventoryConsole);
            _rootConsole.Children.Add(_messageConsole);

            

            // set the active console
            // ** might need to be the _mapConsole **
            //Global.CurrentScreen.IsFocused = true;
            SadConsole.Global.CurrentScreen = _rootConsole;

        }

        //
        private static void OnRootConsoleUpdate(Console _statConsole, Console _mapConsole, Console _inventoryConsole, Console _messageConsole) {            

            // Set background color and text for each console so that we can verify they are in the correct positions
            _statConsole.Fill(Colors.TextHeading, ColorAnsi.Blue, 0);
            _statConsole.Print(1, 1, "Stats");
            _mapConsole.Fill(Colors.TextHeading, ColorAnsi.Black, 0);
            _mapConsole.Print(1, 1, "Map");
            _inventoryConsole.Fill(Colors.TextHeading, ColorAnsi.Red, 0);
            _inventoryConsole.Print(1, 1, "Inventory");
            _messageConsole.Fill(Colors.TextHeading, ColorAnsi.White, 0);
            _messageConsole.Print(1, 1, "Messages");
        }

        //
        private static void OnRootConsoleRender(Console _mapConsole) {

            DungeonMap.Draw(_mapConsole);

            // Blit the sub consoles to the root console in the correct locations
            //Console.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _rootConsole, 0, _inventoryHeight);
            //Console.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _rootConsole, _mapWidth, 0);
           // Console.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight, _rootConsole, 0, _screenHeight - _messageHeight);
            //Console.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight, _rootConsole, 0, 0);

            // Tell RLNET to draw the console that we set
            //_rootConsole.Draw();

        }
    }
}
