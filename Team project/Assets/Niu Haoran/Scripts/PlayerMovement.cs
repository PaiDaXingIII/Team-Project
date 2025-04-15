using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("�ƶ�����")]
    [SerializeField] private float moveSpeed = 5f;     // ǰ��/�����ٶ�
    [SerializeField] private float rotateSpeed = 120f; // ��ת�ٶȣ���/�룩

    private Rigidbody rb;
    private float moveInput;   // W/S ����ֵ (-1��1)
    private float rotateInput; // A/D ����ֵ (-1��1)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ��ֹ������ת����
    }

    void Update()
    {
        // ��ȡ����
        moveInput = Input.GetAxis("Vertical");    // W/S ��
        rotateInput = Input.GetAxis("Horizontal"); // A/D ��
    }

    void FixedUpdate()
    {
        // ǰ��/�����ƶ�
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // ������ת
        float rotation = rotateInput * rotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
