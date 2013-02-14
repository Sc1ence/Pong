using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Paddle
    {
        private readonly bool left;
        protected int _height;
        protected float _movespeed;
        protected Texture2D _texture;

        public Paddle(Vector2 pos, int height, bool l)
        {
            Position = pos;
            _movespeed = 0.3f;
            _height = height;
            left = l;
        }

        public Vector2 Position { get; protected set; }

        public int Score { get; private set; }

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, _texture.Width, _texture.Height); }
        }

        public virtual void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("sprites/paddle");
        }

        public virtual void Update(KeyboardState state, GameTime gameTime)
        {
            if (left)
            {
                if (state.IsKeyDown(Keys.W))
                {
                    MoveUp(gameTime);
                }
                else if (state.IsKeyDown(Keys.S))
                {
                    MoveDown(gameTime);
                }
            }
            else
            {
                if (state.IsKeyDown(Keys.Up))
                {
                    MoveUp(gameTime);
                }
                else if (state.IsKeyDown(Keys.Down))
                {
                    MoveDown(gameTime);
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }


        public void IncreaseScore()
        {
            Score++;
        }

        public virtual void MoveUp(GameTime gameTime)
        {
            var newPos = new Vector2(Position.X,
                                     (float) (Position.Y - _movespeed*gameTime.ElapsedGameTime.TotalMilliseconds));

            if (newPos.Y > 0)
            {
                Position = newPos;
            }
        }

        public virtual void MoveDown(GameTime gameTime)
        {
            var newPos = new Vector2(Position.X,
                                     (float) (Position.Y + _movespeed*gameTime.ElapsedGameTime.TotalMilliseconds));

            if (newPos.Y + _texture.Height < _height)
            {
                Position = newPos;
            }
        }
    }
}