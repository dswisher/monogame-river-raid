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


        public LevelManager(ILevelCreator levelCreator)
        {
            this.levelCreator = levelCreator;

            currentLevel = levelCreator.Create(null);
            nextLevel = levelCreator.Create(currentLevel);
        }


        /// <summary>
        /// Gets or sets the upward velocity of the plane (downward velocity of the terrain).
        /// </summary>
        public float Velocity { get; set; }


        public void Update(GameTime gameTime)
        {
            // TODO - update the river (scroll down), possibly creating a new level
            // TODO - spawn any enemies that are now visible (delegate to levels?)
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO - draw the visible levels
            var fromRow = currentLevel.Rows - Globals.ScreenRowHeight;
            var toRow = currentLevel.Rows - 1;

            currentLevel.Draw(spriteBatch, fromRow, toRow, 0);
        }
    }
}
