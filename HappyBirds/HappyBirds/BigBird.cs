using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    public class BigBird : Bird
    {

        public BigBird(Vector2 startPosition, Vector2 startAngleVect, float power)
            : base(startPosition, startAngleVect, power)
        {
            collisionRect = new Rectangle((int)position.X, (int)position.Y, Globals.BigBirdSize, Globals.BigBirdSize);
        }

        public override void Update(GameTime gameTime)
        {
            //Flag Removal if needed
            if (position.Y > Globals.windowY || position.X < 0 || position.X > Globals.windowX)
            {
                shouldBeRemoved = true;
            }

            //Apply Gravity to Velocity
            velocityVect.Y += Globals.gravity;

            //Update position
            position += velocityVect;
            collisionRect.X = (int)position.X;
            collisionRect.Y = (int)position.Y;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.slingShotText, position, new Rectangle(0, 129, Globals.BigBirdSize, Globals.BigBirdSize), Color.White, 0f, new Vector2(Globals.BigBirdSize / 2, Globals.BigBirdSize / 2), 1f, SpriteEffects.None, 1f);
        }

    }
}
