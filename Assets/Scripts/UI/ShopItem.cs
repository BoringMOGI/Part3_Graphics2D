using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : Button
{
    [SerializeField] Image itemImage;       // 이미지.
    [SerializeField] Image selectedImage;   // 선택 이미지.
    [SerializeField] Text nameText;         // 이름.
    [SerializeField] Text typeText;         // 아이템 타입.
    [SerializeField] Text ownedText;        // 보유 개수.
    [SerializeField] Text priceText;        // 가격.

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
        OnSubmitItem.Invoke(itemData);          // 상점 매니저에게 자신이 선택되었다고 알린다.
    }
}
