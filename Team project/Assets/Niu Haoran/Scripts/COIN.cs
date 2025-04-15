using UnityEngine;

public class COIN : MonoBehaviour
{
    [Header("音效设置")]
    public AudioClip collectSound;  // 收集音效（在Inspector中拖入音频文件）
    private AudioSource audioSource; // 音频组件

    void Start()
    {
        // 获取或添加AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        transform.Rotate(1, 0, 0); // 保持原有的旋转效果
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检测是否是玩家碰撞
        if (other.CompareTag("Player"))
        {
            // 播放音效
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            // 销毁金币对象（延迟0.1秒确保音效播放）
            Destroy(gameObject, 0.1f);
        }
    }
}