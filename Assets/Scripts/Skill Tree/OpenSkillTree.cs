using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSkillTree : MonoBehaviour
{
    [SerializeField]
    private GameObject SkillTreeMeniu;
    [SerializeField]
    private GameObject livesUI;
    private AudioSource open;
    // Update is called once per frame
    void Update()
    {
        //audio
        open = GetComponent<AudioSource>();
        if (Input.GetKeyDown(KeyCode.E))
        {
            //audio
            if (SkillTreeMeniu.activeSelf)
            {
                SkillTreeMeniu.SetActive(false);
                livesUI.SetActive(true);
                Time.timeScale = 1.0f;
                open.Play();
            }
            else
            {
                
                SkillTreeMeniu.SetActive(true);
                livesUI.SetActive(false);
                Time.timeScale = 0f;
                open.Play();
            }
        }
    }
}
