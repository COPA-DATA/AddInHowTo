using System;
using CommunicationLibrary;

namespace AddInCommunicationSample
{
    public class DemoService : MarshalByRefObject, IDemoService
    {
        public override object InitializeLifetimeService()
        {
            // Ensure that the service is not released
            return null;
        }

        public string GetHelloWorldMessage()
        {
            return "Hello, World!";
        }
    }
}
