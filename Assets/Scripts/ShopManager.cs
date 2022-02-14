using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    static ShopManager instance;
    public static ShopManager Instance => instance;

    [SerializeField] ShopItem prefab;
    [SerializeField] Transform itemParent;
    [SerializeField] GameObject panel;

    List<ShopItem> shopItemList = new List<ShopItem>();

    private void Awake()
    {
        instance = this;        
    }
    private void Start()
    {
        OpenShop();
    }

    public void OpenShop()
    {
        // 10개의 아이템 생성.
        for (int i = 0; i<10; i++)
        {
            ShopItem newItem = Instantiate(prefab);
            newItem.transform.SetParent(itemParent);
            newItem.Setup();            

            shopItemList.Add(newItem);
        }

        SelectManager.Instance.SetCurrentObject(shopItemList[0].gameObject);
    }

}
