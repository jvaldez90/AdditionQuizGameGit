using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(LoadSceneDelay());

        SceneManager.LoadScene("Gameplay");
    }
    public void ReturnToMenu()
    {
        StartCoroutine(LoadSceneDelay());
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        StartCoroutine(LoadSceneDelay());

        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    private IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(3f);
    }
}
