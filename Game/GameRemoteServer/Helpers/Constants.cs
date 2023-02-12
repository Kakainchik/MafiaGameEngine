namespace GameRemoteServer.Helpers;

internal static class Constants
{
    internal const string PASSWORD_REGEX = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{7,}$";
}