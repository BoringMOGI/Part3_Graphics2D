using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : SelectManager<MenuManager>
{
    public enum MENU
    {
        Character,
        Inventory,
        Skill,
        DataLoad,
    }

    [SerializeField] Animation menuAnim;
    [SerializeField] MenuButton[] menuButtons;

    const string KEY_SHOW = "TopMenu_Show";
    const string KEY_CLOSE = "TopMenu_Close";

    private void Start()
    {
        // 최초에 버튼들에게 세팅한다.
        for(int i = 0; i<menuButtons.Length; i++)
            menuButtons[i].Setup((MENU)i, OnSelectedMenu);
    }

    public override void Open()
    {
        base.Open();

        menuAnim.Play(KEY_SHOW);                // 등장 애니메이션 출력.
        SetButton(menuButtons[0]);              // 최초 버튼 선택.
    }
    public override void Close()
    {
        base.Close();

        ClearButton();                          // 선택 버튼 해제.
        menuAnim.Play(KEY_CLOSE);               // 퇴장 애니메이션 출력.
    }

    private void OnSelectedMenu(MENU menu)
    {
        Close();

        switch (menu)
        {
            case MENU.Inventory:
                InventoryUI.Instance.Open();
                break;
        }
    }

    protected override void OnCancel()
    {
        Close();
    }
}
