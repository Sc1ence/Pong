using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong.GameStates
{
    internal class InGame : GameState
    {
        public static SoundEffect WallHit;
        private readonly Ball _ball;
        private readonly Paddle _leftPaddle;
        private readonly Paddle _rightPaddle;
        private readonly GraphicsDeviceManager graphics;
        private SpriteFont _font;
        private bool _isPaused;
        private Texture2D _pScreen;

        private SoundEffect _paddleHit;
        private Song _song;
        private KeyboardState _newState;
        private KeyboardState _oldState;

        public InGame(Game1 game, bool singleplayer) : base(game)
        {
            graphics = game.graphics;
            _ball =
                new Ball(new Vector2(graphics.PreferredBackBufferWidth/2f, graphics.PreferredBackBufferHeight/2 + 16),
                         0.1f, graphics.PreferredBackBufferHeight);

            if (singleplayer)
            {
                _rightPaddle =
                    new Paddle(
                        new Vector2(graphics.PreferredBackBufferWidth - 44, graphics.PreferredBackBufferHeight/2f),
                        graphics.PreferredBackBufferHeight, false);
                _leftPaddle = new CpuPaddle(new Vector2(22, graphics.PreferredBackBufferHeight/2f),
                                            graphics.PreferredBackBufferHeight, ref _ball);
            }
            else
            {
                _rightPaddle =
                    new Paddle(
                        new Vector2(graphics.PreferredBackBufferWidth - 44, graphics.PreferredBackBufferHeight/2f),
                        graphics.PreferredBackBufferHeight, false);
                _leftPaddle = new Paddle(new Vector2(22, graphics.PreferredBackBufferHeight/2f),
                                         graphics.PreferredBackBufferHeight, true);
            }

            _oldState = _newState = Keyboard.GetState();
        }

        public override void Load(ContentManager content)
        {
            _leftPaddle.LoadContent(content);
            _rightPaddle.LoadContent(content);
            _ball.LoadContent(content);
            _font = content.Load<SpriteFont>("sprites/font");
            _pScreen = content.Load<Texture2D>("sprites/pScreen");
            _paddleHit = content.Load<SoundEffect>("sounds/ping");
            WallHit = content.Load<SoundEffect>("sounds/pong");

            _song = content.Load<Song>("sounds/music");
            MediaPlayer.Play(_song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
        }

        public override void Update(GameTime gameTime)
        {
            _newState = Keyboard.GetState();


            if (_newState.IsKeyDown(Keys.P) && _oldState.IsKeyUp(Keys.P))
            {
                _isPaused = !_isPaused;
                if (_isPaused) MediaPlayer.Pause();
                else MediaPlayer.Resume();
            }

            if (_isPaused)
            {
                _oldState = _newState;
                return;
            }

            if (_leftPaddle.Score >= 10)
            {
                if (_leftPaddle is CpuPaddle)
                {
                    Game.switchState(new GameOver(Game, "CPU Wins!"));
                }
                else
                {
                    Game.switchState(new GameOver(Game, "Player 1 Wins!"));
                }
            }
            else if (_rightPaddle.Score >= 10)
            {
                if (_leftPaddle is CpuPaddle)
                {
                    Game.switchState(new GameOver(Game, "Player Wins!"));
                }
                else
                {
                    Game.switchState(new GameOver(Game, "Player 2 Wins!"));
                }
            }

            if (_newState.IsKeyDown(Keys.Escape))
            {
                Game.switchState(new Menu(Game));
                return;
            }

            _leftPaddle.Update(_newState, gameTime);
            _rightPaddle.Update(_newState, gameTime);

            if (_ball.BoundingBox.Intersects(_leftPaddle.BoundingBox) && _ball.Position.X >= _leftPaddle.Position.X)
            {
                _paddleHit.Play();
                _ball.PaddleCollision(_leftPaddle, true);
            }
            else if (_ball.BoundingBox.Intersects(_rightPaddle.BoundingBox) &&
                     _ball.Position.X <= _rightPaddle.Position.X)
            {
                _paddleHit.Play();
                _ball.PaddleCollision(_rightPaddle, false);
            }

            _ball.Update(gameTime);

            if (_ball.Position.X < 0)
            {
                _rightPaddle.IncreaseScore();
                _ball.Reset(false);
            }
            else if (_ball.Position.X > graphics.PreferredBackBufferWidth - _ball.BoundingBox.Width)
            {
                _leftPaddle.IncreaseScore();
                _ball.Reset(true);
            }
            _oldState = _newState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _leftPaddle.Draw(spriteBatch);
            _rightPaddle.Draw(spriteBatch);
            _ball.Draw(spriteBatch);
            string score = _leftPaddle.Score + " : " + _rightPaddle.Score;
            spriteBatch.DrawString(_font, score,
                                   new Vector2(graphics.PreferredBackBufferWidth/2f - _font.MeasureString(score).X/2f, 5),
                                   Color.White);
            if (_isPaused)
            {
                spriteBatch.Draw(_pScreen, Vector2.Zero, Color.White);
                spriteBatch.DrawString(_font, "Paused",
                                       new Vector2(
                                           graphics.PreferredBackBufferWidth/2f - _font.MeasureString("Paused").X/2f,
                                           400),
                                       Color.White);
            }
        }

        public override void Unload()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
        }
    }
}