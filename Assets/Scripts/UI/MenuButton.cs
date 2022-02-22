using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : Button
{
    [SerializeField] Image selectedImage;

    public delegate void SubmitEvent(MenuManager.MENU menu);
    private SubmitEvent Submit;

    MenuManager.MENU menuType;

    public void Setup(MenuManager.MENU menuType, SubmitEvent Submit)
    {
        this.menuType = menuType;       // 메뉴 버튼의 타입.
        this.Submit = Submit;           // 메뉴 버튼 선택 이벤트.

        OnDeselect();
    }

    public override void OnDeselect()
    {
        selectedImage.enabled = false;
    }
    public override void OnSelect()
    {
        selectedImage.enabled = true;
    }

    public override void OnSubmit()
    {
        Submit?.Invoke(menuType);
    }
}
