using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "Item/newItem")]
public class Item : ScriptableObject
{
    public string itemName;         // 아이템 이름.
    public string itemType;         // 아이템 타입.
    public string itemContent;      // 아이템 설명.
    public Sprite itemSprite;       // 아이템 스프라이트.
    public int itemPrice;           // 아이템 가격.
}
