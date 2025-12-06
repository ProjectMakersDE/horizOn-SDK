using System;

namespace PM.horizOn.Cloud.Objects.Network.Requests
{
    /// <summary>
    /// Request object for submitting user feedback.
    /// </summary>
    [Serializable]
    public class SubmitFeedbackRequest
    {
        public string UserId; // Required - User ID submitting feedback
        public string Title; // Required, 1-100 characters
        public string Category; // BUG, FEATURE, GENERAL
        public string Message;
        public string Email;
        public string DeviceInfo;
    }
}
