using System;

namespace PM.horizOn.Cloud.Objects.Network.Requests
{
    /// <summary>
    /// Request object for user sign in.
    /// </summary>
    [Serializable]
    public class SignInRequest
    {
        public string Type; // EMAIL, ANONYMOUS, or GOOGLE
        public string Email;
        public string Password;
        public string AnonymousToken;
        public string GoogleAuthorizationCode;
    }
}
