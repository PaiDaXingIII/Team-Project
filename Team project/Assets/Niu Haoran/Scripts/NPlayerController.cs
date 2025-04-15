using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))] // ȷ����AudioSource���
public class NPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float rotationSpeed = 100f;

    [Header("UI")]
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    [Header("Audio")]
    public AudioClip collectSound; // ����ռ���Ч����Inspector��������Ƶ�ļ���

    private Rigidbody rb;
    private AudioSource audioSource;
    private int count;
    private Vector2 inputVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // ��ʼ����Ƶ���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        count = 0;
        UpdateCountUI();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y);
        movement = transform.TransformDirection(movement);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            // ������Ч
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            other.gameObject.SetActive(false);
            count++;
            UpdateCountUI();

            if (count >= 9) winTextObject.SetActive(true);
        }
    }

    void UpdateCountUI()
    {
        countText.text = $"Collected: {count}/9";
    }
}