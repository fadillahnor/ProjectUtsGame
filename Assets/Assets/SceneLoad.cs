using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    
    public void LoadSceneBaru(string Scenename)
    {
        SceneManager.LoadScene(Scenename);
    }

    public void OnApplicationQuit()
    {
        Debug.Log("Aplikasi Keluar...");
        Application.Quit();
    }
}
