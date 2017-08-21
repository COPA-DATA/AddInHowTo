using System;
using System.Runtime.Serialization;

namespace AddInSampleLibrary.Communication
{
    /// <summary>
    /// Thrown, if the server communication cannot be activated.
    /// </summary>
    [Serializable]
    public class ServiceHostException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ServiceHostException()
        {
        }

        public ServiceHostException(string message) : base(message)
        {
        }

        public ServiceHostException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ServiceHostException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
