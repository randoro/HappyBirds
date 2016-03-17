using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    static class Globals
    {
        public const int windowX = 1280;
        public const int windowY = 720;

        public const int BlockSize = 32;
        public const int BigBirdSize = 32;

        public const float maxPower = 50f;
        public const float powerMultiplier = 0.5f;
        public const float gravity = 0.3f;

        public static SpriteFont font;

        public static Random rand = new Random();


    }
}
