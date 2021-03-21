using Mirage.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mirage
{
    public class NetworkRoomClient : NetworkClient
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomClient));

        public NetworkManager _networkManager;

        private void Awake()
        {
            Connected.AddListener(OnRoomClientConnected);
            Authenticated.AddListener(OnRoomClientAuthenticated);
            Disconnected.AddListener(OnRoomClientDisConnected);
            //_networkManager.SceneManager.ClientChangeScene.AddListener((str, load) => { Debug.Log("[Client Scene Changed!]"); });
        }

        #region room client virtual
        /// <summary>
        /// Event fires once the Client has connected its Server(Room).
        /// </summary>
        public virtual void OnRoomClientConnected(INetworkPlayer conn)
        {
            Debug.Log($"[OnRoomClientConnected]");
        }

        /// <summary>
        /// Event fires after the Client connection has sucessfully been authenticated with its Server(Room).
        /// </summary>
        public virtual void OnRoomClientAuthenticated(INetworkPlayer conn)
        {
            Debug.Log($"[OnRoomClientAuthenticated]");
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