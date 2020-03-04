using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public string[] leaders;
    public bool active = false;

    IEnumerator Start() {
        WWW leaderData = new WWW("http://localhost/sqlconnect/leaderboard.php");
        yield return leaderData;
        string leaderDataString = leaderData.text;
        active = true;
        Debug.Log(leaderDataString);
        leaders = leaderDataString.Split(';');
        Debug.Log(GetDataValue(leaders[0], "Time:"));

    }

    public string GetDataValue(string data, string index) {
        string value = data.Substring(data.IndexOf(index)+index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        if (value.Contains(".")) value = value.Replace(".", ",");
        return value;
    }

    public string GetLeaderboardEntryData (int position, string index) {
        string privateIndex;
        switch (index) {
            case "name":
                privateIndex = "Name:";
            break;

            case "time":
                privateIndex = "Time:";
            break;

            default:
                return "Invalid Index!";
        }

        if (leaders.Length < position - 1) {
            return "Empty";
        }
        else {
            Debug.Log(GetDataValue(leaders[position-1], privateIndex));
            return GetDataValue(leaders[position-1], privateIndex);
        }
    }

}
