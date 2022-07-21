using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Transform rootSlot;
    public Store store;
    public int money;
    private List<Slot> slots;
    public Button yesBtn;
    public GameObject buyCheck;
    public TextMeshProUGUI checktext;


    TextMeshProUGUI coin;
    TextMeshProUGUI changeCoin;
    Animator anim;

    public int Money 
    { 
        get => money;
        set
        {
            money = value;
            string stringMoney = string.Format("{0:#,###}", money);
            coin.text = stringMoney;
        }
    }

    private void Awake()
    {
        coin = GetComponentInChildren<TextMeshProUGUI>();
        changeCoin = GetComponentInChildren<FindAnim>().GetComponentInChildren<TextMeshProUGUI>();
        anim = GetComponentInChildren<FindAnim>().GetComponent<Animator>();
        Money = 100000;
    }

    void Start()
    {
        slots = new List<Slot>();

        int slotCnt = rootSlot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();

            slots.Add(slot);
        }
        HideOn();
        store.onSlotClick += CheckBuy;
    }

    public void BuyItem(ItemProperty item)
    {
        yesBtn.onClick.RemoveAllListeners();
        buyCheck.SetActive(false);
        var emptySlot = slots.Find(t =>
        {
            return t.item == null || t.item.name == string.Empty;
        });

        if(emptySlot != null)
        {
            if(Money >= item.price)
            {
                emptySlot.SetItem(item);
                Money -= item.price;
                anim.SetTrigger("Buy");
                changeCoin.text = "-" + ($"{item.price}");
            }
        }
    }

    public void CheckBuy(ItemProperty item)
    {
        buyCheck.SetActive(true);
        checktext.text = ($"{item.name}을(를) {(int)(item.price)}코인에 \n구매 하시겠습니까?");
        yesBtn.onClick.AddListener(() => BuyItem(item));
    }

    public void HideOn()
    {
        gameObject.SetActive(false);
    }

    public void HideOff()
    {
        gameObject.SetActive(true);
    }

}
