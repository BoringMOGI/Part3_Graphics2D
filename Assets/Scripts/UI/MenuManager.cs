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
        // ���ʿ� ��ư�鿡�� �����Ѵ�.
        for(int i = 0; i<menuButtons.Length; i++)
            menuButtons[i].Setup((MENU)i, OnSelectedMenu);
    }

    public void OpenMenu()
    {
        menuAnim.Play(KEY_SHOW);                // ���� �ִϸ��̼� ���.
        SetButton(menuButtons[0]);              // ���� ��ư ����.
        Player.Instance.SwitchControl(false);   // �÷��̾� ��Ʈ�� ����.
        SwitchInputEvent(true);                 // ���� �̺�Ʈ ���.
    }
    private void CloseMenu()
    {       
        SwitchInputEvent(false);                // ���� �̺�Ʈ ����.
        Player.Instance.SwitchControl(true);    // �÷��̾� ��Ʈ�� ���.
        ClearButton();                          // ���� ��ư ����.
        menuAnim.Play(KEY_CLOSE);               // ���� �ִϸ��̼� ���.
    }

    private void OnSelectedMenu(MENU menu)
    {
        Debug.Log("�޴��� ���� �Ǿ��� : " + menu);
        CloseMenu();
    }

    protected override void CancelButton()
    {
        CloseMenu();
    }
}
