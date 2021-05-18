using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.src.blocks {

    class SquareBlock : Block {

        public SquareBlock(Vector2f position) : base(position) {

        }

        protected override void init() {
            texture = new RenderTexture(40, 40);
            blocks[0].Position = new Vector2f(0, 0);
            blocks[1].Position = new Vector2f(20, 0);
            blocks[2].Position = new Vector2f(20, 20);
            blocks[3].Position = new Vector2f(0, 20);
        }

        protected override bool rotateBlocks() {
            return false;
        }
    }
}
