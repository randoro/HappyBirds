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
            blockList.Clear();

            //for (int i = 0; i < 2; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        blockList.Add(new Block(new Point(j + 10, i + 5)));
            //    }
            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 2; j++)
            //    {
            //        blockList.Add(new Block(new Point(j + 15, i + 5)));
            //    }
            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 1; j++)
            //    {
            //        blockList.Add(new Block(new Point(j + 2, i + 2)));
            //    }
            //}

            blockList.Add(new Block(new Point(10, 10)));
            blockList.Add(new Block(new Point(11, 10)));
            blockList.Add(new Block(new Point(12, 9)));
            blockList.Add(new Block(new Point(13, 9)));
            blockList.Add(new Block(new Point(14, 8)));
            blockList.Add(new Block(new Point(15, 8)));
            blockList.Add(new Block(new Point(16, 8)));
            blockList.Add(new Block(new Point(17, 8)));
            blockList.Add(new Block(new Point(18, 8)));
            blockList.Add(new Block(new Point(19, 9)));
            blockList.Add(new Block(new Point(20, 9)));
            blockList.Add(new Block(new Point(21, 10)));
            blockList.Add(new Block(new Point(22, 10)));

            blockList.Add(new Block(new Point(10, 15)));
            blockList.Add(new Block(new Point(11, 15)));
            blockList.Add(new Block(new Point(12, 15)));
            blockList.Add(new Block(new Point(13, 15)));
            blockList.Add(new Block(new Point(14, 15)));
            blockList.Add(new Block(new Point(15, 14)));
            blockList.Add(new Block(new Point(16, 14)));
            blockList.Add(new Block(new Point(17, 14)));
            blockList.Add(new Block(new Point(18, 13)));
            blockList.Add(new Block(new Point(19, 13)));
            blockList.Add(new Block(new Point(20, 13)));
            blockList.Add(new Block(new Point(21, 13)));
            blockList.Add(new Block(new Point(22, 13)));

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
