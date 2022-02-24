using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonHandler
{
    void OnSelect();            // 선택 되었을 때.
    void OnDeselect();          // 비선택 되었을 때.
    void OnSubmit();            // 버튼을 눌렀을 때.
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

        // 이전 선택이 있다면 해제.
        if (current != null)
            current.OnDeselect();

        // 현재 선택으로 변경.
        current = target;
        current.OnSelect();
    }
    protected virtual void ClearButton()
    {
        if(current != null)
            current.OnDeselect();

        current = null;
    }

    // 인터페이스 구현.
    public virtual void InputVector(VECTOR v, bool isDown)
    {
        // 현재 선택중인 버튼이 없거나 버튼 up이 아니라면 return.
        if (current == null || isDown)
            return;

        // vector에 해당하는 다음 버튼이 있다면
        Button nextButton = current.GetButtonOf(v);
        if(nextButton != null)
        {
            // 그것을 선택.
            SetButton(nextButton);
        }
    }
    public virtual void Submit()
    {
        if (current == null)
            return;

        // 클릭.
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
