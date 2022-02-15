using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Player player; 

    public void MobileInput(int vector)
    {
        // 들어온 입력을 VECTOR로 변환.
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
