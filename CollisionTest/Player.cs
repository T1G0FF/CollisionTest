using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGameLibrary;

namespace CollisionTest
{
    public class Player : MoveableSprite
    {
        // Asset information
        const int NO_OF_DIRECTIONS = 1;
        
        // In-game Information
        const int DEFAULT_SPEED = 400;
        const int WIDTH = 32;
        const int HEIGHT = WIDTH;
        const float SCALE = 2.0F;
        
        public Color _color = Color.Black;
        public PlayerIndex _index;

        #region Constructor
        public Player(Game game, PlayerIndex playerIndex, Color color, Vector2? startPosition = null, Rectangle? footprint = null, float? movementSpeed = DEFAULT_SPEED, Face? facing = Face.Down, float? scale = SCALE)
            : base(game, NO_OF_DIRECTIONS, new Vector2(((game.GraphicsDevice.Viewport.Width / 2) - WIDTH / 2), game.GraphicsDevice.Viewport.Height - HEIGHT), new Rectangle(0, 0, WIDTH, HEIGHT), movementSpeed, facing, scale)
        {
            _index = playerIndex;
            _color = color;
        }
        #endregion
        public void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager, "Ship");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DummyTexture, new Vector2(Geometry.X, Geometry.Y), _color);
        }

        public override void Update(GameTime gameTime)
        {
            if(this.Health.Current <= 0 )
            {
                CurrentState = State.Dead;
            }
            base.Update(gameTime);
        }
    }
}