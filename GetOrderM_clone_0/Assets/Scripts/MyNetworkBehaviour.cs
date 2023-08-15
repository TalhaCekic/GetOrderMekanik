using Mirror;
using UnityEngine;

public class MyNetworkBehaviour : NetworkBehaviour
{
    [SerializeField] private Table table;

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool newTableState = !table.isTableOccupied; // Masa durumunu tersine çevir
            CmdChangeTableState(newTableState);
        }
    }

    [Command]
    private void CmdChangeTableState(bool newState)
    {
        table.InteractWithTable(newState);
        print(table.isTableOccupied);
    }
}
