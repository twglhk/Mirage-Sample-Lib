using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mirage
{
    public class NetworkRoomServer : NetworkServer
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomServer));

        [Header("Manager Registry")]
        [FormerlySerializedAs("NetworkManager")]
        public NetworkManager networkManager;

        void Awake()
        {
            Started.AddListener(OnRoomStartServer);
            Stopped.AddListener(OnRoomStopServer);
            Connected.AddListener(OnRoomServerConnected);
            Authenticated.AddListener(OnRoomServerAuthenticated);
            Disconnected.AddListener(OnRoomServerDisConnected);
            OnStartHost.AddListener(OnRoomStartHost);
            OnStopHost.AddListener(OnRoomStopHost);
        }

        #region room server virtuals
        /// <summary>
        /// This is invoked when a server is started - including when a host is started.
        /// </summary>
        public virtual void OnRoomStartServer() 
        {
            Debug.Log("[OnRoomStartServer]");
            //networkManager.SceneManager.ChangeServerScene(_roomScenePath);
        }

        public virtual void OnRoomStopServer()
        {
            Debug.Log("[OnRoomStopServer]");
            //networkManager.SceneManager.ChangeServerScene(_offlineScenePath);
        }

        /// <summary>
        /// Event fires once a new Client has connect to the Room
        /// </summary>
        public virtual void OnRoomServerConnected(INetworkPlayer conn)
        {
            Debug.Log($"[OnRoomServerConnected]");
        }

        /// <summary>
        /// Event fires once a new Client has passed Authentication to the Room
        /// </summary>
        public virtual void OnRoomServerAuthenticated(INetworkPlayer conn)
        {
            Debug.Log($"[OnRoomServerAuthenticated]");
        }

        /// <summary>
        /// Event fires once a Client has Disconnected from the Room.
        /// </summary>
        public virtual void OnRoomServerDisConnected(INetworkPlayer conn)
        {
            Debug.Log($"[OnRoomServerDisConnected]");
        }

        /// <summary>
        /// This is invoked when a host is started.
        /// </summary>
        public virtual void OnRoomStartHost()
        {
            Debug.Log("[OnRoomStartHost]");
        }

        /// <summary>
        /// This is called when a host is stopped.
        /// </summary>
        public virtual void OnRoomStopHost()
        {
            Debug.Log("[OnRoomStopHost]");
        }
        #endregion
    }
}