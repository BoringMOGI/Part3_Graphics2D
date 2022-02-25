using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Է��� ����ϴ� �Ŵ���.
// ��ϵ� �̺�Ʈ���� ȣ���Ѵ�.
public interface IMobileInput
{
    void InputVector(VECTOR vector, bool isDown);
    void Submit();
    void Cancel();
}

public class InputManager : Singleton<InputManager>
{
    // ���� �������� ����� �̺�Ʈ ����ڰ� ��ư �̺�Ʈ�� ��� �޴´�.
    Stack<IMobileInput> eventers = new Stack<IMobileInput>();

    public void RegestedEventer(IMobileInput eventer)           // �Է� �̺�Ʈ ����� ���.
    {
        eventers.Push(eventer);
    }
    public void ReleaseEventer()                                // �Է� �̺�Ʈ ����� ����.
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

    // ��ϵ� �̺�Ʈ �Լ� ȣ��.
    public void InputDown(VECTOR vector)
    {
        // Stack.Peek : �ֻ���� ���� ������ �Ѵ�. (�������� �ʴ´�)
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
        // ���� �̺�Ʈ ȣ��.
        IMobileInput target = eventers.Peek();
        if (target != null)
            target.Submit();
    }
    public void Cancel()
    {
        // ��� �̺�Ʈ ȣ��.
        IMobileInput target = eventers.Peek();
        if (target != null)
            target.Cancel();
    }
}
