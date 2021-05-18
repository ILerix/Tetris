using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris.src.blocks;
using Tetris.src.system;

namespace Tetris.src.controller {

    class GameController : Controller {

        private static Vector2f BOUND;
        private static BlockFactory factory;
        private static float UPDATE_TIME;

        private Clock clock;
        private Vector2u windowSize;
        private RenderTexture texture;
        private Sprite totalBlocksSprite;
        private RectangleShape frame;

        private Block block;
        private List<List<RectangleShape>> totalBlocks;

        static GameController() {
            BOUND = new Vector2f(300, 500);
            UPDATE_TIME = 0.5f;
            factory = BlockFactory.init(BOUND);
        }

        public GameController() {
            App.instance.update += update;
            App.instance.KeyPressed += keyPressed;
        }

        public void init() {
            windowSize = App.instance.Size;
            block = factory.generateBlock();
            totalBlocks = new List<List<RectangleShape>>((int)windowSize.Y / 20);
            for (int i = 0; i < totalBlocks.Capacity; i++) {
                totalBlocks.Add(new List<RectangleShape>(ConstantPool.ROW_WIDTH));
            }
            frame = new RectangleShape(new Vector2f(300, windowSize.Y));
            frame.Position = new Vector2f(240, 0);
            frame.FillColor = Color.Transparent;
            frame.OutlineColor = Color.White;
            frame.OutlineThickness = 1;

            texture = new RenderTexture(windowSize.X, windowSize.Y);
            texture.Clear(Color.Transparent);
            totalBlocksSprite = new Sprite(texture.Texture);

            clock = new Clock();
        }

        public void draw(RenderTarget target) {
            target.Draw(block);
            target.Draw(totalBlocksSprite);
            target.Draw(frame);
        }

        private void update(object e, EventArgs args) {
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) {
                UPDATE_TIME = 0.1f;
            }

            if (clock.ElapsedTime.AsSeconds() > UPDATE_TIME) {
                UPDATE_TIME = 1f;
                block.move(ConstantPool.BLOCK_OFFSET_DOWN);
                clock.Restart();

                if (isCollide()) {
                    transformToTotalBlocks();

                    int fullRow = getFirstFullRow();
                    if (fullRow != -1) {
                        destroyFullRows(fullRow);
                    }
                    redrawTotalBlocks();

                    block.Dispose();
                    block = factory.generateBlock();
                }
            }
        }

        private bool isCollide() {
            for (int i = 0; i < ConstantPool.BLOCK_PARTS; i++) {
                int row = ((int)windowSize.Y - (int)block.getPosition(i).Y) / 20 - 1;
                if (row == 0) {
                    return true;
                }

                for (int j = 0; j < totalBlocks[row - 1].Count; j++) {
                    if (totalBlocks[row - 1][j].Position.X == block.getPosition(i).X) {
                        return true;
                    }
                }
            }
            return false;
        }

        private void transformToTotalBlocks() {
            for (int j = 0; j < ConstantPool.BLOCK_PARTS; j++) {
                int row = (int)(windowSize.Y - block.getPosition(j).Y) / (int)ConstantPool.BLOCK_SIZE.X - 1;
                block.getAt(j).Position = block.getPosition(j);
                totalBlocks[row].Add(block.getAt(j));
            }
        }

        private int getFirstFullRow() {
            for (int i = 0; i < totalBlocks.Count; i++) {
                if (totalBlocks[i].Count == ConstantPool.ROW_WIDTH) {
                    return i;
                }
            }
            return -1;
        }

        private void destroyFullRows(int firstFullRow) {
            int deletedRows = 0;
            while (totalBlocks[firstFullRow].Count == ConstantPool.ROW_WIDTH) {
                for (int i = 0; i < totalBlocks[firstFullRow].Count; i++) {
                    totalBlocks[firstFullRow][i].Dispose();
                }
                totalBlocks[firstFullRow].Clear();

                deletedRows++;
                for (int i = 0; i < totalBlocks[firstFullRow + deletedRows].Count; i++) {
                    totalBlocks[firstFullRow + deletedRows][i].Position += ConstantPool.BLOCK_OFFSET_DOWN * deletedRows;
                }
                totalBlocks[firstFullRow].AddRange(totalBlocks[firstFullRow + deletedRows]);
                totalBlocks[firstFullRow + deletedRows].Clear();
            }
            for (int i = firstFullRow + deletedRows + 1; i < totalBlocks.Count; i++) {
                totalBlocks[i - deletedRows].AddRange(totalBlocks[i]);
                totalBlocks[i].Clear();

                for (int j = 0; j < totalBlocks[i - deletedRows].Count; j++) {
                    totalBlocks[i - deletedRows][j].Position += ConstantPool.BLOCK_OFFSET_DOWN * deletedRows;
                }
            }
        }

        private void redrawTotalBlocks() {
            texture.Clear(Color.Transparent);
            foreach (List<RectangleShape> row in totalBlocks) {
                foreach (RectangleShape rect in row) {
                    texture.Draw(rect);
                }
            }
            texture.Display();
        }

        private void keyPressed(Object e, KeyEventArgs args) {
            switch (args.Code) {
                case Keyboard.Key.A:
                    block.move(ConstantPool.BLOCK_OFFSET_LEFT);
                    return;
                case Keyboard.Key.D:
                    block.move(ConstantPool.BLOCK_OFFSET_RIGHT);
                    return;
                case Keyboard.Key.Space:
                    block.rotate();
                    return;
                default:
                    return;
            }
        }

    }
}
