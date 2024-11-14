using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDDialogueByActivatingGO : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject enableObjectOnCollosion;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enableObjectOnCollosion.SetActive(true);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enableObjectOnCollosion.SetActive(true);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
