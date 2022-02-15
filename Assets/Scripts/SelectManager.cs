using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonHandler
{
    void OnSelect();            // ���� �Ǿ��� ��.
    void OnDeselect();          // ���� �Ǿ��� ��.
    void OnSubmit();            // ��ư�� ������ ��.
    IButtonHandler GetButtonOf(VECTOR v);
}
public struct ButtonOf
{
    public IButtonHandler upButton;
    public IButtonHandler downButton;
    public IButtonHandler leftButton;
    public IButtonHandler rightButton;

    public IButtonHandler GetButtonOf(VECTOR v)
    {
        switch(v)
        {
            case VECTOR.Up:
                return upButton;
            case VECTOR.Down:
                return downButton;
            case VECTOR.Left:
                return leftButton;
            case VECTOR.Right:
                return rightButton;
        }

        return null;
    }
}

public class SelectManager : MonoBehaviour
{
    static SelectManager instance;
    public static SelectManager Instance => instance;

    IButtonHandler current;

    private void Awake()
    {
        instance = this;
    }

    public void SetButton(IButtonHandler target)
    {
        if (target == null)
            return;

        // ���� ������ �ִٸ� ����.
        if (current != null)
            current.OnDeselect();

        // ���� �������� ����.
        current = target;
        current.OnSelect();
    }
    public void ClearButton()
    {
        if(current != null)
            current.OnDeselect();

        current = null;
    }
    public void MoveButton(VECTOR v)
    {
        if (current == null)
            return;

        // vector�� �ش��ϴ� ���� ��ư�� �ִٸ�
        IButtonHandler nextButton = current.GetButtonOf(v);
        if(nextButton != null)
        {
            // �װ��� ����.
            SetButton(nextButton);
        }
    }
    public void SubmitButton()
    {
        if (current == null)
            return;

        // Ŭ��.
        current.OnSubmit();
    }
}
