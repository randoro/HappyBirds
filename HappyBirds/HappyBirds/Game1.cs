using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HappyBirds
{


    public enum WhoPlaying { Player, GAM }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D slingShotText;
        public static SlingShot slingShot;
        public static List<Bird> flyingbirds;
        public static Level level;
        WhoPlaying whoPlaying = WhoPlaying.Player;


        GeneticAlgorithmManager GAManager;
        Player player;
        //AI ai;

        Agent currentPlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Globals.windowX; 
            graphics.PreferredBackBufferHeight = Globals.windowY;
            this.IsMouseVisible = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            slingShotText = Content.Load<Texture2D>("slingshot");
            Globals.font = Content.Load<SpriteFont>("font");
            flyingbirds = new List<Bird>();
            slingShot = new SlingShot(new Vector2(100, 600));
            level = new Level();
            GAManager = new GeneticAlgorithmManager();
            player = new Player();
            //ai = new AI();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Controller.Update(gameTime);
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();


            if (Controller.KeyPressed(Keys.P))
            {
                GAManager.PrintThings();
            }

            if (Controller.KeyPressed(Keys.Q))
            {
                GAManager.playall = !GAManager.playall;
            }


            if (Controller.KeyPressed(Keys.R))
            {
                if (whoPlaying.Equals(WhoPlaying.Player))
                {
                    flyingbirds.Clear();
                    player.ResetPlayer();
                    level.CreateDefaultLevel();
                }
            }

            if (Controller.KeyPressed(Keys.E))
            {
                flyingbirds.Clear();
                player.ResetPlayer();
                level.CreateDefaultLevel();
                GAManager = new GeneticAlgorithmManager();
                switch (whoPlaying)
                {
                    case WhoPlaying.Player:
                        whoPlaying = WhoPlaying.GAM;
                        break;
                    case WhoPlaying.GAM:
                        whoPlaying = WhoPlaying.Player;
                        break;
                    default:
                        break;
                }
                
            }


            switch (whoPlaying)
            {
                case WhoPlaying.Player:
                    player.Update(gameTime);
                    break;
                case WhoPlaying.GAM:
                    GAManager.Update(gameTime);
                    break;
                default:
                    break;
            }


            slingShot.Update(gameTime);

            for (int i = flyingbirds.Count; i-- > 0; )
            {
                flyingbirds[i].Update(gameTime);
                if (flyingbirds[i].shouldBeRemoved)
                {
                    flyingbirds.Remove(flyingbirds[i]);
                }
            }

            level.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach (Bird b in flyingbirds)
            {
                b.Draw(spriteBatch);
            }
            slingShot.Draw(spriteBatch);
            level.Draw(spriteBatch);


            Vector2 offset = new Vector2(30, 20);
            spriteBatch.DrawString(Globals.font, "GA Current Generation:" + GAManager.currentGeneration, offset, Color.White);
            offset.Y += 20;
            spriteBatch.DrawString(Globals.font, "GA Current Member:" + GAManager.numberOfMembersTried, offset, Color.White);
            offset.Y += 20;
            switch (whoPlaying)
            {
                case WhoPlaying.Player:
                    spriteBatch.DrawString(Globals.font, "Birds Left:" + player.BirdsToThrow, offset, Color.White);
                    offset.Y += 20;
                    break;
                case WhoPlaying.GAM:
                    spriteBatch.DrawString(Globals.font, "Birds Left:" + GAManager.ai.BirdsToThrow, offset, Color.White);
                    offset.Y += 20;
                    break;
                default:
                    break;
            }
            spriteBatch.DrawString(Globals.font, "Current Fitness:" + level.removedBlocks, offset, Color.White);
            offset.Y += 20;
            spriteBatch.DrawString(Globals.font, "Play all:" + GAManager.playall, offset, Color.White);
            offset.Y += 20;


            Vector2 offset2 = new Vector2(800, 20);
            spriteBatch.DrawString(Globals.font, "GA Member : Fitness", offset2, Color.White);
            offset2.Y += 20;
            for (int i = 0; i < GAManager.memberList.Count; i++)
            {
                Color textColor = Color.White;
                if (GAManager.numberOfMembersTried == i)
                {
                    textColor = Color.Red;
                }
                spriteBatch.DrawString(Globals.font, i+ " : " + GAManager.memberList[i].fitness, offset2, textColor);
                offset2.Y += 20;
            }


            Vector2 offset3 = new Vector2(200, 480);
            if (GAManager.currentGeneration > 0)
            {
                spriteBatch.DrawString(Globals.font, "Last generation's(Gen." + (GAManager.currentGeneration - 1) + ") top fitness: " + GAManager.lastGenTopFitness, offset3, Color.White);
                offset3.Y += 20;
                spriteBatch.DrawString(Globals.font, "Last generation's(Gen." + (GAManager.currentGeneration - 1) + ") average fitness: " + GAManager.lastGenAverageFitness, offset3, Color.White);
                offset3.Y += 20;
                spriteBatch.DrawString(Globals.font, "Last generation's(Gen." + (GAManager.currentGeneration - 1) + ") median fitness: " + GAManager.lastGenMedianFitness, offset3, Color.White);
                offset3.Y += 20;
                spriteBatch.DrawString(Globals.font, "Last generation's(Gen." + (GAManager.currentGeneration - 1) + ") lowest fitness: " + GAManager.lastGenLowestFitness, offset3, Color.White);
                offset3.Y += 20;
                offset3.Y += 20;
            }
            else
            {
                offset3.Y += 20;
                offset3.Y += 20;
                offset3.Y += 20;
            }
            spriteBatch.DrawString(Globals.font, "Controls: ", offset3, Color.White);
            offset3.Y += 20;
            spriteBatch.DrawString(Globals.font, "E - Change between PlayerControlled/GAControlled. (This resets GAManager)", offset3, Color.White);
            offset3.Y += 20;
            spriteBatch.DrawString(Globals.font, "R - Restart level. (For players)", offset3, Color.White);
            offset3.Y += 20;
            spriteBatch.DrawString(Globals.font, "Mouse - Used to aim the slingshot. (For players)", offset3, Color.White);
            offset3.Y += 20;
            spriteBatch.DrawString(Globals.font, "P - Prints a lot of stats to Console (Most likely blocks mainthread).", offset3, Color.White);
            offset3.Y += 20;
            spriteBatch.DrawString(Globals.font, "Q - Toggle between GAManager playing all members, or just modified/changed members.", offset3, Color.White);
            offset3.Y += 20;

            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
