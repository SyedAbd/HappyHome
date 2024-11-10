using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToInkFromTutorial : MonoBehaviour
{
    [SerializeField] GameObject scarySpider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scarySpider.active == false)
        {
            GameManager.Instance.ChnageSceneToInkFromTutorial();
        }
    }
   
}
