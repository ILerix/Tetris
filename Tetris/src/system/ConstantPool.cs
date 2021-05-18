using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.src.system {

    class ConstantPool {

        public static Vector2f BLOCK_SIZE = new Vector2f(20, 20);
        public static Vector2f BLOCK_OFFSET_DOWN = new Vector2f(0, 20);
        public static Vector2f BLOCK_OFFSET_LEFT = new Vector2f(-20, 0);
        public static Vector2f BLOCK_OFFSET_RIGHT = new Vector2f(20, 0);

        public static int BLOCK_PARTS = 4;
        public static int ROW_WIDTH = 15;

    }
}
