using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirage;
using Mirage.Logging;

public abstract class NetworkSpawnable : NetworkBehaviour
{
    static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkSpawnable));

    [SyncVar]
    private float syncvalue = 3;

    private void Awake()
    {
        Debug.Log($"[NetworkIdentity] {NetIdentity.NetId}");
        NetIdentity.OnAuthorityChanged.AddListener(OnClinetStartAuthority);
    }

    private void Start()
    {
        Debug.Log($"[NetworkIdentity] {NetIdentity.NetId}");
    }

    private void Update()
    {
        Debug.Log($"[NetworkIdentity] {NetIdentity.NetId}");
        Debug.Log($"[SyncValue] {syncvalue}");
    }

    public virtual void OnClinetStartAuthority(bool b)
    {
        logger.Log("[Client] OnClinetStartAuthority()");
    }
}
