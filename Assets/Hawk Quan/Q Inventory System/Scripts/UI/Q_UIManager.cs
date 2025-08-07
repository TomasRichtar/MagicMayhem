using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QInventory
{
    public class Q_UIManager : MonoBehaviour
    {
        public void Close(GameObject target)
        {
            Debug.Log("Close Window");
            Q_GameMaster.Instance.inventoryManager.PlayOpenPanelClip();
            target.SetActive(false);
        }
    }
}

