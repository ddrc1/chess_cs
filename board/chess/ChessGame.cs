using board;
using System;
using board.Exceptions;
using System.Collections.Generic;

namespace chess
{
    public class ChessGame
    {
        public ChessBoard Board { get; private set; }  
        public int Turn { get; private set; } 
        public Color ActualPlayer { get; private set; }
        public bool Finished { get; set; }
        private List<Piece> Pieces;
        public bool Check { get; private set; }
        public Peon EmPassantVulnerable { get; set; }

        public ChessGame(){
            Board = new ChessBoard();
            ActualPlayer = Color.WHITE;
            Turn = 1;
            Finished = false;
            Pieces = new List<Piece>();
            InsertPieces();
        }

        public static ChessPosition ReadPosition(){
            string s = Console.ReadLine();
            char col = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(col, row);
        }

        public void Play(Position origin, Position dest){
            Piece captured = ExecuteMoviment(origin, dest);
            if(IsInCheck(ActualPlayer)){
                UndoMoviment(origin, dest, captured);
                throw new ChessException("You put yourself in check!");
            }

            if(IsInCheck(Opponent(ActualPlayer))){
                Check = true;
            }else{
                Check = false;
            }

            if(IsCheckMate(Opponent(ActualPlayer))){
                Finished = true;
            }else{
                Turn ++;
                if(ActualPlayer == Color.WHITE){
                    ActualPlayer = Color.BLACK;
                }else{
                    ActualPlayer = Color.WHITE;
                }
            }

            Piece p = Board.GetPiece(dest);

            // Special move en passant
            if(p is Peon && (dest.Row == origin.Row-2 || dest.Row == origin.Row+2)){
                EmPassantVulnerable = (Peon) p;
            }

            // Special move promotion
            if(p is Peon){
                if((p.Color == Color.WHITE && dest.Row == 0) || (p.Color == Color.BLACK && dest.Row == 7)){
                    p = Board.RemovePiece(dest);
                    Pieces.Remove(p);
                    Piece queen = new Queen(p.Color, Board);
                    InsertNewPiece(queen, p.Position);
                }
            }
        }

        public void ValidateOriginPosition(Position pos){
            UnoccupiedPositionError(pos);
            WrongPlayerTurnError(pos);
            InexistentMovimentsAvailable(pos);
        }

        public void ValidateDestinationPosition(Position origin, Position dest){
            if(!Board.GetPiece(origin).CanMoveTo(dest)){
                throw new ChessException("Invalid Destination Position!");
            }
        }

        public bool IsInCheck(Color color){
            Piece? king = null;
            foreach (Piece p in Pieces){
                if(p is King && p.Color == color){
                    king = p;
                    System.Console.WriteLine(king.Position);
                    System.Console.WriteLine(king.Color);
                    break;
                }
            }

            foreach (Piece p in Pieces){
                bool[,] possibleMoviments = p.PossibleMoviments();
                if(possibleMoviments[king.Position.Row, king.Position.Col]){
                    return true;
                }
            }

            return false;
        }

        private bool IsCheckMate(Color color){
            if(IsInCheck(color)){
                return false;
            }

            foreach (Piece p in Pieces){
                if(p.Color == color){
                    bool[,] possibleMoviments = p.PossibleMoviments();
                    for (int i = 0; i < Board.Rows; i++){
                        for (int j = 0; j < Board.Cols; j++){
                            if (possibleMoviments[i, j]){
                                Position origin = p.Position;
                                Position dest = new Position(i, j);
                                Piece captured = ExecuteMoviment(origin, dest);
                                UndoMoviment(origin, dest, captured);
                                bool testCheck = IsInCheck(ActualPlayer);
                                if(!testCheck){
                                    return false;
                                } 
                            }
                        }
                            
                    }
                }                
            }

            return false;
        }

        private void UndoMoviment(Position origin, Position dest, Piece captured){
            Piece p = Board.RemovePiece(dest);
            p.DecreaseMoviment();
            if(captured != null){
                InsertNewPiece(captured, dest);
            }

            // special move little roque
            if (p is King && dest.Col == origin.Col + 2){
                Position originT = new Position(origin.Row, origin.Col + 3);
                Position destT = new Position(dest.Row, origin.Col + 1);
                Piece T = Board.RemovePiece(destT);
                T.DecreaseMoviment();
                Board.InsertPiece(T, originT);
            }

            // special move big roque
            if (p is King && dest.Col == origin.Col - 2){
                Position originT = new Position(origin.Row, origin.Col - 4);
                Position destT = new Position(dest.Row, origin.Col - 1);
                Piece T = Board.RemovePiece(destT);
                T.IncreaseMoviment();
                Board.InsertPiece(T, originT);
            }

            // special move en passant
            if(p is Peon){
                if(origin.Col != dest.Col && captured == EmPassantVulnerable){
                    Piece peon = Board.RemovePiece(dest);
                    Position posP;
                    if(p.Color == Color.WHITE){
                        posP = new Position(3, dest.Col);
                    }else{
                        posP = new Position(4, dest.Col);
                    }
                    Board.InsertPiece(peon, posP);
                }
            }

            InsertNewPiece(p, origin);
        }

        private Color Opponent(Color color){
            if(color == Color.WHITE){
                return Color.BLACK;
            }

            return Color.WHITE;
        }
        
        private Piece ExecuteMoviment(Position origin, Position dest){
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMoviment();
            Piece captured = Board.RemovePiece(dest);
            if(captured != null){
                Pieces.Remove(captured);
            }

            InsertNewPiece(p, dest);
            p.Position = dest;

            // special move little roque
            if (p is King && dest.Col == origin.Col + 2){
                Position originT = new Position(origin.Row, origin.Col + 3);
                Position destT = new Position(dest.Row, origin.Col + 1);
                Piece T = Board.RemovePiece(originT);
                T.IncreaseMoviment();
                Board.InsertPiece(T, destT);
            }

            // special move big roque
            if (p is King && dest.Col == origin.Col - 2){
                Position originT = new Position(origin.Row, origin.Col - 4);
                Position destT = new Position(dest.Row, origin.Col - 1);
                Piece T = Board.RemovePiece(originT);
                T.IncreaseMoviment();
                Board.InsertPiece(T, destT);
            }

            // special move en passant
            if(p is Peon){
                if(origin.Col != dest.Col && captured == null){
                    Position posP;
                    if(p.Color == Color.WHITE){
                        posP = new Position(dest.Row+1, dest.Col);
                    }else{
                        posP = new Position(dest.Row-1, dest.Col);
                    }
                    captured = Board.RemovePiece(posP);
                }
            }

            return captured;
        }

        private void InsertNewPiece(Piece piece, Position pos){
            Board.InsertPiece(piece, pos);
            Pieces.Add(piece);
        }

        private void InsertPieces(){
            InsertNewPiece(new Peon(Color.BLACK, Board, this), new ChessPosition('A', 7));
            InsertNewPiece(new Peon(Color.BLACK, Board, this), new ChessPosition('B', 7));
            InsertNewPiece(new Peon(Color.BLACK, Board, this), new ChessPosition('C', 7));
            InsertNewPiece(new Peon(Color.BLACK, Board, this), new ChessPosition('D', 7));
            InsertNewPiece(new Peon(Color.BLACK, Board, this), new ChessPosition('E', 7));
            InsertNewPiece(new Peon(Color.BLACK, Board, this), new ChessPosition('F', 7));
            InsertNewPiece(new Peon(Color.BLACK, Board, this), new ChessPosition('G', 7));
            InsertNewPiece(new Peon(Color.BLACK, Board, this), new ChessPosition('H', 7));

            InsertNewPiece(new Tower(Color.BLACK, Board), new ChessPosition('A', 8));
            InsertNewPiece(new Horse(Color.BLACK, Board), new ChessPosition('B', 8));
            InsertNewPiece(new Bishop(Color.BLACK, Board), new ChessPosition('C', 8));
            InsertNewPiece(new King(Color.BLACK, Board, this), new ChessPosition('D', 8));
            InsertNewPiece(new Queen(Color.BLACK, Board), new ChessPosition('E', 8));
            InsertNewPiece(new Bishop(Color.BLACK, Board), new ChessPosition('F', 8));
            InsertNewPiece(new Horse(Color.BLACK, Board), new ChessPosition('G', 8));
            InsertNewPiece(new Tower(Color.BLACK, Board), new ChessPosition('H', 8));

            InsertNewPiece(new Tower(Color.WHITE, Board), new ChessPosition('A', 1));
            InsertNewPiece(new Horse(Color.WHITE, Board), new ChessPosition('B', 1));
            InsertNewPiece(new Bishop(Color.WHITE, Board), new ChessPosition('C', 1));
            InsertNewPiece(new King(Color.WHITE, Board, this), new ChessPosition('D', 1));
            InsertNewPiece(new Queen(Color.WHITE, Board), new ChessPosition('E', 1));
            InsertNewPiece(new Bishop(Color.WHITE, Board), new ChessPosition('F', 1));
            InsertNewPiece(new Horse(Color.WHITE, Board), new ChessPosition('G', 1));
            InsertNewPiece(new Tower(Color.WHITE, Board), new ChessPosition('H', 1));

            InsertNewPiece(new Peon(Color.WHITE, Board, this), new ChessPosition('A', 2));
            InsertNewPiece(new Peon(Color.WHITE, Board, this), new ChessPosition('B', 2));
            InsertNewPiece(new Peon(Color.WHITE, Board, this), new ChessPosition('C', 2));
            InsertNewPiece(new Peon(Color.WHITE, Board, this), new ChessPosition('D', 2));
            InsertNewPiece(new Peon(Color.WHITE, Board, this), new ChessPosition('E', 2));
            InsertNewPiece(new Peon(Color.WHITE, Board, this), new ChessPosition('F', 2));
            InsertNewPiece(new Peon(Color.WHITE, Board, this), new ChessPosition('G', 2));
            InsertNewPiece(new Peon(Color.WHITE, Board, this), new ChessPosition('H', 2));
        }

        private void WrongPlayerTurnError(Position pos){
            if(ActualPlayer != Board.GetPiece(pos).Color){
                throw new ChessException($"It's {ActualPlayer} turn");
            }
        }

        private void UnoccupiedPositionError(Position pos){
            if(Board.GetPiece(pos) == null){
                throw new ChessException("Position unoccupied!");
            }
        }

        private void InexistentMovimentsAvailable(Position pos){
            if(!Board.GetPiece(pos).AreThereAvailableMoviments()){
                throw new ChessException("Position unoccupied!");
            }
        }
    }
}