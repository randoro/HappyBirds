using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    class AI : Agent
    {
        public bool isDone { set; get; }

        public AI()
        {
            CreateDefaultAI();
        }

        public void CreateDefaultAI()
        {
            isAiming = true;
            BirdsToThrow = 3;
            isDone = false;
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

        public void Shoot(Member gen)
        {
            if (Game1.slingShot.canShoot)
            {
                Vector2 newAngleVect;
                float newPower;

                switch (BirdsToThrow)
                {
                    case 0:
                        isDone = true;
                        break;
                    case 1:
                        newAngleVect = new Vector2(gen.ThirdAngleVectX, gen.ThirdAngleVectY);
                        newPower = gen.ThirdPower;
                        Game1.slingShot.SetVariables(newAngleVect, newPower);
                        Game1.slingShot.ShootNew();
                        break;
                    case 2:
                        newAngleVect = new Vector2(gen.SecondAngleVectX, gen.SecondAngleVectY);
                        newPower = gen.SecondPower;
                        Game1.slingShot.SetVariables(newAngleVect, newPower);
                        Game1.slingShot.ShootNew();
                        break;
                    case 3:
                        newAngleVect = new Vector2(gen.FirstAngleVectX, gen.FirstAngleVectY);
                        newPower = gen.FirstPower;
                        Game1.slingShot.SetVariables(newAngleVect, newPower);
                        Game1.slingShot.ShootNew();
                        break;
                    default:
                        break;
                }

                BirdsToThrow--;
            }
        }
    }
}
