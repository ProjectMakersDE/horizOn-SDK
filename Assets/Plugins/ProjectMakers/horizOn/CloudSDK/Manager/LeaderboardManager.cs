using System.Collections.Generic;
using System.Threading.Tasks;
using PM.horizOn.Cloud.Base;
using PM.horizOn.Cloud.Core;
using PM.horizOn.Cloud.Enums;
using PM.horizOn.Cloud.Objects.Network.Requests;
using PM.horizOn.Cloud.Objects.Network.Responses;

namespace PM.horizOn.Cloud.Manager
{
    /// <summary>
    /// Manager for leaderboard functionality.
    /// Supports score submission, ranking, and leaderboard queries.
    /// </summary>
    public class LeaderboardManager : BaseManager<LeaderboardManager>
    {
        private Dictionary<string, List<SimpleLeaderboardEntry>> _leaderboardCache = new Dictionary<string, List<SimpleLeaderboardEntry>>();

        /// <summary>
        /// Submit a score to a leaderboard.
        /// Score is only updated if it's higher than the previous best.
        /// </summary>
        /// <param name="score">Score value</param>
        /// <param name="metadata">Optional metadata JSON string</param>
        /// <returns>True if submission succeeded, false otherwise</returns>
        public async Task<bool> SubmitScore(long score, string metadata = null)
        {
            if (!PM.horizOn.Cloud.Manager.UserManager.Instance.IsSignedIn)
            {
                HorizonApp.Log.Error("User must be signed in to submit score");
                return false;
            }

            var user = PM.horizOn.Cloud.Manager.UserManager.Instance.CurrentUser;

            var request = new SubmitScoreRequest
            {
                UserId = user.UserId,
                Username = user.DisplayName,
                Score = score,
                Metadata = metadata
            };

            var response = await HorizonApp.Network.PostAsync<SubmitScoreResponse>(
                "/api/v1/app/leaderboard/submit",
                request,
                useSessionToken: false
            );

            if (response.IsSuccess)
            {
                HorizonApp.Log.Info($"Score submitted: {score}");
                HorizonApp.Events.Publish(EventKeys.LeaderboardDataChanged, score);

                // Invalidate cache
                _leaderboardCache.Clear();

                return true;
            }
            else
            {
                HorizonApp.Log.Error($"Score submission failed: {response.Error}");
                return false;
            }
        }

        /// <summary>
        /// Get top entries from the leaderboard.
        /// </summary>
        /// <param name="limit">Number of entries to retrieve (max 100)</param>
        /// <param name="useCache">Whether to use cached data if available</param>
        /// <returns>List of leaderboard entries, or null if failed</returns>
        public async Task<List<SimpleLeaderboardEntry>> GetTop(int limit = 10, bool useCache = true)
        {
            if (!PM.horizOn.Cloud.Manager.UserManager.Instance.IsSignedIn)
            {
                HorizonApp.Log.Error("User must be signed in to get leaderboard");
                return null;
            }

            if (limit > 100)
            {
                HorizonApp.Log.Warning("Limit capped at 100 entries");
                limit = 100;
            }

            // Check cache
            string cacheKey = $"top{limit}";
            if (useCache && _leaderboardCache.ContainsKey(cacheKey))
            {
                HorizonApp.Events.Publish(EventKeys.CacheHit, cacheKey);
                return new List<SimpleLeaderboardEntry>(_leaderboardCache[cacheKey]);
            }

            string userId = PM.horizOn.Cloud.Manager.UserManager.Instance.CurrentUser.UserId;

            var response = await HorizonApp.Network.GetAsync<AppLeaderboardTopResponse>(
                $"/api/v1/app/leaderboard/top?userId={userId}&limit={limit}",
                useSessionToken: false
            );

            if (response.IsSuccess && response.Data != null && response.Data.Entries != null)
            {
                var entries = new List<SimpleLeaderboardEntry>(response.Data.Entries);

                // Cache the results
                _leaderboardCache[cacheKey] = entries;

                HorizonApp.Log.Info($"Loaded top {entries.Count} entries");
                HorizonApp.Events.Publish(EventKeys.LeaderboardDataLoaded, entries);

                return entries;
            }
            else
            {
                HorizonApp.Log.Error($"Failed to get top leaderboard entries: {response.Error}");
                return null;
            }
        }

        /// <summary>
        /// Get the current user's rank in the leaderboard.
        /// </summary>
        /// <returns>Rank response, or null if failed</returns>
        public async Task<AppUserRankResponse> GetRank()
        {
            if (!PM.horizOn.Cloud.Manager.UserManager.Instance.IsSignedIn)
            {
                HorizonApp.Log.Error("User must be signed in to get rank");
                return null;
            }

            string userId = PM.horizOn.Cloud.Manager.UserManager.Instance.CurrentUser.UserId;

            var response = await HorizonApp.Network.GetAsync<AppUserRankResponse>(
                $"/api/v1/app/leaderboard/rank?userId={userId}",
                useSessionToken: false
            );

            if (response.IsSuccess && response.Data != null)
            {
                HorizonApp.Log.Info($"User rank: {response.Data.Position} (Score: {response.Data.Score})");
                return response.Data;
            }
            else
            {
                HorizonApp.Log.Error($"Failed to get rank: {response.Error}");
                return null;
            }
        }

        /// <summary>
        /// Get leaderboard entries around the current user's position.
        /// </summary>
        /// <param name="range">Number of entries before and after the user (default 10)</param>
        /// <param name="useCache">Whether to use cached data if available</param>
        /// <returns>List of leaderboard entries, or null if failed</returns>
        public async Task<List<SimpleLeaderboardEntry>> GetAround(int range = 10, bool useCache = true)
        {
            if (!PM.horizOn.Cloud.Manager.UserManager.Instance.IsSignedIn)
            {
                HorizonApp.Log.Error("User must be signed in to get leaderboard");
                return null;
            }

            // Check cache
            string cacheKey = $"around{range}";
            if (useCache && _leaderboardCache.ContainsKey(cacheKey))
            {
                HorizonApp.Events.Publish(EventKeys.CacheHit, cacheKey);
                return new List<SimpleLeaderboardEntry>(_leaderboardCache[cacheKey]);
            }

            string userId = PM.horizOn.Cloud.Manager.UserManager.Instance.CurrentUser.UserId;

            var response = await HorizonApp.Network.GetAsync<AppLeaderboardAroundResponse>(
                $"/api/v1/app/leaderboard/around?userId={userId}&range={range}",
                useSessionToken: false
            );

            if (response.IsSuccess && response.Data != null && response.Data.Entries != null)
            {
                var entries = new List<SimpleLeaderboardEntry>(response.Data.Entries);

                // Cache the results
                _leaderboardCache[cacheKey] = entries;

                HorizonApp.Log.Info($"Loaded {entries.Count} entries around user");
                HorizonApp.Events.Publish(EventKeys.LeaderboardDataLoaded, entries);

                return entries;
            }
            else
            {
                HorizonApp.Log.Error($"Failed to get leaderboard entries around user: {response.Error}");
                return null;
            }
        }

        /// <summary>
        /// Clear the leaderboard cache.
        /// </summary>
        public void ClearCache()
        {
            _leaderboardCache.Clear();
            HorizonApp.Log.Info("Leaderboard cache cleared");
            HorizonApp.Events.Publish(EventKeys.CacheCleared, "Leaderboard");
        }
    }
}
