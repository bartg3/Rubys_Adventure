using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int scoreAmount = 0;
    private Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreAmount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
       scoreText.text =  "Score: " + scoreAmount;
    }
}
