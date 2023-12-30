// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.Xna.Framework.Content;

namespace RiverRaid.Desktop.Levels
{
    public interface ILevelCreator
    {
        Level Create(Level previousLevel);

        void LoadContent(ContentManager content);
    }
}
