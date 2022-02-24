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
