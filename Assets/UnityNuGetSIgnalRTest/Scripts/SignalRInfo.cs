using UnityEngine;

namespace UnityNuGetSignalRTest
{
    [CreateAssetMenu(fileName = "SignalRInfo", menuName = "SignalRTest/info", order = 0)]
    public class SignalRInfo : ScriptableObject
    {
        public string apiBaseUrl;
    }
}