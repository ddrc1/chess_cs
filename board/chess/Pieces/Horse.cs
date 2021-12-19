using board;

namespace chess
{
    public class Horse : Piece
    {
        public Horse(Color color, Board board) : base(color, board){}

        public bool canMove(Position pos){
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoviments(){
            bool[,] mat = new bool[Board.Rows, Board.Cols];
            
            Position pos = new Position(Position.Row-1, Position.Col-2);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            pos = new Position(Position.Row-2, Position.Col+1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            pos = new Position(Position.Row-1, Position.Col+2);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            pos = new Position(Position.Row+1, Position.Col+2);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            pos = new Position(Position.Row+2, Position.Col+1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            pos = new Position(Position.Row+2, Position.Col-1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //O
            pos = new Position(Position.Row+1, Position.Col-2);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //NO
            pos = new Position(Position.Row-1, Position.Col-1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }
             
            return mat;
        }

        public override string ToString()
        {
            return "H";
        }
    }
}