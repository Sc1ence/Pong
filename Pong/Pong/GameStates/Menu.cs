using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.GameStates
{
    internal class Menu : GameState
    {
        private SpriteFont _font;
        private SpriteFont _sfont;
        private Random random;

        public Menu(Game1 game) : base(game)
        {
            random = new Random();
        }

        public override void Load(ContentManager content)
        {
            _font = content.Load<SpriteFont>("sprites/font");
            _sfont = content.Load<SpriteFont>("sprites/smallfont");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Game.switchState(new InGame(Game, true));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                Game.switchState(new InGame(Game, false));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                Game.switchState(new Credits(Game));
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Pong!",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f - _font.MeasureString("Pong!").X/2f, 50),
                                   Color.White);
            spriteBatch.DrawString(_sfont, "Press S for Singleplayer",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f -
                                       _sfont.MeasureString("Press S for Singleplayer").X/2f, 250), Color.White);
            spriteBatch.DrawString(_sfont, "and M for Multiplayer",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f -
                                       _sfont.MeasureString("and M for Multiplayer").X/2f, 300), Color.White);
            spriteBatch.DrawString(_sfont, "Or C for Credits",
                                   new Vector2(
                                       Game.graphics.PreferredBackBufferWidth/2f -
                                       _sfont.MeasureString("Or C for Credits").X/2f, 350), Color.White);
        }

        public override void Unload()
        {
        }
    }
}