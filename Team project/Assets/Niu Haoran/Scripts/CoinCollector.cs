using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public AudioClip collectSound;  // ������Ч�ļ�
    private AudioSource audioSource;

    void Start()
    {
        // ��ȡ�򴴽�AudioSource���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // ������Ч
            audioSource.PlayOneShot(collectSound);

            // ���ٽ��
            Destroy(other.gameObject);
        }
    }
}
