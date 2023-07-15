using Unity.Netcode.Components;
using Unity.Netcode;
using UnityEngine;


public class AnimationReceiver : NetworkBehaviour
{
    public NetworkAnimator networkAnimator;

 

    void OnAnimationEventTriggered(NetworkAnimator animator, byte triggerIndex)
    {
        if (animator != networkAnimator) return;

        if (!IsLocalPlayer) // Yerel oyuncu olmayanlar i�in animasyon tetiklendi�inde
        {
            networkAnimator.SetTrigger(triggerIndex);
        }
    }
}
