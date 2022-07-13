using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Player player;
    public Store store;
    public Inventory inventory;
    GameObject useText;

    private void Awake()
    {
        useText = GameObject.Find("UseText_GameObject");
    }

    private void OnTriggerStay(Collider other)
    {
        if (player.tryUse)
        {
            store.HideOff();
            inventory.HideOff();
            //useText.SetActive(false);
        }
        else
        {
            store.HideOn();
            inventory.HideOn();
            //useText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        store.HideOn();
        inventory.HideOn();
    }

}
