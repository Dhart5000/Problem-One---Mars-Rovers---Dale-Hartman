using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Problem_One___Mars_Rovers___Dale_Hartman.Model;
using System.Linq;

namespace Problem_One___Mars_Rovers___Dale_Hartman.Helpers
{
    public class InstructionParser
    {
        public StreamReader InstructionFileStream { get; set; }

        public (int xCoord,int yCoord) GridCorner { get; set; }
        public List<((int xCoord, int yCoord, Heading heading) Start, string Instructions)> RoverInstructions { get; set; } = new List<((int xCoord, int yCoord, Heading heading) Start, string Instructions)> { };

        public InstructionParser(StreamReader file)
        {
            this.InstructionFileStream = file;
        }

        ///<summary>Scans the input file, checking to make sure all provided setup and instructions are valid.
        ///Returns 0 if there are no issues, returns 1 and writes to Standard Out if there are inconsistencies.
        /// </summary>
        public int ValidateAndParse()
        {
            string readLine = null;

            string roverStart = null;
            string roverInstructions = null;

            //Cut out any empty lines at file start, then read the Plateau data
            do
            {
                readLine = InstructionFileStream.ReadLine().Trim();
                if (readLine == "") { continue; }
                if (!ValidateAndParseGridCorner(readLine))
                {
                    return 1;
                }
                else
                {
                    break;
                }
            } while (true);

            do
            {
                readLine = InstructionFileStream.ReadLine();
                roverStart = readLine;

                readLine = InstructionFileStream.ReadLine();
                roverInstructions = readLine;

                //If we've reached end of file...
                if(roverStart == null && roverInstructions == null)
                {
                    //Instructions for 0 rovers is an error
                    if(RoverInstructions.Count == 0)
                    {
                        Console.WriteLine("Found no instruction sets for Rovers");
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }

                if (!ValidateAndParseRoverInstructions(roverStart, roverInstructions))
                {
                    return 1;
                }

            } while (true);
        }

        private bool ValidateAndParseGridCorner(string input)
        {
            string[] coordStrings = input.Split(' ');
            int xCoord;
            int yCoord;
            if (coordStrings.Length != 2)
            {
                Console.WriteLine("Did not find exactly 2 values when trying to parse grid corners");
                return false;
            }

            try
            {
                xCoord = Int32.Parse(coordStrings[0]);
                yCoord = Int32.Parse(coordStrings[1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Failed to parse grid corner input to an integer, check that there are no text characters or punctuation");
                return false;
            }

            if (xCoord > 0 && yCoord > 0)
            {
                GridCorner = (xCoord, yCoord);
                return true;
            }
            else
            {
                Console.WriteLine("Grid Coordinates must be positive, nonzero integers");
                return false;
            }
            
        }

        private bool ValidateAndParseRoverInstructions(string roverStart, string roverInstructionsString)
        {
            if (string.IsNullOrEmpty(roverStart) || string.IsNullOrEmpty(roverInstructionsString))
            {
                Console.WriteLine("There is an incomplete rover instruction set in the input, or there are empty lines in the input");
                return false;
            }

            string[] startStrings = roverStart.Split(' ');
            int xCoord;
            int yCoord;
            Heading heading;

            if(startStrings.Length != 3)
            {
                Console.WriteLine("Did not find exactly 3 values when trying to parse a rover's start location");
                return false;
            }

            try
            {
                xCoord = Int32.Parse(startStrings[0]);
                yCoord = Int32.Parse(startStrings[1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Failed to parse rover start input to an integer, check that there are no text characters or punctuation");
                return false;
            }
            try
            {
                heading = (Heading) Enum.Parse(typeof(Heading), startStrings[2]);
            }
            catch (ArgumentException  e)
            {
                Console.WriteLine("Failed to parse rover heading, this system only accepts cardinal compass directions as input");
                return false;
            }

            if (xCoord <= 0 || yCoord <= 0)
            {
                Console.WriteLine("Grid Coordinates must be positive, nonzero integers");
                return false;
            }

            roverInstructionsString = roverInstructionsString.Trim().ToUpper();
            if (roverInstructionsString.All(c => "LRM".Contains(c)))
            {
                RoverInstructions.Add(((xCoord, yCoord, heading), roverInstructionsString));
                return true;
            }
            else
            {
                Console.WriteLine("There are undefined instrucions in the rover's instruction set, only accepts L, R, and M");
                return false;
            }
        }
    }
}
