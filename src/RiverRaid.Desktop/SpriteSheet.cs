// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Desktop
{
    public class SpriteSheet
    {
        private readonly string contentName;
        private readonly int borderWidth;
        private Texture2D texture;


        public SpriteSheet(string contentName, int tileWidth, int tileHeight, int tileRows, int tileCols, int borderWidth = 0)
        {
            this.contentName = contentName;
            this.borderWidth = borderWidth;

            TileWidth = tileWidth;
            TileHeight = tileHeight;

            TileRows = tileRows;
            TileCols = tileCols;
        }


        public int TileWidth { get; }
        public int TileHeight { get; }
        public int TileRows { get; set; }
        public int TileCols { get; set; }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(contentName);
        }


        public void Draw(SpriteBatch spriteBatch, int row, int col, float x, float y)
        {
            // TODO - precompute the source rectangles for each sprite?
            // TODO - validate row/col (Debug.Assert or w/e)
            var sourceRect = new Rectangle(borderWidth + col * (TileWidth + borderWidth), borderWidth + row * (TileHeight + borderWidth), TileWidth, TileHeight);

            spriteBatch.Draw(texture, new Vector2(x, y), sourceRect, Color.White);
        }
    }
}
