using System.Collections;
using UnityEngine;

public class RoadCarAnim : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelStartText;
    public void OnComplete()
    {
        StartCoroutine("TimeEnabler");
    }

    private IEnumerator TimeEnabler()
    {
        yield return new WaitForSeconds(1f);
        _levelStartText.SetActive(true);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}