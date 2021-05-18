using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris.src.blocks;

namespace Tetris.src.system {

    class BlockFactory {

        private Vector2f xBound;

        private static Random random;
        private static BlockFactory instance;

        static BlockFactory() {
            random = new Random();
        }

        private BlockFactory(Vector2f bound) {
            xBound = bound;
        }

        public static BlockFactory init(Vector2f xBound) {
            if (instance == null) {
                instance = new BlockFactory(xBound);
            }
            return instance;
        }

        public Block generateBlock() {
            Vector2f position = new Vector2f(random.Next((int)xBound.X, (int)xBound.Y), -20);
            position.X = position.X - (position.X % 20);
            switch (random.Next(7)) {
                case 0:
                    return new TBlock(position);
                case 1:
                    return new SquareBlock(position);
                case 2:
                    return new LineBlock(position);
                case 3:
                    return new ZRightBlock(position);
                case 4:
                    return new ZLeftBlock(position);
                case 5:
                    return new LRightBlock(position);
                case 6:
                    return new LLeftBlock(position);
                default:
                    return null;
            }
        }
    }
}
