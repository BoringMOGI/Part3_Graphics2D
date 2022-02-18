using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : Singleton<TalkManager>
{
    [SerializeField] RectTransform talkRect;
    [SerializeField] RectTransform downPivot;
    [SerializeField] Text talkText;

    Vector3 originPosition;     // 나타나는 위치.
    Vector3 hidePosition;       // 숨는 위치.

    string[] comments;          // 대화 내용.
    bool isClick = false;       // 유저가 입력함.

    private void Start()
    {
        originPosition = talkRect.position;
        hidePosition = downPivot.position;

        // 최초 시작시에는 숨는 위치에 이동.
        talkRect.position = hidePosition;
    }

    public void Talk(string[] comments)
    { 
        this.comments = comments;

        Player.Instance.SwitchControl(false);               // 플레이어 입력 이벤트 제거.
        InputManager.Instance.OnSubmit += OnSubmit;         // 확인 이벤트 등록.
        InputManager.Instance.OnCancel += OnCancel;         // 취소 이벤트 등록.

        StartCoroutine(MovePanel(false, () => StartCoroutine(Talk())));
    }
    private void Close()
    {
        Player.Instance.SwitchControl(true);                // 플레이어 입력 이벤트 등록.
        InputManager.Instance.OnSubmit -= OnSubmit;         // 확인 이벤트 제거.
        InputManager.Instance.OnCancel -= OnCancel;         // 취소 이벤트 제거.

        StartCoroutine(MovePanel(true));
    }

    private void OnSubmit()
    {
        isClick = true;
    }
    private void OnCancel()
    {

    }

    IEnumerator MovePanel(bool isHide, System.Action OnEndMove = null)
    {
        Vector3 destination = isHide ? hidePosition : originPosition;
        float speed = Screen.height;

        while (Vector3.Distance(talkRect.position, destination) > 0.01f)
        {
            
            talkRect.position = Vector3.MoveTowards(talkRect.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        talkRect.position = destination;
        OnEndMove?.Invoke();
    }
    IEnumerator Talk()
    {
        foreach (string c in comments)
        {
            talkText.text = c;                          // 대화 텍스트 입력.
            yield return StartCoroutine(Waitting());    // 유저가 입력할때까지 대기.                        
        }

        Close();
    }    
    IEnumerator Waitting()
    {
        isClick = false;
        while (!isClick)                // 유저의 입력이 들어오기 전까지 반복.
            yield return null;          // 한 프레임 대기.
    }
}
