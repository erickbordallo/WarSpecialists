using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;

    public float GameTime;
    public Text TimeText;

    private void Awake()
    {
        if (Instance != this)
            Instance = this;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
        System.TimeSpan t = System.TimeSpan.FromSeconds(GameTime);
        TimeText.text = string.Format("{0:D2}:{1:D2}",t.Minutes,t.Seconds);
    }
}
