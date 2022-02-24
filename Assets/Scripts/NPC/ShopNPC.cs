using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPC
{
    public override void Interaction()
    {
        //Debug.Log("상점 구매창 열기");
        ShopManager.Instance.Open();
    }
}
