using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// ����һ����Ϊ Move �Ĺ����࣬�̳��� MonoBehaviour�����ڿ��ƽ�ɫ���ƶ��ͽ���
public class Moving : MonoBehaviour
{
    // Character Controller of the player.
    private CharacterController controller;
    // ��ɫ�� Animator ��������ڿ��ƽ�ɫ�Ķ�������
    private Animator animator;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 5f;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;

    // ������Ч��ر���
    [Header("Audio Settings")]
    public AudioClip pickupSound;  // ʰȡ��Ч
    private AudioSource audioSource;  // ��Ƶ���

    // ��תƽ����
    public float rotationSmoothing = 10f;

    // ����
    public float gravity = 9.81f;
    private Vector3 velocity;

    // �����������
    [Header("Animation Settings")]
    // ���������������ڿ��ƴ�ֱ�ƶ������Ĳ�������
    public string verticalParam = "Vert";
    // ���������������ڿ��ƽ�ɫ״̬�����ƶ���վ�����Ĳ�������
    public string stateParam = "State";

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Character Controller component attached to the player.
        controller = GetComponent<CharacterController>();
        // ��ȡ��ɫ�� Animator ���
        animator = GetComponent<Animator>();

        // ��ʼ����Ƶ���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Initialize count to zero.
        count = 0;

        // Update the count display.
        SetCountText();

        // Initially set the win text to be inactive.
        winTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;

        Debug.Log($"Movement Input: X={movementX}, Y={movementY}");
    }

    // Update is called once per frame.
    private void Update()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // ������ƶ����룬���ý�ɫת���ƶ�����
        if (movement != Vector3.zero)
        {
            // ����Ŀ����ת
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            // ƽ����ת
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
        }

        // Ӧ������
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y -= gravity * Time.deltaTime;

        // �ƶ���ɫ
        Vector3 move = movement.normalized * speed * Time.deltaTime;
        move.y = velocity.y * Time.deltaTime;
        controller.Move(move);

        // ���ö����������д�ֱ�ƶ�������ֵ
        animator.SetFloat(verticalParam, movement.magnitude);
        // �����Ƿ����ƶ����ö����������н�ɫ״̬������ֵ
        animator.SetFloat(stateParam, movement.magnitude > 0 ? 1f : 0f);

        Debug.Log($"Velocity: {velocity}, Movement: {move}");
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected.
            count = count + 1;

            // ����ʰȡ��Ч
            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // Update the count display.
            SetCountText();
        }
    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = $"Count: {count}/7";

        // Check if the count has reached or exceeded the win condition.
        if (count >= 7)
        {
            // Display the win text.
            winTextObject.SetActive(true);
        }
    }
}