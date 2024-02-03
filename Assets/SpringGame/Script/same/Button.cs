using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("SpringGame");
    }
    public void GameTitle()
    {
        //今開いているシーンを再読み込み
        SceneManager.LoadScene("StartScene");
    }
}
