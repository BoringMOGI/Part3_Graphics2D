using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] Image itemImage;       // �̹���.
    [SerializeField] Image selectedImage;   // ���� �̹���.
    [SerializeField] Text nameText;         // �̸�.
    [SerializeField] Text countText;        // ����.
    [SerializeField] Text ownedText;        // ���� ����.
    [SerializeField] Text priceText;        // ����.

    public void Setup()
    {
        OnDeselect(null);
    }

    // event system�� current object�� ��ϵǸ� �ڵ����� ȣ��.
    public void OnSelect(BaseEventData eventData)
    {
        selectedImage.enabled = true;
    }

    // event system�� current object���� ��� ���� �Ǹ� �ڵ����� ȣ��.
    public void OnDeselect(BaseEventData eventData)
    {
        selectedImage.enabled = false;
    }
}
