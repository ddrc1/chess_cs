using board.Exceptions;
using System.Text;

namespace board
{
    public class Board
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        protected Piece[,] Pieces;

        public Board(int rows, int cols){
            Rows = rows;
            Cols = cols;
            Pieces = new Piece[Rows, Cols];
        }

        public void InsertPiece(Piece piece, Position pos){
            OccupiedPositionError(pos);
            Pieces[pos.Row, pos.Col] = piece;
            piece.Position = pos;
        }

        public Piece RemovePiece(Position pos){
            Piece removedPiece = GetPiece(pos);

            Pieces[pos.Row, pos.Col] = null;
            return removedPiece;

        }

        public bool IsValidPosition(Position pos){
            if(pos.Row >= Rows || pos.Row < 0 || pos.Col >= Cols || pos.Col < 0){
                return false;
            }
            return true;
        }

        public Piece GetPiece(Position pos){
            InvalidPositionError(pos);
            return Pieces[pos.Row, pos.Col];
        }

        private void InvalidPositionError(Position pos){
            if(!IsValidPosition(pos)){
                throw new BoardException("Invalid position!");
            }
        }

        private void OccupiedPositionError(Position pos){
            InvalidPositionError(pos);
            if(Pieces[pos.Row, pos.Col] != null){
                throw new BoardException("Position already occupied!");
            }
        }
    }
}