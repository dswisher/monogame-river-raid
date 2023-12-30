// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Desktop.Levels
{
    public class Level
    {
        private readonly SpriteSheet spriteSheet;
        private readonly Cell[,] cells;


        public Level(SpriteSheet spriteSheet, int rows)
        {
            this.spriteSheet = spriteSheet;
            Rows = rows;

            cells = new Cell[Rows, Globals.ScreenColumnWidth];

            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Globals.ScreenColumnWidth; c++)
                {
                    cells[r, c] = new Cell();
                }
            }
        }


        public int Rows { get; }


        public void SetSprite(int row, int col, int spriteRow, int spriteCol)
        {
            cells[row, col].SpriteRow = spriteRow;
            cells[row, col].SpriteCol = spriteCol;
        }


        public void Draw(SpriteBatch spriteBatch, int fromRow, int toRow, float top)
        {
            for (var row = fromRow; row <= toRow; row += 1)
            {
                for (var col = 0; col < Globals.ScreenColumnWidth; col += 1)
                {
                    var cell = cells[row, col];

                    spriteSheet.Draw(spriteBatch, cell.SpriteRow, cell.SpriteCol, col * 16, row * 16 + top);
                }
            }
        }


        private class Cell
        {
            public int SpriteRow { get; set; }
            public int SpriteCol { get; set; }
        }
    }
}
