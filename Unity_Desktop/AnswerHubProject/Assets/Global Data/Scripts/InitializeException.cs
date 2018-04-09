#define DEBUG 
//#undef DEBUG

using System;

//Ripped from https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/creating-and-throwing-exceptions

namespace GlobalProject
{
    [Serializable()]
    public class InitializeException : Exception
    {
        public InitializeException() : base() { }
        public InitializeException(string message) : base(message) { }
        public InitializeException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InitializeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}