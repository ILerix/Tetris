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

        private static Vector2f OFFSET_DOWN;
        private static Vector2f OFFSET_LEFT;
        private static Vector2f OFFSET_RIGHT;
        private static Vector2f BOUND;
        private static BlockFactory factory;
        private static float UPDATE_TIME;
        private static int ROW_WIDTH;

        private Clock clock;
        private Vector2u windowSize;
        private RenderTexture texture;
        private Sprite totalBlocksSprite;
        private RectangleShape frame;

        private bool onGround;
        private Block block;
        private List<List<RectangleShape>> totalBlocks;

        static GameController() {
            OFFSET_DOWN = new Vector2f(0, 20);
            OFFSET_LEFT = new Vector2f(-20, 0);
            OFFSET_RIGHT = new Vector2f(20, 0);
            BOUND = new Vector2f(300, 500);
            UPDATE_TIME = 1f;
            ROW_WIDTH = 15;
            factory = BlockFactory.init(BOUND);
        }

        public GameController() {

        }

        public void init() {
            App.instance.update += update;
            App.instance.KeyPressed += keyPressed;

            windowSize = App.instance.Size;
            block = factory.generateBlock();
            totalBlocks = new List<List<RectangleShape>>((int)windowSize.Y / 20);
            for (int i = 0; i < totalBlocks.Capacity; i++) {
                totalBlocks.Add(new List<RectangleShape>(ROW_WIDTH));
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
                block.move(OFFSET_DOWN);
                clock.Restart();

                for (int i = 0; i < ConstantPool.BLOCK_PARTS; i++) {
                    if ((block.getPosition(i).Y <= windowSize.Y - 20) && isCollide()) {
                        onGround = true;
                        break;
                    }
                }
                if (onGround) {
                    for (int i = 0; i < ConstantPool.BLOCK_PARTS; i++) {
                        int row = (int)(windowSize.Y - block.getPosition(i).Y) / 20 - 1;
                        block.getAt(i).Position = block.getPosition(i);
                        totalBlocks[row].Add(block.getAt(i));
                    }

                    int fullRow = -1;
                    int deletedRows = 0;
                    for (int i = 0; i < totalBlocks.Count; i++) {
                        if (totalBlocks[i].Count == 15) {
                            fullRow = i;
                            break;
                        }
                    }
                    while (fullRow != -1 && totalBlocks[fullRow].Count == 15) {
                        for (int i = 0; i < totalBlocks[fullRow].Count; i++) {
                            totalBlocks[fullRow][i].Dispose();
                        }
                        totalBlocks[fullRow].Clear();

                        deletedRows++;
                        for (int i = 0; i < totalBlocks[fullRow + deletedRows].Count; i++) {
                            totalBlocks[fullRow + deletedRows][i].Position += OFFSET_DOWN * deletedRows;
                        }
                        totalBlocks[fullRow].AddRange(totalBlocks[fullRow + deletedRows]);
                        totalBlocks[fullRow + deletedRows].Clear();
                    }
                    if (fullRow != -1) {
                        for (int i = fullRow + deletedRows + 1; i < totalBlocks.Count; i++) {
                            totalBlocks[i - deletedRows].AddRange(totalBlocks[i]);
                            totalBlocks[i].Clear();

                            for (int j = 0; j < totalBlocks[i - deletedRows].Count; j++) {
                                totalBlocks[i - deletedRows][j].Position += OFFSET_DOWN * deletedRows;
                            }
                        }
                    }

                    texture.Clear(Color.Transparent);
                    foreach (List<RectangleShape> row in totalBlocks) {
                        foreach (RectangleShape rect in row) {
                            texture.Draw(rect);
                        }
                    }
                    texture.Display();

                    block.destroy();
                    block = factory.generateBlock();
                    onGround = false;
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

        private void keyPressed(Object e, KeyEventArgs args) {
            switch (args.Code) {
                case Keyboard.Key.A:
                    block.move(OFFSET_LEFT);
                    return;
                case Keyboard.Key.D:
                    block.move(OFFSET_RIGHT);
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
