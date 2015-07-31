using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGameLibrary;
using TGExtensions;

namespace CollisionTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CollisionTest : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont Consolas14;
        SpriteFont Consolas56;
        
        private InputState _inputState;
        private Rectangle mapBounds;
        private Player[] players;
        private Obstacle[] blocks;
        private Random rand;
        
        public CollisionTest()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            _inputState = new InputState();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            mapBounds = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            rand = new Random(this.GetHashCode());

            players = new Player[] { 
                new Player(this, PlayerIndex.One, Color.Blue)
            };

            blocks = new Obstacle[] {
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15)),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0, rand.Next(5,15))
            };

            foreach (Obstacle block in blocks)
            {
                block.Damage.Generic = 10;
            }

            foreach(Player p in players)
            {
                p.Status.Invunerable = false;
                p.Health.Max = 100;
                p.Health.Current = p.Health.Max;
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Consolas14 = Content.Load<SpriteFont>("Consolas14");
            Consolas56 = Content.Load<SpriteFont>("Consolas56");

            foreach (Player player in players)
            {
                player.LoadContent(this.Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _inputState.Update();

            if (_inputState.IsCancel(PlayerIndex.One))
            {
                Exit();
            }

            foreach (Obstacle b in blocks)
            {
                b.OffsetPosition(0, b.MovementSpeed);
                if (b.Position.Y > mapBounds.Height)
                {
                    float RandX = rand.Next(mapBounds.Width);
                    b.SetPosition(RandX, 0 - b.Footprint.Height);
                }
            }

            foreach (Player p in players)
            {
                p.UpdateMovement(_inputState, p._index);
                bool collideMap = p.CheckBounds(mapBounds);
                int collidedBlocks = p.CheckCollisions(blocks);

                if (collideMap && collidedBlocks != 0 && !p.Status.Invunerable)
                {
                    p.Health.Current = p.Health.Current - blocks[0].Damage.Generic;
                }

                if (p.CurrentState == Player.State.Dead && _inputState.IsSelect(p._index))
                {
                    p.Health.Current = p.Health.Max;
                    p.CurrentState = Player.State.Walking;
                }

                p.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (Obstacle block in blocks)
            {
                block.Draw(spriteBatch, Color.Gray, 1.0F);
            }

            string healthString = "";
            foreach (Player player in players)
            {
                if (player.CurrentState == Player.State.Dead)
                {
                    string gameOverString = "GAME OVER!";
                    // Fits Text in centre of screen
                    Vector2 textSize = Consolas56.MeasureString(gameOverString)/2;
                    Vector2 centerScreen = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
                    Vector2 textPosition = centerScreen - textSize;
                    spriteBatch.DrawString(Consolas56, gameOverString, textPosition, Color.Red, 0.0F, Vector2.Zero, 1.0F, SpriteEffects.None, 1.0F);
                }
                else
                {
                    player.Draw(spriteBatch, player._color);
                    healthString += string.Format("Player {0}: {1:P}", player._index, player.Health.Percent);
                    healthString += System.Environment.NewLine;
                }
            }
            // Fits Text just above bottom of screen and removes final line-break
            float textWidth = Consolas14.MeasureString(healthString).Y;
            textWidth = textWidth - (textWidth / (players.Length + 1));
            spriteBatch.DrawString(Consolas14, healthString, new Vector2(0, graphics.GraphicsDevice.Viewport.Height - textWidth), Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
