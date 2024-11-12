using System.Reflection;

namespace WebConfigurationControl.Notifications
{
    public class AssemblyConfiguration
    {
        public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
    }
}
