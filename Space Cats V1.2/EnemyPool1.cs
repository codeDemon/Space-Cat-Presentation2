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
    class EnemyPool1
    {
        //Instance Variables
        private List<Enemy1> z_EnemyPool;
        private ContentManager z_content;
        private Rectangle z_viewPort;
        private Texture2D z_image;

        //Constructor
        public EnemyPool1(ContentManager content, Rectangle viewPort)
        {
            this.z_content = content;
            this.z_viewPort = viewPort;
            this.z_EnemyPool = new List<Enemy1>();
            this.z_image = z_content.Load<Texture2D>("Content\\Images\\EnemyShips\\EnemyShip1");

            for (int i = 0; i < 100; i++)
            {
                this.z_EnemyPool.Add(new Enemy1(this.z_image,viewPort));
                this.z_EnemyPool[i].setIsAvailable(true);
            }
        }

        //Accessor
        public Enemy1 getNextAvailableEnemy()
        {
            foreach (Enemy1 enemy in this.z_EnemyPool)
            {
                if (enemy.getIsAvailable())
                {
                    enemy.setIsAvailable(false);
                    return enemy;
                }
            }
            //No enemies available from the pool, make a new one
            return new Enemy1(z_content.Load<Texture2D>("Images\\EnemyShips\\EnemyShip1"),this.z_viewPort);
        }








    }
}
