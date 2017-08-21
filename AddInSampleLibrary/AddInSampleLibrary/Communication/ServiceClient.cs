using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace AddInSampleLibrary.Communication
{
    /// <summary>
    /// Manages the lifetime of IPC communication on client side.
    /// </summary>
    /// <typeparam name="TServiceInterface"></typeparam>
    public class ServiceClient<TServiceInterface> : IDisposable where TServiceInterface : class
    {
        private readonly string _addInName;
        private bool _disposed;
        private IpcChannel _channel;

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="addInName">An unique name of the Add-In</param>
        public ServiceClient(string addInName)
        {
            _addInName = addInName;
        }

        ~ServiceClient()
        {
            Dispose(false);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        #endregion

        public TServiceInterface ServiceProxy { get; private set; }

        private void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                ChannelServices.UnregisterChannel(_channel);
                ServiceProxy = null;
                _channel = null;

                // Indicate that the instance has been disposed.
                _disposed = true;
            }
        }

        public void Open(string wellknownName)
        {
            try
            {
                if (_channel == null)
                {
                    _channel = new IpcChannel();
                    ChannelServices.RegisterChannel(_channel, false);
                }

                if (ServiceProxy == null)
                {
                    string url = $"ipc://{_addInName}/{wellknownName}";
                    ServiceProxy = (TServiceInterface)Activator.GetObject(typeof(TServiceInterface), url);
                }
            }
            catch (Exception ex)
            {
                throw new ServiceClientException("An error occurred while creating IPC connection.", ex);
            }
        }
    }
}
