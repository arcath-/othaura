/************************************************************
25 November 2020 - Started refactor using Sadconsole
  
************************************************************/

using System;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using RogueSharp.Random;
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
        
        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int _mapWidth = 128;
        private static readonly int _mapHeight = 60;
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
        public static Player Player { get; set; }
        // 
        public static DungeonMap DungeonMap { get; private set; }

        //
        private static bool _renderRequired = true;
        
        //
        public static CommandSystem CommandSystem { get; private set; }

        // Singleton of IRandom used throughout the game when generating random numbers
        public static IRandom Random { get; private set; }




        //
        public static void Main() {   

            
            CommandSystem = new CommandSystem();

            




            // Setup the engine and create the main window.
            SadConsole.Game.Create(_screenWidth, _screenHeight);


            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Game.OnUpdate = OnRootConsoleUpdate;

            // Start the game.
            SadConsole.Game.Instance.Run();

            //
            // Code here will not run until the game window closes.
            //

            SadConsole.Game.Instance.Dispose();

        }

        //
        private static void Init() {

            // Any startup code for your game. We will use an example console for now 

            // Establish the seed for the random number generator from the current time
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            // The title will appear at the top of the console window 
            // also include the seed used to generate the level
            string consoleTitle = $"RougeSharp V3 Tutorial - Level 1 - Seed {seed}";

            // This must be the exact name of the bitmap font file we are using or it will error.
            string fontFileName = "Assets/terminal8x8.font";
            
            
            // The title will appear at the top of the console window
            SadConsole.Game.Instance.Window.Title = consoleTitle;

            // Loading a new font.
            var fontMaster = SadConsole.Global.LoadFont(fontFileName);
            var normalSizedFont = fontMaster.GetFont(SadConsole.Font.FontSizes.One);



            // width, height, maxRooms, roomMaxSize, roomMinSize 
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight, 20, 13, 7);


            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();






            // main console var
            _rootConsole = new Console(_screenWidth, _screenHeight);

            // Use the font created earlier
            //_rootConsole.Font = normalSizedFont;

            // children console vars
            _statConsole = new Console(_statWidth, _statHeight);
            _statConsole.Position = new Point(128, 0);
            _statConsole.Fill(Colors.TextHeading, ColorAnsi.Blue, 0);
            _statConsole.Print(1, 1, "Stats");

            _mapConsole = new Console(_mapWidth, _mapHeight, normalSizedFont);
            _mapConsole.Position = new Point(0, 8);
            //_mapConsole.Fill(Colors.TextHeading, ColorAnsi.Black, 0);
            //_mapConsole.Print(1, 1, "Map");
            
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

            DungeonMap.Draw(_mapConsole);
            Player.Draw(_mapConsole, DungeonMap);

            // Set console focus
            _rootConsole.IsFocused = true;

            // set the active console
            // ** might need to be the _mapConsole **
            //Global.CurrentScreen.IsFocused = true;
            SadConsole.Global.CurrentScreen = _rootConsole;

        }

        
        //
        private static void OnRootConsoleUpdate(GameTime time) {

            // Called each logic update.

            bool didPlayerAct = false;
            _renderRequired = false;


            // F5 key to make the game full screen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5)) {
                SadConsole.Settings.ToggleFullScreen();
            }

            // Escape to quit
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Escape)) {
                //_rootConsole.Close();
            }


            // Player Movement
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
                didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
            else if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
                didPlayerAct = CommandSystem.MovePlayer(Direction.Down);

            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
                didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
            else if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
                didPlayerAct = CommandSystem.MovePlayer(Direction.Right);

            if (didPlayerAct) {          

                _renderRequired = true;
            }

            // Don't bother redrawing all of the consoles if nothing has changed.
            if (_renderRequired) {

                DungeonMap.Draw(_mapConsole);
                Player.Draw(_mapConsole, DungeonMap);
                
                // Blit the sub consoles to the root console in the correct locations
                //RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _rootConsole, 0, _inventoryHeight);
                //RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight, _rootConsole, 0, _screenHeight - _messageHeight);
                //RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _rootConsole, _mapWidth, 0);
                //RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight, _rootConsole, 0, 0);
                
                // Tell RLNET to draw the console that we set
                // _rootConsole.Draw();

                _renderRequired = false;
            }

        }
        



        //
        //private static void OnRootConsoleRender() {

            
        //}

        
    }


}
