using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Collections.Generic;
using Tetris.src.blocks;
using System.Reflection;
using Tetris.src;
using Tetris.src.system;
using Tetris.src.controller;

namespace Tetris {

    class Program {

        static void Main(string[] args) {
            App app = App.instance;
            GameController controller = new GameController();

            app.register(controller);
            app.init();

            app.start();
        }
    }
}
