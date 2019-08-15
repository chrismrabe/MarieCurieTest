using log4net;
using log4net.Config;

namespace InterviewTask.Helpers
{
    public class ServicesLogger
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(ServicesLogger));

        public ServicesLogger()
        {
            XmlConfigurator.Configure();
        }

        public ILog Logger()
        {
            return logger;
        }

    }
}