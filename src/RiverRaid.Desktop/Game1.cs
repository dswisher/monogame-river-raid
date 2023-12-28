// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiverRaid.Desktop
{
    public class Game1 : Game
    {
        private readonly SpriteSheet plane;

        private SpriteBatch spriteBatch;

        // The position of the plane
        private int planeX;
        private int planeY;

        // TODO - remove this, and the ball resource
        private Texture2D ballTexture;


        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Globals.Width;
            graphics.PreferredBackBufferHeight = Globals.Height;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            plane = new SpriteSheet("fighter", 49, 70, 1, 3);

            planeX = Globals.Width / 2 - (plane.TileWidth / 2);
            planeY = Globals.Height - plane.TileHeight * 3 / 2;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            plane.Load(Content);

            ballTexture = Content.Load<Texture2D>("ball");
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && planeX > 0)
            {
                // TODO - need a "speed" variable; also need to take gameTime into account!
                planeX -= 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && planeX < Globals.Width - plane.TileWidth)
            {
                // TODO - need a "speed" variable; also need to take gameTime into account!
                planeX += 2;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(ballTexture, new Vector2(0, 0), Color.White);

            plane.Draw(spriteBatch, 0, 1, planeX, planeY);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
