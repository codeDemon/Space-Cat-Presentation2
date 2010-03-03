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
    abstract class IEnemyShip : GameObject
    {
        public IEnemyShip(Texture2D loadedSprite)
            : base(loadedSprite)
        {

        }
        //This method should be calaulated using some sort of AI
        abstract public void AIUpdate(GameTime gameTime);

        //Methods for isAvailable
        abstract public bool getIsAvailable();
        abstract public void setIsAvailable(bool isAvailable);
        //abstract public bool readyToFire();

        
    }
}
