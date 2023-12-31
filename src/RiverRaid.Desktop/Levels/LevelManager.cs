// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Desktop.Levels
{
    public class LevelManager
    {
        private readonly ILevelCreator levelCreator;
        private Level nextLevel;
        private Level currentLevel;

        private float top;


        public LevelManager(ILevelCreator levelCreator)
        {
            this.levelCreator = levelCreator;

            currentLevel = levelCreator.Create(null);
            nextLevel = levelCreator.Create(currentLevel);

            top = Globals.Height - currentLevel.Height;
        }


        /// <summary>
        /// Gets or sets the upward velocity of the plane (downward velocity of the terrain).
        /// </summary>
        public float Velocity { get; set; } = 100f;


        public void Update(GameTime gameTime)
        {
            // TODO - update the river (scroll down), possibly creating a new level
            // TODO - spawn any enemies that are now visible (delegate to levels?)

            top += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (top > currentLevel.Height)
            {
                top -= currentLevel.Height;

                currentLevel = nextLevel;
                nextLevel = levelCreator.Create(currentLevel);
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO - only draw the visible rows

            currentLevel.Draw(spriteBatch, 0, currentLevel.Rows - 1, top);
            nextLevel.Draw(spriteBatch, 0, nextLevel.Rows - 1, top - nextLevel.Height);
        }
    }
}
