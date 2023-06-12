using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnTest : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(LoadingScean.SceanName);
        StopAllCoroutines();
        StartCoroutine(WaitTitleLoad());
    }
    IEnumerator WaitTitleLoad() { 
        yield return new WaitForSeconds(2.0f);
        LoadingScean.LoadScean(EnumList.SceanName.Title);
    }
}
