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
        CreateItems();                                      // ������ �´� ������ ����.
        LinkedItems();                                      // ������ �����۵��� ��ũ ��Ų��.

        Player.Instance.SwitchControl(false);               // �÷��̾� �Է� ����.
        SwitchInputEvent(true);                             // �Է� �̺�Ʈ Ȱ��ȭ.

        SetButton(shopItemList[0]);                         // ���� ���� ��ư ����.

        panel.SetActive(true);                              // �г� Ȱ��ȭ.
    }
    public void CloseShop()
    {
        panel.SetActive(false);                             // �г� ����.

        ClearButton();                                      // ��ư ���� ����.
        SwitchInputEvent(false);                            // �Է� �̺�Ʈ Ȱ��ȭ.
        Player.Instance.SwitchControl(true);                // �÷��̾� �Է� ����.

        ClearItems();                                       // ������ UI ����.
    }

    private void CreateItems()
    {
        // butItems�� ������ ��ŭ ������ ����.
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
    
    protected override void MoveButton(VECTOR v)
    {
        base.MoveButton(v);

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
    private void OnSubmitShopItem(Item item)
    {
        // ���� �Ŵ����� �̺�Ʈ ��Ȱ��ȭ.
        SwitchInputEvent(false);                    

        // �Ű������� ���� �޼ҵ�(�Լ�)�ν� CountPanelUI�� ������ �ִٰ�
        // submitȤ�� cancel�Ҷ� �ش� �Լ��� ȣ���Ѵ�.
        countPanel.Open((count, isSubmit) => {

            // count : ������ ����, isSubmit : ����, ��� ����.
            if (isSubmit)
            {
                Debug.Log("���� ���� : " + item.itemPrice * count);
            }
            else
            {
                Debug.Log("������ ���� ���");
            }

            SwitchInputEvent(true);                 // ���� �Ŵ����� �̺�Ʈ Ȱ��ȭ.
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

        SwitchInputEvent(true);                 // ���� �Ŵ����� �̺�Ʈ Ȱ��ȭ.
    }

    protected override void CancelButton()
    {
        CloseShop();
    }
}
