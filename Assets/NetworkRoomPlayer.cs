using Mirage.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mirage
{
    public class NetworkRoomPlayer : NetworkBehaviour
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomPlayer));

        public GameObject testPrefab;
        public bool IsJump = false;
        public NetworkManager _networkManager;

        [FormerlySerializedAs("Game Scene")]
        [Scene]
        [SerializeField] string _gameSceneName;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            // ��Ʈ��ũ �Ŵ��� ĳ��
            _networkManager = FindObjectOfType<NetworkManager>().GetComponent<NetworkManager>();

            _networkManager.SceneManager.ClientChangeScene.AddListener(OnClientSceneChange);
            //NetIdentity.OnStartAuthority.AddListener(Spawn);

            Debug.Log("AWAKE!");
        }

        public void Spawn()
        {
            //NetworkIdentity identity = NetIdentity.ClientObjectManager.GetPrefab();

            //Debug.Log($"[identity] {identity.name} {identity.NetId}");

            ServerRequestSettingCharacter(NetIdentity);
            //ServerRequestSettingCharacter(0, NetIdentity);

            //NetIdentity.ClientObjectManager.RegisterPrefab(testPrefab.GetComponent<NetworkIdentity>());

            Debug.Log("[Spawn!]");
        }

        public void OnClientSceneChange(string sceneName, SceneOperation sceneOperation)
        {
            if (sceneName != "Game Scene") return;

            Debug.Log("[Client] OnClientSceneChange");
            ServerRequestSettingCharacter(NetIdentity);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsClient)
                {
                    Debug.Log("SPACE!!");
                    transform.position += new Vector3(0f, 1f, 0f);
                    //ServerRpcJump();
                }
            }

            if (IsJump)
            {
                if (IsServer)
                {
                    transform.position += new Vector3(0f, 1f, 0f);
                    IsJump = false;
                }
            }
        }


        [ServerRpc]
        protected virtual void ServerRpcJump()
        {
            IsJump = true;
        }

        [ServerRpc]
        private void ServerRequestSettingCharacter(NetworkIdentity owner)
        {
            // TO DO : ĳ������ ������ �߰��� �޾Ƽ� ���� �� �ʱ�ȭ
            //Debug.Log($"[Spawn Prefabs] {selected.name}");

            var characterObj = GameObject.Instantiate(Resources.Load("Spawnable", typeof(GameObject))) as GameObject;
            NetIdentity.ServerObjectManager.Spawn(characterObj, owner.ConnectionToClient);
            
            Debug.Log($"[ServerRequestSettingCharacter] {NetIdentity.NetId}");

            // ���� ������
            //if (selected >= networkIdentity.ClientObjectManager.spawnPrefabs.Count)
            //{
            //    Debug.LogError("[Selected size is not matched]");
            //    return;
            //}

            // ������ ���� �� ZooportsIdentity �Ҵ�
            //characterObj.GetComponent<CharacterDataBase>()._zooportsPlayerIdentity = networkIdentity;

            // �ٸ� Ŭ���̾�Ʈ�� ���� ����
            //networkIdentity.ServerObjectManager.Spawn(characterObj, gameObject);
        }
    }
}