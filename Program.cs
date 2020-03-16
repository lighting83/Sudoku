using System;
using System.IO;
namespace Sudoku
{
	class Program
	{
		static void Main(string[] args)
		{
			int[,] sudoku = new int[9, 9];

			Console.WriteLine("Izaberi nacin unosa: (1-seed, 2-rucno) :"); int nacinUnosa = int.Parse(Console.ReadLine());

			if (nacinUnosa == 1)
			{
				int seed = 0;
				while (seed < 1 || seed > 1000000)
				{
					Console.WriteLine("Seed (1-1000000):"); seed = int.Parse(Console.ReadLine());
				}
				UcitajSudoku(sudoku, seed);
			}
			else if(nacinUnosa == 2)
			{
				string unos = Console.ReadLine();
				UcitajSudoku(sudoku, 0, unos);
			}

			Console.WriteLine("Zadatak:"); IspisiSudoku(sudoku);

			ResiSudoku(sudoku);
			
		}
		static void ResiSudoku(int[,] sudoku)
		{
			for (int i = 0; i < sudoku.GetLength(0); i++)
			{
				for (int j = 0; j < sudoku.GetLength(1); j++)
				{
					if (sudoku[i, j] == 0)
					{
						for (int n = 1; n < 10; n++)
						{
							if (MozeDaSePostavi(sudoku, n, i, j))
							{
								sudoku[i, j] = n;
								ResiSudoku(sudoku);
								sudoku[i, j] = 0;
							}
						}
						return;
					}
				}
			}
			Console.WriteLine("Resenje: "); IspisiSudoku(sudoku);
			return;
		}

		static bool MozeDaSePostavi(int[,] sudoku, int n, int x, int y)
		{
			/*if (sudoku[x, y] != 0)
				return false;
			*/
			for (int i = 0; i < sudoku.GetLength(0); i++)
			{
				if (sudoku[i, y] == n)
					return false;
			}

			for (int j = 0; j < sudoku.GetLength(0); j++)
			{
				if (sudoku[x, j] == n)
					return false;
			}

			int xKvadrant = x / 3;
			int yKvadrant = y / 3;

			for (int i = xKvadrant*3; i < (xKvadrant+1)*3; i++)
			{
				for (int j = yKvadrant*3; j < (yKvadrant+1)*3; j++)
				{
					if (sudoku[i, j] == n)
						return false;
				}
			}

			return true;
		}


		static void UcitajSudoku(int[,] sudoku, int seed = 0, string d = "")
		{
			string data = "";
			if (seed == 0)
				data = d;
			else
				data = UcitajLiniju(seed);

			//Console.WriteLine(data);
			for (int i = 0; i < sudoku.GetLength(0); i++)
			{
				for (int j = 0; j < sudoku.GetLength(1); j++)
				{
					sudoku[i, j] = (int)char.GetNumericValue(data[i * sudoku.GetLength(0) + j]);
				}
			}
		}
		
		static string UcitajLiniju(int seed)
		{
			var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "sudoku.txt");
			StreamReader reader = new StreamReader(filePath);

			for (int i = 0; i < seed; i++)
			{
				reader.ReadLine();
			}
			string data = reader.ReadLine();
			return data;
		}

		static void IspisiSudoku(int[,] sudoku)
		{
			for (int i = 0; i < sudoku.GetLength(0); i++)
			{
				for (int j = 0; j < sudoku.GetLength(1); j++)
				{
					Console.Write($"{sudoku[i, j]} ");
				}
				Console.WriteLine();
			}
		}

	}
}
