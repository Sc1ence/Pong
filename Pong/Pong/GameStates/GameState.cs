using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.GameStates
{
    public abstract class GameState
    {
        protected Game1 Game;

        protected GameState(Game1 game)
        {
            Game = game;
        }

        public abstract void Load(ContentManager content);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Unload();
    }
}