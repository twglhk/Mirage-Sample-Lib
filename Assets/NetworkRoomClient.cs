using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mirage
{
    public class NetworkRoomClient : NetworkClient
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomClient));

        [FormerlySerializedAs("m_RoomPlayerPrefab")]
        [SerializeField]
        [Tooltip("Prefab to use for the Room Player")]
        public NetworkRoomPlayer roomPlayerPrefab;

        private void Awake()
        {
            Connected.AddListener(OnRoomClientConnected);
            Authenticated.AddListener(OnRoomClientAuthenticated);
            Disconnected.AddListener(OnRoomClientDisConnected);
        }

        #region room client virtual
        /// <summary>
        /// Event fires once the Client has connected its Server(Room).
        /// </summary>
        public virtual void OnRoomClientConnected(INetworkPlayer conn)
        {
            Debug.Log($"[OnRoomClientConnected] {conn.Identity.name}");
        }

        /// <summary>
        /// Event fires after the Client connection has sucessfully been authenticated with its Server(Room).
        /// </summary>
        public virtual void OnRoomClientAuthenticated(INetworkPlayer conn)
        {
            Debug.Log($"[OnRoomClientAuthenticated] {conn.Identity.name}");
        }

        /// <summary>
        /// Event fires after the Client has disconnected from its Server(Room) and Cleanup has been called.
        /// </summary>
        public virtual void OnRoomClientDisConnected()
        {
            Debug.Log("[OnRoomClientDisConnected]");
        }
        #endregion
    }
}