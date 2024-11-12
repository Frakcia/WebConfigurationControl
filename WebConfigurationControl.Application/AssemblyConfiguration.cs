using System.Reflection;

namespace WebConfigurationControl.Application
{
    public class AssemblyConfiguration
    {
        public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
    }
}
