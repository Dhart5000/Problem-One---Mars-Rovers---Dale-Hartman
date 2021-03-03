using System;
using System.Collections.Generic;
using System.Text;

namespace Problem_One___Mars_Rovers___Dale_Hartman.Model
{
    public class Plateau
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public Plateau(int maxX, int maxY)
        {
            this.MaxX = maxX;
            this.MaxY = maxY;
        }

        ///<summary>The Plateau checks to see if a particular set of X and Y coordinates is in its bounds
        /// </summary>
        public bool IsInBounds(int xCoord, int yCoord)
        {
            //coordinates must be betweeen 0 and the Plateau's max coordinate in that direction
            if ((xCoord <= MaxX && xCoord >= 0) && (yCoord <= MaxY && yCoord >= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
