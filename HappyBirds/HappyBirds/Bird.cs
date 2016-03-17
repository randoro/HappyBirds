using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    public abstract class Bird
    {
        public Vector2 position { get; set; }
        protected Vector2 velocityVect;
        public bool shouldBeRemoved { get; protected set; }
        public Rectangle collisionRect; 

        public Bird(Vector2 startPosition, Vector2 startAngleVect, float power)
        {
            position = startPosition;
            velocityVect = new Vector2(startAngleVect.X * power * Globals.powerMultiplier, startAngleVect.Y * power * Globals.powerMultiplier);
            shouldBeRemoved = false;

        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
