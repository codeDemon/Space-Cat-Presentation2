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
    class MissleManager
    {
        /*
         * 
         * The Missle Manager for the time being will only handle FriendlyMissles fired by the
         * human player. Later on the EnemyMissles should be implemented in a way such that missle 
         * manager can interact with enemy AI classes.
         * 
         * */


        //Instance Variables ---------------------------------------------------------
        private List<MissleObject> z_FriendlyMissles;
        private List<MissleObject> z_EnemyMissles;
        private Rectangle z_viewPort;
        private Texture2D z_friendlyMissleSprite1;
        private SoundEffect z_fireSound1;
        private List<IEnemyShip> z_EnemyShipList;
        //The Enemy Manager
        private EnemyManager z_enemyManager;
        private BulletPool1 z_bulletPool;
        public static MissleManager instanceOf;
        public Vector2 missleStartPosition;
        private PlayerShip z_playerShip;
        private EnemyBulletPool1 z_enemyBulletPool1;

        public static MissleManager getCurrent()
        {
            return instanceOf;
        }

        //Constructor ----------------------------------------------------------------
        public MissleManager(Rectangle newViewPort, ContentManager content, SoundEffect sound, SpriteBatch spriteBatch, PlayerShip playerShip)
        {
            this.z_playerShip = playerShip;
            this.z_enemyManager = EnemyManager.getInstance(content, spriteBatch, newViewPort);
            this.z_FriendlyMissles = new List<MissleObject>();
            this.z_EnemyMissles = new List<MissleObject>();
            this.z_viewPort = newViewPort;
            this.z_friendlyMissleSprite1 = content.Load<Texture2D>("Content\\Images\\Missles\\Ball1");
            this.z_fireSound1 = sound;
            this.z_EnemyShipList = new List<IEnemyShip>();
            this.z_bulletPool = new BulletPool1(content, this.z_viewPort, this.z_playerShip, spriteBatch);
            this.z_enemyBulletPool1 = new EnemyBulletPool1(content, this.z_viewPort, spriteBatch);
            instanceOf = this;
        }

        //Accessor Methods -----------------------------------------------------------
        public Rectangle getViewPort()
        {
            return this.z_viewPort;
        }
        public Texture2D getFriendlyMissleSprite1()
        {
            return this.z_friendlyMissleSprite1;
        }
        public SoundEffect getFireSound1()
        {
            return this.z_fireSound1;
        }

        //Mutator Methods ------------------------------------------------------------
        public void setViewPort(Rectangle viewPort)
        {
            this.z_viewPort = viewPort;
        }
        public void setFireSound1(SoundEffect newSound)
        {
            this.z_fireSound1 = newSound;
        }
        public void setFirendlyMissle1(Texture2D newSprite)
        {
            this.z_friendlyMissleSprite1 = newSprite;
        }

        //Update and Draw Methods --------------------------------------------------------------

        //Main Update Method for Keyboard
        public void MissleManagerUpdateFriendlyKeyboard(KeyboardState currentKeyState, KeyboardState previousKeyState,
                                                        PlayerShip playerShip, SpriteBatch spriteBatch)
        {
            this.z_EnemyShipList = this.z_enemyManager.getEnemiesList();
            //The Alogrithm:
            //Determine if the player shot a missle
            //If so then add it the List
            //If the list is not empty, update each missle
            //While checking each missle, make sure it hasn't left the screen or collided with something
            //If so, remove it from the list

            if (currentKeyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) && playerShip.getIsAlive())
            {
                //Play a fire sound
                this.z_fireSound1.Play(.2f, 0, 0);

                //Create and add a new Missle Object
                this.z_FriendlyMissles.Add(this.z_bulletPool.getNextAvailableEnemy());
                /*
                this.z_FriendlyMissles.Add(new PlayerMissle1(this.z_friendlyMissleSprite1,
                                                             new Vector2(playerShip.getPosition().X
                                                                         + playerShip.getSprite().Width / 2
                                                                         - this.z_friendlyMissleSprite1.Width / 2,
                                                                 playerShip.getPosition().Y), spriteBatch));
                 * */
            }

            this.MissleManagerUpdateEnemy();

            //If List is empty, nothing to update, exit this update function
            if (this.z_FriendlyMissles.Count <= 0)
                return;

            this.UpdateFriendlyList();
            
        }


        //Main Update Method for GamePad
        public void MissleManagerUpdateFriendlyGamepad(GamePadState currentPadState, GamePadState previousPadState,
                                                        PlayerShip playerShip, SpriteBatch spriteBatch)
        {
            //For the simple collision checking
            //this.z_EnemyShipList = enemyList;
            //Same Algorithm as before, but with a gamePad controller [Fire = right Trigger]
            if (currentPadState.Triggers.Right >= .5f && previousPadState.Triggers.Right == 0 && playerShip.getIsAlive())
            {
                this.z_FriendlyMissles.Add(new PlayerMissle1(this.z_friendlyMissleSprite1,
                                                             new Vector2(playerShip.getPosition().X
                                                                         + playerShip.getSprite().Width / 2
                                                                         - this.z_friendlyMissleSprite1.Width / 2,
                                                                 playerShip.getPosition().Y), spriteBatch));

                //Play a fire sound
                this.z_fireSound1.Play(.2f, 0, 0);
            }

            this.MissleManagerUpdateEnemy();
            //If List is empty, nothing to update, exit this update function
            if (this.z_FriendlyMissles.Count <= 0)
                return;

            this.UpdateFriendlyList();
           


        }


        //Helper Method for Updating all Missles in the FriendlyMissles List
        private void UpdateFriendlyList()
        {
            for (int i = 0; i < this.z_FriendlyMissles.Count; i++)
            {
                if (this.z_viewPort.Contains(this.z_FriendlyMissles[i].getHitRec()))
                {
                    this.z_FriendlyMissles[i].upDateMissle();
                }
                else
                {
                    this.z_FriendlyMissles[i].setIsAlive(false);
                    if (this.z_FriendlyMissles[i] is PlayerMissle1)
                    {

                        ((PlayerMissle1)this.z_FriendlyMissles[i]).setIsAvailable(true);
                    }
                    
                    this.z_FriendlyMissles.Remove(this.z_FriendlyMissles[i]);
                    //Since a Missle was just removed from the list, ensure i is poitning to the next missle
                    i--;
                    continue;
                }

                //Do some simple colision checking

                for (int j = 0; j < this.z_EnemyShipList.Count; j++)
                {
                    if (this.z_FriendlyMissles[i].getHitRec().Intersects(this.z_EnemyShipList[j].getHitRec()))
                    {
                        this.z_playerShip.score++;
                        this.z_FriendlyMissles[i].setIsAlive(false);
                        this.z_FriendlyMissles.Remove(this.z_FriendlyMissles[i]);
                        i--;


                        this.z_EnemyShipList[j].setIsAlive(false);
                        //this.z_EnemyShipList.RemoveAt(j);
                        if (i < 0)
                            break;
                        continue;
                        
                    }
                }


            }
        }

        public void fireEnemyMissle(Vector2 position, Texture2D sprite)
        {
            this.z_EnemyMissles.Add(this.z_enemyBulletPool1.FireNextAvailableMissle(position, sprite));
            //Play a fire sound
            this.z_fireSound1.Play(.2f, 0, 0);
        }

        //Main Update Method for enemy Missles
        public void MissleManagerUpdateEnemy()
        {
            for (int i = 0; i < this.z_EnemyMissles.Count; i++)
            {
                this.z_EnemyMissles[i].upDateMissle();

                if (this.z_playerShip.getHitRec().Intersects(this.z_EnemyMissles[i].getHitRec()))
                {
                    this.z_playerShip.setHealth(0);
                    if (this.z_EnemyMissles[i] is EnemyMissle1)
                        ((EnemyMissle1)this.z_EnemyMissles[i]).setIsAvailable(true);
                    this.z_EnemyMissles.RemoveAt(i);
                    i--;
                }

            }
        }

        //Main Draw Method for drawing all missles
        public void MissleManagerDrawAllMissles()
        {
            if (this.z_FriendlyMissles.Count > 0)
            {
                
                //That means there is something to draw in the missle list
                foreach (MissleObject missle in this.z_FriendlyMissles)
                {
                    missle.DrawMissle();
                }
            }

            if (this.z_EnemyMissles.Count > 0)
            {
                //That means there is something to draw in the missle list
                foreach (MissleObject missle in this.z_EnemyMissles)
                    missle.DrawMissle();
            }

        }



        public void reset()
        {
            for (int i = 0; i < this.z_EnemyMissles.Count; i++)
            {
                if (this.z_EnemyMissles[i] is EnemyMissle1)
                    ((EnemyMissle1)this.z_EnemyMissles[i]).setIsAvailable(true);
                this.z_EnemyMissles.RemoveAt(i);
            }
            for (int i = 0; i < this.z_FriendlyMissles.Count; i++)
            {
                if (this.z_FriendlyMissles[i] is PlayerMissle1)
                {
                    ((PlayerMissle1)this.z_FriendlyMissles[i]).setIsAvailable(true);
                }
                this.z_FriendlyMissles.RemoveAt(i);
            }
            this.z_FriendlyMissles = new List<MissleObject>();
            this.z_EnemyMissles = new List<MissleObject>();
        }











    }
}
