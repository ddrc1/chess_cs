using System.Text;
using board;
using System;

namespace chess
{
    public class ChessBoard : Board
    {
        public ChessBoard() : base(8, 8){}

        public void PrintBoard()
        {
            for (int i = 0; i < Rows; i++)
            {   
                System.Console.Write($"{Cols - i} ");
                for (int j = 0; j < Cols; j++)
                {   
                    if(Pieces[i, j] == null){
                        System.Console.Write("- ");
                    }else{
                        if(Pieces[i, j].Color == Color.BLACK){
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            System.Console.Write($"{Pieces[i, j]} ");
                            Console.ResetColor();
                        }else{
                            System.Console.Write($"{Pieces[i, j]} ");
                        }
                    }
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  A B C D E F G H");
        }

        public void PrintBoard(bool[,] possiblePositions){
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            ConsoleColor alteredBackgroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < Rows; i++)
            {   
                System.Console.Write($"{Cols - i} ");
                for (int j = 0; j < Cols; j++)
                {   
                    if(possiblePositions[i, j]){
                        Console.BackgroundColor = alteredBackgroundColor;
                    }else{
                        Console.BackgroundColor = originalBackgroundColor;
                    }

                    if(Pieces[i, j] == null){
                        System.Console.Write("- ");
                    }else{
                        if(Pieces[i, j].Color == Color.BLACK){
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            System.Console.Write($"{Pieces[i, j]} ");
                            Console.ResetColor();
                        }else{
                            System.Console.Write($"{Pieces[i, j]} ");
                        }
                    }
                    Console.ResetColor();
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  A B C D E F G H");
        }
    }
}