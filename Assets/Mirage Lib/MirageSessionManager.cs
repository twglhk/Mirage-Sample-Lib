using Cysharp.Threading.Tasks;
using Mirage;
using Mirage.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace John.MirageLib
{
    public enum NetworkMode
    {
        Host,
        Server,
        Client
    }


    public class MirageSessionManager : MonoBehaviour
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(MirageSessionManager));

        [Header("Manager Registy")]
        [FormerlySerializedAs("Network Manager")]
        [SerializeField] NetworkManager _networkManager;

        [Header("Network Setting")]
        [FormerlySerializedAs("NetworkAddress")]
        [SerializeField] string _networkAddress = "localhost";

        [FormerlySerializedAs("MaxPlayerCount")]
        [SerializeField] int _maxconnections = 2;

        [FormerlySerializedAs("NetworkMode")]
        [SerializeField] NetworkMode _networkMode;

        [Header("Scene Registry")]
        [FormerlySerializedAs("Lobby Scene")]
        [Scene]
        [SerializeField] string _lobbyScene;

        [FormerlySerializedAs("Game Scene")]
        [Scene]
        [SerializeField] string _gameScene;

        [Header("Test UI")]
        [FormerlySerializedAs("Connect Button")]
        [SerializeField] Button _connectButton;

        List<INetworkPlayer> _networkPlayers;

        public int NumberOfPlayers => _networkPlayers.Count();

        #region UnityEvent
        private void Awake()
        {
            DontDestroyOnLoad(this);

            _connectButton.onClick.AddListener(() =>
            {
                switch (_networkMode)
                {
                    case NetworkMode.Host:
                        logger.Log("[Connection Mode] Host");
                        _networkManager.Server.MaxConnections = _maxconnections;
                        _networkManager.Server.Started.AddListener(OnServerStart);
                        _networkManager.Server.Connected.AddListener(OnServerConnected);
                        _networkManager.Server.Authenticated.AddListener(OnServerAuthenticated);
                        _networkManager.Server.Disconnected.AddListener(OnServerDisconnected);
                        _networkManager.Server.Stopped.AddListener(OnServerStop);
                        _networkManager.NetworkSceneManager.ServerSceneChanged.AddListener(OnServerSceneChanged);
                        _networkManager.Client.Connected.AddListener(OnClientConnected);
                        _networkManager.Client.Authenticated.AddListener(OnClientAuthenticated);
                        _networkManager.NetworkSceneManager.ClientSceneChanged.AddListener(OnClientSceneChanged);
                        _networkManager.Server.StartHost(_networkManager.Client).Forget();
                        break;

                    case NetworkMode.Server:
                        logger.Log("[Connection Mode] Server");
                        _networkManager.Server.MaxConnections = _maxconnections;
                        _networkManager.Server.Started.AddListener(OnServerStart);
                        _networkManager.Server.Connected.AddListener(OnServerConnected);
                        _networkManager.Server.Authenticated.AddListener(OnServerAuthenticated);
                        _networkManager.Server.Disconnected.AddListener(OnServerDisconnected);
                        _networkManager.Server.Stopped.AddListener(OnServerStop);
                        _networkManager.NetworkSceneManager.ServerChangeScene.AddListener(OnServerSceneChanged);
                        _networkManager.Server.ListenAsync().Forget();
                        break;

                    case NetworkMode.Client:
                        logger.Log("[Connection Mode] Client");
                        _networkManager.Client.Connected.AddListener(OnClientConnected);
                        _networkManager.Client.Authenticated.AddListener(OnClientAuthenticated);
                        _networkManager.Client.Disconnected.AddListener(OnClientDisconnected);
                        _networkManager.NetworkSceneManager.ClientSceneChanged.AddListener(OnClientSceneChanged);
                        _networkManager.Client.ConnectAsync(_networkAddress).Forget();
                        break;
                }
            });
        }
        #endregion

        #region Server Handler
        private void OnServerStart()
        {
            _networkPlayers = new List<INetworkPlayer>();
            StartCoroutine("CoMatchingPlayerCount");
        }

        private void OnServerStop() { }

        private void OnServerConnected(INetworkPlayer networkPlayer)
        {
            if (networkPlayer == null) return;
            logger.Log($"[Server][OnServerConnected] {networkPlayer.Connection}");
        }

        private void OnServerDisconnected(INetworkPlayer networkPlayer)
        {
            logger.Log($"[Server][OnServerDisconnected] {networkPlayer.Connection}");
            _networkPlayers.Remove(networkPlayer);
            networkPlayer.UnregisterHandler<AddCharacterMessage>();
        }

        private void OnServerAuthenticated(INetworkPlayer networkPlayer)
        {
            logger.Log($"[Server][OnServerAuthenticated] {networkPlayer.Connection}");
            _networkPlayers.Add(networkPlayer);
            networkPlayer.RegisterHandler<AddCharacterMessage>(OnServerAddPlayerInternal);
        }

        void OnServerSceneChanged(string sceneName, SceneOperation sceneOperation)
        {
            if (sceneName != _gameScene) return;
            logger.Log($"[Server][OnServerSceneChanged] Game Scene Changed");
        }

        /// <summary>
        /// [Server] Handle <see cref="AddCharacterMessage"/> on server
        /// </summary>
        /// <param name="player"></param>
        /// <param name="msg"></param>
        void OnServerAddPlayerInternal(INetworkPlayer player, AddCharacterMessage msg)
        {
            if (player.Identity != null)
                throw new InvalidOperationException("There is already a player for this connection.");

            logger.Log($"[Server][OnServerAddPlayerInternal] recv {msg} from {player}");

            OnServerAddPlayer(player);
        }

        /// <summary>
        /// Called on the server when a client adds a new player with ClientScene.AddPlayer.
        /// <para>The default implementation for this function creates a new player object from the playerPrefab.</para>
        /// </summary>
        /// <param name="player">Connection from client.</param>
        void OnServerAddPlayer(INetworkPlayer player)
        {
            // Instantiation
            //_networkManager.ServerObjectManager.AddCharacter(player, characterObj);
        }
        #endregion

        #region Client Handler
        private void OnClientConnected(INetworkPlayer networkPlayer)
        {
            logger.Log($"[OnClientConnected][{networkPlayer}]");
        }

        private void OnClientDisconnected()
        {
            logger.Log($"[OnClientDisconnected]");
        }

        private void OnClientAuthenticated(INetworkPlayer networkPlayer)
        {
            logger.Log($"[OnClientAuthenticated][{networkPlayer}]");
        }

        private void OnClientSceneChanged(string sceneName, SceneOperation sceneOperation)
        {
            if (sceneName != _gameScene) return;

            logger.Log($"[Client][OnClientSceneChanged] Game Scene Changed");

            _networkManager.Client.Send(new AddCharacterMessage());

            logger.Log($"[Client][OnClientSceneChanged] <AddCharacterMsg> Send");
        }
        #endregion

        #region Coroutines
        private IEnumerator CoMatchingPlayerCount()
        {
            YieldInstruction yield = new WaitForSeconds(0.5f);

            while (true)
            {
                yield return yield;
                if (NumberOfPlayers < _maxconnections) continue;
                
                logger.Log($"[Server] All Player Ready");
                _networkManager.NetworkSceneManager.ChangeServerScene(_gameScene);
                break;
            }
        }
        #endregion
    }
}