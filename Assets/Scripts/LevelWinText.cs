using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWinText : MonoBehaviour
{
    public void OnComplete()
    {
        StartCoroutine("TimeEnabler");
    }

    private IEnumerator TimeEnabler()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}