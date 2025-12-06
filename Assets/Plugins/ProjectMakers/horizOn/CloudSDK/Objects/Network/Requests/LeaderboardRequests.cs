using System;
using System.Collections.Generic;

namespace PM.horizOn.Cloud.Objects.Network.Requests
{
    /// <summary>
    /// Request object for submitting a leaderboard score.
    /// </summary>
    [Serializable]
    public class SubmitScoreRequest
    {
        public string UserId;
        public string Username;
        public long Score;
        public string Metadata; // JSON string
    }
}
