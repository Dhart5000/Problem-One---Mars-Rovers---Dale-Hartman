using Problem_One___Mars_Rovers___Dale_Hartman.Helpers;
using Problem_One___Mars_Rovers___Dale_Hartman.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Problem_One___Mars_Rovers___Dale_Hartman
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please provide the path to a text file with NASA's input:");
                Console.WriteLine("Press enter with no input to default to an \"Instructions.txt\" file located in the same directory as this executable.");
                string path = Console.ReadLine();

                if (String.IsNullOrEmpty(path))
                {
                    path = "Instructions.txt";
                }

                StreamReader sr;
                InstructionParser ip;
                int roverID = 1;
                List<Rover> roverList = new List<Rover>();

                try
                {
                    sr = new StreamReader(path);
                    ip = new InstructionParser(sr);

                    if (ip.ValidateAndParse() != 0)
                    {
                        Console.WriteLine("There was an issue validating the input from NASA.  Check the instructions for errors and try again.");
                        Console.WriteLine();
                        continue;
                    }

                    Plateau plateau = new Plateau(ip.GridCorner.xCoord, ip.GridCorner.yCoord);
                    foreach (var roverSet in ip.RoverInstructions)
                    {
                        int xCoord = roverSet.Start.xCoord;
                        int yCoord = roverSet.Start.yCoord;
                        Heading heading = roverSet.Start.heading;
                        string instructions = roverSet.Instructions;
                        roverList.Add(new Rover(xCoord, yCoord, heading, plateau, roverID, instructions));
                        roverID++;
                    }

                    foreach (Rover rover in roverList)
                    {
                        if (rover.Run() == 1)
                        {
                            Console.WriteLine("Rover " + rover.RoverID + " Ran into a problem on its route, it stopped in progress at:");
                        }
                        Console.WriteLine(rover.ReportCoordsAndHeading());

                    }
                }
                catch (Exception e)
                {
                    if (e is FileNotFoundException || e is DirectoryNotFoundException || e is IOException)
                    {
                        Console.WriteLine("Could not find the file or directory specified, please check the path provided.");
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            
        }
    }
}
