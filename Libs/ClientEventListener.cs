using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    public class ClientEventListener : EventListenerBase
    {
        private void OnEnable()
        { Event.OnClientRegisterListener(this); }

        private void OnDisable()
        { Event.OnClientUnregisterListener(this); }

        public override void OnEventRaised()
        {
            if (NetIdentity.IsClient)
                base.OnEventRaised();
        }
    }
}