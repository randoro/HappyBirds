﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    class Player : Agent
    {
        public Player()
        {
            ResetPlayer();

        }

        public void ResetPlayer()
        {
            isAiming = true;
            BirdsToThrow = 3;
        }

        public override void Update(GameTime gameTime)
        {
            if (isAiming)
            {
                getPlayerInput(Game1.slingShot);

                if (Controller.LeftClick && BirdsToThrow > 0)
                {
                    if (Game1.slingShot.ShootNew())
                    {
                        BirdsToThrow--;
                    }
                }
            }
        }

        public void getPlayerInput(SlingShot slingShot)
        {
            Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 powerVector = slingShot.position - mousePos;
            float newPower = Math.Min(powerVector.Length(), Globals.maxPower);

            Vector2 newAngleVect = new Vector2(powerVector.X, powerVector.Y);
            newAngleVect.Normalize();


            slingShot.SetVariables(newAngleVect, newPower);


        }

    }
}
