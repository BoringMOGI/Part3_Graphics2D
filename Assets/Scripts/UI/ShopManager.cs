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

        CreateItems();                                      // ������ �´� ������ ����.
        LinkedItems();                                      // ������ �����۵��� ��ũ ��Ų��.

        SetButton(shopItemList[0]);                         // ���� ���� ��ư ����.
        panel.SetActive(true);                              // �г� Ȱ��ȭ.
    }
    public override void Close()
    {
        base.Close();

        panel.SetActive(false);                             // �г� ����.

        ClearButton();                                      // ��ư ���� ����.
        ClearItems();                                       // ������ UI ����.

        TalkManager.Instance.Close(false);                  // ���â ����.
    }

    private void CreateItems()
    {
        // butItems�� ������ ��ŭ ������ ����.
        for (int i = 0; i < buyItems.Length; i++)
        {
            // ������ ���� �� ����.
            ShopItem newItem = Instantiate(prefab);
            newItem.Setup(buyItems[i]);

            // �̺�Ʈ �Լ� ���.
            newItem.OnSelectedItem += OnSelectedShopItem;
            newItem.OnSubmitItem += OnSubmitShopItem;

            // ��ġ, ũ�� ���.
            RectTransform itemRect = newItem.transform as RectTransform;
            itemRect.SetParent(itemParent);
            itemRect.localScale = new Vector3(1, 1, 1);

            shopItemList.Add(newItem);
        }
    }
    private void LinkedItems()
    {
        // shopItem���� ������ ���⿡ ���� ��ư�� ����.
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
        // �����ߴ� ���� ������ ��� ����.
        for (int i = 0; i < shopItemList.Count; i++)
            Destroy(shopItemList[i].gameObject);

        // ����Ʈ Clear.
        shopItemList.Clear();
    }
    
    public override void InputVector(VECTOR v, bool isDown)
    {
        base.InputVector(v, isDown);
        if (isDown)
            return;

        // ���� ����� ���� ����.
        float viewHeight = scrollView.rect.height;
        float currentY = current.transform.localPosition.y;
        float currentHeight = current.GetComponent<RectTransform>().rect.height;

        float contentY = Mathf.Abs(currentY) - viewHeight + currentHeight;   // content�� y�� ����.

        // �������� ���� �� y���� ���� ���� �� ����.
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
        // �Ű������� ���� �޼ҵ�(�Լ�)�ν� CountPanelUI�� ������ �ִٰ�
        // submitȤ�� cancel�Ҷ� �ش� �Լ��� ȣ���Ѵ�.
        countPanel.Open(item, (count, isSubmit) => {

            // count : ������ ����, isSubmit : ����, ��� ����.
            if (isSubmit)
            {
                int price = item.itemPrice * count;             // ���� ���� ���.
                Inventory.Instance.UseCoin(price);              // ���� ���.
                Inventory.Instance.PushItem(item, count);       // ������ �κ��丮�� ����.
            }
            else
            {
                Debug.Log("������ ���� ���");
            }
        });
    }
    private void SelectedCount(int count, bool isSubmit)
    {
        // count : ������ ����, isSubmit : ����, ��� ����.
        if (isSubmit)
        {
            Debug.Log($"������ {count}�� ����!!");
        }
        else
        {
            Debug.Log("������ ���� ���");
        }
    }

    protected override void OnCancel()
    {
        Close();
    }
}
