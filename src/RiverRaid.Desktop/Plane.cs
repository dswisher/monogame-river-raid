// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiverRaid.Desktop
{
    public class Plane
    {
        private const float MaxSpeed = 500f;
        private const float MinSpeed = 100f;

        private readonly SpriteSheet spriteSheet;

        private float speed = (MinSpeed + MaxSpeed) / 2f;

        private float deltaX = 500f;
        private float deltaY = 200f;

        private int spriteCol = 1;
        private Vector2 pos;


        public Plane()
        {
            spriteSheet = new SpriteSheet("fighter", 49, 70, 1, 3);

            pos.X = Globals.Width / 2f - (spriteSheet.TileWidth / 2f);
            pos.Y = Globals.Height - spriteSheet.TileHeight * 3f / 2;
        }


        public void LoadContent(ContentManager content)
        {
            spriteSheet.LoadContent(content);
        }


        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && speed < MaxSpeed)
            {
                speed += deltaY * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // TODO - fire off an event
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && speed > MinSpeed)
            {
                speed -= deltaY * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // TODO - fire off an event
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && pos.X > 0)
            {
                pos.X -= deltaX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spriteCol = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && pos.X < Globals.Width - spriteSheet.TileWidth)
            {
                pos.X += deltaX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spriteCol = 2;
            }
            else
            {
                spriteCol = 1;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteSheet.Draw(spriteBatch, 0, spriteCol, pos.X, pos.Y);
        }
    }
}
