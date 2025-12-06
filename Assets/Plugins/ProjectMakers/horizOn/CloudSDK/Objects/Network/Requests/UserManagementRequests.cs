using System;

namespace PM.horizOn.Cloud.Objects.Network.Requests
{
    /// <summary>
    /// Request object for checking authentication.
    /// </summary>
    [Serializable]
    public class CheckAuthRequest
    {
        public string UserId;
        public string SessionToken;
    }

    /// <summary>
    /// Request object for email verification.
    /// </summary>
    [Serializable]
    public class VerifyEmailRequest
    {
        public string Token;
    }

    /// <summary>
    /// Request object for forgot password.
    /// </summary>
    [Serializable]
    public class ForgotPasswordRequest
    {
        public string Email;
    }

    /// <summary>
    /// Request object for password reset.
    /// </summary>
    [Serializable]
    public class ResetPasswordRequest
    {
        public string Token;
        public string NewPassword;
    }

    /// <summary>
    /// Request object for changing user display name.
    /// </summary>
    [Serializable]
    public class ChangeNameRequest
    {
        public string UserId;
        public string SessionToken;
        public string NewName;
    }
}
