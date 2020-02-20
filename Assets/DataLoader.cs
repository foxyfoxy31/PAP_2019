using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public string[] leaders;

    IEnumerator Start() {
        WWW leaderData = new WWW("http://localhost/sqlconnect/leaderboard.php");
        yield return leaderData;
        string leaderDataString = leaderData.text;
        Debug.Log(leaderDataString);
        leaders = leaderDataString.Split(';');
        Debug.Log(GetDataValue(leaders[0], "Name:")); 
    }

    string GetDataValue(string data, string index) {
        string value = data.Substring(data.IndexOf(index)+index.Length);
        value = value.Remove(value.IndexOf("|"));
        return value;
    }
}
