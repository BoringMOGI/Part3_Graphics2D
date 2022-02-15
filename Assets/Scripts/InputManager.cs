using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Player player; 

    public void MobileInput(int vector)
    {
        // ���� �Է��� VECTOR�� ��ȯ.
        VECTOR input = (VECTOR)vector;
        player.OnMovement(input);
    }
    public void Submit()
    {
        player.OnSubmit();
    }
    public void Cancel()
    {

    }
}
