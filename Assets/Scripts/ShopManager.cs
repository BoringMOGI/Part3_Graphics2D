using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : SelectManager
{
    static ShopManager instance;
    public static ShopManager Instance => instance;

    [SerializeField] ShopItem prefab;
    [SerializeField] Transform itemParent;
    [SerializeField] GameObject panel;

    [SerializeField] RectTransform scrollView;
    [SerializeField] RectTransform content;

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
        InputManager.Instance.OnCancel += CloseShop;        // 상점 닫기 이벤트 등록.

        SetButton(shopItemList[0]);
    }
    public void CloseShop()
    {
        InputManager.Instance.OnCancel -= CloseShop;    // 상점 닫기 이벤트 등록 해제.
        Player.Instance.SwitchControl(true);            // 플레이어 입력 재등록.
        panel.SetActive(false);                         // 패널 해제.

        // 버튼 선택 해제.
        ClearButton();

        // 생성했던 상점 아이템 목록 삭제.
        for (int i = 0; i<shopItemList.Count; i++)
            Destroy(shopItemList[i].gameObject);

        // 리스트 Clear.
        shopItemList.Clear();                
    }

    protected override void MoveButton(VECTOR v)
    {
        base.MoveButton(v);

        // 상점 목록의 높이 갱신.
        float viewHeight = scrollView.rect.height;
        float currentY = current.transform.localPosition.y;
        float currentHeight = current.GetComponent<RectTransform>().rect.height;

        float contentY = Mathf.Abs(currentY) - viewHeight + currentHeight;   // content의 y축 높이.

        // 컨텐츠의 높이 중 y축의 값을 변경 후 대입.
        Vector3 localPosition = content.transform.localPosition;
        localPosition.y = contentY;
        content.transform.localPosition = localPosition;
    }
}
