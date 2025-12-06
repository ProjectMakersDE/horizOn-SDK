using System;

namespace PM.horizOn.Cloud.Objects.Network.Responses
{
    /// <summary>
    /// Response object for checking authentication.
    /// </summary>
    [Serializable]
    public class CheckAuthResponse
    {
        public string UserId;
        public bool IsAuthenticated;
        public string AuthStatus; // AUTHENTICATED, TOKEN_EXPIRED, etc.
        public string Message;
    }

    /// <summary>
    /// Generic message response.
    /// </summary>
    [Serializable]
    public class MessageResponse
    {
        public bool Success;
        public string Message;
    }
}
