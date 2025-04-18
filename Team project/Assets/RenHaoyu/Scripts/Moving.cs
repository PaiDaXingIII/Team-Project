using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// 定义一个名为 Move 的公共类，继承自 MonoBehaviour，用于控制角色的移动和交互
public class Moving : MonoBehaviour
{
    // Character Controller of the player.
    private CharacterController controller;
    // 角色的 Animator 组件，用于控制角色的动画播放
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

    // 新增音效相关变量
    [Header("Audio Settings")]
    public AudioClip pickupSound;  // 拾取音效
    private AudioSource audioSource;  // 音频组件

    // 旋转平滑度
    public float rotationSmoothing = 10f;

    // 重力
    public float gravity = 9.81f;
    private Vector3 velocity;

    // 动画设置相关
    [Header("Animation Settings")]
    // 动画控制器中用于控制垂直移动动画的参数名称
    public string verticalParam = "Vert";
    // 动画控制器中用于控制角色状态（如移动、站立）的参数名称
    public string stateParam = "State";

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Character Controller component attached to the player.
        controller = GetComponent<CharacterController>();
        // 获取角色的 Animator 组件
        animator = GetComponent<Animator>();

        // 初始化音频组件
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

        // 如果有移动输入，则让角色转向移动方向
        if (movement != Vector3.zero)
        {
            // 计算目标旋转
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            // 平滑旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
        }

        // 应用重力
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y -= gravity * Time.deltaTime;

        // 移动角色
        Vector3 move = movement.normalized * speed * Time.deltaTime;
        move.y = velocity.y * Time.deltaTime;
        controller.Move(move);

        // 设置动画控制器中垂直移动参数的值
        animator.SetFloat(verticalParam, movement.magnitude);
        // 根据是否有移动设置动画控制器中角色状态参数的值
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

            // 播放拾取音效
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