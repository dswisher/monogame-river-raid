// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiverRaid.Desktop
{
    public class Plane
    {
        private const float MaxVelocity = 500f;
        private const float MinVelocity = 150f;

        private readonly SpriteSheet spriteSheet;
        private readonly List<Action<float>> velocityChangedCallbacks = new ();

        private float velocity = (MinVelocity + MaxVelocity) / 2f;

        private float deltaX = 300f;
        private float deltaY = 400f;

        private int spriteCol = 1;
        private Vector2 upperLeftCorner;


        public Plane()
        {
            spriteSheet = new SpriteSheet("fighter", 49, 70, 1, 3);

            upperLeftCorner.X = Globals.Width / 2f - (spriteSheet.TileWidth / 2f);
            upperLeftCorner.Y = Globals.Height - spriteSheet.TileHeight * 3f / 2;
        }


        public Vector2 Center => new(upperLeftCorner.X + spriteSheet.TileWidth / 2f, upperLeftCorner.Y + spriteSheet.TileHeight / 2f);


        public void AddVelocityCallback(Action<float> callback)
        {
            velocityChangedCallbacks.Add(callback);
        }


        public void LoadContent(ContentManager content)
        {
            spriteSheet.LoadContent(content);
        }


        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && velocity < MaxVelocity)
            {
                velocity += deltaY * (float)gameTime.ElapsedGameTime.TotalSeconds;

                OnVelocityChanged();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && velocity > MinVelocity)
            {
                velocity -= deltaY * (float)gameTime.ElapsedGameTime.TotalSeconds;

                OnVelocityChanged();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && upperLeftCorner.X > 0)
            {
                upperLeftCorner.X -= deltaX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spriteCol = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && upperLeftCorner.X < Globals.Width - spriteSheet.TileWidth)
            {
                upperLeftCorner.X += deltaX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spriteCol = 2;
            }
            else
            {
                spriteCol = 1;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteSheet.Draw(spriteBatch, 0, spriteCol, upperLeftCorner.X, upperLeftCorner.Y);
        }


        private void OnVelocityChanged()
        {
            foreach (var cb in velocityChangedCallbacks)
            {
                cb(velocity);
            }
        }
    }
}
