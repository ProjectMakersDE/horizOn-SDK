using System;

namespace PM.horizOn.Cloud.Objects.Network.Responses
{
    /// <summary>
    /// Response object for redeeming a gift code.
    /// </summary>
    [Serializable]
    public class RedeemGiftCodeResponse
    {
        public bool Success;
        public string Message;
        public string GiftData; // JSON string containing gift data
    }

    /// <summary>
    /// Response object for validating a gift code.
    /// </summary>
    [Serializable]
    public class ValidateGiftCodeResponse
    {
        public bool Valid;
    }
}
