using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : SelectManager<InventoryUI>
{
    [SerializeField] int column;    // ����(��)
    [SerializeField] int row;       // ����(��)
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
            button.Setup(null);                             // ��ư���� item�� ������ �� ����.
            button.OnSelectedItem += OnSelectedItem;        // ���� �̺�Ʈ ���.
            button.OnSubmitItem += OnSubmitItem;            // Ȯ�� �̺�Ʈ ���.

            // ================== ���⺰ ��ư ��ũ ================================================

            // ���� (���� ������ ������ ����.)
            if(i % column > 0)
            {
                button.SetButtonOf(VECTOR.Left, items[i - 1]);
            }
            // ������ (���� ������ �������� ����.)
            if (i % column < column - 1)
            {
                button.SetButtonOf(VECTOR.Right, items[i + 1]);
            }
            // ���� (���� ����� ������ ����.)
            if(i / column > 0)
            {
                button.SetButtonOf(VECTOR.Up, items[i - column]);
            }
            // �Ʒ���  
            if(i / column < row - 1)
            {
                button.SetButtonOf(VECTOR.Down, items[i + column]);
            }
        }

        panel.SetActive(false);
    }

    // Inventory�� UI���� �ڽ��� �������� ���� �� �ִ� �̺�Ʈ �Լ� ���.
    public void Setup(GetItemEvent GetItem)
    {
        // ���ʿ� �ܺ� ���� ����.
        this.GetItem = GetItem;
    }

    public override void Open()
    {
        base.Open();

        OnUpdateItem();                                 // ���� ������ ������ ������Ʈ.
        SetButton(items[0]);                            // ���� ���� ��ư.
        
        panel.SetActive(true);                          // panel Ȱ��ȭ.                
    }
    public override void Close()
    {
        base.Close();

        TalkManager.Instance.Close(false);              // ��� â ��Ȱ��ȭ (��, ���â�� �̺�Ʈ ��Ȱ��ȭ �κ��� ����)
        panel.SetActive(false);                         // panel ��Ȱ��ȭ.
    }

    private void OnUpdateItem()
    {
        // ���� �κ��丮�� �������� UI�� ����.
        Item[] invItems = GetItem();
        for(int i = 0; i<items.Length; i++)
            items[i].Setup(invItems[i]);
    }

    private void OnSelectedItem(Item item)
    {
        if (item == null)
        {
            TalkManager.Instance.TextOutput(string.Empty);
            return;
        }

        TalkManager.Instance.TextOutput(item.itemContent);
    }
    private void OnSubmitItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("������ ����");
            return;
        }

        TalkManager.Instance.TextOutput($"{item.itemName}�� ����� �� �����ϴ�.");
    }

    protected override void OnCancel()
    {
        Close();
    }

}
