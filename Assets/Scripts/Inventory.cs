using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item
{
    private ItemData data;
    public Item(ItemData data, int count)
    {
        this.data = data;
        this.count = count;
    }

    public string itemName => data.itemName;
    public string itemType => data.itemType;
    public string itemContent => data.itemContent;
    public Sprite itemSprite => data.itemSprite;
    public int itemPrice => data.itemPrice;

    public int count;
}

public class Inventory : Singleton<Inventory>
{
    const int MAX_INVENTORY = 36;

    [SerializeField] int coin;

    Item[] inventory;

    public int Coin
    {
        get
        {
            return coin;
        }
        private set
        {
            coin = value;
            StatusUI.Instance.UpdateCoinUI(coin);
        }
    }

    private void Start()
    {
        Coin = coin;
        inventory = new Item[MAX_INVENTORY];
        InventoryUI.Instance.Setup(() => inventory);        // Setup InventoryUI.
    }

    public bool IsEnoughCoin(int amount)
    {
        return Coin >= amount;
    }
    public bool UseCoin(int amount)
    {
        if (IsEnoughCoin(amount))
        {
            Coin -= amount;
            return true;
        }

        return false;
    }

    private int IndexOfNull()
    {
        for(int i= 0; i<inventory.Length; i++)
        {
            if (inventory[i] == null)
                return i;
        }

        return -1;
    }

    public bool PushItem(ItemData newItem, int count)       // 성공적으로 인벤토리에 넣었는지 리턴.
    {
        int index = IndexOfNull();                          // 비어있는 공간 체크.
        if (index >= 0)                                     // 만약 꽉 차있지 않다면.
        {
            inventory[index] = new Item(newItem, count);    // 해당 위치에 newItem 대입.
            return true;
        }

        return false;
    }

}
