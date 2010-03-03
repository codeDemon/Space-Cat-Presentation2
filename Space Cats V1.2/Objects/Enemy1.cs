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
    class Enemy1 : IEnemyShip
    {
        //Instance Variables
        private AI_ZigZag z_AI;
        private bool z_isAvailable;
        public float fireTime;
        public float fireCoolOff;

        //Constructor
        public Enemy1(Texture2D loadedSprite, Rectangle viewPort)
            : base(loadedSprite)
        {
            z_AI = new AI_ZigZag(viewPort);
            this.setPosition(this.z_AI.getStartingPosition());
            this.z_isAvailable = true;
            this.fireTime = 0;
            this.fireCoolOff = 1000;
        }

        //Accessors

        //Mutators











        public override void AIUpdate(GameTime gameTime)
        {
            Random gen = new Random();
            float time = (float)gameTime.TotalGameTime.TotalMilliseconds;
            if (time > fireTime)
            {
                fireTime = time + this.fireCoolOff;
                MissleManager.getCurrent().fireEnemyMissle(this.getPosition(), this.getSprite());
            }
            this.fireCoolOff = MathHelper.Lerp(1000, 5000, (float)gen.NextDouble());
            if (this.z_AI.okToRemove())
            {
                this.setIsAlive(false);
                return;
            }

            this.setVelocity(this.z_AI.calculateNewVelocity(this.getPosition(), gameTime));

            this.upDatePosition();
            this.setHitRec(new Rectangle((int)this.getPosition().X, (int)this.getPosition().Y,
                          (int)this.getSprite().Width, (int)this.getSprite().Height));
        }

        public override bool getIsAvailable()
        {
            return this.z_isAvailable;
        }

        public override void setIsAvailable(bool isAvailable)
        {
            this.z_isAvailable = isAvailable;
        }

        public void reset()
        {
            this.setPosition(Vector2.Zero);
            this.setVelocity(Vector2.Zero);
            this.setSpeed(1.0f);
            this.setIsAlive(false);
            this.setHitRec(new Rectangle(0, 0, 0, 0));
            this.setIsKillerObject(false);
            this.setIsPickUp(false);
            this.z_isAvailable = true;
        }
    }
}
