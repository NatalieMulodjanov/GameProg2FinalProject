using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadLeaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // create an empty leaderboard
        Leaderboard leaderboard = LeaderboardHelper.LoadLeaderboard();
        GameObject canvas = GameObject.Find("Canvas");
        leaderboard.records.Sort((x, y) => x.TimeElapsed.CompareTo(y.TimeElapsed));
        // Show top 5
        for (int i = 0; i < 5; i++)
        {
            GameObject name = GameObject.Find("Name" + (i + 1));
            GameObject time = GameObject.Find("Score" + (i + 1));

            if (leaderboard.records.Count > i)
            {
                if (leaderboard.records[i] != null)
                {
                    name.GetComponent<Text>().text = leaderboard.records[i].Name;
                    time.GetComponent<Text>().text = leaderboard.records[i].TimeElapsed;
                }
            }

            if (leaderboard.records.Count <= i)
            {
                name.SetActive(false);
                time.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}