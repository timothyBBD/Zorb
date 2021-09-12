using UnityEngine;
using System.ComponentModel;

public static class GameState
{
    [DefaultValue(0)]
    public static int DeathCount {get;set;}

    [DefaultValue(new bool[] {false, false, false})]
    public static bool[] PartsCollected {get;set;}
}
