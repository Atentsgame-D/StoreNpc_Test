using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Player player;
    public Store store;
    public Inventory inventory;
    TextMeshProUGUI useText = null;

    private void Awake()
    {
        useText = GameObject.Find("UseText").GetComponent<TextMeshProUGUI>();

    }

    private void OnTriggerStay(Collider other)
    {
        if (player.tryUse)
        {
            store.HideOff();
            inventory.HideOff();
            useText.gameObject.SetActive(false);
        }
        else
        {
            store.HideOn();
            inventory.HideOn();
            useText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        store.HideOn();
        inventory.HideOn();
    }

}
