using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectManager : MonoBehaviour
{
    static SelectManager instance;
    public static SelectManager Instance => instance;

    GameObject currentSelected
    {
        get 
        {
            return EventSystem.current.currentSelectedGameObject;
        }
        set
        {
            EventSystem.current.SetSelectedGameObject(value);
        }
    }     // ���� �������� ������Ʈ.

    private void Awake()
    {
        instance = this;
    }

    public void SetCurrentObject(GameObject target)
    {
        currentSelected = target;
    }
    public void ClearCurrentObject()
    {
        currentSelected = null;
    }

}
