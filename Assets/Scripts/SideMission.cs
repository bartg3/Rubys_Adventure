using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMission : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
      RubyController controller = other.GetComponent<RubyController>();

      if (controller != null)
        {
            controller.ChangeRedCogs (1);
	        Destroy (gameObject);
        }

    }     

    // Update is called once per frame
    void Update()
    {
        
    }
}
