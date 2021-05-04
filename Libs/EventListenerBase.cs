using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirage;

namespace Game.Event
{
    public class EventListenerBase : NetworkBehaviour
    {
        public MirageNetworkEventCaller Event;
        public UnityEvent Response;

        public virtual void OnEventRaised()
        { Response.Invoke(); }
    }
}