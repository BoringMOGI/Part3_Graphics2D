using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IButtonHandler
{
    [SerializeField] Image itemImage;       // 이미지.
    [SerializeField] Image selectedImage;   // 선택 이미지.
    [SerializeField] Text nameText;         // 이름.
    [SerializeField] Text countText;        // 개수.
    [SerializeField] Text ownedText;        // 보유 개수.
    [SerializeField] Text priceText;        // 가격.
    [SerializeField] ButtonOf buttons;      // 방향에 따른 버튼들.

    public void Setup()
    {
        OnDeselect();
    }
    public void OnSelect()
    {
        selectedImage.enabled = true;
    }
    public void OnDeselect()
    {
        selectedImage.enabled = false;
    }

    public void OnSubmit()
    {

    }

    public IButtonHandler GetButtonOf(VECTOR v)
    {
        return buttons.GetButtonOf(v);
    }
}
