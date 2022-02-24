using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : Singleton<TalkManager>, IMobileInput
{
    [SerializeField] RectTransform talkRect;
    [SerializeField] RectTransform downPivot;
    [SerializeField] Text talkText;
    [SerializeField] Animation talkCursor;

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
        talkText.text = string.Empty;
        talkCursor.gameObject.SetActive(false);
    }

    public void Talk(string[] comments)
    { 
        this.comments = comments;

        InputManager.Instance.RegestedEventer(this);        // 입력 매니저에게 나를 등록.
        talkText.text = string.Empty;                       // 기존에 있던 텍스트 제거.

        // 코루틴 실행.
        // 매개 변수로 무명 메소드 전달 (=람다식)
        StartCoroutine(MovePanel(false, () => StartCoroutine(Talk())));
    }
    private void Close()
    {
        InputManager.Instance.ReleaseEventer();             // 입력 매니저에게 나를 등록 해제.
        StartCoroutine(MovePanel(true));
    }

    public void InputVector(VECTOR vector, bool isDown)
    {

    }
    public void Submit()
    {
        isClick = true;
    }
    public void Cancel()
    {

    }

    IEnumerator MovePanel(bool isHide, System.Action OnEndMove = null)
    {
        Vector3 destination = isHide ? hidePosition : originPosition;           // 목적지 위치 지정.
        float speed = Screen.height;                                            // 패널 속도를 화면의 높이로 대입.
        
        while (Vector3.Distance(talkRect.position, destination) > 0.01f)        // 목적지까지 이동.
        {
            talkRect.position = Vector3.MoveTowards(talkRect.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        talkRect.position = destination;                                        // 이동이 끝나면 목적지로 정확히 Fix.

        // 아래와 같다.
        //if (OnEndMove != null)
        //    OnEndMove.Invoke();

        OnEndMove?.Invoke();                                                    // 이벤트 함수 실행. (단, 있으면)
    }
    IEnumerator Talk()
    {
        foreach (string str in comments)
        {
            yield return StartCoroutine(TextAnimation(str));    // 텍스트 애니메이션 출력.
            yield return StartCoroutine(Waitting());            // 유저가 입력할때까지 대기.                        
        }

        Close();
    }    

    IEnumerator TextAnimation(string str)
    {
        // 미리 만들어두고 캐싱해서 쓴다.
        WaitForSeconds wait = new WaitForSeconds(0.03f);

        talkText.text = string.Empty;
        for (int i = 0; i <= str.Length; i++)
        {
            // string.Substring(int, int) : string
            //  = "매개변수1" 번째부터 "매개변수2" 개의 문자를 반환.
            talkText.text = str.Substring(0, i);

            // 사용 할때마다 new연산하면 CG가 쌓이므로 미리 객체 생성 후 재사용.
            yield return wait;
        }
    }
    IEnumerator Waitting()
    {
        // 기다릴 때는 커서 애니메이팅을 켠다.
        talkCursor.gameObject.SetActive(true);

        isClick = false;
        while (!isClick)                // 유저의 입력이 들어오기 전까지 반복.
            yield return null;          // 한 프레임 대기.

        // 커서 애니메이션 종료.
        talkCursor.gameObject.SetActive(false);             
    }
}
