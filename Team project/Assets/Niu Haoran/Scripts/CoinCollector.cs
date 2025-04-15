using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public AudioClip collectSound;  // 拖入音效文件
    private AudioSource audioSource;

    void Start()
    {
        // 获取或创建AudioSource组件
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
            // 播放音效
            audioSource.PlayOneShot(collectSound);

            // 销毁金币
            Destroy(other.gameObject);
        }
    }
}
