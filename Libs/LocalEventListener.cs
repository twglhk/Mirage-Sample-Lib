using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    public class LocalEventListener : EventListenerBase
    {
        private void OnEnable()
        { Event.OnLocalRegisterListener(this); }

        private void OnDisable()
        { Event.OnLocalUnregisterListener(this); }

        public override void OnEventRaised()
        {
            if (NetIdentity.HasAuthority)
                base.OnEventRaised();
        }
    }
}