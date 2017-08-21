using System;
using System.Runtime.CompilerServices;

namespace AddInSampleLibrary
{
    internal static class ErrorHandler
    {
        /// <summary>
        /// Error wrapper for Zenon Methods, checks if an error occured and gives appropiate error exceptions.
        /// </summary>
        /// <param name="value">return value of the called Zenon method</param>
        /// <param name="memberName">Optional parameter filled out by compiler. Entails the name of the method in which it was called.</param>
        /// <param name="filePath">Optional parameter filled out by compiler. Entails the name of the file in which the method was called.</param>
        /// <param name="lineNumber">Optional parameter filled out by compiler. Entails the linenumber in which the method was called.</param>
        public static void ThrowOnError(bool value, [CallerMemberName] string memberName = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (!value)
            {
                throw new InvalidProgramException("An API function returned false:" +
                    "\nName of the Parent-Method: " + memberName +
                    "\nFilePath in which the error occured: " + filePath +
                    "\nLineNumber in which the Method was executed: " + lineNumber
                    );
            }
        }
    }
}
