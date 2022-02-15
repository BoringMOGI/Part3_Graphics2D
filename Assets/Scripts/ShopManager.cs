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
        panel.SetActive(false);
    }

    public void OpenShop()
    {
        panel.SetActive(true);
        
        // 10개의 아이템 생성.
        for (int i = 0; i<10; i++)
        {
            ShopItem newItem = Instantiate(prefab);
            newItem.transform.SetParent(itemParent);
            newItem.Setup();            

            shopItemList.Add(newItem);
        }

        // shopItem끼리 서로의 방향에 따라 버튼을 대입.
        Button up = shopItemList[0];
        for(int i = 1; i<shopItemList.Count; i++)
        {
            Button down = shopItemList[i];
            up.SetButtonOf(VECTOR.Down, down);
            down.SetButtonOf(VECTOR.Up, up);

            up = down;
        }

        // 플레이어의 입력 해제.
        Player.Instance.SwitchControl(false);               // 플레이어 입력 해제.
        SelectManager.Instance.SetButton(shopItemList[0]);  // 버튼 매니저에게 최초 버튼 등록.
        InputManager.Instance.OnCancel += CloseShop;        // 상점 닫기 이벤트 등록.
    }
    public void CloseShop()
    {
        InputManager.Instance.OnCancel -= CloseShop;    // 상점 닫기 이벤트 등록 해제.
        Player.Instance.SwitchControl(true);            // 플레이어 입력 재등록.
        panel.SetActive(false);                         // 패널 해제.
    }

}
