using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkNPC : NPC
{
    [SerializeField] string[] talks;

    public override void Interaction()
    {
        TalkManager.Instance.Talk(talks);
    }
}
