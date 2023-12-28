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
        private Texture2D texture;

        // TODO - save the rows and cols, and use to validate .Draw call parameters
        public SpriteSheet(string contentName, int tileWidth, int tileHeight, int tileRows, int tileCols)
        {
            this.contentName = contentName;

            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }


        public int TileWidth { get; }
        public int TileHeight { get; }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(contentName);
        }

        public void Draw(SpriteBatch spriteBatch, int row, int col, float x, float y)
        {
            // TODO - precompute the source rectangles for each sprite?
            var sourceRect = new Rectangle(col * TileWidth, row * TileHeight, TileWidth, TileHeight);

            spriteBatch.Draw(texture, new Vector2(x, y), sourceRect, Color.White);
        }
    }
}
