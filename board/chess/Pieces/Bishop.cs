using board;

namespace chess
{
    public class Bishop : Piece
    {
        public Bishop(Color color, Board board) : base(color, board){}


        private bool CanMove(Position pos){
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoviments(){
            bool[,] mat = new bool[Board.Rows, Board.Cols];
            
            //NE
            Position pos = new Position(Position.Row-1, Position.Col+1);
            while(Board.IsValidPosition(pos) && CanMove(pos)){
                mat[pos.Row, pos.Col] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color){
                    break;
                }
                pos.Row = pos.Row - 1;
                pos.Col = pos.Col + 1;
            }

            //SE
            pos = new Position(Position.Row-1, Position.Col+1);
            while(Board.IsValidPosition(pos) && CanMove(pos)){
                mat[pos.Row, pos.Col] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color){
                    break;
                }
                pos.Col = pos.Col + 1;
                pos.Row = pos.Row - 1;
            }

            //SO
            pos = new Position(Position.Row+1, Position.Col-1);
            while(Board.IsValidPosition(pos) && CanMove(pos)){
                mat[pos.Row, pos.Col] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color){
                    break;
                }
                pos.Row = pos.Row + 1;
                pos.Col = pos.Col - 1;
            }

            //NO
            pos = new Position(Position.Row-1, Position.Col-1);
            while(Board.IsValidPosition(pos) && CanMove(pos)){
                mat[pos.Row, pos.Col] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color){
                    break;
                }
                pos.Col = pos.Col - 1;
                pos.Row = pos.Row - 1;
            }
             
            return mat;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}