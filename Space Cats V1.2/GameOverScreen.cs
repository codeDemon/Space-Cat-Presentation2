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
    class GameOverScreen : IScreenMenu
    {
        //Instace Variables
        private Rectangle z_viewPort;
        private bool z_isLoaded;
        private Texture2D z_screenImage;

        //Constructor
        public GameOverScreen(Rectangle viewPort)
        {
            this.z_screenImage = null;
            this.z_viewPort = viewPort;
            this.z_isLoaded = false;

        }

        //Accessors
        public Texture2D getMainScreenTexture()
        {
            return this.z_screenImage;
        }
        public bool getIsLoaded()
        {
            return this.z_isLoaded;
        }

        //Mutator
        public void setIsLoaded(bool isLoaded)
        {
            this.z_isLoaded = isLoaded;
        }

        //Required Load Method
        public void loadTexture(ContentManager content)
        {
            this.z_screenImage = content.Load<Texture2D>("Content\\Screens\\GameOverScreen");
            this.z_isLoaded = true;
        }


        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            //Create a new Vector that will scale the background
            Vector2 ScaledVec = new Vector2(((float)this.z_viewPort.Width / this.z_screenImage.Width),
                                ((float)this.z_viewPort.Height / this.z_screenImage.Height));

            spriteBatch.Draw(this.z_screenImage, new Vector2(0, 0), null, Color.White,
                             0, new Vector2(0, 0), ScaledVec, SpriteEffects.None, 1);
        }



    }
}
