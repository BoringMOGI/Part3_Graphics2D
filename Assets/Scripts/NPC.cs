using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    void Interaction();
    string GetName();
}

public class NPC : MonoBehaviour, IInteraction
{
    [SerializeField] string npcName;

    public void Interaction()
    {
        //Debug.Log("상점 구매창 열기");
        //ShopManager.Instance.OpenShop();

        TalkManager.Instance.Talk(new string[] { 
            "안녕?",
            "이곳은 우리가 게임을 개발하는 곳이야.",
            "수업이 끝나버렸다.",
            "주말 잘 쉬다 오자."
        });
    }

    public string GetName()
    {
        return npcName;
    }
}
