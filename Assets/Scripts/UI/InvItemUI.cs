using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvItemUI : Button
{
    [SerializeField] Image iconImage;
    [SerializeField] Image selected;

    public delegate void ItemEvent(Item item);
    
    public event ItemEvent OnSelectedItem;              // 아이템이 선택되었을 때.
    public event ItemEvent OnSubmitItem;                // 아이템을 사용했을 때.

    Item item;

    public void Setup(Item item)
    {
        this.item = item;

        // 아이콘 이미지 갱신.
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
