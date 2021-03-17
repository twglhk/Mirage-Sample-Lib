using Mirage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRoomPlayerSub : NetworkRoomPlayer
{
    protected override void ServerRpcJump()
    {
        IsJump = true;
    }
}
