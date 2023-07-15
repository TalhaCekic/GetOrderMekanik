using Unity.Netcode.Components;
using Unity.Netcode;
using UnityEngine;

public class AnimationSender : NetworkAnimator
{
    public NetworkAnimator networkAnimator;

    void Update()
    {
        if (IsLocalPlayer) // Sadece yerel oyuncu için animasyon oynatma iþlemi yapýlacak
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                networkAnimator.SetTrigger("test"); // "Jump" adýndaki animasyonu tetikle
          //      networkAnimator.GetComponent<Animator>().SetFloat("Speed", 1);

            }
        }
    }

    public NetworkVariable<float> animationState = new NetworkVariable<float>(0f); // Senkronize edilecek animasyon durumu

    //void Update()
    //{
    //    if (IsLocalPlayer)
    //    {
    //        // Animasyon durumunu güncelleme iþlemleri
    //        float newState = CalculateAnimationState();
    //        SetAnimationState(newState);
    //        SendAnimationState(newState);
    //    }
    //}

    void SetAnimationState(float state)
    {
        animationState.Value = state; // Animasyon durumunu güncelle
    }

    float CalculateAnimationState()
    {
        // Animasyon durumunu hesaplama iþlemleri
        return 0.5f;
    }


    void SendAnimationState(float state)
    {
        animationState.Value = state; // Animasyon durumunu sunucuya gönder
    }
}
