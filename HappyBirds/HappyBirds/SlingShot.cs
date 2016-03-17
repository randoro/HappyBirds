using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    public class SlingShot
    {
        public Vector2 angleVect { get; set; }
        public float power { get; set; }

        public Vector2 position { get; private set; }
        public bool canShoot { get; private set; }

        private BigBird fakeBird;

        public SlingShot(Vector2 position)
        {
            this.position = position;
            fakeBird = new BigBird(position, Vector2.Zero, 0f);
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.flyingbirds.Count == 0)
            {
                canShoot = true;
            }

            fakeBird.position = position - new Vector2(angleVect.X * power, angleVect.Y * power);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Game1.slingShotText, position, new Rectangle(0, 0, 64, 128), Color.White, 0f, new Vector2(32, 32), 1f, SpriteEffects.None, 1f);
            fakeBird.Draw(spriteBatch);
        }

        public void SetVariables(Vector2 angleVect, float power)
        {
            this.angleVect = angleVect;
            this.power = power;
        }

        public void ShootNew() 
        {
            if (canShoot)
            {
                canShoot = false;
                Game1.flyingbirds.Add(new BigBird(position, angleVect, power));
            }
        }
    }
}
