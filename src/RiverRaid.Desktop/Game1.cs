// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RiverRaid.Desktop.Levels;

namespace RiverRaid.Desktop
{
    public class Game1 : Game
    {
        private readonly Plane plane;
        private readonly LevelManager levelManager;
        private readonly ExplosionManager explosionManager;

        private readonly ILevelCreator levelCreator;

        private SpriteBatch spriteBatch;
        private bool crashed;


        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Globals.Width;
            graphics.PreferredBackBufferHeight = Globals.Height;

            levelCreator = new SimpleLevelCreator();
            levelManager = new LevelManager(levelCreator);

            plane = new Plane();
            plane.AddVelocityCallback(x => levelManager.Velocity = x);

            explosionManager = new ExplosionManager();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            levelCreator.LoadContent(Content);
            plane.LoadContent(Content);
            explosionManager.LoadContent(Content);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            levelManager.Update(gameTime);
            plane.Update(gameTime);
            explosionManager.Update(gameTime);

            // TODO - replace this with actual collision logic!
            if (Keyboard.GetState().IsKeyDown(Keys.C) && !crashed)
            {
                Crash(gameTime);
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO - move spriteBatch to Globals
            spriteBatch.Begin();

            levelManager.Draw(spriteBatch);
            plane.Draw(spriteBatch);
            explosionManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        private void Crash(GameTime gameTime)
        {
            // Stop the movement and remember that we have crashed
            levelManager.Velocity = 0;
            crashed = true;

            // Add some explosions
            var pos = plane.Center;
            var chaos = new Random();

            foreach (var num in Enumerable.Range(1, 5))
            {
                // TODO - right now, the images are coupled with the sound; split that apart, and have one big sound (with multiple small images)
                //     -> OR have one long animation that shows the plane exploding
                explosionManager.Add(gameTime, pos + new Vector2(chaos.Next(-10, 10), chaos.Next(-20, 20)), num * 130);
            }

            // Decrement the number of lives
            // TODO

            // Queue up an event to restart the game/level
            // TODO
        }
    }
}
