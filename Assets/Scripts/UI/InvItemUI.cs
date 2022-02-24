using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvItemUI : Button
{
    [SerializeField] Image iconImage;
    [SerializeField] Image selected;

    public delegate void ItemEvent(Item item);
    
    public event ItemEvent OnSelectedItem;              // �������� ���õǾ��� ��.
    public event ItemEvent OnSubmitItem;                // �������� ������� ��.

    Item item;

    public void Setup(Item item)
    {
        this.item = item;

        // ������ �̹��� ����.
        iconImage.enabled = (item != null);
        iconImage.sprite = (item != null) ? item.itemSprite : null;

        OnDeselect();
    }

    public override void OnDeselect()
    {
        selected.enabled = false;
    }
    public override void OnSelect()
    {
        selected.enabled = true;
        OnSelectedItem?.Invoke(item);
    }

    public override void OnSubmit()
    {
        OnSubmitItem?.Invoke(item);
    }
}
