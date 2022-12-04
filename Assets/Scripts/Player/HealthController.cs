using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;

    [SerializeField]
    private GameObject healthSpriteObject;
    [SerializeField]
    private Vector3 healthOffset;
    private Transform canvasTransform;
    private List<GameObject> healthObjectsList = new List<GameObject>();

    //audio
    public AudioSource src;
    public AudioClip damage, death;

    public int Health
    {
        get { return _health; }
        private set
        {
            _health = value;
            UpdateHealth();
            if (_health <= 0)
            {
                Death();
            }
        }
    }

    void Start()
    {
        //audio
        src = gameObject.GetComponent<AudioSource>();

        canvasTransform = GameObject.Find("Canvas").transform;
        UpdateHealth();
    }

    public void Damage(int health)
    {
        //audio
        src.clip = damage;
        src.Play();

        Health -= health;
    }

    public void Heal(int health)
    {
        Health += health;
    }

    private void UpdateHealth()
    {
        foreach (GameObject obj in healthObjectsList)
        {
            Destroy(obj);
        }
        healthObjectsList.Clear();

        for (int i = 0; i < Health; i++)
        {
            Debug.Log(healthSpriteObject);
            GameObject obj = Instantiate(healthSpriteObject.gameObject, healthSpriteObject.transform.position + (healthOffset * i), Quaternion.identity, canvasTransform);
            obj.SetActive(true);
            healthObjectsList.Add(obj);
        }
    }

    private void Death()
    {
        //audio
        src.clip = death;
        src.Play();

        Debug.Log("Death");
    }

}
