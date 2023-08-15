using Mirror;
using UnityEngine;

public class Table : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnTableStateChanged))]
    public bool isTableOccupied = false;

    private void OnTableStateChanged(bool oldValue, bool newValue)
    {
        // Durum deðiþtiðinde yapýlacak iþlemler
    }

    public void InteractWithTable(bool newState)
    {
        isTableOccupied = newState;
        
    }
}
