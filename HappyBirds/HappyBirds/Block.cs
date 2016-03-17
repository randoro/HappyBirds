using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    class Block
    {
        Vector2 position;
        Rectangle collisionRect;
        public bool shouldBeRemoved { get; private set; }

        public Block(Point gridPos)
        {
            this.position = new Vector2(gridPos.X * Globals.BlockSize, gridPos.Y * Globals.BlockSize);
            collisionRect = new Rectangle((int)position.X, (int)position.Y, Globals.BlockSize, Globals.BlockSize);
        }

        public void Update(GameTime gameTime)
        {
            shouldBeRemoved = ShouldRemove();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.slingShotText, position, new Rectangle(1, 162, Globals.BlockSize, Globals.BlockSize), Color.White, 0f, new Vector2(Globals.BlockSize / 2, Globals.BlockSize / 2), 1f, SpriteEffects.None, 1f);
        }


        private bool ShouldRemove()
        {
            for (int i = Game1.flyingbirds.Count; i-- > 0; )
            {
                if(Game1.flyingbirds[i].collisionRect.Intersects(collisionRect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
