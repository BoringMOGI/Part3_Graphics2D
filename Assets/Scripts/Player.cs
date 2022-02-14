using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum VECTOR
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }
    readonly Vector2[] vectors = new Vector2[] {
        new Vector2(0,0),
        new Vector2(-1,0),      // 왼쪽.
        new Vector2(+1,0),      // 오른쪽.
        new Vector2(0,+1),      // 위쪽.
        new Vector2(0,-1),      // 아래쪽.
    };

    [SerializeField] float moveSpeed = 5f;

    Animator anim;
    Rigidbody2D rigid;

    bool isMoving = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    VECTOR beforeInput = VECTOR.None;

    private Collider2D RayToVector(VECTOR vector)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectors[(int)vector], 1f);
        return hit.collider;
    }

    public void MobileInput(int vector)
    {
        if (isMoving)
            return;

        // 들어온 입력을 VECTOR로 변환.
        VECTOR input = (VECTOR)vector;

        // 이전과 다른 방향으로 움직였으면 애니메이션 재생, 상호작용 UI닫기.
        if (input != beforeInput)
        {
            anim.SetInteger("MoveVector", vector);
            anim.SetTrigger("onMove");

            InteractionUI.Instance.Close();
        }

        // 이동 시작.
        StartCoroutine(Movement(input));
    }
    IEnumerator Movement(VECTOR vector)
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

    public void Submit()
    {
        Collider2D node = RayToVector(beforeInput);
        if (node == null)
            return;

        IInteraction target = node.GetComponent<IInteraction>();
        if (target != null)
        {
            target.Interaction();
        }
    }
    public void Cancel()
    {

    }

    

}


