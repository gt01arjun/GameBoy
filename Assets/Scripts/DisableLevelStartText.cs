using System.Collections;
using UnityEngine;

public class DisableLevelStartText : MonoBehaviour
{
    [SerializeField]
    private GameObject _gasBarParent;
    [SerializeField]
    private GameObject _level;
    [SerializeField]
    private GameObject _car1;
    [SerializeField]
    private GameObject _car2;
    [SerializeField]
    private GameObject _randomObject;
    [SerializeField]
    private GameObject _player;

    public void OnComplete()
    {
        StartCoroutine("TimeDisabler");
    }

    private IEnumerator TimeDisabler()
    {
        yield return new WaitForSeconds(1f);
        _gasBarParent.SetActive(true);
        _level.SetActive(true);
        _car1.SetActive(true);
        _car2.SetActive(true);
        _randomObject.SetActive(true);
        _player.SetActive(true);
        gameObject.SetActive(false);
    }
}