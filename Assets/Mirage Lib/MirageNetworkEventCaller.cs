using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    [CreateAssetMenu(fileName = "NewEventCaller", menuName = "ScriptableObjects/Event/EventCaller", order = 1)]
    public class MirageNetworkEventCaller : ScriptableObject 
    {
        private List<MirageNetworkEventListener> _serverListeners = new List<MirageNetworkEventListener>();
        private List<MirageNetworkEventListener> _clientListeners = new List<MirageNetworkEventListener>();
        private List<MirageNetworkEventListener> _localListeners = new List<MirageNetworkEventListener>();

        public void OnServerRaise()
        {
            for (int i = _serverListeners.Count - 1; i >= 0; i--)
                _serverListeners[i].OnEventRaised();
        }

        public void OnServerRegisterListener(MirageNetworkEventListener listener)
        { _serverListeners.Add(listener); }

        public void OnServerUnregisterListener(MirageNetworkEventListener listener)
        { _serverListeners.Remove(listener); }

        public void OnClientRaise()
        {
            for (int i = _clientListeners.Count - 1; i >= 0; i--)
                _clientListeners[i].OnEventRaised();
        }

        public void OnClientRegisterListener(MirageNetworkEventListener listener)
        { _clientListeners.Add(listener); }

        public void OnClientUnregisterListener(MirageNetworkEventListener listener)
        { _clientListeners.Remove(listener); }

        public void OnLocalClientRaise()
        {
            for (int i = _localListeners.Count - 1; i >= 0; i--)
                _localListeners[i].OnEventRaised();
        }

        public void OnLocalClientRegisterListener(MirageNetworkEventListener listener)
        { _localListeners.Add(listener); }

        public void OnLocalClientUnregisterListener(MirageNetworkEventListener listener)
        { _localListeners.Remove(listener); }
    }
}