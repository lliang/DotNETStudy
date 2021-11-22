using System;

namespace DotNETStudy.IoC.AutofacConsoleApp.Components
{
    public class MyComponent
    {
        private ILogger _logger;
        private IConfigReader _reader;

        public MyComponent()
        {

        }

        public MyComponent(ILogger logger)
        {
            _logger = logger;
        }

        public MyComponent(ILogger logger, IConfigReader configReader)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _reader = configReader ?? throw new ArgumentNullException(nameof(configReader));
        }
    }
}
