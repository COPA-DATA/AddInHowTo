using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace AddInSampleLibrary.Subscription
{
    public class VariableSubscription
    {
        private readonly Action<IEnumerable<IVariable>> _variableChangeReceivedAction;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly string _containerName;
        private IOnlineVariableContainer _container;
        private IProject _project;

        public VariableSubscription(Action<IEnumerable<IVariable>> variableChangeReceivedAction)
        {
            _variableChangeReceivedAction = variableChangeReceivedAction;
            _containerName = "MyOnlineContainerCollection-" + Guid.NewGuid();
        }

        public void Start(IProject context, IEnumerable<string> variables)
        {
            if (_project != null || _container != null)
            {
                throw new InvalidOperationException("Cannot start a new online container again.");
            }

            _project = context;
            try
            {
                // Ensure that the container is deleted
                context.OnlineVariableContainerCollection.Delete(_containerName);

                // Create a new container
                _container = context.OnlineVariableContainerCollection.Create(_containerName);

                // Add variables and register Event
                ErrorHandler.ThrowOnError(_container.AddVariable(variables.ToArray()));
                _container.BulkChanged += Container_BulkChanged;

                // Activate OnlineContainer
                ErrorHandler.ThrowOnError(_container.ActivateBulkMode());
                ErrorHandler.ThrowOnError(_container.Activate());

                _logger.Info("OnlineContainer " + _containerName + " successfully started.");
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
        }

        public void Stop()
        {
            if (_container == null)
            {
                throw new InvalidOperationException("Stop() cannot be called before Start()");
            }

            // All events are removed here - the container gets disabled and deleted.
            _container.BulkChanged -= Container_BulkChanged;

            _container.Deactivate();
            try
            {
                ErrorHandler.ThrowOnError(_project.OnlineVariableContainerCollection.Delete(_containerName));
                _container = null;
                _project = null;
                _logger.Info("OnlineContainer "+ _containerName +" successfully stopped.");
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
        }

        private void Container_BulkChanged(object sender, BulkChangedEventArgs e)
        {
            try
            {
                // Do not execute long-running processes here. They are blocking the
                // main thread of zenon. Therefore we use the TPL (Task Parallel Library) to run the 
                // Action as separate thread
                _logger.Info("Bulk update received.");
                Task.Factory.StartNew(() => _variableChangeReceivedAction(e.Variables));
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
        }
    }
}
