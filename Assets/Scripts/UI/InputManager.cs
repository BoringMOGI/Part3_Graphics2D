using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 입력을 담당하는 매니저.
// 등록된 이벤트들을 호출한다.
public interface IMobileInput
{
    void InputVector(VECTOR vector, bool isDown);
    void Submit();
    void Cancel();
}

public class InputManager : Singleton<InputManager>
{
    // 가장 마지막에 등록한 이벤트 대상자가 버튼 이벤트의 제어를 받는다.
    Stack<IMobileInput> eventers = new Stack<IMobileInput>();

    public void RegestedEventer(IMobileInput eventer)           // 입력 이벤트 대상자 등록.
    {
        eventers.Push(eventer);
    }
    public void ReleaseEventer()                                // 입력 이벤트 대상자 해제.
    {
        eventers.Pop();
    }


    readonly KeyCode[] arrowKeys = new KeyCode[] {
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow, 
        KeyCode.RightArrow 
    };

#if UNITY_EDITOR
    private void Update()
    {
        for(int i = 0; i<arrowKeys.Length; i++)
        {
            if (Input.GetKeyDown(arrowKeys[i]))
                InputUp((VECTOR)i);
        }

        for(int i = 0; i<arrowKeys.Length; i++)
        {
            if (Input.GetKey(arrowKeys[i]))
                InputDown((VECTOR)i);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Submit();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }
#endif

    // 등록된 이벤트 함수 호출.
    public void InputDown(VECTOR vector)
    {
        // Stack.Peek : 최상단의 값을 참조만 한다. (꺼내오지 않는다)
        IMobileInput target = eventers.Peek();
        if(target != null)
            target.InputVector(vector, true);
    }
    public void InputUp(VECTOR vector)
    {
        IMobileInput target = eventers.Peek();
        if (target != null)
            target.InputVector(vector, false);
    }
    public void Submit()
    {
        // 선택 이벤트 호출.
        IMobileInput target = eventers.Peek();
        if (target != null)
            target.Submit();
    }
    public void Cancel()
    {
        // 취소 이벤트 호출.
        IMobileInput target = eventers.Peek();
        if (target != null)
            target.Cancel();
    }
}
