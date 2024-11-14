using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToInk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        GameManager.Instance.ChangeSceneToink();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ChangeSceneToink();


        }
    }
}
