// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Desktop
{
    public class ExplosionManager
    {
        private readonly SpriteSheet spriteSheet;
        private readonly Vector2 offset = new (16, 16);
        private readonly LinkedList<ExplosionEntry> explosions = new ();
        private readonly TimeSpan frameTime = TimeSpan.FromMilliseconds(10);

        private SoundEffect sound;


        public ExplosionManager()
        {
            spriteSheet = new SpriteSheet("explosion", 32, 32, 1, 9);
        }


        public void LoadContent(ContentManager content)
        {
            spriteSheet.LoadContent(content);

            sound = content.Load<SoundEffect>("explosion1");
        }


        public void Update(GameTime gameTime)
        {
            // TODO - update all the explosions (if any)

            var node = explosions.First;
            while (node != null)
            {
                var entry = node.Value;
                if (gameTime.TotalGameTime > entry.NextTransition)
                {
                    entry.SpriteCol += 1;
                    if (entry.SpriteCol < spriteSheet.TileCols)
                    {
                        if (entry.SpriteCol == 0)
                        {
                            sound.Play();
                        }

                        entry.NextTransition = gameTime.TotalGameTime.Add(frameTime);
                        node = node.Next;
                    }
                    else
                    {
                        var next = node.Next;
                        explosions.Remove(node);
                        node = next;
                    }
                }
                else
                {
                    node = node.Next;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entry in explosions)
            {
                if (entry.SpriteCol >= 0)
                {
                    spriteSheet.Draw(spriteBatch, 0, entry.SpriteCol, entry.UpperLeftCorner.X, entry.UpperLeftCorner.Y);
                }
            }
        }


        public void Add(GameTime gameTime, Vector2 centerPos, int delayMillis = 0)
        {
            // Create the entry...
            var entry = new ExplosionEntry
            {
                NextTransition = gameTime.TotalGameTime.Add(TimeSpan.FromMilliseconds(delayMillis)),
                UpperLeftCorner = centerPos - offset
            };

            // ...and add it to the list.
            explosions.AddLast(entry);
        }


        private class ExplosionEntry
        {
            public TimeSpan NextTransition { get; set; }
            public int SpriteCol { get; set; } = -1;
            public Vector2 UpperLeftCorner { get; set; }
        }
    }
}
