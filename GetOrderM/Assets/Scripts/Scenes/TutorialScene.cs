using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class TutorialScene : NetworkBehaviour
{
    public void TutorialSceneButton()
    {
        SceneManager.LoadScene(2);
    }
}
