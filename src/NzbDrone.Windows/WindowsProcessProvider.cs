using NLog;
using NzbDrone.Common.Processes;

namespace NzbDrone.Windows
{
    public class WindowsProcessProvider : ProcessProvider
    {
        public WindowsProcessProvider(Logger logger)
            : base(logger)
        {
        }
    }
}
