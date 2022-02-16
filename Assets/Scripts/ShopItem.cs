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
    [SerializeField] Text countText;        // 개수.
    [SerializeField] Text ownedText;        // 보유 개수.
    [SerializeField] Text priceText;        // 가격.

    public void Setup()
    {
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

    }
}
