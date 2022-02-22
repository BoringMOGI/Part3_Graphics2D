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

    public void OpenMenu()
    {
        menuAnim.Play(KEY_SHOW);                // 등장 애니메이션 출력.
        SetButton(menuButtons[0]);              // 최초 버튼 선택.
        Player.Instance.SwitchControl(false);   // 플레이어 컨트롤 제거.
        SwitchInputEvent(true);                 // 나의 이벤트 등록.
    }
    private void CloseMenu()
    {       
        SwitchInputEvent(false);                // 나의 이벤트 해제.
        Player.Instance.SwitchControl(true);    // 플레이어 컨트롤 등록.
        ClearButton();                          // 선택 버튼 해제.
        menuAnim.Play(KEY_CLOSE);               // 퇴장 애니메이션 출력.
    }

    private void OnSelectedMenu(MENU menu)
    {
        Debug.Log("메뉴가 선택 되었다 : " + menu);
        CloseMenu();
    }

    protected override void CancelButton()
    {
        CloseMenu();
    }
}
