using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonHandler
{
    void OnSelect();            // 선택 되었을 때.
    void OnDeselect();          // 비선택 되었을 때.
    void OnSubmit();            // 버튼을 눌렀을 때.
    IButtonHandler GetButtonOf(VECTOR v);
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

    public IButtonHandler GetButtonOf(VECTOR v)
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

        // 최초에 버튼 세팅 시 이벤트 등록.
        if(current == null)
        {
            // 입력 매니저에게 자신의 이벤트 함수 등록.
            InputManager.Instance.OnInputUp += MoveButton;
            InputManager.Instance.OnSubmit += SubmitButton;
            InputManager.Instance.OnCancel += CancelButton;
        }

        // 이전 선택이 있다면 해제.
        if (current != null)
            current.OnDeselect();

        // 현재 선택으로 변경.
        current = target;
        current.OnSelect();
    }
    public void ClearButton()
    {
        if(current != null)
            current.OnDeselect();

        current = null;

        // 입력 매니저에게 자신의 이벤트 함수 등록 해제.
        InputManager.Instance.OnInputUp -= MoveButton;
        InputManager.Instance.OnSubmit -= SubmitButton;
        InputManager.Instance.OnCancel -= CancelButton;
    }

    public void MoveButton(VECTOR v)
    {
        if (current == null)
            return;

        // vector에 해당하는 다음 버튼이 있다면
        IButtonHandler nextButton = current.GetButtonOf(v);
        if(nextButton != null)
        {
            // 그것을 선택.
            SetButton(nextButton);
        }
    }
    public void SubmitButton()
    {
        if (current == null)
            return;

        // 클릭.
        current.OnSubmit();
    }
    public void CancelButton()
    {

    }
}
