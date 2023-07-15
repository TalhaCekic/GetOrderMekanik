using Unity.Netcode.Components;
using Unity.Netcode;
using UnityEngine;


public class AnimationSender : NetworkBehaviour
{
    public NetworkAnimator networkAnimator;

    void Update()
    {
        if (IsLocalPlayer) // Sadece yerel oyuncu için animasyon oynatma iþlemi yapýlacak
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                networkAnimator.SetTrigger("Jump"); // "Jump" adýndaki animasyonu tetikle
            }
        }
    }
}
