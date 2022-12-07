using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public Animator fade;


    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fade.SetTrigger("FadeIn");
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
