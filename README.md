# Problem-One---Mars-Rovers---Dale-Hartman

This is a small command line application targeting .NET Core 3.1, designed to complete problem 1: "Mars Rovers".

My design started with the model of this application.  I created two classes for the rovers and the plateau on which they'd be operating.  I made the assumption that the rover should be able to catch itself before making an invalid move (driving off the plateau), so each rover includes a reference to the plateau its driving on, allowing it to query the plateau for information about whether its safe to travel forward.  I did not attempt to keep track of other obstacles in each rover's path, and made the assumption that the plateau grid spaces were large enough for the rovers to maneuver around each other safely.

Error handling and input validation were two of my focuses when I started writing this application.  The InstructionParser class was designed with this in mind.  This class separates the task of parsing user input out of the main function, and includes helpful outputs to the console to provide descriptive feedback as to what problems are being encountered with the input, if any.  It also validates the input for the Model classes, meaning their methods aren't cluttered with nearly as many try-catch blocks.

With the rover class handling its instruction set on its own, and the InstructionParser class handling data validation, the program's Main function is kept to under 100 lines, only handling user input, passing data between objects, and calling each rover to complete its instructions in turn.

Unit testing was written using MSTest.  I wrote unit tests for methods as the methods were written, which allowed me to write the program's Main funciton last, once the models were already constructed and debugged.
