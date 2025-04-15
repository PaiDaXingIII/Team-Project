using UnityEngine;

public class COIN : MonoBehaviour
{
    [Header("��Ч����")]
    public AudioClip collectSound;  // �ռ���Ч����Inspector��������Ƶ�ļ���
    private AudioSource audioSource; // ��Ƶ���

    void Start()
    {
        // ��ȡ�����AudioSource���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        transform.Rotate(1, 0, 0); // ����ԭ�е���תЧ��
    }

    private void OnTriggerEnter(Collider other)
    {
        // ����Ƿ��������ײ
        if (other.CompareTag("Player"))
        {
            // ������Ч
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            // ���ٽ�Ҷ����ӳ�0.1��ȷ����Ч���ţ�
            Destroy(gameObject, 0.1f);
        }
    }
}