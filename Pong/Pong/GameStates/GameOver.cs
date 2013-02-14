using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.GameStates
{
    internal class GameOver : GameState
    {
        private readonly string _message;
        private SpriteFont _font;
        private Vector2 _pos;
        private SpriteFont _sfont;

        public GameOver(Game1 game, string msg) : base(game)
        {
            _message = msg;
        }

        public override void Load(ContentManager content)
        {
            _sfont = content.Load<SpriteFont>("sprites/smallfont");
            _font = content.Load<SpriteFont>("sprites/font");
            _pos = new Vector2(Game.graphics.PreferredBackBufferWidth/2f - _font.MeasureString(_message).X/2f, 50);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game.switchState(new Menu(Game));
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                Game.switchState(new Credits(Game));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _message, _pos, Color.White);
            spriteBatch.DrawString(_sfont, "Press Escape for a new Game",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f -
                                       _sfont.MeasureString("Press Escape for a new Game!").X/2f, 200), Color.White);
            spriteBatch.DrawString(_sfont, "And C for Credits",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f -
                                       _sfont.MeasureString("And C for Credits").X/2f, 250), Color.White);
        }

        public override void Unload()
        {

        }
    }
}