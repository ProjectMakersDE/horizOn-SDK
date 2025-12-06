using System;

namespace PM.horizOn.Cloud.Objects.Network.Responses
{
    /// <summary>
    /// Response object for authentication requests (signup, signin).
    /// </summary>
    [Serializable]
    public class AuthResponse
    {
        public string UserId;
        public string Username;
        public string Email;
        public string AccessToken;
        public string AuthStatus; // AUTHENTICATED, USER_NOT_FOUND, etc.
        public string Message;
        public bool IsAnonymous;
        public bool IsVerified;
        public string AnonymousToken;
        public string GoogleId;
        public string CreatedAt;
    }
}
