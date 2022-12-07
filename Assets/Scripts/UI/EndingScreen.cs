using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public Animator fade;

    public float wait = 1f;

    public EraseAchievements script;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSceneWait());
    }

    public IEnumerator ChangeSceneWait()
    {
        yield return new WaitForSeconds(wait);
        fade.SetTrigger("FadeIn");
        script.Erase();

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
