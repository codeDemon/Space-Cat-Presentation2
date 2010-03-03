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
    class TitleScreen : IScreenMenu
    {
        //Enum
        public enum TitleState
        {
            Start,
            Options,
            Exit

        }


        //Instance Variables
        private Texture2D z_logo;
        private Texture2D z_options;
        private GameObject z_arrow;
        private TitleState z_currentState;
        private Rectangle z_viewPort;
        private bool z_isLoaded;

        //Constructor
        public TitleScreen(Rectangle viewPort)
        {
            this.z_currentState = TitleState.Start;
            this.z_isLoaded = false;
            this.z_viewPort = viewPort;
        }

        //Accessors
        public Texture2D getLogo()
        {
            return this.z_logo;
        }
        public Texture2D getOptions()
        {
            return this.z_options;
        }
        public GameObject getArrow()
        {
            return this.z_arrow;
        }
        public TitleState getState()
        {
            return this.z_currentState;
        }
        public bool getIsLoaded()
        {
            return this.z_isLoaded;
        }


        //Mutators
        public void setLogo(Texture2D newLogo)
        {
            this.z_logo = newLogo;
        }
        public void setOptions(Texture2D newOptions)
        {
            this.z_options = newOptions;
        }
        public void setArrow(GameObject newArrow)
        {
            this.z_arrow = newArrow;
        }
        public void setState(TitleState newState)
        {
            this.z_currentState = newState;
        }
        public void setIsLoaded(bool isLoaded)
        {
            this.z_isLoaded = isLoaded;
        }
        public void setViewPort(Rectangle viewPort)
        {
            this.z_viewPort = viewPort;
        }


        //Update Method
        public void update(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            //Don't update anything if the titleScreen is not loaded
            if (!this.z_isLoaded)
                return;

            switch (this.z_currentState)
            {
                case TitleState.Start:
                    {
                        if (previousKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Down)
                            && previousKeyboardState.IsKeyUp(Keys.Up))
                        {
                            this.z_currentState = TitleState.Options;
                            this.z_arrow.setPosition(new Vector2((float)((this.z_viewPort.Width / 2) - 200), (float)(this.z_viewPort.Height / 2)+125));
                        }
                        break;
                    }
                case TitleState.Options:
                    {
                        if (previousKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Down)
                            && previousKeyboardState.IsKeyUp(Keys.Up))
                        {
                            this.z_currentState = TitleState.Exit;
                            this.z_arrow.setPosition(new Vector2((float)((this.z_viewPort.Width / 2) - 200), (float)(this.z_viewPort.Height / 2)+190));
                        }
                        else if (previousKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Up)
                                && previousKeyboardState.IsKeyUp(Keys.Up))
                        {
                            this.z_currentState = TitleState.Start;
                            this.z_arrow.setPosition(new Vector2((float)((this.z_viewPort.Width / 2) - 200), (float)(this.z_viewPort.Height / 2)+50));
                        }
                        break;
                    }
                case TitleState.Exit:
                    {
                        if (previousKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Up)
                                && previousKeyboardState.IsKeyUp(Keys.Up))
                        {
                            this.z_currentState = TitleState.Options;
                            this.z_arrow.setPosition(new Vector2((float)((this.z_viewPort.Width / 2) - 200), (float)(this.z_viewPort.Height / 2)+125));
                        }
                        break;
                    }


            }
        }



        //Draw Method
        public void Draw(SpriteBatch spritebatch, Rectangle viewPort)
        {
            //Don't draw anything if the titleScreen is not yet loaded.
            if (this.z_logo==null || this.z_options==null || this.z_arrow == null)
                return;

            //Create a new Vector that will scale the background image logo
            Vector2 ScaledVec = new Vector2(((float)viewPort.Width / this.z_logo.Width),
                                ((float)viewPort.Height / this.z_logo.Height));

            spritebatch.Draw(this.z_logo, new Vector2(0, 0), null, Color.White,
                             0, new Vector2(0, 0), ScaledVec, SpriteEffects.None, 1);
            spritebatch.Draw(this.z_options, new Vector2((float)((viewPort.Width / 2) - 400), (float)(viewPort.Height / 2)), null,
                             Color.White, 0, new Vector2(0,0), new Vector2(1,1), SpriteEffects.None, 1);

            
            
            switch (this.z_currentState)
            {
                case TitleState.Start:
                    {
                        spritebatch.Draw(this.z_options, 
                                         new Rectangle((int)((viewPort.Width / 2) - 400)+255, 
                                                       (int)(viewPort.Height / 2)+40, 245, 60), 
                                         new Rectangle(255, 40, 245, 60), Color.Blue);
                        break;
                    }
                case TitleState.Options:
                    {
                        spritebatch.Draw(this.z_options, 
                                         new Rectangle((int)((viewPort.Width / 2) - 400)+255, 
                                                       (int)(viewPort.Height / 2)+100, 245, 80), 
                                         new Rectangle(255, 100, 245, 80), Color.Blue);
                        break;
                    }
                case TitleState.Exit:
                    {
                        spritebatch.Draw(this.z_options, 
                                         new Rectangle((int)((viewPort.Width / 2) - 400)+255, 
                                                       (int)(viewPort.Height / 2)+180, 245, 60), 
                                         new Rectangle(255, 180, 245, 60), Color.Blue);
                        break;
                    }

            }

            spritebatch.Draw(this.z_arrow.getSprite(), this.z_arrow.getPosition(), Color.White);

        }


        //Required Load Method
        public void loadTexture(ContentManager content)
        {
            this.z_logo = content.Load<Texture2D>("Content\\Screens\\LogoScreen2");
            this.z_options = content.Load<Texture2D>("Content\\Screens\\TitleOptions3");
            this.z_arrow = new GameObject(content.Load<Texture2D>("Content\\Screens\\ArrowSelection2"));
            //Try to fiqure the starting position for arrow ^^
            this.z_arrow.setPosition(new Vector2((float)((this.z_viewPort.Width / 2) - 200), (float)(this.z_viewPort.Height / 2)+50));
            this.z_isLoaded = true;
        }

    }
}
