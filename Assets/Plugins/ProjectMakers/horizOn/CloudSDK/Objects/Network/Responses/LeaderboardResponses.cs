using System;

namespace PM.horizOn.Cloud.Objects.Network.Responses
{
    /// <summary>
    /// Simple leaderboard entry.
    /// </summary>
    [Serializable]
    public class SimpleLeaderboardEntry
    {
        public long Position;
        public string Username;
        public long Score;
    }

    /// <summary>
    /// Response object for submitting a score.
    /// </summary>
    [Serializable]
    public class SubmitScoreResponse
    {
        // API returns empty body on success (200 OK)
    }

    /// <summary>
    /// Response containing top leaderboard entries.
    /// </summary>
    [Serializable]
    public class AppLeaderboardTopResponse
    {
        public SimpleLeaderboardEntry[] Entries;
    }

    /// <summary>
    /// Response containing user rank information.
    /// </summary>
    [Serializable]
    public class AppUserRankResponse
    {
        public long Position;
        public string Username;
        public long Score;
    }

    /// <summary>
    /// Response containing leaderboard entries around user.
    /// </summary>
    [Serializable]
    public class AppLeaderboardAroundResponse
    {
        public SimpleLeaderboardEntry[] Entries;
    }
}
