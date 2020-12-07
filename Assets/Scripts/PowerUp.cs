using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip collectedClip;

    // Start is called before the first frame update
    void OnTriggerEnter2D (Collider2D other)
    {

        RubyController controller = other.GetComponent<RubyController>();

        if(controller != null)
        {
            controller.ChangeSpeed();
            controller.PlaySound(collectedClip);
            Debug.Log("test");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
