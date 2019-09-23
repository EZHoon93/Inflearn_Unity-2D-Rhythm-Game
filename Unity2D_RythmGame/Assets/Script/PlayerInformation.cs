using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;

public static class PlayerInformation 
{
    public static int maxCombo { get; set; }
    public static float socre { get; set; }
    public static string selectedMusic { get; set; }
    public static string musicTitle { get; set; }
    public static string musicArtist { get; set; }
    public static Firebase.Auth.FirebaseAuth auth;

    public static DatabaseReference GetDatabaseReference()
    {
        DatabaseReference reference;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://rhythme-gametest.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference;
    }
}
