using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : Button
{
    [SerializeField] Image itemImage;       // �̹���.
    [SerializeField] Image selectedImage;   // ���� �̹���.
    [SerializeField] Text nameText;         // �̸�.
    [SerializeField] Text typeText;         // ������ Ÿ��.
    [SerializeField] Text ownedText;        // ���� ����.
    [SerializeField] Text priceText;        // ����.

    Item itemData;

    public delegate void SubmitEvent(Item item);
    private SubmitEvent OnSubmitItem;

    public void Setup(Item itemData, SubmitEvent OnSubmitItem)
    {
        this.itemData = itemData;
        this.OnSubmitItem = OnSubmitItem;

        itemImage.sprite = itemData.itemSprite;
        nameText.text = itemData.itemName;
        typeText.text = itemData.itemType;
        priceText.text = string.Format("BUY : {0:#,##0G}", itemData.itemPrice);

        OnDeselect();
    }

    public override void OnSelect()
    {
        selectedImage.enabled = true;
    }
    public override void OnDeselect()
    {
        selectedImage.enabled = false;
    }
    public override void OnSubmit()
    {
        OnSubmitItem.Invoke(itemData);          // ���� �Ŵ������� �ڽ��� ���õǾ��ٰ� �˸���.
    }
}
