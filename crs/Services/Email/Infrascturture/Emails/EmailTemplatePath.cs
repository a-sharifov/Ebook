﻿namespace Infrastructure.Emails;

/// <summary>
/// This class is used to get the path of the email templates.
/// </summary>
internal static class EmailTemplatePath
{
    /// <summary>
    /// required change this template: {{firstName}}, {{lastName}}, {{confirmationLink}}
    /// </summary>
    public static string ConfirmEmailTemplate => GetTemplatePath("ConfirmEmailTemplate.html");

    /// <summary>
    /// required change this template: {{firstName}}, {{lastName}}, {{code}}
    /// </summary>
    public static string ForgotPasswordTemplate => GetTemplatePath("ForgotPasswordTemplate.html");


    private static string GetTemplatePath(string templateName) =>
        Path.Combine(Templates.AssemblyReference.AssemblyPath, templateName);
}
