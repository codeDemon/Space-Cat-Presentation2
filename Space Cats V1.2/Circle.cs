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
    class Circle
    {
        //Variables
        private Vector2 z_center;
        private float z_radius;

        //Constructor
        public Circle(Vector2 position, float size)
        {
            this.z_center = position;
            this.z_radius = size;
        }

        //Intersection between two circles
        public bool intersects(Circle circleOther)
        {
            //Method 2
            //Get the distance between the two circles
            //Return true if the distance is less than or equal to the bigger radius
             return (Vector2.Distance(z_center, circleOther.getCenter()) <= Math.Max(z_radius, circleOther.z_radius));


            //Method 1
            //Calculate the distance between the two circles.
            //Distance = squareRoot( (X1 - X2)^2 + (Y1 - Y2)^2 )
            //Check that the distance is less than than the sum of both radii
            //If it is, then there is a collision

            /*
            return (Math.Sqrt(((this.z_center.X - circleOther.getCenter().X) *
                            (this.z_center.X - circleOther.getCenter().X)) +
                            ((this.z_center.Y - circleOther.getCenter().Y) *
                            (this.z_center.Y - circleOther.getCenter().Y))) <
                            (this.z_radius + circleOther.z_radius));
             * */
            
        }

        //Intersection between this circle and a Rectangle
        public bool intersects(Rectangle rectangleOther)
        {
            //Check the distance between the center of the circle against each corner of the rectangle

            //check upper left corner
            //check upper right corner
            //check bottom left corner
            //check bottom right corner
            return (Vector2.Distance(this.z_center, new Vector2(rectangleOther.Left, rectangleOther.Top)) < this.z_radius ||
                    Vector2.Distance(this.z_center, new Vector2(rectangleOther.Right, rectangleOther.Top)) < this.z_radius ||
                    Vector2.Distance(this.z_center, new Vector2(rectangleOther.Left, rectangleOther.Bottom)) < this.z_radius ||
                    Vector2.Distance(this.z_center, new Vector2(rectangleOther.Right, rectangleOther.Bottom)) < this.z_radius );

        }

        //Accessors
        public Vector2 getCenter()
        {
            return this.z_center;
        }
        public float getRadius()
        {
            return this.z_radius;
        }

        //Mutators
        public void setCenter(Vector2 newCenter)
        {
            this.z_center = newCenter;
        }

        public void setRadius(float newRadius)
        {
            this.z_radius = newRadius;
        }

    }
}
