using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public Animator fade;

    public void ChangeScene()
    {
        StartCoroutine(ChangeSceneWait());
    }

    public IEnumerator ChangeSceneWait()
    {
        Debug.Log("huh");
        fade.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
}
