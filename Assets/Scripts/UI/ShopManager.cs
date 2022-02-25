using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : SelectManager<ShopManager>
{
    [Header("Prefab")]
    [SerializeField] ShopItem prefab;
    [SerializeField] Transform itemParent;
    
    [Header("Panel")]
    [SerializeField] GameObject panel;
    [SerializeField] CountPanelUI countPanel;

    [Header("Rect")]
    [SerializeField] RectTransform scrollView;
    [SerializeField] RectTransform content;

    [Header("ItemData")]
    [SerializeField] ItemData[] buyItems;

    List<ShopItem> shopItemList = new List<ShopItem>();

    private void Start()
    {
        panel.SetActive(false);
        countPanel.gameObject.SetActive(false);
    }

    public override void Open()
    {
        base.Open();

        CreateItems();                                      // 상점에 맞는 아이템 생성.
        LinkedItems();                                      // 생성된 아이템들을 링크 시킨다.

        SetButton(shopItemList[0]);                         // 최초 선택 버튼 세팅.
        panel.SetActive(true);                              // 패널 활성화.
    }
    public override void Close()
    {
        base.Close();

        panel.SetActive(false);                             // 패널 해제.

        ClearButton();                                      // 버튼 선택 해제.
        ClearItems();                                       // 아이템 UI 삭제.

        TalkManager.Instance.Close(false);                  // 출력창 끄기.
    }

    private void CreateItems()
    {
        // butItems의 데이터 만큼 아이템 생성.
        for (int i = 0; i < buyItems.Length; i++)
        {
            // 프리팹 생성 및 세팅.
            ShopItem newItem = Instantiate(prefab);
            newItem.Setup(buyItems[i]);

            // 이벤트 함수 등록.
            newItem.OnSelectedItem += OnSelectedShopItem;
            newItem.OnSubmitItem += OnSubmitShopItem;

            // 위치, 크기 계산.
            RectTransform itemRect = newItem.transform as RectTransform;
            itemRect.SetParent(itemParent);
            itemRect.localScale = new Vector3(1, 1, 1);

            shopItemList.Add(newItem);
        }
    }
    private void LinkedItems()
    {
        // shopItem끼리 서로의 방향에 따라 버튼을 대입.
        Button up = shopItemList[0];
        for (int i = 1; i < shopItemList.Count; i++)
        {
            Button down = shopItemList[i];
            up.SetButtonOf(VECTOR.Down, down);
            down.SetButtonOf(VECTOR.Up, up);

            up = down;
        }
    }
    private void ClearItems()
    {
        // 생성했던 상점 아이템 목록 삭제.
        for (int i = 0; i < shopItemList.Count; i++)
            Destroy(shopItemList[i].gameObject);

        // 리스트 Clear.
        shopItemList.Clear();
    }
    
    public override void InputVector(VECTOR v, bool isDown)
    {
        base.InputVector(v, isDown);
        if (isDown)
            return;

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

    private void OnSelectedShopItem(ItemData item)
    {
        TalkManager.Instance.TextOutput(item.itemContent);
    }
    private void OnSubmitShopItem(ItemData item)
    {
        // 매개변수는 무명 메소드(함수)로써 CountPanelUI가 가지고 있다가
        // submit혹은 cancel할때 해당 함수를 호출한다.
        countPanel.Open(item, (count, isSubmit) => {

            // count : 선택한 개수, isSubmit : 선택, 취소 여부.
            if (isSubmit)
            {
                int price = item.itemPrice * count;             // 구매 가격 계산.
                Inventory.Instance.UseCoin(price);              // 코인 사용.
                Inventory.Instance.PushItem(item, count);       // 아이템 인벤토리에 대입.
            }
            else
            {
                Debug.Log("아이템 구매 취소");
            }
        });
    }
    private void SelectedCount(int count, bool isSubmit)
    {
        // count : 선택한 개수, isSubmit : 선택, 취소 여부.
        if (isSubmit)
        {
            Debug.Log($"아이템 {count}개 구매!!");
        }
        else
        {
            Debug.Log("아이템 구매 취소");
        }
    }

    protected override void OnCancel()
    {
        Close();
    }
}
