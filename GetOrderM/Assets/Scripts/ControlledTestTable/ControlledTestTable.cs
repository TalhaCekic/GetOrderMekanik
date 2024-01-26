using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControlledTestTable : NetworkBehaviour
{
    [SyncVar] public bool kasaDolu = false;
    [SyncVar] public bool anaKartDolu = false;
    [SyncVar] public bool cpuDolu = false;
    [SyncVar] public bool ekranKartiDolu = false;
    [SyncVar] public bool ramDolu = false;
    [SyncVar] public bool nullObject = true;

    [SyncVar] public float submidID;

    public List<GameObject> childObject = new List<GameObject>();
    public List<GameObject> uiGameobject = new List<GameObject>();

    [SyncVar] public string defectivePart;
    [SyncVar] public string selectedBoolVariableName;
    [SyncVar] public bool selectedBoolValue;

    //controlled system
    [SyncVar] public float controlledTime = 5;
    [SerializeField] public Slider controlledTimeSlider;
    [SyncVar] public bool isControling;
    [SyncVar] public bool isControlled;
    [SyncVar] public bool isReady;

    public GameObject canvas;
    public GameObject unsuccessful;
    public GameObject successful;

    //arýzalý ürün seçimi
    [Header("Selection Probability")]
    public float noSelectionProbability = 0.2f; // Hiçbirini seçme olasýlýðý (örneðin, 0.2 = %20)
    private Dictionary<string, bool> boolVariables = new Dictionary<string, bool>();
    private void Start()
    {
        isControling = false;
        isControlled = false;
        isReady = false;
        nullObject = true;
        controlledTime = 5;
    }
    void Update()
    {
        if (isServer)
        {
            serverServer();
            CmdSliderBar();
            CmdDefectivePart(selectedBoolVariableName);
            cmdidCheck();
        }
    }
    [Command(requiresAuthority = false)]
    public void CmdinteractID(float objectNumber)
    {
        interactID(objectNumber);
    }
    [ClientRpc]
    public void RpcinteractID(float objectNumber)
    {
        interactID(objectNumber);
    }
    public void interactID(float objectNumber)
    {
        if (objectNumber == 0) // boþ
        {
            submidID = 0;
            selectedBoolVariableName = null;
            isControling = false;
            isReady = false;
            isControlled = false;
            canvas.SetActive(false);
            unsuccessful.SetActive(false);
            successful.SetActive(false);
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }
            for (int i = 0; i < uiGameobject.Count; i++)
            {
                uiGameobject[i].SetActive(false);
            }
        }
        if (!isReady)
        {
            if (objectNumber == 1) // kasa
            {
                submidID = 1;
                childObject[1].SetActive(true);
                childObject[2].SetActive(false);
                childObject[3].SetActive(false);
                childObject[4].SetActive(false);
                childObject[5].SetActive(false);
            }
            if (objectNumber == 2) // anakar
            {
                childObject[1].SetActive(false);
                childObject[2].SetActive(true);
                childObject[3].SetActive(false);
                childObject[4].SetActive(false);
                childObject[5].SetActive(false);
                submidID = 2;
            }
            if (objectNumber == 3) // CPU
            {
                childObject[3].SetActive(true);
                childObject[1].SetActive(false);
                childObject[2].SetActive(false);
                childObject[4].SetActive(false);
                childObject[5].SetActive(false);
                submidID = 3;
            }
            if (objectNumber == 4) // ekran kartý
            {
                childObject[4].SetActive(true);
                childObject[1].SetActive(false);
                childObject[2].SetActive(false);
                childObject[3].SetActive(false);
                childObject[5].SetActive(false);
                submidID = 4;
            }
            if (objectNumber == 5) // ram
            {
                childObject[5].SetActive(true);
                childObject[1].SetActive(false);
                childObject[2].SetActive(false);
                childObject[3].SetActive(false);
                childObject[4].SetActive(false);
                submidID = 5;
            }
            if (objectNumber == 12)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                submidID = 12;
            }
            if (objectNumber == 123)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[3].SetActive(true);
                submidID = 123;
            }
            if (objectNumber == 124)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[4].SetActive(true);
                submidID = 124;
            }
            if (objectNumber == 125)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[5].SetActive(true);
                submidID = 125;
            }
            if (objectNumber == 1234)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[3].SetActive(true);
                childObject[4].SetActive(true);
                submidID = 1234;
            }
            if (objectNumber == 1235)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[3].SetActive(true);
                childObject[5].SetActive(true);
                submidID = 1235;
            }
            if (objectNumber == 1245)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[4].SetActive(true);
                childObject[5].SetActive(true);
                submidID = 1245;
            }
            if (objectNumber == 12345)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[3].SetActive(true);
                childObject[4].SetActive(true);
                childObject[5].SetActive(true);
                submidID = 12345;
            }
        }
        if (isReady)
        {
            if (objectNumber == 1) // kasa
            {
                submidID = 1.2f;
                childObject[1].SetActive(true);
                childObject[2].SetActive(false);
                childObject[3].SetActive(false);
                childObject[4].SetActive(false);
                childObject[5].SetActive(false);
            }
            if (objectNumber == 2) // anakar
            {
                submidID = 2.2f;
                childObject[1].SetActive(false);
                childObject[2].SetActive(true);
                childObject[3].SetActive(false);
                childObject[4].SetActive(false);
                childObject[5].SetActive(false);
            }
            if (objectNumber == 3) // CPU
            {
                childObject[3].SetActive(true);
                childObject[1].SetActive(false);
                childObject[2].SetActive(false);
                childObject[4].SetActive(false);
                childObject[5].SetActive(false);
                submidID = 3.2f;
            }
            if (objectNumber == 4) // ekran kartý
            {
                childObject[4].SetActive(true);
                childObject[1].SetActive(false);
                childObject[2].SetActive(false);
                childObject[3].SetActive(false);
                childObject[5].SetActive(false);
                submidID = 4.2f;
            }
            if (objectNumber == 5) // ram
            {
                childObject[5].SetActive(true);
                childObject[1].SetActive(false);
                childObject[2].SetActive(false);
                childObject[3].SetActive(false);
                childObject[4].SetActive(false);
                submidID = 5.2f;
            }
            if (objectNumber == 12)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                submidID = 12.2f;
            }
            if (objectNumber == 123)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[3].SetActive(true);
                submidID = 123.2f;
            }
            if (objectNumber == 124)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[4].SetActive(true);
                submidID = 124.2f;
            }
            if (objectNumber == 125)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[5].SetActive(true);
                submidID = 125.2f;
            }
            if (objectNumber == 1234)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[3].SetActive(true);
                childObject[4].SetActive(true);
                submidID = 1234.2f;
            }
            if (objectNumber == 1235)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[3].SetActive(true);
                childObject[5].SetActive(true);
                submidID = 1235.2f;
            }
            if (objectNumber == 1245)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[4].SetActive(true);
                childObject[5].SetActive(true);
                submidID = 1245.2f;
            }
            if (objectNumber == 12345)
            {
                childObject[1].SetActive(true);
                childObject[2].SetActive(true);
                childObject[3].SetActive(true);
                childObject[4].SetActive(true);
                childObject[5].SetActive(true);
                submidID = 12345.2f;
            }
        }

        if (submidID >= 12 && controlledTime >= 0)
        {
            isControling = true;
            if (isControlled)
            {
                controlledTime = 5;
            }
            canvas.SetActive(true);
        }
        if (controlledTime <= 0)
        {
            if (!isControlled && !isControling)
            {
                controlledTime = 5;
            }
        }
    }
    [Command(requiresAuthority = false)]
    public void serverServer()
    {
        ServerRandom();
    }
    [Server] // random hata verdiği yer
    public void ServerRandom()
    {
        if (selectedBoolVariableName == null)
        {
            if (controlledTime <= 0 && isControling)
            {
                // arýzalý ürün seçimi
                // Bool deðiþkenlerini bir dictionary içinde topla
                boolVariables["kasaDolu"] = kasaDolu;
                boolVariables["anaKartDolu"] = anaKartDolu;
                boolVariables["cpuDolu"] = cpuDolu;
                boolVariables["ekranKartýDolu"] = ekranKartiDolu;
                boolVariables["ramDolu"] = ramDolu;
                boolVariables["null"] = nullObject;

                // Sadece true olanlarý filtrele
                List<string> trueKeys = boolVariables.Where(kv => kv.Value == true).Select(kv => kv.Key).ToList();

                // Rastgele bir bool deðiþkeni seç
                if (trueKeys.Count > 0)
                {
                    int randomIndex = new System.Random().Next(0, trueKeys.Count);
                    selectedBoolVariableName = trueKeys[randomIndex];

                    // Seçilen bool deðiþkeninin adýný kullanabilirsiniz
                    selectedBoolValue = boolVariables[selectedBoolVariableName];
                    isControlled = true;
                }
            }
        }
        else
        {
            isControlled = true;
            isControling = false;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdDefectivePart(string strings)
    {
        RpcDefectivePart(strings);
    }
    [ClientRpc]
    public void RpcDefectivePart(string strings)
    {
        if (selectedBoolVariableName == "kasaDolu")
        {
            uiGameobject[1].SetActive(true);
            unsuccessful.SetActive(true);
            successful.SetActive(false);
        }
        if (selectedBoolVariableName == "anaKartDolu")
        {
            uiGameobject[2].SetActive(true);
            unsuccessful.SetActive(true);
            successful.SetActive(false);
        }
        if (selectedBoolVariableName == "cpuDolu")
        {
            uiGameobject[3].SetActive(true);
            unsuccessful.SetActive(true);
            successful.SetActive(false);
        }
        if (selectedBoolVariableName == "ekranKartiDolu")
        {
            uiGameobject[4].SetActive(true);
            unsuccessful.SetActive(true);
            successful.SetActive(false);
        }
        if (selectedBoolVariableName == "ramDolu")
        {
            uiGameobject[5].SetActive(true);
            unsuccessful.SetActive(true);
            successful.SetActive(false);
        }
        if (selectedBoolVariableName == "null")
        {
            unsuccessful.SetActive(false);
            successful.SetActive(true);
            isReady = true;
            for (int i = 0; i < uiGameobject.Count; i++)
            {
                uiGameobject[i].SetActive(false);
            }
        }
    }

    [Command(requiresAuthority = false)]
    void cmdidCheck()
    {
        idCheck();
    }
    public void idCheck()
    {
        if (!kasaDolu && !anaKartDolu && !cpuDolu && !ekranKartiDolu && !ramDolu)
        {
            if (isServer)
            {
                RpcinteractID(0);
            }
            else if (isClient)
            {
                CmdinteractID(0);
            }
        }
        if (!isReady)
        {
            if (kasaDolu || anaKartDolu || cpuDolu || ekranKartiDolu || ramDolu)
            {
                if (kasaDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1);
                    }
                    else
                    {
                        CmdinteractID(1);
                    }
                    submidID = 1;
                }
                if (anaKartDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(2);
                    }
                    else
                    {
                        CmdinteractID(2);
                    }
                    submidID = 2;
                }
                if (cpuDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(3);
                    }
                    else
                    {
                        CmdinteractID(3);
                    }
                    submidID = 3;
                }
                if (ekranKartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(4);
                    }
                    else
                    {
                        CmdinteractID(4);
                    }
                    submidID = 4;
                }
                if (ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(5);
                    }
                    else
                    {
                        CmdinteractID(5);
                    }
                    submidID = 5;
                }
                if (ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(5);
                    }
                    else
                    {
                        CmdinteractID(5);
                    }
                    submidID = 5;
                }
                if (kasaDolu && anaKartDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(12);
                    }
                    else
                    {
                        CmdinteractID(12);
                    }
                    submidID = 12;
                }
                if (kasaDolu && anaKartDolu && cpuDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(123);
                    }
                    else
                    {
                        CmdinteractID(123);
                    }
                    submidID = 123;
                }
                if (kasaDolu && anaKartDolu && ekranKartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(124);
                    }
                    else
                    {
                        CmdinteractID(124);
                    }
                    submidID = 124;
                }
                if (kasaDolu && anaKartDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(125);
                    }
                    else
                    {
                        CmdinteractID(125);
                    }
                    submidID = 125;
                }
                if (kasaDolu && anaKartDolu && ekranKartiDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1245);
                    }
                    else
                    {
                        CmdinteractID(1245);
                    }
                    submidID = 1245;
                }
                if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1234);
                    }
                    else
                    {
                        CmdinteractID(1234);
                    }
                    submidID = 1234;
                }
                if (kasaDolu && anaKartDolu && cpuDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1235);
                    }
                    else
                    {
                        CmdinteractID(1235);
                    }
                    submidID = 1235;
                }
                if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(12345);
                    }
                    else
                    {
                        CmdinteractID(12345);
                    }
                    submidID = 12345;
                }
            }
        }
        if (isReady)
        {
            selectedBoolVariableName = "null";
            if (kasaDolu || anaKartDolu || cpuDolu || ekranKartiDolu || ramDolu)
            {
                if (kasaDolu)
                {
                    submidID = 1.2f;
                    if (isServer)
                    {
                        RpcinteractID(1);
                    }
                    else
                    {
                        CmdinteractID(1);
                    }
                }
                if (anaKartDolu)
                {
                    submidID = 2.2f;
                    if (isServer)
                    {
                        RpcinteractID(2);
                    }
                    else
                    {
                        CmdinteractID(2);
                    }

                }
                if (cpuDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(3);
                    }
                    else
                    {
                        CmdinteractID(3);
                    }
                    submidID = 3.2f;
                }
                if (ekranKartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(4);
                    }
                    else
                    {
                        CmdinteractID(4);
                    }
                    submidID = 4.2f;
                }
                if (ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(5);
                    }
                    else
                    {
                        CmdinteractID(5);
                    }
                    submidID = 5.2f;
                }
                if (kasaDolu && anaKartDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(12);
                    }
                    else
                    {
                        CmdinteractID(12);
                    }
                    submidID = 12.2f;
                }
                if (kasaDolu && anaKartDolu && cpuDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(123);
                    }
                    else
                    {
                        CmdinteractID(123);
                    }
                    submidID = 123.2f;
                }
                if (kasaDolu && anaKartDolu && ekranKartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(124);
                    }
                    else
                    {
                        CmdinteractID(124);
                    }
                    submidID = 124.2f;
                }
                if (kasaDolu && anaKartDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(125);
                    }
                    else
                    {
                        CmdinteractID(125);
                    }
                    submidID = 125.2f;
                }
                if (kasaDolu && anaKartDolu && ekranKartiDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1245);
                    }
                    else
                    {
                        CmdinteractID(1245);
                    }
                    submidID = 1245.2f;
                }
                if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1234);
                    }
                    else
                    {
                        CmdinteractID(1234);
                    }
                    submidID = 1234.2f;
                }
                if (kasaDolu && anaKartDolu && cpuDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1235);
                    }
                    else
                    {
                        CmdinteractID(1235);
                    }
                    submidID = 1235.2f;
                }
                if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(12345);
                    }
                    else
                    {
                        CmdinteractID(12345);
                    }
                    submidID = 12345.2f;
                }
            }
        }
        //kasasız kombinasyonlar
        if (anaKartDolu && cpuDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(23);
            }
            else
            {
                CmdinteractID(23);
            }
            submidID = 23;
        }
        if (anaKartDolu && ekranKartiDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(24);
            }
            else
            {
                CmdinteractID(24);
            }
            submidID = 24;
        }
        if (anaKartDolu && ramDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(25);
            }
            else
            {
                CmdinteractID(25);
            }
            submidID = 25;
        }
        if (anaKartDolu && cpuDolu && ekranKartiDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(234);
            }
            else
            {
                CmdinteractID(234);
            }
            submidID = 234;
        }
        if (anaKartDolu && cpuDolu && ramDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(235);
            }
            else
            {
                CmdinteractID(235);
            }
            submidID = 235;
        }
        if (anaKartDolu && ekranKartiDolu && ramDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(245);
            }
            else
            {
                CmdinteractID(245);
            }
            submidID = 245;
        }
        if (anaKartDolu && cpuDolu && ekranKartiDolu && ramDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(2345);
            }
            else
            {
                CmdinteractID(2345);
            }
            submidID = 2345;
        }
    }

    // zaman barı için
    [Command(requiresAuthority = false)]
    public void CmdSliderBar()
    {
        RpcSliderBar();
    }
    [ClientRpc]
    public void RpcSliderBar()
    {
        if (isControling && controlledTime >= 0)
        {
            controlledTime -= Time.deltaTime; // Bu özgün deðeri azalt
            controlledTimeSlider.value = controlledTime;
            OnSliderValueChanged(controlledTime, controlledTimeSlider.value);
        }
    }
    [Command(requiresAuthority = false)]
    public void CmdSetSubmidID(float newValue)
    {
        submidID = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetisControlled(bool newValue)
    {
        isControlled = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetisReady(bool newValue)
    {
        isReady = newValue;
    }
    // bool deðiþkenlerin sunucuya gönderilmesi
    [Command(requiresAuthority = false)]
    public void CmdSetKasaDolu(bool newValue)
    {
        kasaDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetAnakartDolu(bool newValue)
    {
        anaKartDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetCpuDolu(bool newValue)
    {
        cpuDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetEkranKartiDolu(bool newValue)
    {
        ekranKartiDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetRamDolu(bool newValue)
    {
        ramDolu = newValue;
    }

    private void OnSliderValueChanged(float oldValue, float newValue)
    {
        controlledTimeSlider.value = newValue;
    }
}
