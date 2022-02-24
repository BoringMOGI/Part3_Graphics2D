using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonHandler
{
    void OnSelect();            // ���� �Ǿ��� ��.
    void OnDeselect();          // ���� �Ǿ��� ��.
    void OnSubmit();            // ��ư�� ������ ��.
    Button GetButtonOf(VECTOR v);
}

[System.Serializable]
public struct ButtonOf
{
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;
}

public abstract class Button : MonoBehaviour, IButtonHandler
{
    [SerializeField]
    protected ButtonOf buttons;

    public void SetButtonOf(VECTOR v, Button button)
    {
        switch (v)
        {
            case VECTOR.Up:
                buttons.upButton = button;
                break;
            case VECTOR.Down:
                buttons.downButton = button;
                break;
            case VECTOR.Left:
                buttons.leftButton = button;
                break;
            case VECTOR.Right:
                buttons.rightButton = button;
                break;
        }
    }
    public Button GetButtonOf(VECTOR v)
    {
        switch (v)
        {
            case VECTOR.Up:
                return buttons.upButton;
            case VECTOR.Down:
                return buttons.downButton;
            case VECTOR.Left:
                return buttons.leftButton;
            case VECTOR.Right:
                return buttons.rightButton;
        }

        return null;
    }
    public abstract void OnDeselect();
    public abstract void OnSelect();
    public abstract void OnSubmit();
}
public abstract class SelectManager<T> : Singleton<T>, IMobileInput
    where T : MonoBehaviour
{
    protected Button current;

    protected virtual void SetButton(Button target)
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
    protected virtual void ClearButton()
    {
        if(current != null)
            current.OnDeselect();

        current = null;
    }

    // �������̽� ����.
    public virtual void InputVector(VECTOR v, bool isDown)
    {
        // ���� �������� ��ư�� ���ų� ��ư up�� �ƴ϶�� return.
        if (current == null || isDown)
            return;

        // vector�� �ش��ϴ� ���� ��ư�� �ִٸ�
        Button nextButton = current.GetButtonOf(v);
        if(nextButton != null)
        {
            // �װ��� ����.
            SetButton(nextButton);
        }
    }
    public virtual void Submit()
    {
        if (current == null)
            return;

        // Ŭ��.
        current.OnSubmit();
    }
    public virtual void Cancel()
    {
        OnCancel();
    }

    public virtual void Open()
    {
        InputManager.Instance.RegestedEventer(this);
    }
    public virtual void Close()
    {
        InputManager.Instance.ReleaseEventer();
    }

    protected virtual void OnCancel()
    {

    }
}
