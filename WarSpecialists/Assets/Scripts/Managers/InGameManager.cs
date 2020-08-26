using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;

    public float GameTime;
    public Text TimeText;
    public List<WaveManager> Teams;


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

    public static List<GameObject> MakePath(int team,int lane)
    {
        List<GameObject> newPath = new List<GameObject>();
        int otherTeam = team == 0 ? 1 : 0;

        foreach (GameObject go in Instance.Teams[team].LaneSpawnPoints[lane].Waypoints)
        {
            newPath.Add(go);
        }

        for(int i = Instance.Teams[otherTeam].LaneSpawnPoints[lane].Waypoints.Count-1;i>=0;i--)
        {
            newPath.Add(Instance.Teams[otherTeam].LaneSpawnPoints[lane].Waypoints[i]);
        }
        return newPath;
    }
}
