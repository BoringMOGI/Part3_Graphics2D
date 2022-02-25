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

    ItemData itemData;

    public delegate void ItemEvent(ItemData item);

    public event ItemEvent OnSelectedItem;
    public event ItemEvent OnSubmitItem;

    public void Setup(ItemData itemData)
    {
        this.itemData = itemData;

        itemImage.sprite = itemData.itemSprite;
        nameText.text = itemData.itemName;
        typeText.text = itemData.itemType;
        priceText.text = string.Format("BUY : {0:#,##0G}", itemData.itemPrice);

        OnDeselect();
    }

    public override void OnSelect()
    {
        selectedImage.enabled = true;
        OnSelectedItem?.Invoke(itemData);       // ���� �Ŵ���(=�̺�Ʈ �����)���� �ڽ��� ���õǾ��ٰ� �˸���.
    }
    public override void OnDeselect()
    {
        selectedImage.enabled = false;
    }
    public override void OnSubmit()
    {
        OnSubmitItem?.Invoke(itemData);          // ���� �Ŵ������� �ڽ��� ���� �õ��Ǿ��ٰ� �˸���.
    }
}
