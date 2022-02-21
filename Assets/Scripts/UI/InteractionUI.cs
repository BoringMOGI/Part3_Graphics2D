using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    static InteractionUI instance;
    public static InteractionUI Instance => instance;

    [SerializeField] GameObject panel;
    [SerializeField] Text text;

    private void Awake()
    {
        instance = this;
        Close();
    }

    public void Open(string str)
    {
        panel.SetActive(true);
        text.text = str;
    }
    public void Close()
    {
        panel.SetActive(false);
    }
}
