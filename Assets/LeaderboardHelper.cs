using System.Collections.Generic;
using System.IO;

public class LeaderboardHelper
{
    public LeaderboardHelper()
    {

    }

    public static void SaveRecord(PlayerRecord record)
    {
        Leaderboard leaderboard = new Leaderboard();
        leaderboard.records = new List<PlayerRecord>();
        if (File.Exists("leaderboard.json"))
        {
            leaderboard = FileHelper.ReadFromFile<Leaderboard>("leaderboard.json");
        }

        leaderboard.records.Add(record);
        FileHelper.WriteToFile("leaderboard.json", leaderboard, false);
    }

    public static Leaderboard LoadLeaderboard()
    {
        Leaderboard leaderboard = new Leaderboard();
        leaderboard.records = new List<PlayerRecord>();
        if (File.Exists("leaderboard.json"))
        {
            leaderboard = FileHelper.ReadFromFile<Leaderboard>("leaderboard.json");
        }

        return leaderboard;
    }
}