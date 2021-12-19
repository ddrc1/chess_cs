using board;

namespace chess
{
    public class King : Piece
    {
        private ChessGame Match;
        public King(Color color, Board board, ChessGame match) : base(color, board){
            Match = match;
        }

        public bool canMove(Position pos){
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoviments(){
            bool[,] mat = new bool[Board.Rows, Board.Cols];
            
            //N
            Position pos = new Position(Position.Row-1, Position.Col);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //NE
            pos = new Position(Position.Row-1, Position.Col+1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //L
            pos = new Position(Position.Row, Position.Col+1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //SE
            pos = new Position(Position.Row+1, Position.Col+1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //S
            pos = new Position(Position.Row+1, Position.Col);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //SO
            pos = new Position(Position.Row+1, Position.Col-1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //O
            pos = new Position(Position.Row, Position.Col-1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            //NO
            pos = new Position(Position.Row-1, Position.Col-1);
            if(Board.IsValidPosition(pos) && canMove(pos)){
                mat[pos.Row, pos.Col] = true;
            }

            // Special move little roque
            if(QtdMoviments == 0 && !Match.Check){
                Position posT1 = new Position(Position.Row, Position.Col + 3);
                Piece t1 = Board.GetPiece(posT1);
                bool roqueAllowedT1 = t1 != null && t1 is Tower && 
                                        t1.Color == Color && 
                                        QtdMoviments == 0;
                if (roqueAllowedT1){
                    Position p1 = new Position(Position.Row, Position.Col + 1);
                    Position p2 = new Position(Position.Row, Position.Col + 2);
                    if(p1 == null && p2 == null){
                        mat[Position.Row, Position.Col + 2] = true;
                    }
                }
            }

            // Special move big roque
            if(QtdMoviments == 0 && !Match.Check){
                Position posT2 = new Position(Position.Row, Position.Col + 4);
                Piece t2 = Board.GetPiece(posT2);
                bool roqueAllowedT1 = t2 != null && t2 is Tower && 
                                        t2.Color == Color && 
                                        QtdMoviments == 0;
                if (roqueAllowedT1){
                    Position p1 = new Position(Position.Row, Position.Col - 1);
                    Position p2 = new Position(Position.Row, Position.Col - 2);
                    Position p3 = new Position(Position.Row, Position.Col - 3);
                    if(p1 == null && p2 == null && p3 == null){
                        mat[Position.Row, Position.Col - 2] = true;
                    }
                }
            } 
            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}