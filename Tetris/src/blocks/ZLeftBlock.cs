using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.src.blocks {


    class ZLeftBlock : Block {

        public ZLeftBlock(Vector2f position) : base(position) {

        }

        protected override void init() {
            texture = new RenderTexture(60, 60);
            blocks[0].Position = new Vector2f(0, 20);
            blocks[1].Position = new Vector2f(20, 20);
            blocks[2].Position = new Vector2f(20, 40);
            blocks[3].Position = new Vector2f(40, 40);
        }

        protected override bool rotateBlocks() {
            angle = angle == 0 ? 90 : 0;
            switch (angle) {
                case 0:
                    blocks[0].Position = new Vector2f(0, 20);
                    blocks[1].Position = new Vector2f(20, 20);
                    blocks[2].Position = new Vector2f(20, 40);
                    blocks[3].Position = new Vector2f(40, 40);
                    break;
                case 90:
                    blocks[0].Position = new Vector2f(20, 0);
                    blocks[1].Position = new Vector2f(20, 20);
                    blocks[2].Position = new Vector2f(0, 20);
                    blocks[3].Position = new Vector2f(0, 40);
                    break;
            }
            return true;
        }
    }
}
