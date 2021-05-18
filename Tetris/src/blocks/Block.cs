using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.src.blocks {

    public abstract class Block : Drawable {

        private static Random random = new Random();
        private static Dictionary<int, Color> colors = new Dictionary<int, Color>()
        {
            { 0, Color.White },
            { 1, Color.Green },
            { 2, Color.Red },
            { 3, Color.Yellow },
            { 4, Color.Magenta },
            { 5, Color.Blue }
        };
        private Sprite sprite;

        protected int angle = 0;
        protected RenderTexture texture;
        protected readonly RectangleShape[] blocks;

        public Block(Vector2f position) {
            blocks = new RectangleShape[4];
            Color color = colors[random.Next(6)];
            for (int i = 0; i < 4; i++) {
                blocks[i] = new RectangleShape(new Vector2f(20, 20));
                blocks[i].FillColor = color;
            }

            init();
            drawBlocks();
            sprite = new Sprite(texture.Texture);
            sprite.Position = position;
        }

        protected abstract void init();

        protected abstract bool rotateBlocks();

        public void rotate() {
            if (rotateBlocks()) {
                drawBlocks();
            }
        }

        protected void drawBlocks() {
            texture.Clear();
            foreach (var rect in blocks) {
                texture.Draw(rect);
            }
            texture.Display();
        }

        public void destroy() {
            texture.Dispose();
            sprite.Dispose();
        }

        public void move(Vector2f offset) {
            sprite.Position += offset;
        }

        public Vector2f getPosition(int idx) {
            return sprite.Position + blocks[idx].Position;
        }

        public RectangleShape getAt(int idx) {
            return blocks[idx];
        }

        public void Draw(RenderTarget target, RenderStates states) {
            target.Draw(sprite);
        }
    }
}
