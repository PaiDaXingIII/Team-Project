using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5f;    // 前进/后退速度
    public float rotateSpeed = 100f; // 旋转速度

    void Update()
    {
        // 获取键盘输入
        float moveInput = Input.GetAxis("Vertical");    // W/S 或 上/下箭头 (范围: -1 到 1)
        float rotateInput = Input.GetAxis("Horizontal"); // A/D 或 左/右箭头 (范围: -1 到 1)

        // 前后移动（W/S）
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // 左右旋转（A/D）
        float rotation = rotateInput * rotateSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);
    }
}
