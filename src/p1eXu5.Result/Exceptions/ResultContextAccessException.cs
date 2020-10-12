using System;

namespace p1eXu5.Result.Exceptions
{
    public class ResultContextAccessException : InvalidOperationException
    {
        public ResultContextAccessException( string error ) : base( error )
        { }
    }
}
