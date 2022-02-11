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

        VECTOR input = (VECTOR)vector;

        if (input != beforeInput)
        {
            anim.SetInteger("MoveVector", vector);
            anim.SetTrigger("onMove");
        }

        // 특정 방향으로 레이를 발사.
        if (RayToVector(input) == null)
        {
            // 도착지점을 계산 후 이동.
            Vector2 myPosition = transform.position;
            Vector2 destination = myPosition + vectors[(int)input];
            StartCoroutine(Movement(destination));
        }

        beforeInput = input;
    }
    IEnumerator Movement(Vector3 destination)
    {
        isMoving = true;


        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
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


