using Unity.Netcode.Components;
using Unity.Netcode;
using UnityEngine;


public class AnimationSender : NetworkBehaviour
{
    public NetworkAnimator networkAnimator;

    void Update()
    {
        if (IsLocalPlayer) // Sadece yerel oyuncu i�in animasyon oynatma i�lemi yap�lacak
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                networkAnimator.SetTrigger("Jump"); // "Jump" ad�ndaki animasyonu tetikle
            }
        }
    }
}
