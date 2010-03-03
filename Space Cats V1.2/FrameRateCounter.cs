using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Text;

namespace Space_Cats_V1._2
{
    public class FrameRateCounter : DrawableGameComponent
    {
        ContentManager z_content;
        SpriteBatch z_spriteBatch;
        SpriteFont z_spriteFont;

        int z_frameRate = 0;
        int z_frameCounter = 0;
        TimeSpan z_elapsedTime = TimeSpan.Zero;


        public FrameRateCounter(Game game)
            : base(game)
        {
            z_content = new ContentManager(game.Services);
        }


        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                z_spriteBatch = new SpriteBatch(GraphicsDevice);
                z_spriteFont = z_content.Load<SpriteFont>("Content\\Fonts\\TimerFont");
            }
        }


        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
                z_content.Unload();
        }


        public override void Update(GameTime gameTime)
        {
            
            this.z_elapsedTime += gameTime.ElapsedGameTime;

            if (this.z_elapsedTime > TimeSpan.FromSeconds(1))
            {
                this.z_elapsedTime -= TimeSpan.FromSeconds(1);
                this.z_frameRate = z_frameCounter;
                this.z_frameCounter = 0;
            }
        }


        public override void Draw(GameTime gameTime)
        {
            this.z_frameCounter++;

            string fps = string.Format("fps: {0}", this.z_frameRate);

            this.z_spriteBatch.Begin();

            this.z_spriteBatch.DrawString(z_spriteFont, fps, new Vector2(133, 6), Color.Black);
            this.z_spriteBatch.DrawString(z_spriteFont, fps, new Vector2(132, 5), Color.White);

            this.z_spriteBatch.End();
        }
    }
}
