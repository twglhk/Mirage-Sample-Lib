using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [Scene]
        public string gameScene;

        public List<INetworkPlayer> _connectedPlayers = new List<INetworkPlayer>();
        public int NumberOfPlayers => _connectedPlayers.Count(kv => kv.Connection != null);

        private void Awake()
        {
            Server.Authenticated.AddListener(OnServerAuthenticated);
            Server.Connected.AddListener(OnServerConnected);
            Server.Disconnected.AddListener(OnServerDisconnected);
        }

        private void Update()
        {
            Debug.Log($"[Server][NumberOfPlayers] {NumberOfPlayers}");
        }

        public void SceneLoad()
        {
            SceneManager.ChangeServerScene(gameScene, SceneOperation.Normal);
        }

        #region Server
        private void OnServerConnected(INetworkPlayer networkPlayer)
        {
            Debug.Log($"[Server][OnServerConnected] {networkPlayer.Connection}");
            
            if (networkPlayer != null)
            {
                _connectedPlayers.Add(networkPlayer);
            }
        }

        private void OnServerDisconnected(INetworkPlayer networkPlayer)
        {
            Debug.Log($"[Server][OnServerDisconnected] {networkPlayer.Connection}");
            if (networkPlayer != null)
            {
                _connectedPlayers.Remove(networkPlayer);
            }
        }

        private void OnServerAuthenticated(INetworkPlayer player)
        {
            player.RegisterHandler<AddCharacterMessage>(OnServerAddPlayerInternal);
        }

        /// <summary>
        /// [Server] 클라이언트에서 AddCharacterMessage 를 받았을 때 호출
        /// </summary>
        /// <param name="player"></param>
        /// <param name="msg"></param>
        void OnServerAddPlayerInternal(INetworkPlayer player, AddCharacterMessage msg)
        {
            if (player.Identity != null)
                throw new InvalidOperationException("There is already a player for this connection.");

            OnServerAddPlayer(player);
        }

        /// <summary>
        /// Called on the server when a client adds a new player with ClientScene.AddPlayer.
        /// <para>The default implementation for this function creates a new player object from the playerPrefab.</para>
        /// </summary>
        /// <param name="player">Connection from client.</param>
        public virtual void OnServerAddPlayer(INetworkPlayer player)
        {
            //Instantiation
            //ServerObjectManager.AddCharacter(player, character.gameObject);
        }
        #endregion

        #region Client
        public void ClientRequestAddPlayer()
        {
            Client.Send(new AddCharacterMessage());
        }

        public void ClientRegisterPrefab()
        {
            // Case 1. Register prefab manually
            //ClientObjectManager.RegisterPrefab(playerPrefab);

            // Case 2. Register prefab to ClientObjectManager->Prefaps on inspector
        }
        #endregion

        /// <summary>
        /// True if the server or client is started and running
        /// <para>This is set True in StartServer / StartClient, and set False in StopServer / StopClient</para>
        /// </summary>
        public bool IsNetworkActive => Server.Active || Client.Active;
    }
}
