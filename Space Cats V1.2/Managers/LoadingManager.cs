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
    class LoadingManager
    {
        //The different states a level could be in
        public enum LevelStates
        {
            Running,
            Paused,
            Froze
        }
        //Instance Variables
        private bool z_initialLoadFinished;
        private bool z_isLoading;
        private float z_delay;
        private Game z_game;
        

        //Screens and Menus
        private TitleScreen z_titleScreen;

        //Constructor
        public LoadingManager(Game game)
        {
            this.z_game = game;
            this.z_initialLoadFinished = false;
            this.z_isLoading = false;
            this.z_delay = 0;
            
        }


        //Accessors
        public bool getInitialLoadFinished()
        {
            return this.z_initialLoadFinished;
        }
        public bool getIsLoading()
        {
            return this.z_isLoading;
        }
        public TitleScreen getTitleScreen()
        {
            return this.z_titleScreen;
        }

        //Mutators
        public void setInitialLoad(bool restartInitialLoad)
        {
            this.z_initialLoadFinished = restartInitialLoad;
        }
        public void setIsLoading(bool isLoading)
        {
            this.z_isLoading = isLoading;
        }

        //Perform the first loading for the game
        public void InitialLoad(List<IScreenMenu> listScreen, ContentManager content, TitleScreen titleScreen)
        {
            //Load all necessary images/content for all Screens and Menus
            for (int i = 0; i < listScreen.Count; i++)
                listScreen[i].loadTexture(content);

            //Load all audio files into the AudioManager

            //this.z_initialLoadFinished = true;
        }

        //Load all the content for a level
        public void LoadLevel(ILevel nextLevel)
        {
            this.z_isLoading = true;
            nextLevel.loadAssets();
            this.z_isLoading = false;
        }

        //Unload all the content for a previous level
        public void UnLoadLevel(ILevel lastLevel)
        {
            this.z_isLoading = true;
            lastLevel.unLoadAssets();
            this.z_isLoading = false;
        }

        //Update Method
        public void Update(GameTime gameTime)
        {
            this.z_delay += gameTime.ElapsedGameTime.Milliseconds;
            //Just to pretend like something were loading
            if (this.z_delay > 5000)
                this.z_initialLoadFinished = true;
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            
        }


    }
}
