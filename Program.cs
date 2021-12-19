using System;
using board;
using chess;
using board.Exceptions;

namespace chessCS
{
    class Program
    {
        static void Main(string[] args)
        {   
            ChessGame match = new ChessGame();
            try{
                while(!match.Finished){
                    try{
                        Console.Clear();
                        match.Board.PrintBoard();

                        System.Console.WriteLine($"\nTurn: {match.Turn}");
                        System.Console.WriteLine($"Waiting for {match.ActualPlayer} to play...");
                        
                        if(match.Check){
                            System.Console.WriteLine("You are in check!");
                        }

                        System.Console.WriteLine();
                        System.Console.Write("Origin: ");
                        Position origin = ChessGame.ReadPosition();
                        match.ValidateOriginPosition(origin);

                        bool [,] possiblePositions = match.Board.GetPiece(origin).PossibleMoviments();

                        Console.Clear();
                        match.Board.PrintBoard(possiblePositions);
                        
                        System.Console.WriteLine();
                        System.Console.Write("Destination: ");
                        Position dest = ChessGame.ReadPosition();
                        
                        match.ValidateDestinationPosition(origin, dest);

                        match.Play(origin, dest);
                    }catch(ChessException e){
                        System.Console.WriteLine(e.Message + " press ENTER");
                        Console.Read();
                    }
                }
                System.Console.WriteLine("CHECKMATE");
                System.Console.WriteLine($"O vencedor foi: {match.ActualPlayer}");
            }catch(BoardException e){
                System.Console.WriteLine(e.Message);
            }catch(Exception e){
                System.Console.WriteLine("Unhandled exception:");
                System.Console.WriteLine(e);
            }
            
        }
    }
}
