using UnityEngine;

public static class UserData
{
    public static string id;
    public static string gamercode;
    public static string username;
    public static string photo;

    public static Texture2D photoTexture;

    public static bool IsLoggedIn => gamercode != null && username != null && photo != null;

    public static void LogOff()
    {
        id = null;
        gamercode = null;
        username = null;
        photo = null;
        photoTexture = null;
    }
}
