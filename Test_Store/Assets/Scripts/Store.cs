using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    private List<Slot> slots;

    public System.Action<ItemProperty> onSlotClick;

    private void Start()
    {
        slots = new List<Slot>();
        
        int SlotCnt = slotRoot.childCount;

        for(int i=0; i<SlotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if(i < itemBuffer.items.Count)
            {
                slot.SetItem(itemBuffer.items[i]);
            }

            else
            {
                slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
            HideOn();
            slots.Add(slot);
        }
    }

    public void HideOn()
    {
        gameObject.SetActive(false);
    }

    public void HideOff()
    {
        gameObject.SetActive(true);
    }

    public void OnClick(Slot slot)
    {
        if(onSlotClick != null)
        {
            onSlotClick(slot.item);
        }
    }
}
