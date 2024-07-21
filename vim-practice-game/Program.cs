using System;
using System.Diagnostics;

class VIMPracticeApp
{
	static Random random = new Random();

	static void Main()
	{
		while (true)
		{
			DisplayMainMenu();
			char choice = Console.ReadKey().KeyChar;

			switch (choice)
			{
				case '1':
					StartTargetGame();
					break;
				case '2':
					ExitApplication();
					return;
				default:
					DisplayInvalidOptionMessage();
					break;
			}
		}
	}

	static void DisplayMainMenu()
	{
		string text = @"
         __      ___                 
           \ \    / (_)                
            \ \  / / _ _ __ ___        
             \ \/ / | | '_ ` _ \       
              \  /  | | | | | | |      
               \/   |_|_| |_| |_|      
  _____                __  _
 |  __ \              | | (_)          
 | |__) | __ __ _  ___| |_ _  ___ ___  
 |  ___/ '__/ _` |/ __| __| |/ __/ _ \ 
 | |   | | | (_| | (__| |_| | (_|  __/ 
 |_|   |_|  \__,_|\___|\__|_|\___\___| 

           _____                       
          / ____|                      
         | |  __  __ _ _ __ ___   ___  
         | | |_ |/ _` | '_ ` _ \ / _ \ 
         | |__| | (_| | | | | | |  __/ 
          \_____|\__,_|_| |_| |_|\___| 
                                       
                                    ";

		Console.Clear();
		Console.WriteLine(text);
		Console.WriteLine("Please select a game:");
		Console.WriteLine("1. Target Game");
		Console.WriteLine("2. Exit");
	}

	static void StartTargetGame()
	{
		const int width = 10;
		const int height = 10;
		int playerX = 0;
		int playerY = 0;
		int targetX = GetRandomPosition(width);
		int targetY = GetRandomPosition(height);
		int score = 0;
		const int maxScore = 10;

		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();

		while (score < maxScore)
		{
			Console.Clear();
			RenderGameBoard(width, height, playerX, playerY, targetX, targetY);
			DisplayScore(score, maxScore);
			char command = GetUserCommand();

			ProcessCommand(ref playerX, ref playerY, ref score, ref targetX, ref targetY, command);
		}

		stopwatch.Stop();
		DisplayGameResults(score, stopwatch.Elapsed);
	}

	static void RenderGameBoard(int width, int height, int playerX, int playerY, int targetX, int targetY)
	{
		int offsetX = (Console.WindowWidth - (width * 2)) / 2;
		int offsetY = (Console.WindowHeight - (height + 2)) / 2;

		for (int y = 0; y < height; y++)
		{
			Console.SetCursorPosition(offsetX, offsetY + y);
			for (int x = 0; x < width; x++)
			{
				if (x == playerX && y == playerY)
				{
					Console.Write("X ");
				}
				else if (x == targetX && y == targetY)
				{
					Console.Write("T ");
				}
				else
				{
					Console.Write(". ");
				}
			}
		}
	}

	static void DisplayScore(int score, int maxScore)
	{
		Console.SetCursorPosition(0, Console.WindowHeight - 2);
		Console.WriteLine($"Target Count: {score}/{maxScore}");
		Console.WriteLine("Enter command (h, j, k, l) or press 'X' on target:");
	}

	static char GetUserCommand()
	{
		return Console.ReadKey().KeyChar;
	}

	static void ProcessCommand(ref int playerX, ref int playerY, ref int score, ref int targetX, ref int targetY, char command)
	{
		switch (command)
		{
			case 'h':
				if (playerX > 0) playerX--;
				break;
			case 'j':
				if (playerY < 9) playerY++;
				break;
			case 'k':
				if (playerY > 0) playerY--;
				break;
			case 'l':
				if (playerX < 9) playerX++;
				break;
			case 'x':
			case 'X':
				if (playerX == targetX && playerY == targetY)
				{
					score++;
					targetX = GetRandomPosition(10);
					targetY = GetRandomPosition(10);
				}
				else
				{
					Console.WriteLine("\nInvalid position! You can't press 'X' when not on the target.");
				}
				break;
			default:
				Console.WriteLine("\nInvalid command!");
				break;
		}
	}

	static int GetRandomPosition(int max)
	{
		return random.Next(max);
	}

	static void DisplayGameResults(int score, TimeSpan timeTaken)
	{
		double finalScore = CalculateScoreBasedOnTime(timeTaken);

		Console.Clear();
		Console.SetCursorPosition(0, Console.WindowHeight / 2);
		Console.WriteLine("Congratulations! You completed all targets.");
		Console.WriteLine($"Time Taken: {timeTaken.TotalSeconds:F2} seconds");
		Console.WriteLine($"Your Score: {finalScore:F2}");
		Console.WriteLine("Press any key to return to the main menu...");
		Console.ReadKey();
	}

	static double CalculateScoreBasedOnTime(TimeSpan timeTaken)
	{
		const double maxScore = 100.0;
		double timeInSeconds = timeTaken.TotalSeconds;
		double score = maxScore - (timeInSeconds - 30) * 2;

		return Math.Max(0, score);
	}

	static void ExitApplication()
	{
		Console.WriteLine("\nExiting...");
	}

	static void DisplayInvalidOptionMessage()
	{
		Console.WriteLine("\nInvalid option! Please try again.");
	}
}
