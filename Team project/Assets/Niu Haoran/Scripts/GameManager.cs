using UnityEngine;
using UnityEngine.SceneManagement;  // �Ѿ�����SceneManagement�����ռ�

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void RestartGame()
    {
        // ֱ��ʹ��SceneManager������Ҫ�ټ�SceneManagementǰ׺
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        // ��ѡ�������߼�
        // Time.timeScale = 1f; 
    }
}
