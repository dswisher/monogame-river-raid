// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;
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
            // This must be larger than the screen height
            var rows = 50;

            var level = new Level(spriteSheet, rows);
            var chaos = new Random(levelSeed.Next());

            var leftWidth = 2;
            var rightWidth = 2;
            if (previousLevel != null)
            {
                leftWidth = previousLevel.BridgeLeft;
                rightWidth = Globals.ScreenColumnWidth - previousLevel.BridgeRight;
            }

            for (var r = rows - 1; r >= 0; r--)
            {
                // Move the river banks
                var dx = chaos.Next(-1, 2);
                leftWidth = Math.Clamp(leftWidth + dx, 1, 15);

                dx = chaos.Next(-1, 2);
                rightWidth = Math.Clamp(rightWidth + dx, 1, 15);

                // Set all the sprites
                for (var c = 0; c < Globals.ScreenColumnWidth; c += 1)
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
            }

            // TODO - draw a decent bridge, not just black blocks!
            level.BridgeLeft = leftWidth + 1;
            level.BridgeRight = Globals.ScreenColumnWidth - rightWidth - 1;

            level.SetSprite(0, level.BridgeLeft, 1, 1);
            level.SetSprite(0, level.BridgeRight, 1, 1);

            for (var c = level.BridgeLeft + 1; c < level.BridgeRight; c += 1)
            {
                level.SetSprite(0, c, 1, 0);
            }

            return level;
        }


        public void LoadContent(ContentManager content)
        {
            spriteSheet.LoadContent(content);
        }
    }
}
