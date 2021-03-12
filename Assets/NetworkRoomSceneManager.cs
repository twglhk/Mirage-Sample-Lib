using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mirage
{
    [AddComponentMenu("Network/NetworkRoomSceneManager")]
    public class NetworkRoomSceneManager : NetworkSceneManager
    {
        [Header("Scene Registry")]
        [FormerlySerializedAs("OfflineScene")]
        [Scene]
        [SerializeField] string _offlineScenePath;

        [FormerlySerializedAs("RoomScene")]
        [Scene]
        [SerializeField] string _roomScenePath;

        [FormerlySerializedAs("GameScene")]
        [Scene]
        [SerializeField] string _gameScenePath;
    }
}