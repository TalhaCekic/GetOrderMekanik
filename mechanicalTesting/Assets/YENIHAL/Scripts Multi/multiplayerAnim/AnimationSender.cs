using Unity.Netcode.Components;
using Unity.Netcode;
using UnityEngine;

public class AnimationSender : NetworkAnimator
{
    public NetworkAnimator networkAnimator;

    void Update()
    {
        if (IsLocalPlayer) // Sadece yerel oyuncu i�in animasyon oynatma i�lemi yap�lacak
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                networkAnimator.SetTrigger("test"); // "Jump" ad�ndaki animasyonu tetikle
          //      networkAnimator.GetComponent<Animator>().SetFloat("Speed", 1);

            }
        }
    }

    public NetworkVariable<float> animationState = new NetworkVariable<float>(0f); // Senkronize edilecek animasyon durumu

    //void Update()
    //{
    //    if (IsLocalPlayer)
    //    {
    //        // Animasyon durumunu g�ncelleme i�lemleri
    //        float newState = CalculateAnimationState();
    //        SetAnimationState(newState);
    //        SendAnimationState(newState);
    //    }
    //}

    void SetAnimationState(float state)
    {
        animationState.Value = state; // Animasyon durumunu g�ncelle
    }

    float CalculateAnimationState()
    {
        // Animasyon durumunu hesaplama i�lemleri
        return 0.5f;
    }


    void SendAnimationState(float state)
    {
        animationState.Value = state; // Animasyon durumunu sunucuya g�nder
    }
}
