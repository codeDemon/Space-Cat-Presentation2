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
    class BulletPool1
    {
        //Instance Variables
        private List<PlayerMissle1> z_BulletPool;
        private ContentManager z_content;
        private Rectangle z_viewPort;
        private Texture2D z_image;
        private PlayerShip z_playerShip;
        private SpriteBatch z_spriteBatch;

        //Constructor
        public BulletPool1(ContentManager content, Rectangle viewPort, PlayerShip ship, SpriteBatch spriteBatch)
        {
            this.z_playerShip = ship;
            this.z_content = content;
            this.z_viewPort = viewPort;
            this.z_spriteBatch = spriteBatch;
            this.z_BulletPool = new List<PlayerMissle1>();
            this.z_image = z_content.Load<Texture2D>("Content\\Images\\Missles\\Ball1");

            for (int i = 0; i < 100; i++)
            {
                this.z_BulletPool.Add(new PlayerMissle1(this.z_image, this.z_playerShip.getPosition(), this.z_spriteBatch));
                this.z_BulletPool[i].setIsAvailable(true);
            }
        }

        //Accessor
        public PlayerMissle1 getNextAvailableEnemy()
        {
            foreach (PlayerMissle1 missle in this.z_BulletPool)
            {
                if (missle.getIsAvailable())
                {
                    missle.setIsAvailable(false);
                    missle.setPosition(new Vector2(this.z_playerShip.getPosition().X
                                                                         + z_playerShip.getSprite().Width / 2
                                                                         - missle.getSprite().Width / 2,
                                                                 z_playerShip.getPosition().Y));
                    missle.setIsAlive(true);
                    missle.upDateMissle();
                    return missle;
                }
            }
            //No enemies available from the pool, make a new one
            PlayerMissle1 missleTemp = new PlayerMissle1(z_content.Load<Texture2D>("Content\\Images\\Missles\\Ball1"), this.z_playerShip.getPosition(), this.z_spriteBatch);
            missleTemp.setPosition(new Vector2(this.z_playerShip.getPosition().X
                                                                         + z_playerShip.getSprite().Width / 2
                                                                         - missleTemp.getSprite().Width / 2,
                                                                 z_playerShip.getPosition().Y));
            missleTemp.setIsAlive(true);
            return missleTemp;

        }

    }
}
