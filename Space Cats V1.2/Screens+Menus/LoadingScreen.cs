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

namespace Space_Cats_V1._2
{
    class LoadingScreen
    {
        private Texture2D z_loadingScreen;
        private Texture2D z_logo;
        private float z_timer;

        public LoadingScreen(Texture2D logo, Texture2D loadingScreen)
        {
            this.z_logo = logo;
            this.z_loadingScreen = loadingScreen;
            this.z_timer = 0f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle viewPort)
        {
            if(gameTime != null)
                this.z_timer += gameTime.ElapsedGameTime.Milliseconds;
            //Create a new Vector that will scale the background image logo
            Vector2 ScaledVec = new Vector2(((float)viewPort.Width / this.z_logo.Width),
                                ((float)viewPort.Height / this.z_logo.Height));

            spriteBatch.Draw(this.z_logo, new Vector2(0, 0), null, Color.White, 
                             0, new Vector2(0, 0), ScaledVec,SpriteEffects.None, 1);

            if (this.z_timer < 700)
                spriteBatch.Draw(this.z_loadingScreen, new Rectangle((int)(viewPort.Width / 2) - 400,
                                (int)(viewPort.Height / 2), 510, 200 + (int)(viewPort.Height / 2)),
                                new Rectangle(0, 0, 510, 200), Color.White);
            else if (this.z_timer < 1400)
                spriteBatch.Draw(this.z_loadingScreen, new Rectangle((int)(viewPort.Width / 2) - 400,
                                (int)(viewPort.Height / 2), 570, 200 + (int)(viewPort.Height / 2)),
                                new Rectangle(0, 0, 570, 200), Color.White);
            else if (this.z_timer < 2100)
                spriteBatch.Draw(this.z_loadingScreen, new Rectangle((int)(viewPort.Width / 2) - 400,
                                (int)(viewPort.Height / 2), 630, 200 + (int)(viewPort.Height / 2)),
                                new Rectangle(0, 0, 630, 200), Color.White);
            else if (this.z_timer < 2800)
                spriteBatch.Draw(this.z_loadingScreen, new Rectangle((int)(viewPort.Width / 2) - 400,
                                (int)(viewPort.Height / 2), 700, 200 + (int)(viewPort.Height / 2)),
                                new Rectangle(0, 0, 700, 200), Color.White);
            else
            {
                spriteBatch.Draw(this.z_loadingScreen, new Rectangle((int)(viewPort.Width / 2) - 400,
                                (int)(viewPort.Height / 2), 510, 200 + (int)(viewPort.Height / 2)),
                                new Rectangle(0, 0, 510, 200), Color.White);
                this.z_timer = 0;
            }
        }





    }
}
