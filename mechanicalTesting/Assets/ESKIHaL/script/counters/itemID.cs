using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class itemID : NetworkBehaviour 
{
 
    public NetworkVariable<float> ItemID = new NetworkVariable<float>();
    public string Name;
}
