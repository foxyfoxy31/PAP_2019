using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<LeaderboardEntry> leaderboardEntryList;
    private List<Transform> leaderboardEntryTransformList;
    
    private DataLoader dataLoader;
    
    private void Awake() {
        dataLoader = FindObjectOfType<DataLoader>();
        entryContainer = transform.Find("leaderboardEntryContainer");
        entryTemplate = entryContainer.Find("leaderboardEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        /*
        leaderboardEntryList = new List<LeaderboardEntry>() {
            new LeaderboardEntry {time = 56f, name = "marmulas"},
            new LeaderboardEntry {time = 60f, name = "foxyfoxy31"},
            new LeaderboardEntry {time = 100f, name = "kubica"},

        };
        */

        StartCoroutine(GetSQLData());

    }

    private void CreateLeaderboardEntryTransform(LeaderboardEntry leaderboardEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
            default:
                rankString = rank + "TH";
            break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;
    
        float time = leaderboardEntry.time;

        string minutes = ((int) time / 60).ToString();
        string seconds = (time % 60).ToString("f2");

        entryTransform.Find("timeText").GetComponent<Text>().text = minutes + ":" + seconds;

        string name = leaderboardEntry.name;

        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);

        entryTemplate.gameObject.SetActive(true);
    }

    IEnumerator GetSQLData () {
        while (dataLoader.active == false) {
            yield return new WaitForSeconds(0);
        }
        Debug.Log("I got the data!");
        leaderboardEntryList = new List<LeaderboardEntry>() {
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(1, "time")), name = dataLoader.GetLeaderboardEntryData(1, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(2, "time")), name = dataLoader.GetLeaderboardEntryData(2, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(3, "time")), name = dataLoader.GetLeaderboardEntryData(3, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(4, "time")), name = dataLoader.GetLeaderboardEntryData(4, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(5, "time")), name = dataLoader.GetLeaderboardEntryData(5, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(6, "time")), name = dataLoader.GetLeaderboardEntryData(6, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(7, "time")), name = dataLoader.GetLeaderboardEntryData(7, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(8, "time")), name = dataLoader.GetLeaderboardEntryData(8, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(9, "time")), name = dataLoader.GetLeaderboardEntryData(9, "name")},
            new LeaderboardEntry {time = float.Parse(dataLoader.GetLeaderboardEntryData(10, "time")), name = dataLoader.GetLeaderboardEntryData(10, "name")},
            
        };
        leaderboardEntryTransformList = new List<Transform>();
        foreach (LeaderboardEntry leaderboardEntry in leaderboardEntryList) {
            CreateLeaderboardEntryTransform(leaderboardEntry, entryContainer, leaderboardEntryTransformList);
        }
    }


    private class LeaderboardEntry {
        public float time;
        public string name;
    }

}
