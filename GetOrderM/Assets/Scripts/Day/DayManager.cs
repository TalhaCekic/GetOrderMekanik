using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayManager : NetworkBehaviour
{
    public static DayManager instance;

    public Light directionalLight;
    public GameObject lightObject;
    
    [SyncVar] private bool change;
    [SyncVar] public bool dayOn;
    [SyncVar] public int day;
    [SyncVar] public int orderDay;
    [SyncVar] public int readyPlayerNumber;

    [SerializeField] public TMP_Text DayText;
    [SerializeField] public TMP_Text lobbyNumberText;
    [SerializeField] public TMP_Text readyPlayerText;

    [SerializeField] public GameObject dayTextObject;
    [SerializeField] public GameObject readyDay;
    [SerializeField] public GameObject daySelect;

    // seçim için objeler
    public ScriptableDaySelect[] daySelects;
    [SyncVar] public bool isdaySelects;
    [SyncVar] public bool _isdaySelects;
    [SyncVar] public bool isdaySelected;
    public Button rightButton;
    public TMP_Text RightText;
    public Button leftButton;
    public TMP_Text LeftText;

    // seçilmişler
    [SyncVar] public string stringSelect1;
    [SyncVar] public string stringSelect2;
    [SyncVar] public int intSelect1;
    [SyncVar] public int intSelect2;
    [SyncVar] public string stringSelectValue1;
    [SyncVar] public string stringSelectValue2;

    // Dönüştürülmüş değeri saklamak için bir değişken
    [SyncVar] public float lightValue; 
    [SyncVar] public float targeValue; 
    [SyncVar] public float smoothTime = 0.5f; 
    [SyncVar] public float velocity = 0.0f; 
    private void Start()
    {
        lightValue = 0;
        orderDay = 0;
        dayOn = false;
        instance = this;
        isdaySelects = false;
        _isdaySelects = true;
        isdaySelected = false;
        readyDay.SetActive(false);
        daySelect.SetActive(false);
        RightText = rightButton.GetComponentInChildren<TMP_Text>();
        LeftText = leftButton.GetComponentInChildren<TMP_Text>();
        lightObject = GameObject.Find("Directional Light");
        directionalLight = lightObject.GetComponent<Light>();

        if (isServer)
        {
            rightButton.onClick.AddListener(() =>
                RightButtonClicked(stringSelect1, intSelect1, stringSelectValue1));
            leftButton.onClick.AddListener(() =>
                LeftButtonClicked(stringSelect2, intSelect2, stringSelectValue2));
        }
  
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (isServer && currentScene.name == "PcScene")
        {
            cmdDayTimer();
            SelectTwoObjects();
            if (day > 0)
            {
                lightSettings(ManagerOrder.instance.orderHistory.Count, orderDay);
            }

        }
    }
    [Command(requiresAuthority = false)]
    void lightSettings(float value1, float value2)
    {
        rpcLightSettings(value1, value2);
    }

    [ClientRpc]
    public void rpcLightSettings(float value1, float value2)
    {
        targeValue =  (1 - (value1 / value2));
        lightValue = Mathf.SmoothDamp(lightValue, targeValue, ref velocity, smoothTime);
        
        directionalLight.intensity = lightValue;
    }
    [Command(requiresAuthority = false)]
    public void cmdDayTimer()
    {
        rpcDayTimer();
    }

    [ClientRpc]
    public void rpcDayTimer()
    {
        DayTimer();
    }

    public void DayTimer()
    {

        if (change)
        {
            day++;
            orderDay += 2;
            change = false;
        }

        // day
        if (dayOn)
        {
            //directionalLight.intensity = 1;
            readyDay.SetActive(false);
            dayTextObject.SetActive(true);
            if (ManagerOrder.instance.orderHistory.Count == orderDay)
            {
                // ManagerOrder.instance.nextOrderTime = 100;
                if (ManagerOrder.instance.orderUI.Count == 0)
                {
                    dayOn = false;
                    readyPlayerNumber = 0;
                }
            }
        }

        if (!dayOn)
        {
            //directionalLight.intensity = 0;
            readyDay.SetActive(true);
            if (ManagerOrder.instance.orderHistory.Count == orderDay && ManagerOrder.instance.orderArray.Count == 0)
            {
                dayOn = false;
            }

            // ready sistemini kontrol eder 
            PlayerManager[] playerManagers = GameObject.FindObjectsOfType<PlayerManager>();
            for (int i = 0; i < playerManagers.Length; i++)
            {
                lobbyNumberText.text = playerManagers.Length.ToString();
                PlayerManager player = playerManagers[i];

                if (player.isDayReady)
                {
                    if (playerManagers.Length == readyPlayerNumber)
                    {
                        dayOn = true;
                        change = true;
                    }

                    break;
                }
            }

            // seçim sistemi
            switch (day)
            {
                case 2:
                    if(isdaySelected) return;
                    daySelect.SetActive(true);
                    readyDay.SetActive(false);
                    isdaySelects = true;
                    break;
                case 6:
                    if(isdaySelected) return;
                    daySelect.SetActive(true);
                    readyDay.SetActive(false);
                    isdaySelects = true;
                    break;
                case 9:
                    if(isdaySelected) return;
                    daySelect.SetActive(true);
                    readyDay.SetActive(false);
                    isdaySelects = true;
                    break;
                case 12:
                    if(isdaySelected) return;
                    daySelect.SetActive(true);
                    readyDay.SetActive(false);
                    isdaySelects = true;
                    break;
                case 15:
                    if(isdaySelected) return;
                    daySelect.SetActive(true);
                    readyDay.SetActive(false);
                    isdaySelects = true;
                    break;
                default:
                    daySelect.SetActive(false);
                    readyDay.SetActive(true);
                    _isdaySelects = true;
                    isdaySelected = false;
                    break;
            }
            DayText.text = day.ToString();
            readyPlayerText.text = readyPlayerNumber.ToString();
        }
        else
        {
            dayTextObject.SetActive(true);
            readyDay.SetActive(false);
            DayText.text = day.ToString();
            readyPlayerText.text = readyPlayerNumber.ToString();
        }
    }

    [Command(requiresAuthority = false)]
    public void readyPlayer(int value)
    {
        readyPlayerNumber += value;
    }

    [Command(requiresAuthority = false)]
    public void notReadyPlayer(int value)
    {
        readyPlayerNumber -= value;
    }

    [Server]
    public void SelectTwoObjects()
    {
        if (isdaySelects && _isdaySelects)
        {
            // Rastgele iki farklı indeks seç
            int index1 = Random.Range(0, daySelects.Length);
            int index2;
            do
            {
                index2 = Random.Range(0, daySelects.Length);
            } while (index2 == index1);

            // Seçilen iki objeyi kullanabilirsiniz
            ScriptableDaySelect selectedObject1 = daySelects[index1];
            ScriptableDaySelect selectedObject2 = daySelects[index2];

            rpcSelectObjects(selectedObject1.objectName, selectedObject1.value, selectedObject1.stringValue,
                selectedObject2.objectName, selectedObject2.value, selectedObject2.stringValue);

            stringSelect1 = selectedObject1.objectName;
            stringSelect2 = selectedObject2.objectName;
            intSelect1 = selectedObject1.value;
            intSelect2 = selectedObject2.value;
            stringSelectValue1 = selectedObject1.stringValue;
            stringSelectValue2 = selectedObject2.stringValue;


            _isdaySelects = false;
            isdaySelects = false;
            Destroy(daySelects[index1]);
            Destroy(daySelects[index2]);
        }
    }

    [ClientRpc]
    public void rpcSelectObjects(string rightText, int rightValue, string rightStringValue, string leftText,
        int leftValue, string leftStringValue)
    {
        RightText.text = rightText + "  " + rightValue + "  " + rightStringValue;
        LeftText.text = leftText + "  " + leftValue + "  " + leftStringValue;
    }

    
    //button
    [Command(requiresAuthority = false)]
    public void RightButtonClicked(string rightText, int rightValue, string rightStringValue)
    {
        rpcRightButtonClicked(rightText,rightValue,rightStringValue);
    }

    [ClientRpc]
    public void rpcRightButtonClicked(string rightText, int rightValue, string rightStringValue)
    {
        switch (rightText)
        {
            case "Yeri Temizleme Hızı":
                if (rightStringValue == "Artar")
                {
                    isdaySelected = true;
                    Debug.Log("yer temizleme hızı arttırırldı");
                    daySelect.SetActive(false);
                }

                if (rightStringValue == "Azalır")
                {
                    isdaySelected = true;
                    Debug.Log("yer temizleme hızı azaltıldı");
                    daySelect.SetActive(false);
                }

                break;
            case "Düşme düşme süresi":
                if (rightStringValue == "Artar")
                {
                    isdaySelected = true;
                    Debug.Log("Düşme düşme süresi arttırırldı");
                    daySelect.SetActive(false);
                }

                if (rightStringValue == "Azalır")
                {
                    isdaySelected = true;
                    Debug.Log("Düşme düşme süresi azaltıldı");
                    daySelect.SetActive(false);
                }

                break;
        }
    }
    
    [Command(requiresAuthority = false)]
    public void LeftButtonClicked(string leftText, int leftValue, string leftStringValue)
    {
        rpcLeftButtonClicked(leftText,leftValue,leftStringValue);
    }

    [ClientRpc]
    public void rpcLeftButtonClicked(string leftText, int leftValue, string leftStringValue)
    {
        switch (leftText)
        {
            case "Yeri Temizleme Hızı":
                if (leftStringValue == "Artar")
                {
                    isdaySelected = true;
                    Debug.Log("yer temizleme hızı arttırırldı");
                    daySelect.SetActive(false);
                }

                if (leftStringValue == "Azalır")
                {
                    isdaySelected = true;
                    Debug.Log("yer temizleme hızı azaltıldı");
                    daySelect.SetActive(false);
                }

                break;
            case "Düşme düşme süresi":
                if (leftStringValue == "Artar")
                {
                    isdaySelected = true;
                    Debug.Log("Düşme düşme süresi arttırırldı");
                    daySelect.SetActive(false);
                }

                if (leftStringValue == "Azalır")
                {
                    isdaySelected = true;
                    Debug.Log("Düşme düşme süresi azaltıldı");
                    daySelect.SetActive(false);
                }

                break;
        }
    }
    
}