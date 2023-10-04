using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Customerize : NetworkBehaviour
{
    public Color[] colors;
    public Material tshitirMaterial;


    public void ChangeTshirtColor(int index)
    {
        //  if(isLocalPlayer)
        tshitirMaterial.color = colors[index];
    }
}
