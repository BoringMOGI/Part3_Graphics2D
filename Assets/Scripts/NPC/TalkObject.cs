using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkObject : MonoBehaviour, IInteraction
{
    [SerializeField] string[] comment;

    public string GetName()
    {
        return string.Empty;
    }

    public void Interaction()
    {
        TalkManager.Instance.Talk(comment);
    }
}
