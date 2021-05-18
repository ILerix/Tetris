using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris.src.system;

namespace Tetris.src.blocks {

    public abstract class Block : Drawable, IDisposable {

        private static Random random;
        private static Dictionary<int, Color> colors;

        static Block() {
            random = new Random();
            colors = new Dictionary<int, Color>() {
                { 0, Color.White },
                { 1, Color.Green },
                { 2, Color.Red },
                { 3, Color.Yellow },
                { 4, Color.Magenta },
                { 5, Color.Blue }
            };
        }

        private Sprite sprite;

        protected int angle = 0;
        protected RenderTexture texture;
        protected readonly RectangleShape[] blocks;

        public Block(Vector2f position) {
            blocks = new RectangleShape[ConstantPool.BLOCK_PARTS];
            Color color = colors[random.Next(colors.Count)];
            for (int i = 0; i < ConstantPool.BLOCK_PARTS; i++) {
                blocks[i] = new RectangleShape(ConstantPool.BLOCK_SIZE);
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

        public void Dispose() {
            texture.Dispose();
            sprite.Dispose();
        }
    }
}
