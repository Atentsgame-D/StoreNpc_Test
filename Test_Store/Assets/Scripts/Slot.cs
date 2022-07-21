using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;
    public Image image;
    public Button sellBtn;
    public Inventory inventory;
    public Button yesBtn;
    public GameObject sellCheck;
    public TextMeshProUGUI checktext;
    public Canvas canvas;


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

    public void CheckSell()
    {
        sellCheck.SetActive(true);
        sellCheck.transform.SetParent(canvas.transform);
        checktext.text = ($"{item.name}을(를) {(int)(item.price * 0.5f)}코인에 \n판매 하시겠습니까?");
    }

    public void OnClickSellBtn()
    {
        sellCheck.transform.SetParent(transform);
        sellCheck.SetActive(false);
        inventory.Money += (int)((item.price) * 0.5f);
        anim.SetTrigger("Sell");
        changeCoin.text = "+" + ($"{(int)(item.price * 0.5f)}");
        SetItem(null);
    }
}
