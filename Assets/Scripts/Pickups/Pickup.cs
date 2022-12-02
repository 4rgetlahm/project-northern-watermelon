using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickup : MonoBehaviour
{
    [SerializeField]
    private CurrencyType pickupCurrency;
    [SerializeField]
    private int amount = 1;
    [SerializeField]
    public GameObject particle;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger enter");
        if (collision == null)
        {
            return;
        }
        if(collision.gameObject.tag != "Player")
        {
            return;
        }
        TriggerPickup();
    }

    void TriggerPickup()
    {
        switch(pickupCurrency)
        {
            case CurrencyType.RedSouls:
                CurrencyController.RedSouls += amount;
                break;
            case CurrencyType.BlueSouls:
                CurrencyController.BlueSouls += amount;
                break;
            case CurrencyType.YellowSouls:
                CurrencyController.YellowSouls += amount;
                break;
            case CurrencyType.Eyece:
                CurrencyController.Eyece += amount;
                break;
            case CurrencyType.FingerBurn:
                CurrencyController.FingerBurn += amount;
                break;
            default:
                return;
        }

        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
