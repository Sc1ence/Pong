using System;
using Pong.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Ball
    {
        private readonly int _height;
        private readonly float _moveSpeed;
        private readonly Vector2 _resetPos;
        private float _currentSpeed;
        private double _direction; //Direction in radians
        private Vector2 _position;
        private Texture2D _texture;

        public Ball(Vector2 position, float direction, int height)
        {
            _position = position;
            _resetPos = position;
            _direction = direction;
            _moveSpeed = 2f;
            _currentSpeed = 2f;
            _height = height;
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, _texture.Width, _texture.Height); }
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("sprites/ball");
        }

        public void Update(GameTime gameTime)
        {
            var newPos = new Vector2(_position.X + _currentSpeed*(float) Math.Cos(_direction),
                                     _position.Y + _currentSpeed*(float) Math.Sin(_direction));

            if (newPos.Y + _texture.Height < _height && newPos.Y > 0)
            {
                _position = newPos;
            }
            else
            {
                while (_direction > 2*Math.PI) _direction -= 2*Math.PI;
                while (_direction < 0) _direction += 2*Math.PI;

                _direction = (float) (2*Math.PI - _direction);
                InGame.WallHit.Play();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public void PaddleCollision(Paddle paddle, bool left)
        {
            int block;
            if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/ 20f) block = 1;
            else if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/10*2) block = 2;
            else if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/10*3) block = 3;
            else if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/10*4) block = 4;
            else if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/10*5) block = 5;
            else if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/10*6) block = 6;
            else if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/10*7) block = 7;
            else if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/10*8) block = 8;
            else if (Position.Y < paddle.Position.Y + paddle.BoundingBox.Height/20*19) block = 9;
            else block = 10;

            if (!left)
            {
                switch (block)
                {
                    case 1:
                        _direction = MathHelper.ToRadians(220);
                        break;
                    case 2:
                        _direction = MathHelper.ToRadians(215);
                        break;
                    case 3:
                        _direction = MathHelper.ToRadians(200);
                        break;
                    case 4:
                        _direction = MathHelper.ToRadians(195);
                        break;
                    case 5:
                        _direction = MathHelper.ToRadians(180);
                        break;
                    case 6:
                        _direction = MathHelper.ToRadians(180);
                        break;
                    case 7:
                        _direction = MathHelper.ToRadians(165);
                        break;
                    case 8:
                        _direction = MathHelper.ToRadians(130);
                        break;
                    case 9:
                        _direction = MathHelper.ToRadians(115);
                        break;
                    case 10:
                        _direction = MathHelper.ToRadians(110);
                        break;
                }
            }
            else
            {
                switch (block)
                {
                    case 1:
                        _direction = MathHelper.ToRadians(290);
                        break;
                    case 2:
                        _direction = MathHelper.ToRadians(295);
                        break;
                    case 3:
                        _direction = MathHelper.ToRadians(310);
                        break;
                    case 4:
                        _direction = MathHelper.ToRadians(345);
                        break;
                    case 5:
                        _direction = MathHelper.ToRadians(0);
                        break;
                    case 6:
                        _direction = MathHelper.ToRadians(0);
                        break;
                    case 7:
                        _direction = MathHelper.ToRadians(15);
                        break;
                    case 8:
                        _direction = MathHelper.ToRadians(50);
                        break;
                    case 9:
                        _direction = MathHelper.ToRadians(65);
                        break;
                    case 10:
                        _direction = MathHelper.ToRadians(70);
                        break;
                }
            }

            IncreaseSpeed();
        }

        public void Reset(bool left)
        {
            _position = _resetPos;
            _currentSpeed = _moveSpeed;
            if (left)
            {
                _direction = Math.PI + 0.1;
            }
            else
            {
                _direction = 0.1;
            }
        }

        private void IncreaseSpeed()
        {
            _currentSpeed += 0.5f;
        }
    }
}