using System.Reflection;

namespace MessageBus;

public class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
