using System.Reflection;

namespace Api;

internal class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
