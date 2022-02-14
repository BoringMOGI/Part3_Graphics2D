using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] Image itemImage;       // 이미지.
    [SerializeField] Image selectedImage;   // 선택 이미지.
    [SerializeField] Text nameText;         // 이름.
    [SerializeField] Text countText;        // 개수.
    [SerializeField] Text ownedText;        // 보유 개수.
    [SerializeField] Text priceText;        // 가격.

    public void Setup()
    {
        OnDeselect(null);
    }

    // event system의 current object에 등록되면 자동으로 호출.
    public void OnSelect(BaseEventData eventData)
    {
        selectedImage.enabled = true;
    }

    // event system의 current object에서 등록 해제 되면 자동으로 호출.
    public void OnDeselect(BaseEventData eventData)
    {
        selectedImage.enabled = false;
    }
}
