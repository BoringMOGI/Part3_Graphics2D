using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IButtonHandler
{
    [SerializeField] Image itemImage;       // �̹���.
    [SerializeField] Image selectedImage;   // ���� �̹���.
    [SerializeField] Text nameText;         // �̸�.
    [SerializeField] Text countText;        // ����.
    [SerializeField] Text ownedText;        // ���� ����.
    [SerializeField] Text priceText;        // ����.
    [SerializeField] ButtonOf buttons;      // ���⿡ ���� ��ư��.

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
