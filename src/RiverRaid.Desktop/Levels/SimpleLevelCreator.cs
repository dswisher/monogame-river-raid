// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Xna.Framework.Content;

namespace RiverRaid.Desktop.Levels
{
    public class SimpleLevelCreator : ILevelCreator
    {
        private readonly SpriteSheet spriteSheet;
        private readonly Random levelSeed = new Random(316);

        public SimpleLevelCreator()
        {
            spriteSheet = new SpriteSheet("terrain", Globals.CellSize, Globals.CellSize, 10, 10, 1);
        }


        public Level Create(Level previousLevel)
        {
            var rows = 50;
            var level = new Level(spriteSheet, rows);
            var chaos = new Random(levelSeed.Next());

            // TODO - tie into the previous level so they align
            var leftWidth = 2;
            var rightWidth = 2;
            for (var r = rows - 1; r >= 0; r--)
            {
                for (var c = 0; c < Globals.ScreenColumnWidth; c++)
                {
                    if (c <= leftWidth)
                    {
                        // TODO - use "left bank" image
                        level.SetSprite(r, c, 0, 1);
                    }
                    else if (c >= Globals.ScreenColumnWidth - rightWidth)
                    {
                        // TODO - use "right bank" image
                        level.SetSprite(r, c, 0, 1);
                    }
                    else
                    {
                        level.SetSprite(r, c, 0, 0);
                    }
                }

                // Move the river banks
                var dx = chaos.Next(-1, 2);
                leftWidth = Math.Clamp(leftWidth + dx, 1, 5);

                dx = chaos.Next(-1, 2);
                rightWidth = Math.Clamp(rightWidth + dx, 1, 5);
            }

            return level;
        }


        public void LoadContent(ContentManager content)
        {
            spriteSheet.LoadContent(content);
        }
    }
}
