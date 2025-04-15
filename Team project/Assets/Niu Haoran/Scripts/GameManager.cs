using UnityEngine;
using UnityEngine.SceneManagement;  // 已经包含SceneManagement命名空间

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void RestartGame()
    {
        // 直接使用SceneManager，不需要再加SceneManagement前缀
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        // 可选的重置逻辑
        // Time.timeScale = 1f; 
    }
}
