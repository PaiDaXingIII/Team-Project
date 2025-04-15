using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    [SerializeField] private float moveSpeed = 5f;     // 前进/后退速度
    [SerializeField] private float rotateSpeed = 120f; // 旋转速度（度/秒）

    private Rigidbody rb;
    private float moveInput;   // W/S 输入值 (-1到1)
    private float rotateInput; // A/D 输入值 (-1到1)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 防止物理旋转干扰
    }

    void Update()
    {
        // 获取输入
        moveInput = Input.GetAxis("Vertical");    // W/S 键
        rotateInput = Input.GetAxis("Horizontal"); // A/D 键
    }

    void FixedUpdate()
    {
        // 前进/后退移动
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // 左右旋转
        float rotation = rotateInput * rotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
