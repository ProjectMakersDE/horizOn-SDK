using System;

namespace PM.horizOn.Cloud.Objects.Network.Responses
{
    /// <summary>
    /// Response object for saving cloud data.
    /// </summary>
    [Serializable]
    public class SaveCloudSaveResponse
    {
        public bool Success;
        public int DataSizeBytes;
    }

    /// <summary>
    /// Response object for loading cloud data.
    /// </summary>
    [Serializable]
    public class LoadCloudSaveResponse
    {
        public bool Found;
        public string SaveData; // UTF-8 string
    }
}
