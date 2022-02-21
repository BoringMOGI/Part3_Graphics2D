using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountPanelUI : MonoBehaviour
{
    // 선택한 수량과 결정,취소 여부.
    public delegate void CountEvent(int count, bool isSubmit);
    CountEvent Callback;

    [SerializeField] Text countText;

    const int MIN_COUNT = 1;
    const int MAX_COUNT = 99;
    int count = 1;

    public void Open(CountEvent Callback)
    {
        this.Callback = Callback;

        InputManager.Instance.OnInputUp += OnInputUp;
        InputManager.Instance.OnSubmit += OnSubmit;
        InputManager.Instance.OnCancel += OnCancel;

        count = 1;
        countText.text = count.ToString();

        gameObject.SetActive(true);
    }
    private void Close()
    {
        InputManager.Instance.OnInputUp -= OnInputUp;
        InputManager.Instance.OnSubmit -= OnSubmit;
        InputManager.Instance.OnCancel -= OnCancel;

        gameObject.SetActive(false);
    }

    private void OnInputUp(VECTOR v)
    {
        if(v == VECTOR.Left)
        {
            count -= 1;
        }
        else if(v == VECTOR.Right)
        {
            count += 1;
        }
        else if(v == VECTOR.Up)
        {
            count += 10;
        }
        else if(v == VECTOR.Down)
        {
            count -= 10;
        }

        count = Mathf.Clamp(count, MIN_COUNT, MAX_COUNT);
        countText.text = count.ToString();
    }
    private void OnSubmit()
    {        
        Callback(count, true);          // 결과 값 전달.
        Close();
    }
    private void OnCancel()
    {        
        Callback(-1, false);            // 결과 값 전달.
        Close();
    }



}
