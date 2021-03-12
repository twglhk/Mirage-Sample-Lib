using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mirage
{
    [AddComponentMenu("Network/NetworkRoomManager")]
    [RequireComponent(typeof(NetworkRoomServer))]
    [RequireComponent(typeof(NetworkRoomClient))]
    [DisallowMultipleComponent]
    public class NetworkRoomManager : MonoBehaviour
    {
        [FormerlySerializedAs("Room server")]
        public NetworkRoomServer Server;
        [FormerlySerializedAs("Room client")]
        public NetworkRoomClient Client;
        [FormerlySerializedAs("sceneManager")]
        public NetworkSceneManager SceneManager;
        [FormerlySerializedAs("serverObjectManager")]
        public ServerObjectManager ServerObjectManager;
        [FormerlySerializedAs("clientObjectManager")]
        public ClientObjectManager ClientObjectManager;

        /// <summary>
        /// True if the server or client is started and running
        /// <para>This is set True in StartServer / StartClient, and set False in StopServer / StopClient</para>
        /// </summary>
        public bool IsNetworkActive => Server.Active || Client.Active;
    }
}
