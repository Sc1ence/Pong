using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class CpuPaddle : Paddle
    {
        private readonly Ball _ball;

        public CpuPaddle(Vector2 pos, int height, ref Ball ball) : base(pos, height, true)
        {
            _ball = ball;

            _movespeed = 0.25f;
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("sprites/paddleCpu");
        }


        public override void Update(KeyboardState state, GameTime gameTime)
        {
            if (_ball.Position.X > 400) return;

            if (Position.Y > _ball.Position.Y - _ball.BoundingBox.Height/2f)
            {
                MoveUp(gameTime);
            }
            else if (Math.Abs(Position.Y - _ball.Position.Y - _ball.BoundingBox.Height/2f) < 32)
            {
            }
            else if (Position.Y < _ball.Position.Y - _ball.BoundingBox.Height/2f)
            {
                MoveDown(gameTime);
            }
        }
    }
}