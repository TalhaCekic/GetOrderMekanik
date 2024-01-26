using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "order Raiting", menuName = "Raiting")]
public class ScriptableRaiting :  ScriptableObject
{
   // public Image[] pp;
    public string[] CustomerName;
    public string[] CustomerCommentGood;
    public string[] CustomerCommentBad;
    public string[] CustomerCommentJustGood;

    public bool isGood;

    public GameObject raitingPrefab;
}
