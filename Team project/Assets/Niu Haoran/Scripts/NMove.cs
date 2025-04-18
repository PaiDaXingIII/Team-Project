using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NMove : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 2;

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

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // ��ʼ����Ƶ���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Adjust Rigidbody settings for better slope movement
        rb.freezeRotation = true; // Freeze rotation to prevent unwanted tipping
        rb.useGravity = true; // Ensure gravity is enabled

        // You can also adjust the friction of the attached collider's material
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            PhysicMaterial material = new PhysicMaterial();
            material.dynamicFriction = 0.1f;
            material.staticFriction = 0.1f;
            col.material = material;
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
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);

        // ������ƶ����룬���ý�ɫת���ƶ�����
        if (movement != Vector3.zero)
        {
            // ����Ŀ����ת
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            // ƽ����ת
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSmoothing * Time.fixedDeltaTime);
        }
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
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= 7)
        {
            // Display the win text.
            winTextObject.SetActive(true);
        }
    }
}
