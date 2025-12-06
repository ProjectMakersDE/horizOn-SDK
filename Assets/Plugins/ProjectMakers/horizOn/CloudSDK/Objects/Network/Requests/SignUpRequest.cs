using System;
using PM.horizOn.Cloud.Enums;

namespace PM.horizOn.Cloud.Objects.Network.Requests
{
    /// <summary>
    /// Request object for user signup.
    /// </summary>
    [Serializable]
    public class SignUpRequest
    {
        public string Type; // ANONYMOUS, EMAIL, GOOGLE
        public string Username;
        public string Email;
        public string Password;
        public string AnonymousToken;
        public string GoogleAuthorizationCode;

        public static SignUpRequest CreateAnonymous(string username = null, string anonymousToken = null)
        {
            // Generate a unique anonymous token if not provided (max 32 chars per API spec)
            if (string.IsNullOrEmpty(anonymousToken))
            {
                // Remove dashes from GUID to fit within 32 char limit
                anonymousToken = System.Guid.NewGuid().ToString("N");
            }

            return new SignUpRequest
            {
                Type = AuthType.ANONYMOUS.ToString(),
                Username = username,
                AnonymousToken = anonymousToken
            };
        }

        public static SignUpRequest CreateEmail(string email, string password, string username = null)
        {
            return new SignUpRequest
            {
                Type = AuthType.EMAIL.ToString(),
                Email = email,
                Password = password,
                Username = username
            };
        }

        public static SignUpRequest CreateGoogle(string googleAuthorizationCode, string username = null)
        {
            return new SignUpRequest
            {
                Type = AuthType.GOOGLE.ToString(),
                GoogleAuthorizationCode = googleAuthorizationCode,
                Username = username
            };
        }
    }
}
