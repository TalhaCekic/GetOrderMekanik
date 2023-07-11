using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform spawnObjectPrefab ;
    private Transform spawnObjectPrefabTransform;

    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData  {
            _int =56,
            _bool = true,
        },NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct MyCustomData : INetworkSerializable
    {
        public int _int;
        public bool _bool;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }
    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) =>
        {
            Debug.Log(OwnerClientId + " ; " + newValue._int + " ; " + newValue._bool + " ; " + newValue.message);
        };
    }

    private void Start()
    {
     
    }
    void Update()
    { 
        if(IsOwner)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                spawnObjectPrefabTransform = Instantiate(spawnObjectPrefab);
                spawnObjectPrefabTransform.GetComponent<NetworkObject>().Spawn(true);

                // TestClientRpc();
                //randomNumber.Value = new MyCustomData
                //{
                //    _int = 10,
                //    _bool = false,
                //    message = "All your base are belnt to us"
                //};
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                spawnObjectPrefabTransform.GetComponent<NetworkObject>().Despawn(true);
             //   Destroy(spawnObjectPrefabTransform.gameObject);
            }
        }
        if (!IsOwner) return;
       
        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    private void TestServerRPC()
    {
        Debug.Log("TEST SERVER RPC   " + OwnerClientId) ;
    }
    [ClientRpc]
    private void TestClientRpc()
    {
        Debug.Log("TEST Client RPC   " + OwnerClientId);
    }
 
}


