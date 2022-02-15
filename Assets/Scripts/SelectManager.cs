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
}
