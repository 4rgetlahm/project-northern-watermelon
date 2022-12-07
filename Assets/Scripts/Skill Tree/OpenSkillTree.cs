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

    public bool isDead = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isDead)
        {
            if (SkillTreeMeniu.activeSelf)
            {
                SkillTreeMeniu.SetActive(false);
                livesUI.SetActive(true);
                Time.timeScale = 1.0f;
            }
            else
            {
                SkillTreeMeniu.SetActive(true);
                livesUI.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }
}
