using System;

namespace PM.horizOn.Cloud.Objects.Network.Requests
{
    /// <summary>
    /// Request object for creating a user log.
    /// </summary>
    [Serializable]
    public class CreateUserLogRequest
    {
        /// <summary>
        /// Log message (max 1000 characters)
        /// </summary>
        public string Message;

        /// <summary>
        /// Log type: INFO, WARN, or ERROR
        /// </summary>
        public string Type;

        /// <summary>
        /// User ID who created the log
        /// </summary>
        public string UserId;

        /// <summary>
        /// Optional error code (max 50 characters)
        /// </summary>
        public string ErrorCode;
    }
}
