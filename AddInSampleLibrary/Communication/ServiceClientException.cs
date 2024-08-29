using System;
using System.Runtime.Serialization;

namespace AddInSampleLibrary.Communication
{
    /// <summary>
    /// Thrown, if the connection to the serve cannot be established.
    /// </summary>
    [Serializable]
    public class ServiceClientException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ServiceClientException()
        {
        }

        public ServiceClientException(string message) : base(message)
        {
        }

        public ServiceClientException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ServiceClientException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
