using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPC
{
    public override void Interaction()
    {
        //Debug.Log("���� ����â ����");
        ShopManager.Instance.Open();
    }
}
