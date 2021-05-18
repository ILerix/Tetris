using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.src.controller {

    interface Controller {

        void init();

        void draw(RenderTarget target);
    }
}
