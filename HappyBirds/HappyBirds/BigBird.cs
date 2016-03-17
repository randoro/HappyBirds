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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.slingShotText, position, new Rectangle(0, 129, 32, 32), Color.White, 0f, new Vector2(16, 16), 1f, SpriteEffects.None, 1f);
        }

    }
}
