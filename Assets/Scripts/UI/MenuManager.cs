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

    public override void Open()
    {
        base.Open();

        menuAnim.Play(KEY_SHOW);                // ���� �ִϸ��̼� ���.
        SetButton(menuButtons[0]);              // ���� ��ư ����.
    }
    public override void Close()
    {
        base.Close();

        ClearButton();                          // ���� ��ư ����.
        menuAnim.Play(KEY_CLOSE);               // ���� �ִϸ��̼� ���.
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
