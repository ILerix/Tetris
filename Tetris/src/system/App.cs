using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris.src.controller;

namespace Tetris.src.system {

    class App : RenderWindow {

        public static App instance { get; }
        
        public event EventHandler update;

        private static readonly VideoMode DEFAULT_RESOLUTION = new VideoMode(800, 600);
        private const string TITLE = "Tetris";
        private const uint MAX_FPS = 60;

        private List<Controller> controllers;

        private App() : base(DEFAULT_RESOLUTION, TITLE) {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 4;
            settings.MajorVersion = 4;
            settings.MinorVersion = 5;

            SetFramerateLimit(MAX_FPS);
            Closed += (e, args) => { Close(); };

            controllers = new List<Controller>();
        }

        static App() {
            instance = new App();
        }

        public void register(Controller controller) {
            controllers.Add(controller);
        }

        public void init() {
            foreach (Controller controller in controllers) {
                controller.init();
            }
        }

        public void start() {
            while (IsOpen) {
                DispatchEvents();
                
                Clear();
                foreach (Controller controller in controllers) {
                    controller.draw(this);
                }
                Display();

                update(this, EventArgs.Empty);
            }
        }
    }
}
