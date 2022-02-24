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

    Vector3 originPosition;     // ��Ÿ���� ��ġ.
    Vector3 hidePosition;       // ���� ��ġ.

    string[] comments;          // ��ȭ ����.
    bool isClick = false;       // ������ �Է���.

    private void Start()
    {
        originPosition = talkRect.position;
        hidePosition = downPivot.position;

        // ���� ���۽ÿ��� ���� ��ġ�� �̵�.
        talkRect.position = hidePosition;
        talkText.text = string.Empty;
        talkCursor.gameObject.SetActive(false);
    }

    public void Talk(string[] comments)
    { 
        this.comments = comments;

        InputManager.Instance.RegestedEventer(this);        // �Է� �Ŵ������� ���� ���.
        talkText.text = string.Empty;                       // ������ �ִ� �ؽ�Ʈ ����.

        // �ڷ�ƾ ����.
        // �Ű� ������ ���� �޼ҵ� ���� (=���ٽ�)
        StartCoroutine(MovePanel(false, () => StartCoroutine(Talk())));
    }
    private void Close()
    {
        InputManager.Instance.ReleaseEventer();             // �Է� �Ŵ������� ���� ��� ����.
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
        Vector3 destination = isHide ? hidePosition : originPosition;           // ������ ��ġ ����.
        float speed = Screen.height;                                            // �г� �ӵ��� ȭ���� ���̷� ����.
        
        while (Vector3.Distance(talkRect.position, destination) > 0.01f)        // ���������� �̵�.
        {
            talkRect.position = Vector3.MoveTowards(talkRect.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        talkRect.position = destination;                                        // �̵��� ������ �������� ��Ȯ�� Fix.

        // �Ʒ��� ����.
        //if (OnEndMove != null)
        //    OnEndMove.Invoke();

        OnEndMove?.Invoke();                                                    // �̺�Ʈ �Լ� ����. (��, ������)
    }
    IEnumerator Talk()
    {
        foreach (string str in comments)
        {
            yield return StartCoroutine(TextAnimation(str));    // �ؽ�Ʈ �ִϸ��̼� ���.
            yield return StartCoroutine(Waitting());            // ������ �Է��Ҷ����� ���.                        
        }

        Close();
    }    

    IEnumerator TextAnimation(string str)
    {
        // �̸� �����ΰ� ĳ���ؼ� ����.
        WaitForSeconds wait = new WaitForSeconds(0.03f);

        talkText.text = string.Empty;
        for (int i = 0; i <= str.Length; i++)
        {
            // string.Substring(int, int) : string
            //  = "�Ű�����1" ��°���� "�Ű�����2" ���� ���ڸ� ��ȯ.
            talkText.text = str.Substring(0, i);

            // ��� �Ҷ����� new�����ϸ� CG�� ���̹Ƿ� �̸� ��ü ���� �� ����.
            yield return wait;
        }
    }
    IEnumerator Waitting()
    {
        // ��ٸ� ���� Ŀ�� �ִϸ������� �Ҵ�.
        talkCursor.gameObject.SetActive(true);

        isClick = false;
        while (!isClick)                // ������ �Է��� ������ ������ �ݺ�.
            yield return null;          // �� ������ ���.

        // Ŀ�� �ִϸ��̼� ����.
        talkCursor.gameObject.SetActive(false);             
    }
}
