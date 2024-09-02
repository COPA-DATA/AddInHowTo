using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Security.Principal;

namespace AddInSampleLibrary.Communication
{
    /// <summary>
    /// Manages the lifetime of IPC communication on server side.
    /// </summary>
    public class ServiceHost : IDisposable
    {
        private IpcChannel _channel;
        private bool _isDisposed;


        ~ServiceHost()
        {
            Dispose(false);
        }

        /// <summary>
        /// Opens the connection
        /// </summary>
        /// <param name="addInName">An unique name of the Add-In</param>
        public void Open(string addInName)
        {
            try
            {
                // create binary formatter to serialize data
                var serverSinkProvider = new BinaryServerFormatterSinkProvider { TypeFilterLevel = TypeFilterLevel.Full };
                var clientSinkProvider = new BinaryClientFormatterSinkProvider();

                // configure named pipe
                var properties = new Dictionary<string, string>
                {
                    {"portName", addInName},
                    {"authorizedGroup", GetNameForSid(WellKnownSidType.BuiltinUsersSid)}
                };

                _channel = new IpcChannel(properties, clientSinkProvider, serverSinkProvider);

                ChannelServices.RegisterChannel(_channel, false);

            }
            catch (Exception exception)
            {
                throw new ServiceHostException(exception.Message, exception);
            }
        }

        public void RegisterService(string wellknownName, MarshalByRefObject serviceObject)
        {
            RemotingServices.SetObjectUriForMarshal(serviceObject, wellknownName);
            RemotingServices.Marshal(serviceObject);

            var x = RemotingServices.GetObjectUri(serviceObject);

        }


        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);

            // Use SuppressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        #endregion

        private void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_isDisposed)
            {
                // release other disposable objects
                if (_channel != null)
                {
                    Close();
                }
                _isDisposed = true;
            }
        }

        public void Close()
        {
            try
            {
                if (_channel == null)
                {
                    return;
                }

                ChannelServices.UnregisterChannel(_channel);
                _channel = null;
            }
            catch (Exception exception)
            {
                throw new ServiceHostException(exception.Message, exception);
            }
        }

        private static string GetNameForSid(WellKnownSidType wellKnownSidType)
        {
            SecurityIdentifier id = new SecurityIdentifier(wellKnownSidType, null);
            return id.Translate(typeof(NTAccount)).Value;
        }
    }
}
