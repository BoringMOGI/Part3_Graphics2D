using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    void Interaction();
    string GetName();
}

public abstract class NPC : MonoBehaviour, IInteraction
{
    [SerializeField] string npcName;

    public abstract void Interaction();
    public string GetName()
    {
        return npcName;
    }
}
