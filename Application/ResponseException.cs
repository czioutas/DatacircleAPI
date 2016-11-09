namespace DatacircleAPI.Application

{
    public class ResponseException : System.Exception
    {
        public ResponseException() { }
        public ResponseException(string message) : base(message) { }
        public ResponseException(string message, System.Exception inner) : base(message, inner) { }
    }
}