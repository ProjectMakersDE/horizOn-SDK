using System;

namespace PM.horizOn.Cloud.Objects.Network.Responses
{
    /// <summary>
    /// Response object for a single news entry.
    /// </summary>
    [Serializable]
    public class UserNewsResponse
    {
        public string ID;
        public string Title;
        public string Message;
        public string ReleaseDate;
        public string LanguageCode;
    }
}
