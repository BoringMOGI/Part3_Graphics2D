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
        new Vector2(-1,0),      // ����.
        new Vector2(+1,0),      // ������.
        new Vector2(0,+1),      // ����.
        new Vector2(0,-1),      // �Ʒ���.
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

        // ���� �Է��� VECTOR�� ��ȯ.
        VECTOR input = (VECTOR)vector;

        // ������ �ٸ� �������� ���������� �ִϸ��̼� ���, ��ȣ�ۿ� UI�ݱ�.
        if (input != beforeInput)
        {
            anim.SetInteger("MoveVector", vector);
            anim.SetTrigger("onMove");

            InteractionUI.Instance.Close();
        }

        // �̵� ����.
        StartCoroutine(Movement(input));
    }
    IEnumerator Movement(VECTOR vector)
    {
        // ���������� ��� �� �̵�.
        if (RayToVector(vector) == null)
        {
            Vector2 myPosition = transform.position;
            Vector3 destination = myPosition + vectors[(int)vector];

            isMoving = true;

            // ���������� �̵�.
            while (transform.position != destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // ������ ���� ���� �̵� �������� ���� �߻�.
        Collider2D collider = RayToVector(vector); 
        if (collider != null)
        {
            IInteraction target = collider.GetComponent<IInteraction>();

            // ��ȣ�ۿ� Ÿ���� �����Ѵٸ� �ش� Ÿ���� �̸��� ���.
            if (target != null)
                InteractionUI.Instance.Open(target.GetName());
        }

        isMoving = false;

        // ���� �� ���� ���� ����.
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


