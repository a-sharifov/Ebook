namespace Infrastructure;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
    public static readonly string AssemblyPath = Path.GetDirectoryName(Assembly.Location)!;
}
