using NLog;
using NLog.Config;
using NLog.Targets;

namespace AddInSampleLibrary.Logging
{
    public class NLogConfigurator
    {
        private readonly string _logFileNamePattern;
        private readonly string _logLayoutPattern;

        public NLogConfigurator(string logFileNamePattern)
        {
            _logFileNamePattern = logFileNamePattern;
        }

        public NLogConfigurator()
        {
            string addInName = this.GetType().Assembly.GetName().Name;
            _logFileNamePattern = "${specialfolder:folder=CommonApplicationData}/Company/zenon/${processname}_" + addInName  + ".log";

            /* Layout of LogMessages:
             * See: https://github.com/nlog/NLog/wiki/Layout-Renderers
             * ${callsite} - The call site (class name, method name and source information).
             * ${message} - The formatted log message.
             * ${onexception} - Only outputs the inner layout when exception has been defined for log message.
             */
            _logLayoutPattern = @"${callsite} ${message} ${onexception:Exception information\:${exception:format=type,message,method,StackTrace:maxInnerExceptionLevel=5:innerFormat=type,message,method,StackTrace}";
        }

        public void Configure()
        {
            // See: https://github.com/nlog/NLog/wiki/Configuration-API

            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration
            // See http://sentinel.codeplex.com/ for a log viewer 
            var viewerTarget = new NLogViewerTarget();
            config.AddTarget("viewer", viewerTarget);

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties 
            viewerTarget.Layout = _logLayoutPattern;
            viewerTarget.Address = "udp://127.0.0.1:9999";
            fileTarget.FileName = _logFileNamePattern;
            fileTarget.Layout = _logLayoutPattern;

            // Step 4. Define rules
            var rule1 = new LoggingRule("*", LogLevel.Debug, viewerTarget);
            config.LoggingRules.Add(rule1);

            var rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;
        }
    }
}
