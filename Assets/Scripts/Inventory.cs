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

    public bool PushItem(ItemData newItem, int count)       // ���������� �κ��丮�� �־����� ����.
    {
        int index = IndexOfNull();                          // ����ִ� ���� üũ.
        if (index >= 0)                                     // ���� �� ������ �ʴٸ�.
        {
            inventory[index] = new Item(newItem, count);    // �ش� ��ġ�� newItem ����.
            return true;
        }

        return false;
    }

}
