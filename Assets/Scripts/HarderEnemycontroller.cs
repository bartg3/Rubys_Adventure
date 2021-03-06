﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarderEnemycontroller : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    Animator animator;

    bool broken = true;

    private RubyController rubyController;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("hello");
        //smokeEffect.Play();
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>(); 
        GameObject rubyControllerObject = GameObject.FindWithTag("RubyController");

        if (rubyControllerObject != null)

        {

            rubyController = rubyControllerObject.GetComponent<RubyController>(); //and this line of code finds the rubyController and then stores it in a variable

            print ("Found the RubyConroller Script!");

        }

        if (rubyController == null)

        {

            print ("Cannot find GameController Script!");

        }
    }
    void Update()
    {
        if(!broken)
        {
            return;

        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!broken)
        {
            return;
        }
        Vector2 position = rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x +Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
           
        }
        
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
         

        if (player != null)
        {
            player.ChangeHealth(-2);
        }
    }

    public void Fix()
    {
        //Debug.Log("fix");
        broken = false;
        
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");

        if(smokeEffect.isPlaying)
        { 
            smokeEffect.Stop();
            smokeEffect.loop = false;
        }

        rubyController.ChangeScore(1);
        //Debug.Log("yes");
    }
    
        
    
}
