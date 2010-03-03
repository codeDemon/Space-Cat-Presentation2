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
    class MissionScreen : IScreenMenu
    {
        //Enum
        public enum MissionMenuState
        {
            Mission1,
            Mission2,
            Mission3,
            Mission4,
            Mission5,
            Back
        }

        //Instance Variables
        private Texture2D z_MissionScreenPicture1;
        private Texture2D z_MissionScreenPicture2;
        private Texture2D z_MissionScreenPicture3;
        private Texture2D z_MissionScreenPicture4;
        private Texture2D z_MissionScreenPicture5;
        private Texture2D z_MissionScreenPicture6;
        private MissionMenuState z_CurrentState;
        private Rectangle z_viewPort;
        private bool z_isLoaded;

        //Constructor
        public MissionScreen(Rectangle viewPort)
        {
            this.z_isLoaded = false;
            this.z_viewPort = viewPort;
            this.z_MissionScreenPicture1 = null;
            this.z_MissionScreenPicture2 = null;
            this.z_MissionScreenPicture3 = null;
            this.z_MissionScreenPicture4 = null;
            this.z_MissionScreenPicture5 = null;
            this.z_MissionScreenPicture6 = null;
            this.z_CurrentState = MissionMenuState.Mission1;
        }

        //Accessors
        public bool getIsLoaded()
        {
            return this.z_isLoaded;
        }
        public MissionMenuState getCurrentState()
        {
            return this.z_CurrentState;
        }


        //Mutators
        public void setIsLoaded(bool isLoaded)
        {
            this.z_isLoaded = isLoaded;
        }
        public void setCurrentState(MissionMenuState newState)
        {
            this.z_CurrentState = newState;
        }
        public void setViewPort(Rectangle viewPort)
        {
            this.z_viewPort = viewPort;
        }


        //Update Method
        public void update(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            //check if the class is loaded
            if (!this.z_isLoaded)
                return;

            //Check that none of the keys are being held down
            if (previousKeyboardState.IsKeyUp(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Down) &&
                previousKeyboardState.IsKeyUp(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Left))
            {
                switch (this.z_CurrentState)
                {
                        //If state is mission 1
                        //Right mission 2
                        //down mission 4
                    case MissionMenuState.Mission1:
                        {
                            if (currentKeyboardState.IsKeyDown(Keys.Right))
                                this.z_CurrentState = MissionMenuState.Mission2;

                            if (currentKeyboardState.IsKeyDown(Keys.Down))
                                 this.z_CurrentState = MissionMenuState.Mission4;

                            break;
                        }
                        //If state is mission 2
                        //Right mission 3
                        //Down mission 5
                        //Left mission 1
                    case MissionMenuState.Mission2:
                        {
                            if (currentKeyboardState.IsKeyDown(Keys.Right))
                                this.z_CurrentState = MissionMenuState.Mission3;

                            if (currentKeyboardState.IsKeyDown(Keys.Down))
                                this.z_CurrentState = MissionMenuState.Mission5;

                            if (currentKeyboardState.IsKeyDown(Keys.Left))
                                this.z_CurrentState = MissionMenuState.Mission1;

                            break;
                        }
                        //If state is mission 3
                        //Down back
                        //Left mission 2
                    case MissionMenuState.Mission3:
                        {
                            if (currentKeyboardState.IsKeyDown(Keys.Down))
                                this.z_CurrentState = MissionMenuState.Back;

                            if (currentKeyboardState.IsKeyDown(Keys.Left))
                                this.z_CurrentState = MissionMenuState.Mission2;

                            break;
                        }
                        //If state is mission 4
                        //Up mission 1
                        //right mission 5
                    case MissionMenuState.Mission4:
                            {
                                if (currentKeyboardState.IsKeyDown(Keys.Up))
                                    this.z_CurrentState = MissionMenuState.Mission1;

                                if (currentKeyboardState.IsKeyDown(Keys.Right))
                                    this.z_CurrentState = MissionMenuState.Mission5;

                                break;
                            }
                        //If state is mission 5
                        //left mission 4
                        //up mission 2
                        //right back
                    case MissionMenuState.Mission5:
                        {
                            if (currentKeyboardState.IsKeyDown(Keys.Left))
                                this.z_CurrentState = MissionMenuState.Mission4;

                            if (currentKeyboardState.IsKeyDown(Keys.Up))
                                this.z_CurrentState = MissionMenuState.Mission2;

                            if (currentKeyboardState.IsKeyDown(Keys.Right))
                                this.z_CurrentState = MissionMenuState.Back;

                            break;
                        }
                        //If back
                        //Up mission 3
                        //left mission 5
                    case MissionMenuState.Back:
                        {
                            if (currentKeyboardState.IsKeyDown(Keys.Up))
                                this.z_CurrentState = MissionMenuState.Mission3;

                            if (currentKeyboardState.IsKeyDown(Keys.Left))
                                this.z_CurrentState = MissionMenuState.Mission5;

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }


        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            //Do not draw anything if nothing is loaded
            if (!this.z_isLoaded || this.z_MissionScreenPicture1 == null || this.z_MissionScreenPicture2 == null
                || this.z_MissionScreenPicture3 == null || this.z_MissionScreenPicture4 == null ||
                this.z_MissionScreenPicture5 == null || this.z_MissionScreenPicture6 == null)
                    return;

            //Create a new Vector that will scale the background
            Vector2 ScaledVec = new Vector2(((float)this.z_viewPort.Width / this.z_MissionScreenPicture1.Width),
                                ((float)this.z_viewPort.Height / this.z_MissionScreenPicture1.Height));

            //Draw the different screens depending on the states
            switch (this.z_CurrentState)
            {
                case MissionMenuState.Mission1:
                    {
                        spriteBatch.Draw(this.z_MissionScreenPicture1, new Vector2(0, 0), null, Color.White,
                             0, new Vector2(0, 0), ScaledVec, SpriteEffects.None, 1);
                        break;
                    }
                case MissionMenuState.Mission2:
                    {
                        spriteBatch.Draw(this.z_MissionScreenPicture2, new Vector2(0, 0), null, Color.White,
                             0, new Vector2(0, 0), ScaledVec, SpriteEffects.None, 1);
                        break;
                    }
                case MissionMenuState.Mission3:
                    {
                        spriteBatch.Draw(this.z_MissionScreenPicture3, new Vector2(0, 0), null, Color.White,
                             0, new Vector2(0, 0), ScaledVec, SpriteEffects.None, 1);
                        break;
                    }
                case MissionMenuState.Mission4:
                    {
                        spriteBatch.Draw(this.z_MissionScreenPicture4, new Vector2(0, 0), null, Color.White,
                             0, new Vector2(0, 0), ScaledVec, SpriteEffects.None, 1);
                        break;
                    }
                case MissionMenuState.Mission5:
                    {
                        spriteBatch.Draw(this.z_MissionScreenPicture5, new Vector2(0, 0), null, Color.White,
                             0, new Vector2(0, 0), ScaledVec, SpriteEffects.None, 1);
                        break;
                    }
                case MissionMenuState.Back:
                    {
                        spriteBatch.Draw(this.z_MissionScreenPicture6, new Vector2(0, 0), null, Color.White,
                             0, new Vector2(0, 0), ScaledVec, SpriteEffects.None, 1);
                        break;
                    }
            }
        }


        //Required Load Method
        public void loadTexture(ContentManager content)
        {
            this.z_MissionScreenPicture1 = content.Load<Texture2D>("Content\\Screens\\MissionMenus\\MissionMenu1");
            this.z_MissionScreenPicture2 = content.Load<Texture2D>("Content\\Screens\\MissionMenus\\MissionMenu2");
            this.z_MissionScreenPicture3 = content.Load<Texture2D>("Content\\Screens\\MissionMenus\\MissionMenu3");
            this.z_MissionScreenPicture4 = content.Load<Texture2D>("Content\\Screens\\MissionMenus\\MissionMenu4");
            this.z_MissionScreenPicture5 = content.Load<Texture2D>("Content\\Screens\\MissionMenus\\MissionMenu5");
            this.z_MissionScreenPicture6 = content.Load<Texture2D>("Content\\Screens\\MissionMenus\\MissionMenu6");
            this.z_isLoaded = true;
        }




    }
}
