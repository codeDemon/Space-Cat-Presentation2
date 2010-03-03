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
    class InputManager
    {
        /*
         * //Some example input
         * 
         * private float z_gameTimer;
        private float z_accelTimerX = 0;
        private float z_accelTimerY = 0;
        private float z_interval1 = 15;
         * private GamePadState z_previousGamePadState = GamePad.GetState(PlayerIndex.One);
        private KeyboardState z_previousKeyboardState = Keyboard.GetState();
         * //Variables for GameObjects
        private PlayerShip z_playerShip;
         * private Vector2 z_startingPosition;
         * 
         * 
         * 
         * 
         * 
         * 
         *  //########### Input for Controls and Options ########################################


            //For Xbox controller 1
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                //Local Variables for 360 controller
                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                //Input for moving the player's ship on the xbox360
                //Acceleration needs to be worked on for xbox controller
                this.z_playerShip.setVelocity(new Vector2(gamePadState.ThumbSticks.Left.X * 0.07f,
                                                        gamePadState.ThumbSticks.Left.Y * 0.07f));
                this.z_playerShip.upDatePosition();

                //Update Missle Manager
                this.z_missleManager.MissleManagerUpdateFriendlyGamepad(gamePadState, this.z_previousGamePadState,
                                                                        this.z_playerShip, this.z_spriteBatch);


                //At the end of Xbox Controller Updates
                this.z_previousGamePadState = gamePadState;
            }


            //For Keyboard
#if !XBOX
            //Local Variables for Keyboard
            KeyboardState keyboardState = Keyboard.GetState();

            #region ShipControls
            //Input for accelerating the ship --------------------------------------------------------
            //Move Left
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    if (z_accelTimerX < 100)
                        z_accelTimerX += (float)gameTime.ElapsedGameTime.Milliseconds;
                    else
                        if (this.z_playerShip.getPosition().X > 1)
                        {
                            this.z_playerShip.accelerateLeft();
                            z_accelTimerX = 0;
                        }
                }

            //Move Right
            if ((keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))) 
            {
                if (z_accelTimerX < 100)
                    z_accelTimerX += (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                    if (this.z_playerShip.getPosition().X + this.z_playerShip.getSprite().Width <
                        this.z_graphics.GraphicsDevice.Viewport.Width - 1)
                    {
                        this.z_playerShip.accelerateRight();
                        z_accelTimerX = 0;
                    }
            }

            //Move Up
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                if (z_accelTimerY < 100)
                    z_accelTimerY += (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                    if (this.z_playerShip.getPosition().Y > 1)
                    {
                        this.z_playerShip.accelerateUp();
                        z_accelTimerY = 0;
                    }
            }
            //Move Down
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                if (z_accelTimerY < 100)
                    z_accelTimerY += (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                    if (this.z_playerShip.getPosition().Y + this.z_playerShip.getSprite().Height < 
                        this.z_graphics.GraphicsDevice.Viewport.Height - 1)
                    {
                        this.z_playerShip.accelerateDown();
                        z_accelTimerY = 0;
                    }
            }


            //Don't Move in the X direction when opposite keys are pressed
            if (((keyboardState.IsKeyDown(Keys.Right) && (keyboardState.IsKeyDown(Keys.Left)) ||
                ((keyboardState.IsKeyDown(Keys.Right) && (keyboardState.IsKeyDown(Keys.A)))) ||
                ((keyboardState.IsKeyDown(Keys.Left) && (keyboardState.IsKeyDown(Keys.D)))) ||
                ((keyboardState.IsKeyDown(Keys.A) && (keyboardState.IsKeyDown(Keys.D)))))))
            {
                this.z_playerShip.setVelocity(new Vector2(0, this.z_playerShip.getVelocity().Y));
                this.z_playerShip.setIsSlowingDownX(false);
            }

            //Don't Move in the Y direction when opposite keys are pressed
            if (((keyboardState.IsKeyDown(Keys.Up) && (keyboardState.IsKeyDown(Keys.Down)) ||
                ((keyboardState.IsKeyDown(Keys.Up) && (keyboardState.IsKeyDown(Keys.S)))) ||
                ((keyboardState.IsKeyDown(Keys.Down) && (keyboardState.IsKeyDown(Keys.W)))) ||
                ((keyboardState.IsKeyDown(Keys.W) && (keyboardState.IsKeyDown(Keys.S)))))))
            {
                this.z_playerShip.setVelocity(new Vector2(this.z_playerShip.getVelocity().X, 0));
                this.z_playerShip.setIsSlowingDownY(false);
            }


            //Check if a key was let go for deAccelerating to a stop -------------------------------------
            if ((this.z_previousKeyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyUp(Keys.Left)) ||
                (this.z_previousKeyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.A)) &&
                (!(keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyUp(Keys.A))) &&
                (!(keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.Left))))
                    this.z_playerShip.setIsSlowingDownX(true);

            else if ((this.z_previousKeyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyUp(Keys.Right)) ||
                (this.z_previousKeyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyUp(Keys.D)) &&
                (!(keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyUp(Keys.D))) &&
                (!(keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyUp(Keys.Right))))
                    this.z_playerShip.setIsSlowingDownX(true);

            if ((this.z_previousKeyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyUp(Keys.Up)) ||
                (this.z_previousKeyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyUp(Keys.W)) &&
                (!(keyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyUp(Keys.W))) &&
                (!(keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyUp(Keys.Up))))
                    this.z_playerShip.setIsSlowingDownY(true);
           
            else if ((this.z_previousKeyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyUp(Keys.Down)) ||
                (this.z_previousKeyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.S)) &&
                (!(keyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyUp(Keys.S))) &&
                (!(keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.Down))))
                    this.z_playerShip.setIsSlowingDownY(true);
            #endregion



            //Perform the Update on The Ship
            this.z_playerShip.playerShipUpdate(gameTime, this.z_viewportRec);


            //Update Missle Manager
            this.z_missleManager.MissleManagerUpdateFriendlyKeyboard(keyboardState, this.z_previousKeyboardState,
                                                                     this.z_playerShip, this.z_spriteBatch);

            //Update the Enemy Manager
            this.z_enemyManager.mainUpdate(gameTime);

            //Update GameStateManager
            this.z_gameStateManager.Update(keyboardState, this.z_previousKeyboardState);

            //End of Keyboard Updates
            this.z_previousKeyboardState = keyboardState;
#endif
         * 
         * 
         * 
         * */

    }
}
