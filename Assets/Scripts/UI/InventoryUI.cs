using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : SelectManager<InventoryUI>
{
    [SerializeField] int column;    // 가로(열)
    [SerializeField] int row;       // 세로(행)
    [SerializeField] Transform itemParent;
    [SerializeField] GameObject panel;

    public delegate Item[] GetItemEvent();
    GetItemEvent GetItem;

    InvItemUI[] items;

    private void Start()
    {
        items = itemParent.GetComponentsInChildren<InvItemUI>();
        for(int i = 0; i<items.Length; i++)
        {
            InvItemUI button = items[i];
            button.Setup(null);                             // 버튼에게 item을 전달한 후 세팅.
            button.OnSelectedItem += OnSelectedItem;        // 선택 이벤트 등록.
            button.OnSubmitItem += OnSubmitItem;            // 확인 이벤트 등록.

            // ================== 방향별 버튼 링크 ================================================

            // 왼쪽 (가장 좌측은 왼쪽이 없다.)
            if(i % column > 0)
            {
                button.SetButtonOf(VECTOR.Left, items[i - 1]);
            }
            // 오른쪽 (가장 우측은 오른쪽이 없다.)
            if (i % column < column - 1)
            {
                button.SetButtonOf(VECTOR.Right, items[i + 1]);
            }
            // 위쪽 (가장 상단은 위쪽이 없다.)
            if(i / column > 0)
            {
                button.SetButtonOf(VECTOR.Up, items[i - column]);
            }
            // 아래쪽  
            if(i / column < row - 1)
            {
                button.SetButtonOf(VECTOR.Down, items[i + column]);
            }
        }

        panel.SetActive(false);
    }

    // Inventory가 UI에게 자신의 아이템을 받을 수 있는 이벤트 함수 등록.
    public void Setup(GetItemEvent GetItem)
    {
        // 최초에 외부 값을 세팅.
        this.GetItem = GetItem;
    }

    public override void Open()
    {
        base.Open();

        OnUpdateItem();         // 내부 아이템 데이터 업데이트.
        SetButton(items[0]);    // 최초 선택 버튼.

        panel.SetActive(true);  // panel 활성화.
    }
    public override void Close()
    {
        base.Close();
        panel.SetActive(false); // panel 비활성화.
    }

    private void OnUpdateItem()
    {
        // 실제 인벤토리의 아이템을 UI에 갱신.
        Item[] invItems = GetItem();
        for(int i = 0; i<items.Length; i++)
            items[i].Setup(invItems[i]);
    }

    private void OnSelectedItem(Item item)
    {
        if (item == null)
            return;
    }
    private void OnSubmitItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("아이템 없음");
            return;
        }

        Debug.Log("선택 중인 아이템은 : " + item.itemName);
    }

    protected override void OnCancel()
    {
        Close();
    }

}
