using Mirror;
using UnityEngine;

public class Table : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnTableStateChanged))]
    public bool isTableOccupied = false;

    private void OnTableStateChanged(bool oldValue, bool newValue)
    {
        // Durum de�i�ti�inde yap�lacak i�lemler
    }

    public void InteractWithTable(bool newState)
    {
        isTableOccupied = newState;
        
    }
}
