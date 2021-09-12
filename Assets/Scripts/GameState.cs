using UnityEngine;

public static class GameState
{
    public static int DeathCount { get; set; } = 0;

    public static bool[] PartsCollected { get; set; } = new bool[] { false, false, false };
}
