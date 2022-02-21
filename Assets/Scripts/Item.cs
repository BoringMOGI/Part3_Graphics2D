using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "Item/newItem")]
public class Item : ScriptableObject
{
    public string itemName;         // ������ �̸�.
    public string itemType;         // ������ Ÿ��.
    public string itemContent;      // ������ ����.
    public Sprite itemSprite;       // ������ ��������Ʈ.
    public int itemPrice;           // ������ ����.
}
