using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VECTOR
{
   Up,
   Down,
   Left,
   Right,
}

public class Player : Singleton<Player>
{
    readonly Vector2[] vectors = new Vector2[] {
       Vector2.up,
       Vector2.down,
       Vector2.left,
       Vector2.right
    };

    [SerializeField] float moveSpeed = 5f;

    Animator anim;
    VECTOR beforeInput = 0;
    bool isMoving = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        SwitchControl(true);
    }

    public void SwitchControl(bool isUnlock)
    {
        if (isUnlock)
        {
            InputManager.Instance.OnInputDown += OnMovement;
            InputManager.Instance.OnSubmit += OnSubmit;
            InputManager.Instance.OnCancel += OnCancel;            
        }
        else
        {
            InputManager.Instance.OnInputDown -= OnMovement;
            InputManager.Instance.OnSubmit -= OnSubmit;
            InputManager.Instance.OnCancel -= OnCancel;
        }
    }

    private void OnSubmit()
    {
        if (isMoving)
            return;

        Collider2D node = RayToVector(beforeInput);
        if (node == null)
            return;

        IInteraction target = node.GetComponent<IInteraction>();
        if (target != null)
            target.Interaction();
    }
    private void OnCancel()
    {

    }
    private void OnMovement(VECTOR input)
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

    private Collider2D RayToVector(VECTOR vector)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectors[(int)vector], 1f);
        return hit.collider;
    }
}


