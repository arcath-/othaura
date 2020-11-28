/************************************************************
28 November 2020 - Started refactor using Sadconsole
  
************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;

using Microsoft.Xna.Framework;
using Othaura.Core;
using Console = SadConsole.Console;

namespace Othaura.Interfaces {

    public interface IDrawable {
        Color Color { get; set; }
        char Symbol { get; set; }
        int X { get; set; }
        int Y { get; set; }

        void Draw(Console console, IMap map);
    }
}
