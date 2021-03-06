﻿/************************************************************
23 November 2020 - Roguesharp V5 implementation  
  
************************************************************/

using System.Collections.Generic;
using System.Linq;
using RLNET;
using Othaura.Core;

namespace Othaura.Systems {

    // Represents a queue of messages that can be added to
    // a method for and drawing to an RLConsole
    public class MessageLog {
               
        // Use a Queue to keep track of the lines of text
        // The first line added to the log will also be the first removed
        private readonly Queue<string> _lines;

        public MessageLog() {
            _lines = new Queue<string>();            
        }

        // Add a line to the MessageLog queue
        public void Add(string message) {
            _lines.Enqueue(message);

            // When exceeding the maximum number of lines remove the oldest one.
            if (_lines.Count > 9) {
                _lines.Dequeue();
            }
        }

        // Draw each line of the MessageLog queue to the console
        public void Draw(RLConsole console) {

            console.Clear();

            string[] lines = _lines.ToArray();
            for (int i = 0; i < lines.Count(); i++) {
                console.Print(1, i + 1, lines[i], Colors.Text);
            }
        }                
    }
}
