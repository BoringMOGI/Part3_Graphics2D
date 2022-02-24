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
        InputManager.Instance.RegestedEventer(this);        // �̺�Ʈ �Ŵ����� ���� ��Ͻ�Ų��.
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

        // ������ �ٸ� �������� ���������� �ִϸ��̼� ���, ��ȣ�ۿ� UI�ݱ�.
        if (input != beforeInput)
        {
            anim.SetInteger("MoveVector", (int)input);
            anim.SetTrigger("onMove");

            InteractionUI.Instance.Close();
        }

        // �̵� ����.
        StartCoroutine(Movement(input));
    }
    private IEnumerator Movement(VECTOR vector)
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

    private Collider2D RayToVector(VECTOR vector, params string[] excepts)
    {
        LayerMask mask = int.MaxValue;      // ��� ���̾ ����.

        for(int i = 0; i<excepts.Length; i++)
        {            
            int layer = 1 << LayerMask.NameToLayer(excepts[i]); // LayerMask.NameToLayer(string) : int   => ���̾��� ����.
            mask ^= layer;                                      // XOR����. 1 xor 1 = 0. => �ش��ϴ� ���� ������ �� ���.
        }

        // mask�� �ش��ϴ� ������Ʈ�� �浹�Ѵ�.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectors[(int)vector], 1f, mask);
        return hit.collider;
    }
}


