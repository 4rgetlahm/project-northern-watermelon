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

    public GameObject endScreen;

    private BoxCollider2D collider;
    public GameObject blood;
    public GameObject sprite;
    private MovementController script;
    private Rigidbody2D body;

    public OpenSkillTree skillTree;

    public bool canDie = true;

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
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        script = GetComponent<MovementController>();
        //audio
        src = gameObject.GetComponent<AudioSource>();

        canvasTransform = GameObject.Find("HealthUI").transform;
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
        if (!canDie)
            return;



        //audio
        src.clip = death;
        src.Play();

        //Debug.Log("Death");

        Instantiate(blood, transform.position, Quaternion.identity);
        body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        collider.enabled = false;
        script.enabled = false;
        sprite.SetActive(false);
        endScreen.SetActive(true);

        skillTree.isDead = true;
    }

}
