using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    public class Level
    {
        List<Block> blockList;
        public int removedBlocks { get; private set; }

        public Level()
        {
            blockList = new List<Block>();
            CreateDefaultLevel();

        }

        public void CreateDefaultLevel()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    blockList.Add(new Block(new Point(j + 10, i + 5)));
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    blockList.Add(new Block(new Point(j + 15, i + 5)));
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    blockList.Add(new Block(new Point(j + 2, i + 2)));
                }
            }


            removedBlocks = 0;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = blockList.Count; i-- > 0; )
            {
                blockList[i].Update(gameTime);
                if (blockList[i].shouldBeRemoved)
                {
                    blockList.Remove(blockList[i]);
                    removedBlocks++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = blockList.Count; i-- > 0; )
            {
                blockList[i].Draw(spriteBatch);
            }
        }
        
    }
}
