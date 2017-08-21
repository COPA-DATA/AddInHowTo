using System;
using AddInSampleLibrary.Communication;
using CommunicationLibrary;
using Scada.AddIn.Contracts;

namespace AddInCommunicationSample
{
    /// <summary>
    /// Implements a runtime service that provides data to others
    /// </summary>
    [AddInExtension("Data Provider Service", "Demo how to communicate between extensions. This extension represents is the 'server' side")]
    public class ProjectServiceExtension : IProjectServiceExtension
    {
        private ServiceHost _serviceHost;

        #region IProjectServiceExtension implementation

        public void Start(IProject context, IBehavior behavior)
        {
            _serviceHost = new ServiceHost();

            try
            {
                _serviceHost.Open(Constants.CommunicationServiceEndPointName);
                _serviceHost.RegisterService(nameof(IAlarmService), new AlarmService(context));
                _serviceHost.RegisterService(nameof(IDemoService), new DemoService());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Stop()
        {
            try
            {
                _serviceHost.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion
    }
}