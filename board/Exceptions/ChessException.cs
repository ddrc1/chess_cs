using board.Exceptions;

namespace board.Exceptions
{
    public class ChessException : BoardException
    {
        public ChessException(string message) : base(message){}
    }
}