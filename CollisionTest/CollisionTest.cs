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
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0),
                new Obstacle(this, new Rectangle(rand.Next(mapBounds.Width),-rand.Next(mapBounds.Height),rand.Next(25,100),rand.Next(25,100)), 0)
            };

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

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
            
            foreach (Player p in players)
            {
                p.HandleInput(_inputState, p._index, mapBounds, blocks);
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

            foreach (Player player in players)
            {
                player.Draw(spriteBatch, player._color);
            }

            foreach (Obstacle block in blocks)
            {
                block.Draw(spriteBatch, Color.Gray, 1.0F);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
