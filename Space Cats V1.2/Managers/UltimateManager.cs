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
    class UltimateManager
    {
        /*
         * This Manager class is responisble for managing all the other manager classes.
         * */

        //Declare all the manager classes
        //private AsteroidManager z_asteroidManager;
        private AudioManager z_audioManager;
        private CollisionManager z_collisionManager;
        //private EnemyManager z_enemyManager;
        private EventManager z_eventManager;
        private GameStateManager z_gameStateManager;
        private InputManager z_inputManager;
        private LoadingManager z_loadingManager;
        private MissionManager z_missionManager;
        //private MissleManager z_missleManager;
        //private ScrollingBackgroundManager z_scrollingBackgroundManager;
        private StageManager z_stageManager;

        //Declare other Variables
        Game z_game;
        SpriteBatch z_spriteBatch;
        ContentManager z_content;
        GraphicsDevice z_graphics;
        Rectangle z_viewPort;

        //Constructor
        public UltimateManager(Game game, ContentManager content)
        {
            this.z_game = game;
            //Get Game Services
            this.z_spriteBatch = ((SpriteBatch)this.z_game.Services.GetService(typeof(SpriteBatch)));
            this.z_content = ((ContentManager)this.z_game.Services.GetService(typeof(ContentManager)));
            this.z_graphics = ((GraphicsDevice)this.z_game.Services.GetService(typeof(GraphicsDevice)));
            this.z_viewPort = ((Rectangle)this.z_game.Services.GetService(typeof(Rectangle)));

            //Initialize necessary Managers
            this.z_gameStateManager = new GameStateManager(this.z_game);
            this.z_loadingManager = new LoadingManager(this.z_game);
            this.z_loadingManager.InitialLoad(this.z_gameStateManager.getListScreen(), content, this.z_gameStateManager.getTitleScreen());

        }


        //Update Method
        public void Update(KeyboardState currentKey, KeyboardState previousKey, GameTime gameTime, ContentManager content,
                            GamePadState currentPad, GamePadState previousPad)
        {
            if (!this.z_loadingManager.getInitialLoadFinished())
            {
                //this.z_loadingManager.InitialLoad(this.z_gameStateManager.getListScreen(), content);
                this.z_loadingManager.Update(gameTime);
                return;
            }
            else
            {
                if (this.z_gameStateManager.getLoadingManagerIsActive())
                    this.z_gameStateManager.setLoadingManagerIsActive(false);
            }
            this.z_gameStateManager.UpdateKeyBoard(currentKey, previousKey, gameTime, currentPad, previousPad);

        }


        //Draw Method
        public void Draw(GameTime gameTime)
        {
            this.z_gameStateManager.Draw(this.z_spriteBatch, gameTime);

        }













    }
}
