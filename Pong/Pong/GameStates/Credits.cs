using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.GameStates
{
    internal class Credits : GameState
    {
        private SpriteFont _font;
        private SpriteFont _sfont;

        public Credits(Game1 game) : base(game)
        {
        }

        public override void Load(ContentManager content)
        {
            _sfont = content.Load<SpriteFont>("sprites/smallfont");
            _font = content.Load<SpriteFont>("sprites/font");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game.switchState(new Menu(Game));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Credits",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f - _font.MeasureString("Credits").X/2f,
                                       50), Color.White);
            spriteBatch.DrawString(_sfont, "Everything:",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f -
                                       _sfont.MeasureString("Everything:").X/2f, 150), Color.White);
            spriteBatch.DrawString(_sfont, "Me",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f - _sfont.MeasureString("Me").X/2f, 175),
                                   Color.Gold);
            spriteBatch.DrawString(_sfont, "Sound:",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f - _sfont.MeasureString("Sound:").X/2f,
                                       250), Color.White);
            spriteBatch.DrawString(_sfont, "Me",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f - _sfont.MeasureString("Me").X/2f,
                                       275), Color.Gold);
            spriteBatch.DrawString(_sfont, "Font:",
                       new Vector2(
                           Game.graphics.PreferredBackBufferWidth / 2f - _sfont.MeasureString("Font:").X / 2f,
                           350), Color.White);
           spriteBatch.DrawString(_sfont, "Visitor",
                       new Vector2(
                           Game.graphics.PreferredBackBufferWidth / 2f - _sfont.MeasureString("Visitor").X / 2f,
                           375), Color.Gold);
        }

        public override void Unload()
        {
        }
    }
}