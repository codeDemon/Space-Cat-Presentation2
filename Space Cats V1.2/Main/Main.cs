#region using
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
// In the .NET pane, scroll down and select System.Xml. 
// Additional Using Statments Needed for the Storage Functions. 
using System.IO;
using System.Xml.Serialization; 

#endregion


namespace Space_Cats_V1._2
{
    //Main Class
    public class Main : Microsoft.Xna.Framework.Game
    {
        #region Variables
        /*
        //Declare Instance Variables ----------------------------------------------------------------------------
        private GraphicsDeviceManager z_graphics;
        private SpriteBatch z_spriteBatch;
        private ScrollingBackground z_backgroundImage1;
        private ScrollingBackground z_backgroundImage2;
        private Rectangle z_viewportRec;
        
        
        private ContentManager z_contentManager;
        
        
        //The Loading Manager
        private LoadingManager z_loadingManager;
        //The GameState Manager
        private GameStateManager z_gameStateManager;
        //The Asteroid Manager
        private AsteroidManager z_asteroidManager;
        //The Missle Manager
        private MissleManager z_missleManager;
        //The Enemy Manager
        private EnemyManager z_enemyManager;
        
        
        //Variables For Text Fonts
        private SpriteFont z_timerFont;
        private SpriteFont z_livesFont;
         * 
         * 
         * */

        //Declare Instance Variables
        private GraphicsDeviceManager z_graphics;
        private SpriteBatch z_spriteBatch;
        private Rectangle z_viewportRec;
        private ContentManager z_contentManager;
        private AudioManager z_audioManager;
        private UltimateManager z_ultimateManager;
        private KeyboardState z_previousKeyboardState = Keyboard.GetState();
        private GamePadState z_previousGamePadState = GamePad.GetState(PlayerIndex.One);



        #endregion


        //Constructor -------------------------------------------------------------------------------------------
        public Main()
        {
            this.z_graphics = new GraphicsDeviceManager(this);
            this.z_audioManager = new AudioManager(this);
            //this.z_graphics.PreferredBackBufferWidth = 1280;
            //this.z_graphics.PreferredBackBufferHeight = 720;

            this.z_graphics.IsFullScreen = false;
            this.z_graphics.SynchronizeWithVerticalRetrace = true;

            Content.RootDirectory = "Content";
            //Adds the xbox live Profile Service to the game
            this.Components.Add(new GamerServicesComponent(this));
            this.Components.Add(this.z_audioManager);
            Components.Add(new FrameRateCounter(this));
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            this.z_viewportRec = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth,
                                                GraphicsDevice.PresentationParameters.BackBufferHeight);
        }


        //Initialize Method -------------------------------------------------------------------------------------
        protected override void Initialize()
        {
            base.Initialize();
        }


        //Load Content Method -----------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            //Set the contentManger
            this.z_contentManager = new ContentManager(Services);

            // Create a new SpriteBatch, which can be used to draw textures.
            this.z_spriteBatch = new SpriteBatch(GraphicsDevice);

            //Set the viewPortRec
            this.z_viewportRec = new Rectangle(0, 0, z_graphics.GraphicsDevice.Viewport.Width,
                                                z_graphics.GraphicsDevice.Viewport.Height);

            //Add Objects to Game Services
            this.Services.AddService(typeof(SpriteBatch), z_spriteBatch);
            this.Services.AddService(typeof(ContentManager), z_contentManager);
            this.Services.AddService(typeof(GraphicsDeviceManager), z_graphics);
            this.Services.AddService(typeof(Rectangle), z_viewportRec);

            //Initialize the Ultimate Manager
            this.z_ultimateManager = new UltimateManager(this, this.z_contentManager);




            










            /*
            //Load the loadingManager
            this.z_loadingManager = new LoadingManager(Content.Load<Texture2D>("Screens\\LoadingStatic"), 
                                                        Content.Load<Texture2D>("Screens\\LogoScreen"),
                                                        this.z_contentManager);

            //Load the gameStateManager
            this.z_gameStateManager = new GameStateManager(new TitleScreen(Content.Load<Texture2D>("Screens\\LogoScreen"),
                                         Content.Load<Texture2D>("Screens\\TitleOptions"),
                                         Content.Load<Texture2D>("Screens\\ArrowSelection")));

            

            

            
            //Load the background Images
            this.z_backgroundImage1 = new ScrollingBackgroundManager(Content.Load<Texture2D>("Textures\\spaceBackground"));
            this.z_backgroundImage2 = new ScrollingBackgroundManager(Content.Load<Texture2D>("Textures\\spaceBackground"));

            //Set the positions for the background Images
            this.z_backgroundImage1.setPosition(new Vector2(0f, 0f));
            this.z_backgroundImage2.setPosition(new Vector2(0f, 0f - this.z_viewportRec.Height));
            
            //Turn the background Images alive
            this.z_backgroundImage1.setIsAlive(true);
            this.z_backgroundImage2.setIsAlive(true);

            //Set the starting position for player's ship
            this.z_startingPosition = new Vector2(this.z_viewportRec.Center.X,
                                                    z_graphics.GraphicsDevice.Viewport.Height - 80);

            //Create the Player's ship image
            this.z_playerShip = new PlayerShip(Content.Load<Texture2D>("Images\\ship2"), this.z_startingPosition);           

            //Set the player alive
            this.z_playerShip.setIsAlive(true);

            

            //Load Fonts
            this.z_timerFont = Content.Load<SpriteFont>("Fonts\\TimerFont");
            this.z_livesFont = Content.Load<SpriteFont>("Fonts\\LivesFont");

            
            

            //Load the Settings for the asteroidManager
            this.z_asteroidManager = new AsteroidManager(AsteroidManager.AsteroidManagerState.Heavy, this.z_viewportRec,
                                                         this.z_contentManager, this.z_spriteBatch);

            //Load the Settings for the MissleManager
            this.z_missleManager = new MissleManager(this.z_viewportRec, this.z_contentManager,
                                                     Content.Load<SoundEffect>("Audio\\SoundFX\\LaserPellet"));

            //Load the Settings for the EnemyManager
            this.z_enemyManager = new EnemyManager(this.z_contentManager, this.z_spriteBatch, this.z_viewportRec);
            **/
        }


        //Unload Content Method ----------------------------------------------------------------------------------
        protected override void UnloadContent()
        {
        }


        //Main Update Method -------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);
            if(this.z_contentManager != null)
                this.z_ultimateManager.Update(currentKeyboardState, this.z_previousKeyboardState, gameTime, this.z_contentManager, currentGamePadState, this.z_previousGamePadState);

            this.z_previousKeyboardState = currentKeyboardState;
            this.z_previousGamePadState = currentGamePadState;
            base.Update(gameTime);
        }


        //Draw Method --------------------------------------------------------------------------------------------
        protected override void Draw(GameTime gameTime)
        {
            //Clear all images
            GraphicsDevice.Clear(Color.Black);
            this.z_spriteBatch.Begin(SpriteBlendMode.AlphaBlend);



            this.z_ultimateManager.Draw(gameTime);


            /*
            //Draw Background images
            if(this.z_backgroundImage1.getIsAlive())
                this.z_spriteBatch.Draw(this.z_backgroundImage1.getSprite(), this.z_backgroundImage1.getPosition(),null,
                    Color.White, 0, new Vector2(0, 0), this.z_backgroundImage1.Scale(this.z_viewportRec)
                    ,SpriteEffects.None,1);                       
            if(this.z_backgroundImage2.getIsAlive())
                this.z_spriteBatch.Draw(this.z_backgroundImage2.getSprite(), this.z_backgroundImage2.getPosition(), null,
                    Color.White, 0, new Vector2(0, 0), this.z_backgroundImage2.Scale(this.z_viewportRec)
                    ,SpriteEffects.None, 1);

            //Draw Enemies from EnemyManager
            this.z_enemyManager.draw();

            //Draw any asteroids from AsteroidManager
            this.z_asteroidManager.drawAsteroids();
            
            //Draw Player Ship
            this.z_playerShip.draw(this.z_spriteBatch, gameTime);

            //Draw Fonts
            this.z_spriteBatch.DrawString(this.z_timerFont, "Time: " + Math.Round(z_gameTimer,2),
                                          new Vector2(.01f * this.z_viewportRec.Width, .01f * this.z_viewportRec.Height),
                                          Color.Yellow);
            this.z_spriteBatch.DrawString(this.z_livesFont, "Lives: " + this.z_playerShip.getLives(),
                                          new Vector2(.01f * this.z_viewportRec.Width, .05f * this.z_viewportRec.Height),
                                          Color.White);

            

            //Draw Missles
            this.z_missleManager.MissleManagerDrawAllMissles();


            //Draw loading screen until loading is done
            if (!this.z_loadingManager.getInitialLoadFinished())
                this.z_loadingManager.Draw(this.z_spriteBatch);
            else
                this.z_gameStateManager.Draw(this.z_spriteBatch);
            **/



            //Close Sprite Batch
            this.z_spriteBatch.End();

            base.Draw(gameTime);
        }



        //Other Methods add here ---------------------------------------------------------------------------------





    }
}
