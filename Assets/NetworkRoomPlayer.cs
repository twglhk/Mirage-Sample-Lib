using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirage
{
    public class NetworkRoomPlayer : NetworkBehaviour
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomPlayer));

        public GameObject testPrefab;

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

        [ServerRpc]
        private void ServerRequestSettingCharacter()
        {
            // TO DO : 캐릭터의 정보도 추가로 받아서 스폰 후 초기화
            //Debug.Log($"[Spawn Prefabs] {selected.name}");

            var characterObj = GameObject.Instantiate(testPrefab, new Vector3(1f, 0f, 0f), default);
            NetIdentity.ServerObjectManager.Spawn(characterObj, gameObject);
            
            Debug.Log($"[ServerRequestSettingCharacter] {NetIdentity.NetId}");

            // 스폰 페이즈
            //if (selected >= networkIdentity.ClientObjectManager.spawnPrefabs.Count)
            //{
            //    Debug.LogError("[Selected size is not matched]");
            //    return;
            //}

            // 서버에 스폰 후 ZooportsIdentity 할당
            //characterObj.GetComponent<CharacterDataBase>()._zooportsPlayerIdentity = networkIdentity;

            // 다른 클라이언트에 복사 스폰
            //networkIdentity.ServerObjectManager.Spawn(characterObj, gameObject);
        }
    }
}