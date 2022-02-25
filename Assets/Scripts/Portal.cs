using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, ITrigger
{
    [SerializeField] Transform exit;

    public void Trigger(Player player)
    {
        player.transform.position = exit.position;
    }
}
