using System;
namespace board.Exceptions
{
    public class BoardException : ApplicationException
    {
        public BoardException(string message) : base(message){}
    }
}