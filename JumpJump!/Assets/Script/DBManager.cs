using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DBManager
{
    public static string username;
    public static int coins;
    public static int highscore;

    public static bool LoggedIn{ get { return username != null;}}

    public static void Logout()
    {
        username = null;
    }
}
