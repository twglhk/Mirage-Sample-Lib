using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    public class ServerEventListener : EventListenerBase
    {
        private void OnEnable()
        { Event.OnServerRegisterListener(this); }

        private void OnDisable()
        { Event.OnServerUnregisterListener(this); }

        public override void OnEventRaised()
        {
            if (NetIdentity.IsServer)
                base.OnEventRaised();
        }
    }
}