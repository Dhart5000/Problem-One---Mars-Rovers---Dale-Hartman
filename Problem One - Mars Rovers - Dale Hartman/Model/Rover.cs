using System;
using System.Collections.Generic;
using System.Text;

namespace Problem_One___Mars_Rovers___Dale_Hartman.Model
{
    public class Rover
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public Heading Heading { get; set; }
        public Plateau Plateau { get; set; }
        public int RoverID { get; set; }
        public string Instructions { get; set; }

        public Rover(int xCoord, int yCoord, Heading heading, Plateau plateau, int roverID, string instructions)
        {
            XCoord = xCoord;
            YCoord = yCoord;
            Heading = heading;
            Plateau = plateau;
            RoverID = roverID;
            Instructions = instructions;
        }

        ///<summary>Asks the rover to look ahead to the location in front of it.
        ///Returns 0 if the location is safe to drive to, and 1 otherwise.
        /// </summary>
        public int Look()
        {
            bool isSafe;

            switch (Heading)
            {
                case Heading.N:
                    isSafe = Plateau.IsInBounds(XCoord, YCoord + 1);
                    break;
                case Heading.E:
                    isSafe = Plateau.IsInBounds(XCoord + 1, YCoord);
                    break;
                case Heading.S:
                    isSafe = Plateau.IsInBounds(XCoord, YCoord - 1);
                    break;
                case Heading.W:
                    isSafe = Plateau.IsInBounds(XCoord - 1, YCoord);
                    break;
                default:
                    isSafe = false;
                    break;
            }

            return isSafe ? 0 : 1;
        }

        ///<summary>Turns the rover left or right, changing its heading
        /// </summary>
        public void Turn(Turn turn)
        {
            //Use the underlying int values for our enums for heading and turns to quickly calculate the new heading.
            //Use a modulus funciton to handle 'wraparound' from West to North
            //Use an ugly if statement since % is actually a remainder operator, not a true modulus
            int rawSum = (int)this.Heading + (int)turn;
            int remainder = rawSum % 4;
            this.Heading = (Heading) (remainder < 0 ? remainder + 4 : remainder);
            
        }

        ///<summary>The Rover drives ahead to the grid space in front of it
        /// </summary>
        public void Drive()
        {
            switch (Heading)
            {
                case Heading.N:
                    this.YCoord += 1;
                    break;
                case Heading.E:
                    this.XCoord += 1;
                    break;
                case Heading.S:
                    this.YCoord -= 1;
                    break;
                case Heading.W:
                    this.XCoord -= 1;
                    break;
                default:
                    break;
            }
        }

        ///<summary>The Rover runs through its full validated instruction set, returning 0 if complete, or 1 if it encounters a problem along the way
        /// </summary>
        public int Run()
        {
            while (Instructions.Length > 0)
            {
                string nextCommand = Instructions.Substring(0, 1);

                if (nextCommand == "M")
                {
                    if (Look() == 0)
                    {
                        Drive();
                    }
                    else
                    {
                        Console.WriteLine("It is not safe to continue, stopping.");
                        return 1;
                    }
                }
                else if (nextCommand == "L" || nextCommand == "R")
                {
                    try
                    {
                        Turn((Turn)Enum.Parse(typeof(Turn), nextCommand));
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Failed to parse a turn instruction");
                        return 1;
                    }
                }
                else
                {
                    Console.WriteLine("Rover recieved bad instructions, did you validate input first?");
                    return 1;
                }

                Instructions = Instructions.Substring(1);
            }
            return 0;
        }

        ///<summary>Returns a string listing the rovers coordinates and heading
        /// </summary>
        public string ReportCoordsAndHeading()
        {
            return XCoord.ToString() + " " + YCoord.ToString() + " " + Enum.GetName(typeof(Heading), this.Heading);
        }
    }

    public enum Heading
    {
        N = 0,
        E = 1,
        S = 2,
        W = 3
    }

    public enum Turn
    {
        L = -1,
        R = 1
    }
}
