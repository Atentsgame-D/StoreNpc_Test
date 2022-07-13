using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Button sellBtn;
    public Inventory inventory;

    TextMeshProUGUI changeCoin;
    Animator anim;

    private void Awake()
    {
        SetSellBtnInteractable(false);
        changeCoin = GameObject.Find("Panel_MoneyChange").GetComponentInChildren<TextMeshProUGUI>();
        anim = GameObject.Find("Panel_MoneyChange").GetComponent<Animator>();
    }

    void SetSellBtnInteractable(bool b)
    {
        if(sellBtn != null)
        {
            //sellBtn.interactable = b;
            sellBtn.gameObject.SetActive(b);
        }
    }

    public void SetItem(ItemProperty item)
    {
        this.item = item;

        if(item == null)
        {
            image.enabled = false;
            SetSellBtnInteractable(false);
            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;
            SetSellBtnInteractable(true);
            gameObject.name = item.name;
            image.sprite = item.sprite; 
        }
    }

    public void OnClickSellBtn()
    {
        inventory.Money += (int)((item.price) * 0.5f);
        anim.SetTrigger("Sell");
        changeCoin.text = "+" + ($"{(int)(item.price * 0.5f)}");
        SetItem(null);
    }
}
