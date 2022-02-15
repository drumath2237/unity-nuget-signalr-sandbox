using System;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

namespace UnityNuGetSignalRTest
{
    public class SignalRClientBehaviour : MonoBehaviour
    {
        [SerializeField]
        private SignalRInfo signalRInfo;

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

                _connection.On("event", (string msg) => { Debug.Log($"event message: {msg}"); });
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }

            Debug.Log("connected!");
        }

        public async void SendSignalREventMessage()
        {
            if (_connection == null)
            {
                return;
            }

            await _connection.SendAsync("event", "helloooo");
        }

        private async void OnApplicationQuit()
        {
            if (_connection == null) return;

            await _connection.StopAsync();
            await _connection.DisposeAsync();
        }
    }
}