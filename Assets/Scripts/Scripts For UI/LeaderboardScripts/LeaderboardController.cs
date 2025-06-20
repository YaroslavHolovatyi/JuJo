using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardController : MonoBehaviour
{
    public Transform leaderboardContent;      // Content у ScrollView
    public GameObject userScorePrefab;        // Префаб UserScore

    public List<PlayerScore> mockScores = new List<PlayerScore>();

    void Start()
    {
        if (mockScores.Count == 0)
        {
            mockScores.Add(new PlayerScore("Yaroslav", 1500));
            mockScores.Add(new PlayerScore("Dmytro", 1300));
            mockScores.Add(new PlayerScore("Anna", 1700));
            mockScores.Add(new PlayerScore("Ivan", 900));
            mockScores.Add(new PlayerScore("Kateryna", 1600));
        }

        UpdateLeaderboard();
    }

    // Update is called once per frame
    public void UpdateLeaderboard()
    {
        foreach (Transform child in leaderboardContent)
            Destroy(child.gameObject);

        mockScores.Sort((a, b) => b.score.CompareTo(a.score));

        foreach (var player in mockScores)
        {
            var entry = Instantiate(userScorePrefab, leaderboardContent);
            entry.GetComponent<UserScoreEntry>().SetData(player.playerName, player.score);
        }
    }
}

[System.Serializable]

public class PlayerScore
{
    public string playerName;
    public int score;

    public PlayerScore(string name, int score)
    {
        playerName = name;
        this.score = score;
    }
}