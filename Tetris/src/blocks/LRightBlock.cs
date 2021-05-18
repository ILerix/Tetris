using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.src.blocks {

    class LRightBlock : Block {
        public LRightBlock(Vector2f position) : base(position) {

        }

        protected override void init() {
            texture = new RenderTexture(60, 60);
            blocks[0].Position = new Vector2f(40, 0);
            blocks[1].Position = new Vector2f(40, 20);
            blocks[2].Position = new Vector2f(20, 20);
            blocks[3].Position = new Vector2f(0, 20);
        }

        protected override bool rotateBlocks() {
            angle += 90;
            if (angle >= 360) {
                angle = 0;
            }
            switch (angle) {
                case 0:
                    blocks[0].Position = new Vector2f(40, 0);
                    blocks[1].Position = new Vector2f(40, 20);
                    blocks[2].Position = new Vector2f(20, 20);
                    blocks[3].Position = new Vector2f(0, 20);
                    break;
                case 90:
                    blocks[0].Position = new Vector2f(40, 40);
                    blocks[1].Position = new Vector2f(20, 40);
                    blocks[2].Position = new Vector2f(20, 20);
                    blocks[3].Position = new Vector2f(20, 0);
                    break;
                case 180:
                    blocks[0].Position = new Vector2f(0, 40);
                    blocks[1].Position = new Vector2f(0, 20);
                    blocks[2].Position = new Vector2f(20, 20);
                    blocks[3].Position = new Vector2f(40, 20);
                    break;
                case 270:
                    blocks[0].Position = new Vector2f(0, 0);
                    blocks[1].Position = new Vector2f(20, 0);
                    blocks[2].Position = new Vector2f(20, 20);
                    blocks[3].Position = new Vector2f(20, 40);
                    break;
            }
            return true;
        }
    }
}
