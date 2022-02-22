using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] int coin;

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
    }

    public bool IsEnoughCoin(int amount)
    {
        return Coin >= amount;
    }
    public bool UseCoin(int amount)
    {
        if(IsEnoughCoin(amount))
        {
            Coin -= amount;
            return true;
        }

        return false;
    }
}
