using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    [CreateAssetMenu(fileName = "NewEventCaller", menuName = "ScriptableObjects/Event/EventCaller", order = 1)]
    public class MirageNetworkEventCaller : ScriptableObject 
    {
        private List<ServerEventListener> _serverListeners = new List<ServerEventListener>();
        private List<ClientEventListener> _clientListeners = new List<ClientEventListener>();
        private List<LocalEventListener> _localListeners = new List<LocalEventListener>();

        public void OnServerRaise()
        {
            for (int i = _serverListeners.Count - 1; i >= 0; i--)
                _serverListeners[i].OnEventRaised();
        }

        public void OnServerRegisterListener(ServerEventListener listener)
        { _serverListeners.Add(listener); }

        public void OnServerUnregisterListener(ServerEventListener listener)
        { _serverListeners.Remove(listener); }

        public void OnClientRaise()
        {
            for (int i = _serverListeners.Count - 1; i >= 0; i--)
                _clientListeners[i].OnEventRaised();
        }

        public void OnClientRegisterListener(ClientEventListener listener)
        { _clientListeners.Add(listener); }

        public void OnClientUnregisterListener(ClientEventListener listener)
        { _clientListeners.Remove(listener); }


        public void OnLocalRaise()
        {
            for (int i = _serverListeners.Count - 1; i >= 0; i--)
                _localListeners[i].OnEventRaised();
        }

        public void OnLocalRegisterListener(LocalEventListener listener)
        { _localListeners.Add(listener); }

        public void OnLocalUnregisterListener(LocalEventListener listener)
        { _localListeners.Remove(listener); }
    }
}