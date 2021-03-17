using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirage
{
    public class NetworkRoomPlayer : NetworkBehaviour
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomPlayer));

        public GameObject testPrefab;
        public bool IsJump = false;

        private void Awake()
        {
            NetIdentity.OnStartAuthority.AddListener(Spawn);

            Debug.Log("AWAKE!");
        }

        public void Spawn()
        {
            //NetworkIdentity identity = NetIdentity.ClientObjectManager.GetPrefab();

            //Debug.Log($"[identity] {identity.name} {identity.NetId}");

            ServerRequestSettingCharacter();
            //ServerRequestSettingCharacter(0, NetIdentity);

            //NetIdentity.ClientObjectManager.RegisterPrefab(testPrefab.GetComponent<NetworkIdentity>());

            Debug.Log("[Spawn!]");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsClient)
                {
                    Debug.Log("SPACE!!");
                    ServerRpcJump();
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
        private void ServerRequestSettingCharacter()
        {
            // TO DO : ĳ������ ������ �߰��� �޾Ƽ� ���� �� �ʱ�ȭ
            //Debug.Log($"[Spawn Prefabs] {selected.name}");

            var characterObj = GameObject.Instantiate(testPrefab, new Vector3(1f, 0f, 0f), default);
            NetIdentity.ServerObjectManager.Spawn(characterObj, gameObject);
            
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