// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Desktop
{
    public class River
    {
        private readonly SpriteSheet spriteSheet;


        public River()
        {
            spriteSheet = new SpriteSheet("terrain", 16, 16, 10, 10, 1);
        }


        public void LoadContent(ContentManager content)
        {
            spriteSheet.LoadContent(content);
        }


        public void Update(GameTime gameTime)
        {
            // TODO - update the river (scroll down)
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO - this is a HACK
            var maxX = Globals.Width / 16;
            var maxY = Globals.Height / 16;

            for (var x = 0; x < maxX; x += 1)
            {
                for (var y = 0; y < maxY; y += 1)
                {
                    var spriteCol = 0;

                    if (x <= 1 || x >= maxX - 2)
                    {
                        spriteCol = 1;
                    }

                    spriteSheet.Draw(spriteBatch, 0, spriteCol, x * 16, y * 16);
                }
            }
        }
    }
}
