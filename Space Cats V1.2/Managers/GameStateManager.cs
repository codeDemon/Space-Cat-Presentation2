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
    class GameStateManager
    {
        /*
         * Welcome to the GameStateManager.
         * This class will control how the game is played
         * by managing all the different states the game could be in.
         * 
         * 
         * */

        //Enum for all the different states
        public enum GameState
        {
            LoadingScreen,
            TitleMenu,
            OptionsMenu,
            ProfileScreen,
            MainMenu,
            MissionMenu,
            PlayingGame,
            GameOverScreen
            //Maybe more
        }


        //Instance Variables
        private GameState z_currentGameState;
        private TitleScreen z_titleScreen;
        private LoadingScreen z_loadingScreen;
        private MainMenuScreen z_MainMenuScreen;
        private MissionScreen z_MissionScreen;
        private StageManager z_StageManager;
        private GameOverScreen z_GameOverScreen;
        private List<IScreenMenu> z_listScreen;
        private bool z_loadingManagerIsActive;
        //Declare Game Services
        private Game z_game;
        private SpriteBatch z_spriteBatch;
        private ContentManager z_content;
        private GraphicsDevice z_graphics;
        private Rectangle z_viewPort;
        private GameTime z_gameTime;
        private GameState z_previousGameState;
        //Use a stack to keep track of previous states
        //private Queue<GameState> z_previousStateQueue;


        //Constructor
        public GameStateManager(Game game)
        {
            this.z_game = game;
            //Get Game Services
            this.z_spriteBatch = ((SpriteBatch)this.z_game.Services.GetService(typeof(SpriteBatch)));
            this.z_content = ((ContentManager)this.z_game.Services.GetService(typeof(ContentManager)));
            this.z_graphics = ((GraphicsDevice)this.z_game.Services.GetService(typeof(GraphicsDevice)));
            this.z_viewPort = ((Rectangle)this.z_game.Services.GetService(typeof(Rectangle)));

            //Initialize States
            this.z_currentGameState = GameState.LoadingScreen;
            this.z_previousGameState = this.z_currentGameState;
            this.z_StageManager = new StageManager(this.z_spriteBatch, this.z_viewPort, this.z_content);

            //Initialize Screens and Menus
            this.z_loadingScreen = new LoadingScreen(this.z_content.Load<Texture2D>("Content\\Screens\\LogoScreen"),
                                                     this.z_content.Load<Texture2D>("Content\\Screens\\LoadingStatic"));
            this.z_titleScreen = new TitleScreen(this.z_viewPort);
            this.z_MainMenuScreen = new MainMenuScreen(this.z_viewPort);
            this.z_MissionScreen = new MissionScreen(this.z_viewPort);
            this.z_GameOverScreen = new GameOverScreen(this.z_viewPort);

            this.z_listScreen = new List<IScreenMenu>();

            this.z_loadingManagerIsActive = true;
            this.addScreensToList();
            //this.z_previousStateQueue = new Queue<GameState>();
            
        }
        //Helper Method for adding all the screens and Menus to the list
        private void addScreensToList()
        {
            this.z_listScreen.Add(this.z_titleScreen);
            this.z_listScreen.Add(this.z_MainMenuScreen);
            this.z_listScreen.Add(this.z_MissionScreen);
            this.z_listScreen.Add(this.z_GameOverScreen);
        }


        //Accessors
        public GameState getCurrentGameState()
        {
            return this.z_currentGameState;
        }

        public GameState getPreviousGameState()
        {
            return this.z_previousGameState;
        }

        public TitleScreen getTitleScreen()
        {
            return this.z_titleScreen;
        }
        public MainMenuScreen getMainMenuScreen()
        {
            return this.z_MainMenuScreen;
        }
        public MissionScreen getMissionScreen()
        {
            return this.z_MissionScreen;
        }
        public List<IScreenMenu> getListScreen()
        {
            return this.z_listScreen;
        }
        public bool getLoadingManagerIsActive()
        {
            return this.z_loadingManagerIsActive;
        }

        //Mutators
        public void setCurrentGameState(GameState newState)
        {
            this.z_currentGameState = newState;
        }
        public void setPreviousGameState(GameState newState)
        {
            this.z_previousGameState = newState;
        }
        public void setTitleScreen(TitleScreen newScreen)
        {
            this.z_titleScreen = newScreen;
        }
        public void setMainMenuScreen(MainMenuScreen newScreen)
        {
            this.z_MainMenuScreen = newScreen;
        }
        public void setMissionScreen(MissionScreen newScreen)
        {
            this.z_MissionScreen = newScreen;
        }
        public void setLoadingManagerIsActive(bool isActive)
        {
            this.z_loadingManagerIsActive = isActive;
            if (!isActive)
                this.z_currentGameState = GameState.TitleMenu;
        }


        //Some Important Methods for the states
        public void UpdateKeyBoard(KeyboardState currentKeyState, KeyboardState previousKeyState, 
                                   GameTime gameTime, GamePadState currentPadState, GamePadState previousPadState)
        {
            this.z_gameTime = gameTime;
            //States will change into other states based on input
            switch (this.z_currentGameState)
            {
                case GameState.LoadingScreen:
                    {
                        //Do Nothing
                        break;
                    }
                case GameState.TitleMenu:
                    {
                        this.z_titleScreen.update(currentKeyState, previousKeyState);
                        if (previousKeyState.IsKeyUp(Keys.Enter) && currentKeyState.IsKeyDown(Keys.Enter))
                        {
                            //Then time to change game states
                            switch (this.z_titleScreen.getState())
                            {
                                case TitleScreen.TitleState.Exit:
                                    {
                                        this.z_game.Exit();
                                        break;
                                    }
                                case TitleScreen.TitleState.Options:
                                    {
                                        //Implement later
                                        break;
                                    }
                                case TitleScreen.TitleState.Start:
                                    {
                                        if (Gamer.SignedInGamers.Count == 0)
                                        {
                                            if (!Guide.IsVisible)
                                                Guide.ShowSignIn(1, false);
                                            //this.z_previousStateQueue.Enqueue(GameState.TitleMenu);
                                            this.z_previousGameState = this.z_currentGameState;
                                            //this.z_currentGameState = GameState.ProfileScreen;
                                            this.z_currentGameState = GameState.MainMenu;
                                        }
                                        else
                                        {
                                            this.z_previousGameState = this.z_currentGameState;
                                            //this.z_previousStateQueue.Enqueue(GameState.TitleMenu);
                                            this.z_currentGameState = GameState.MainMenu;
                                            this.z_MainMenuScreen.setCurrentState(MainMenuScreen.MainMenuState.Missions);
                                        }
                                            
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case GameState.MainMenu:
                    {
                        this.z_MainMenuScreen.update(currentKeyState, previousKeyState);
                        switch (this.z_MainMenuScreen.getCurrentState())
                        {
                            case MainMenuScreen.MainMenuState.Exit:
                                {
                                    this.z_previousGameState = this.z_currentGameState;
                                    //this.z_previousStateQueue.Clear();
                                    this.z_currentGameState = GameState.TitleMenu;
                                    this.z_MainMenuScreen.setCurrentState(MainMenuScreen.MainMenuState.Missions);
                                    break;
                                }
                            default:
                                {
                                    if (previousKeyState.IsKeyUp(Keys.Enter) && currentKeyState.IsKeyDown(Keys.Enter))
                                    {
                                        //Implement changing states later
                                        switch (this.z_MainMenuScreen.getCurrentState())
                                        {
                                            case MainMenuScreen.MainMenuState.Missions:
                                                {
                                                    //this.z_previousStateQueue.Enqueue(GameState.MainMenu);
                                                    //this.z_previousGameState = this.z_currentGameState;
                                                    this.z_currentGameState = GameState.MissionMenu;
                                                    this.z_MissionScreen.setCurrentState(MissionScreen.MissionMenuState.Mission1);

                                                    break;
                                                }
                                            case MainMenuScreen.MainMenuState.Ship:
                                                {

                                                    break;
                                                }
                                            case MainMenuScreen.MainMenuState.Store:
                                                {

                                                    break;
                                                }
                                            case MainMenuScreen.MainMenuState.Achievements:
                                                {

                                                    break;
                                                }
                                            case MainMenuScreen.MainMenuState.Options:
                                                {

                                                    break;
                                                }
                                            case MainMenuScreen.MainMenuState.Back:
                                                {
                                                    //if(this.z_previousStateQueue.Count > 0)
                                                       // this.z_currentGameState = this.z_previousStateQueue.Dequeue();
                                                    this.z_currentGameState = this.z_previousGameState;
                                                    this.z_previousGameState = this.z_currentGameState;
                                                    //This is probably not needed
                                                    //this.z_previousStateQueue.Enqueue(GameState.MainMenu);
                                                    break;
                                                }

                                        }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case GameState.MissionMenu:
                    {
                        this.z_MissionScreen.update(currentKeyState, previousKeyState);
                         if (previousKeyState.IsKeyUp(Keys.Enter) && currentKeyState.IsKeyDown(Keys.Enter))
                                    {
                                        switch (this.z_MissionScreen.getCurrentState())
                                        {
                                            case MissionScreen.MissionMenuState.Mission1:
                                                {
                                                    this.z_currentGameState = GameState.PlayingGame;

                                                    break;
                                                }
                                            case MissionScreen.MissionMenuState.Mission2:
                                                {



                                                    break;
                                                }
                                            case MissionScreen.MissionMenuState.Mission3:
                                                {




                                                    break;
                                                }
                                            case MissionScreen.MissionMenuState.Mission4:
                                                {



                                                    break;
                                                }
                                            case MissionScreen.MissionMenuState.Mission5:
                                                {



                                                    break;
                                                }
                                            case MissionScreen.MissionMenuState.Back:
                                                {
                                                    this.z_currentGameState = GameState.MainMenu;
                                                    break;
                                                }
                                        }


                                    }



                        break;
                    }
                case GameState.OptionsMenu:
                    {





                        break;
                    }
                case GameState.PlayingGame:
                    {
                        
                        this.z_StageManager.Update(currentKeyState, currentPadState, previousKeyState, previousPadState, gameTime);

                        if (this.z_StageManager.getIsGameOver())
                        {
                            this.z_StageManager.mainReset();
                            this.z_currentGameState = GameState.GameOverScreen;
                        }

                        break;
                    }
                case GameState.ProfileScreen:
                    {




                        break;
                    }
                case GameState.GameOverScreen:
                    {
                        if(currentKeyState.IsKeyDown(Keys.Enter))
                            this.z_currentGameState = GameState.MainMenu;


                        break;
                    }



            }
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            //based on the current state, draw the currect screen/menu
            switch (this.z_currentGameState)
            {
                case GameState.LoadingScreen:
                    {
                        this.z_loadingScreen.Draw(gametime, spriteBatch, this.z_viewPort);
                        break;
                    }
                case GameState.TitleMenu:
                    {
                        this.z_titleScreen.Draw(spriteBatch, this.z_viewPort);
                        break;
                    }
                case GameState.MainMenu:
                    {
                        this.z_MainMenuScreen.Draw(spriteBatch);
                        break;
                    }
                case GameState.MissionMenu:
                    {
                        this.z_MissionScreen.Draw(spriteBatch);
                        break;
                    }
                case GameState.PlayingGame:
                    {
                        this.z_StageManager.Draw(gametime);
                        break;
                    }
                case GameState.GameOverScreen:
                    {
                        this.z_GameOverScreen.Draw(spriteBatch);
                        break;
                    }
            }
            
        }


    }
}
