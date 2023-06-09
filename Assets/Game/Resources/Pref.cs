

using UnityEngine;

public class Pref 
{
    public static string SCORE_KEY = "score";
    public static int Score
    {
        set => PlayerPrefs.SetInt(SCORE_KEY, value);
        get => PlayerPrefs.GetInt(SCORE_KEY);

    }
}
