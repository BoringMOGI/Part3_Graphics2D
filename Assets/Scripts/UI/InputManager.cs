using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 입력을 담당하는 매니저.
// 등록된 이벤트들을 호출한다.
public class InputManager : Singleton<InputManager>
{
    public delegate void InputEvent(VECTOR v);
    public event InputEvent OnInputDown;
    public event InputEvent OnInputUp;
    public event System.Action OnSubmit;
    public event System.Action OnCancel;

    // 등록된 이벤트 함수 호출.
    public void InputDown(VECTOR vector)
    {
        OnInputDown?.Invoke(vector);
    }
    public void InputUp(VECTOR vector)
    {
        OnInputUp?.Invoke(vector);
    }

    public void Submit()
    {
        // 선택 이벤트 호출.
        OnSubmit?.Invoke();
    }
    public void Cancel()
    {
        // 취소 이벤트 호출.
        OnCancel?.Invoke();
    }
}
