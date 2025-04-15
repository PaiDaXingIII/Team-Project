using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float moveSpeed = 5f;    // ǰ��/�����ٶ�
    public float rotateSpeed = 100f; // ��ת�ٶ�

    void Update()
    {
        // ��ȡ��������
        float moveInput = Input.GetAxis("Vertical");    // W/S �� ��/�¼�ͷ (��Χ: -1 �� 1)
        float rotateInput = Input.GetAxis("Horizontal"); // A/D �� ��/�Ҽ�ͷ (��Χ: -1 �� 1)

        // ǰ���ƶ���W/S��
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // ������ת��A/D��
        float rotation = rotateInput * rotateSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);
    }
}
