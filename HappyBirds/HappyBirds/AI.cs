using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    class AI : Agent
    {
        public AI()
        {
            isAiming = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.slingShot.canShoot)
            {
                double randomTopX = (Globals.rand.NextDouble() * 2.0D) - 1.0D;
                double randomTopY = (Globals.rand.NextDouble() * 2.0D) - 1.0D;
                Vector2 newAngleVect = new Vector2((float)randomTopX, (float)randomTopY);
                double randomPower = Globals.rand.NextDouble() * Globals.maxPower;
                float newPower = (float)randomPower;

                Game1.slingShot.SetVariables(newAngleVect, newPower);
                Game1.slingShot.ShootNew();
            }
        }
    }
}
