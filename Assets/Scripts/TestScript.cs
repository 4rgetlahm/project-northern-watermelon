using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TestCoroutine");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(5);
        Dialog.Show("Test", "This is a test", "OK", delegate { Debug.Log("Test"); }, "Cancel", () => { Debug.Log("Button 2 click"); });
    }

    void TestAction()
    {
        Debug.Log("Test Action");
    }
}
