using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Game.Data;
using Mirage;

namespace Game.Event
{
    public enum EventListenerType
    {
        Server,
        Client,
        Local
    }

    public class MirageNetworkEventListener : MonoBehaviour
    {
        [Header("Event Setting")]
        [FormerlySerializedAs("Event Listener Type")]
        [SerializeField] EventListenerType EventListenerType;
        public MirageNetworkEventCaller Event;
        public UnityEvent Response;

        void OnEnable()
        {
            switch (EventListenerType)
            {
                case EventListenerType.Server:
                    Event.OnServerRegisterListener(this);
                    break;
                case EventListenerType.Client:
                    Event.OnClientRegisterListener(this);
                    break;
                case EventListenerType.Local:
                    Event.OnLocalClientRegisterListener(this);
                    break;
            }
        }
        void OnDisable()
        {
            switch (EventListenerType)
            {
                case EventListenerType.Server:
                    Event.OnServerUnregisterListener(this);
                    break;
                case EventListenerType.Client:
                    Event.OnClientUnregisterListener(this);
                    break;
                case EventListenerType.Local:
                    Event.OnLocalClientUnregisterListener(this);
                    break;
            }
        }

        public void OnEventRaised()
        { Response.Invoke(); }
    }
}