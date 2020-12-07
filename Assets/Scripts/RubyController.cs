using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class RubyController : MonoBehaviour
{
  AudioSource audioSource;

  public float speed = 3.0f;

  public int maxHealth = 5;
  
  public GameObject projectilePrefab;
  public GameObject backgroundMusic;
  public GameObject healPrefab;
  public GameObject hurtPrefab;
  
  public int health {get { return currentHealth; }}
  int currentHealth;

  public float timeInvincible = 2.0f;
  bool isInvincible;
  float invincibleTimer;

  Rigidbody2D rigidbody2d;
  float horizontal;
  float vertical;

  Animator animator;
  Vector2 lookDirection = new Vector2(1,0);

  //music
  public AudioClip throwSound;
  public AudioClip hitSound;
  public AudioClip winSound;
  public AudioClip loseSound;
  public AudioClip pickUpSound;
  public AudioClip jambiSound;
  public AudioSource musicSource;

  public ParticleSystem DamageObject;
  public ParticleSystem HealthUpObject;

  
  private int score = 0;
  public Text ScoreText;
  public Text winLoseText;
  
  
  public bool gameOver = false;


  public int cogs = 4;
  public Text cogsText;
  public int currentCogs;
  public static int level;

  public int redCogs = 0;
  public Text redCogsText;

  

    // Start is called before the first frame update
    void Start()
    {
      rigidbody2d = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      
      currentHealth = maxHealth;
    
      

      audioSource= GetComponent<AudioSource>(); 

      if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene1"))
        {
          level = 1;
        }

      if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene2"))
        {
          level = 2;
        }

        
    }

    public void PlaySound(AudioClip clip)
    {
      audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
      horizontal = Input.GetAxis("Horizontal");
      vertical = Input.GetAxis("Vertical");

      Vector2 move = new Vector2(horizontal, vertical);

      animator.SetTrigger("Movement");

      

      

      

      if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
          lookDirection.Set(move.x, move.y);
          lookDirection.Normalize();
        }
          animator.SetFloat("Look X", lookDirection.x);
          animator.SetFloat("Look Y", lookDirection.y);
          animator.SetFloat("Speed", move.magnitude);
          
         

      if (isInvincible)
        {
          invincibleTimer -= Time.deltaTime;
          if (invincibleTimer < 0)
              isInvincible = false;
        }
        
         
      if (cogs > 0)
      {

        if(Input.GetKeyDown(KeyCode.C))
          {
            Launch();
            cogs = cogs - 1;
            cogsText.text = "Cogs: " + cogs.ToString();

            
          }
      }
      if (Input.GetKeyDown(KeyCode.X))
        {
          RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
          if (hit.collider != null)
          {
            if (score >= 4)
            {
              SceneManager.LoadScene("Scene2");
            }
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
              character.DisplayDialog();
            }
          }
        }
        if (level == 1)
        {
            if (currentHealth == 0)
            {
              PlaySound(loseSound);
              Destroy(backgroundMusic.gameObject);
              winLoseText.text = "You lose! Game by Geovanni Bartlett! Press R to restart!";
              gameOver = true;
              speed = 0.0f;
            if (Input.GetKey(KeyCode.R))
              {
                if (gameOver == true)
                {
                  SceneManager.LoadScene("Scene1");
                }
              }  

            }
            if (score == 4)
            { 
             winLoseText.text = "Go see Jambi!";
            }
        }
        if (level == 2)
        {

          if (currentHealth == 0)
          {
              PlaySound(loseSound);
              Destroy(backgroundMusic.gameObject);
              winLoseText.text = "You lose! Game by Geovanni Bartlett! Press R to restart!";
              speed = 0.0f;
          if (Input.GetKey(KeyCode.R))
              {
                if (gameOver == true)
                {
                  SceneManager.LoadScene("Scene2");
                }
              }
              
          }

          if (score == 4 && redCogs == 4)
          {
              PlaySound(winSound);
              Destroy(backgroundMusic.gameObject);
              winLoseText.text = "You win! Game by Geovanni Bartlett! Press R to restart!";
              speed = 0.0f;
          if (Input.GetKey(KeyCode.R))
              {
                if (gameOver == true)
                {
                  SceneManager.LoadScene("Scene2");
                }
              }

               
          } 
        
        
       if (Input.GetKey("escape"))
        {
          Application.Quit();
        }

        }
    }

  

    void FixedUpdate()
    {
      Vector2 position = rigidbody2d.position;
      position.x = position.x + speed * horizontal * Time.deltaTime;
      position.y = position.y + speed * vertical * Time.deltaTime;

      rigidbody2d.MovePosition(position);
    }

    public void ChangeScore(int scoreAmount)
    {
      score = score + scoreAmount;
      ScoreText.text = "Robots fixed: "+ score.ToString();

    }

    public void ChangeCogs(int cogAmmo)
    {
      cogs = cogs + cogAmmo;
      cogsText.text = "Cogs: " + cogs.ToString();
      GameObject HealthUpObject = Instantiate(healPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

    }

    public void ChangeRedCogs (int cogCount)
    {
      redCogs = redCogs + cogCount;
      redCogsText.text = "Red Cogs: " +redCogs.ToString();
      GameObject HealthUpObject = Instantiate(healPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
    }

    public void ChangeSpeed ()
    {
      speed = 8.0f;
      GameObject HealthUpObject = Instantiate(healPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

    }

    public void ChangeHealth(int amount)
    {
      if (amount < 0)
        {
          animator.SetTrigger("Hit");
          if (isInvincible)
              return;
            
          isInvincible = true;
          invincibleTimer = timeInvincible;
          GameObject DamageObject = Instantiate(hurtPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
          PlaySound(hitSound);
          //Debug.Log("hitt");

        }
      currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

      UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
      
      if (amount > 0)
      {
        GameObject HealthUpObject = Instantiate(healPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
      }

      
      
    }

    void Launch()
    {
      GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
      Projectile projectile = projectileObject.GetComponent<Projectile>();
      projectile.Launch(lookDirection, 300);

      animator.SetTrigger("Launch");

      PlaySound(throwSound);


    }
    
    
}
