namespace Infrastructure.FileStorages.Policies;

public static class PolicyPath
{
    public static string DefautPolicy => GetTemplatePath("DefaultSecurityPolicy.json");

    private static string GetTemplatePath(string policyName) =>
        Path.Combine(AssemblyReference.AssemblyPath, "FileStorages", "Policies", policyName);
}
