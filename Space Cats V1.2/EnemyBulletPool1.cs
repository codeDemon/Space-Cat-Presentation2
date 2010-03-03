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
    class EnemyBulletPool1
    {
        //Instance Variables
        private List<EnemyMissle1> z_BulletPool;
        private ContentManager z_content;
        private Rectangle z_viewPort;
        private Texture2D z_image;
        private SpriteBatch z_spriteBatch;

        //Constructor
        public EnemyBulletPool1(ContentManager content, Rectangle viewPort, SpriteBatch spriteBatch)
        {
            this.z_content = content;
            this.z_viewPort = viewPort;
            this.z_spriteBatch = spriteBatch;
            this.z_BulletPool = new List<EnemyMissle1>();
            this.z_image = z_content.Load<Texture2D>("Content\\Images\\Missles\\Ball1");

            for (int i = 0; i < 100; i++)
            {
                this.z_BulletPool.Add(new EnemyMissle1(this.z_image, Vector2.Zero, this.z_spriteBatch));
                this.z_BulletPool[i].setIsAvailable(true);
            }
        }

        //Accessor
        public EnemyMissle1 FireNextAvailableMissle(Vector2 position, Texture2D sprite)
        {
            foreach (EnemyMissle1 missle in this.z_BulletPool)
            {
                if (missle.getIsAvailable())
                {
                    missle.setIsAvailable(false);
                    missle.setPosition(new Vector2(position.X + sprite.Width / 2 - missle.getSprite().Width / 2, position.Y + sprite.Height));
                    missle.setIsAlive(true);
                    missle.upDateMissle();
                    return missle;
                }
            }
            //No enemies available from the pool, make a new one
            EnemyMissle1 missleTemp = new EnemyMissle1(z_content.Load<Texture2D>("Content\\Images\\Missles\\Ball1"), position, this.z_spriteBatch);
            missleTemp.setPosition(new Vector2(position.X + sprite.Width / 2 - missleTemp.getSprite().Width / 2, position.Y));
            missleTemp.setIsAlive(true);
            return missleTemp;

        }

    





    }
}
