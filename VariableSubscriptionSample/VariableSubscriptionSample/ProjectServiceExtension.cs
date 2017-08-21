using System;
using System.Collections.Generic;
using System.Linq;
using AddInSampleLibrary.Logging;
using AddInSampleLibrary.Subscription;
using NLog;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace VariableSubscriptionSample
{
    /// <summary>
    /// Sample extension to demonstrate a possible solution for variable subscriptions
    /// </summary>
    [AddInExtension("Variable Subscription", "Subscribes to all Variables which are marked as \"External Visible\"")]
    public class ProjectServiceExtension : IProjectServiceExtension
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly AddInSampleLibrary.Subscription.VariableSubscription _variableSubscription;

        public ProjectServiceExtension()
        {
            _variableSubscription = new VariableSubscription(VariableChangeReceivedAction);
        }

        #region IProjectServiceExtension implementation

        public void Start(IProject context, IBehavior behavior)
        {
            var configurator = new NLogConfigurator();
            configurator.Configure();

            try
            {
                List<string> newOnlineVariables = new List<string>();

                // iterate through all variables in the current project and select all variables which are marked as "External Visible"
                foreach (var item in context.VariableCollection)
                {
                    if ((bool)item.GetDynamicProperty("ExternVisible"))
                    {
                        newOnlineVariables.Add(item.Name);
                    }                    
                }

                if (newOnlineVariables.Any())
                {
                    _variableSubscription.Start(context, newOnlineVariables);
                }
                else
                {
                    Logger.Info("No Variables in the project " + context.Name + " were marked as \"External Visible\"");
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }


        public void Stop()
        {
            try
            {
                _variableSubscription.Stop();

            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        #endregion

        /// <summary>
        /// Action which is called on a variable change event
        /// </summary>
        private void VariableChangeReceivedAction(IEnumerable<IVariable> variables)
        {
            foreach (var variable in variables)
            {
                Logger.Info($"Variable '{variable.Name}' has changed to '{variable.GetValue(0)}'.");
            }
        }
    }
}