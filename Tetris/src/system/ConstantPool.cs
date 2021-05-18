using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.src.system {

    class ConstantPool {

        public static readonly Vector2f BLOCK_SIZE = new Vector2f(20, 20);
        public static readonly Vector2f BLOCK_OFFSET_DOWN = new Vector2f(0, 20);
        public static readonly Vector2f BLOCK_OFFSET_LEFT = new Vector2f(-20, 0);
        public static readonly Vector2f BLOCK_OFFSET_RIGHT = new Vector2f(20, 0);

        public static readonly int BLOCK_PARTS = 4;
        public static readonly int ROW_WIDTH = 15;

    }
}
