using System;
using Microsoft.AspNetCore.SignalR.Client;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityNuGetSignalRTest
{
    public class SignalRClientBehaviour : MonoBehaviour
    {
        [SerializeField]
        private SignalRInfo signalRInfo;

        [SerializeField]
        private TextMeshProUGUI _logText;

        private string _logString = "";


        private HubConnection _connection;

        private async void Start()
        {
            if (signalRInfo == null || signalRInfo.apiBaseUrl == "")
            {
                return;
            }

            try
            {
                _connection = new HubConnectionBuilder().WithUrl(signalRInfo.apiBaseUrl).Build();

                await _connection.StartAsync();

                _connection.On<string>("event", OnSignalRMessageReceive);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }

            Debug.Log("connected!");
        }

        void Update()
        {
            _logText.text = _logString;
        }

        private void OnSignalRMessageReceive(string msg)
        {
            Debug.Log(msg);

            _logString = $"{DateTime.Now:HH:mm:ss}: {msg}\n{_logText.text}";
        }

        public void SendSignalREventMessage()
        {
            if (_connection == null)
            {
                return;
            }

            UnityWebRequest.Get($"{signalRInfo.apiBaseUrl}/send").SendWebRequest();
        }

        private async void OnApplicationQuit()
        {
            if (_connection == null) return;

            await _connection.StopAsync();
            await _connection.DisposeAsync();
        }
    }
}