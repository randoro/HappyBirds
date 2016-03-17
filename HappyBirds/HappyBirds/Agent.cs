using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    abstract class Agent
    {
        public bool isAiming { get; set; }

        public int BirdsToThrow { get; set; }

        public abstract void Update(GameTime gameTime);
    }
}
