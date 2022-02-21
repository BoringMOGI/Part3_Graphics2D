using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : SelectManager
{
    static ShopManager instance;
    public static ShopManager Instance => instance;

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
    [SerializeField] Item[] buyItems;

    List<ShopItem> shopItemList = new List<ShopItem>();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        panel.SetActive(false);
        countPanel.gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        CreateItems();                                      // 상점에 맞는 아이템 생성.
        LinkedItems();                                      // 생성된 아이템들을 링크 시킨다.

        Player.Instance.SwitchControl(false);               // 플레이어 입력 해제.
        SwitchInputEvent(true);                             // 입력 이벤트 활성화.

        SetButton(shopItemList[0]);                         // 최초 선택 버튼 세팅.

        panel.SetActive(true);                              // 패널 활성화.
    }
    public void CloseShop()
    {
        panel.SetActive(false);                             // 패널 해제.

        ClearButton();                                      // 버튼 선택 해제.
        SwitchInputEvent(false);                            // 입력 이벤트 활성화.
        Player.Instance.SwitchControl(true);                // 플레이어 입력 재등록.

        ClearItems();                                       // 아이템 UI 삭제.
    }

    private void CreateItems()
    {
        // butItems의 데이터 만큼 아이템 생성.
        for (int i = 0; i < buyItems.Length; i++)
        {
            ShopItem newItem = Instantiate(prefab);
            newItem.Setup(buyItems[i], OnSubmitShopItem);

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
    private void OnSubmitShopItem(Item item)
    {
        // 상점 매니저의 이벤트 비활성화.
        SwitchInputEvent(false);                    

        // 매개변수는 무명 메소드(함수)로써 CountPanelUI가 가지고 있다가
        // submit혹은 cancel할때 해당 함수를 호출한다.
        countPanel.Open((count, isSubmit) => {

            // count : 선택한 개수, isSubmit : 선택, 취소 여부.
            if (isSubmit)
            {
                Debug.Log("구매 가격 : " + item.itemPrice * count);
            }
            else
            {
                Debug.Log("아이템 구매 취소");
            }

            SwitchInputEvent(true);                 // 상점 매니저의 이벤트 활성화.
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

        SwitchInputEvent(true);                 // 상점 매니저의 이벤트 활성화.
    }

    protected override void CancelButton()
    {
        CloseShop();
    }
}
