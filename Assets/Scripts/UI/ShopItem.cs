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
        OnSelectedItem?.Invoke(itemData);       // 상점 매니저(=이벤트 등록자)에게 자신이 선택되었다고 알린다.
    }
    public override void OnDeselect()
    {
        selectedImage.enabled = false;
    }
    public override void OnSubmit()
    {
        OnSubmitItem?.Invoke(itemData);          // 상점 매니저에게 자신이 구매 시도되었다고 알린다.
    }
}
