using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VECTOR
{
    None = -1,
    Up,
    Down,
    Left,
    Right,
}

public class Player : Singleton<Player>, IMobileInput
{
    readonly Vector2[] vectors = new Vector2[] {
       Vector2.up,
       Vector2.down,
       Vector2.left,
       Vector2.right
    };

    [SerializeField] float moveSpeed = 5f;

    Animator anim;
    VECTOR beforeInput = VECTOR.Down;
    bool isMoving = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        InputManager.Instance.RegestedEventer(this);        // 이벤트 매니저에 나를 등록시킨다.
    }

    const string EXCEPT_ENVIROMENT = "Enviroment";
    const string EXCEPT_OBJECT = "Object";

    public void Submit()
    {
        if (isMoving)
            return;

        Collider2D node = RayToVector(beforeInput, EXCEPT_ENVIROMENT, EXCEPT_OBJECT);       
        if (node == null)
            return;
        
        IInteraction target = node.GetComponent<IInteraction>();
        if (target != null)
            target.Interaction();
    }
    public void Cancel()
    {
        MenuManager.Instance.Open();
    }
    public void InputVector(VECTOR input, bool isDown)
    {
        if (isMoving)
            return;

        // 이전과 다른 방향으로 움직였으면 애니메이션 재생, 상호작용 UI닫기.
        if (input != beforeInput)
        {
            anim.SetInteger("MoveVector", (int)input);
            anim.SetTrigger("onMove");

            InteractionUI.Instance.Close();
        }

        // 이동 시작.
        StartCoroutine(Movement(input));
    }
    private IEnumerator Movement(VECTOR vector)
    {
        // 도착지점을 계산 후 이동.
        if (RayToVector(vector) == null)
        {
            Vector2 myPosition = transform.position;
            Vector3 destination = myPosition + vectors[(int)vector];

            isMoving = true;

            // 목적지까지 이동.
            while (transform.position != destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // 목적지 도착 이후 이동 방향으로 레이 발사.
        Collider2D collider = RayToVector(vector);
        if (collider != null)
        {
            IInteraction target = collider.GetComponent<IInteraction>();

            // 상호작용 타겟이 존재한다면 해당 타겟의 이름을 출력.
            if (target != null)
                InteractionUI.Instance.Open(target.GetName());
        }

        isMoving = false;

        // 종료 후 이전 방향 대입.
        beforeInput = vector;
    }

    private Collider2D RayToVector(VECTOR vector, params string[] excepts)
    {
        LayerMask mask = int.MaxValue;      // 모든 레이어를 포함.

        for(int i = 0; i<excepts.Length; i++)
        {            
            int layer = 1 << LayerMask.NameToLayer(excepts[i]); // LayerMask.NameToLayer(string) : int   => 레이어의 순번.
            mask ^= layer;                                      // XOR연산. 1 xor 1 = 0. => 해당하는 값을 제거할 때 사용.
        }

        // mask에 해당하는 오브젝트만 충돌한다.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectors[(int)vector], 1f, mask);
        return hit.collider;
    }
}


