using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Enemy : MonoBehaviour,IEnemyBehavior
{

    private GameObject target;
    private Experience expValue;
    private Barricade barricade;
    

    [SerializeField]
    protected float health = 200f;  
    protected float startArmor;
    protected float startHealth;
    protected float aivelocity;
    protected Animator EnemyAnim;
    protected float constVelocity;

    public float armor = 5;    
    public float EnemyDamage;
    public int EnemyExp;
    public Collider2D hitBoxBody;   
    public Rigidbody2D rb;   
    public Image healthBar;   
    public GameObject damagePopUp;

    protected void  Start()
    {
       
        startArmor = armor;
        startHealth = health;

        SetVelocity();
        constVelocity = aivelocity;
        expValue = GameObject.FindObjectOfType<Experience>();
        barricade = GameObject.FindObjectOfType<Barricade>();
        rb = GetComponent<Rigidbody2D>();
        EnemyAnim = GetComponent<Animator>();
        
        target = GameObject.FindGameObjectWithTag("Barricade");
        
    }
    public virtual void SetVelocity()
    {
        aivelocity = Random.Range(0.4f, 1.2f);
        
    }
    
    protected float total;

    public virtual void PushHit()
    {
        rb.AddForce(transform.right * 350);
    }
    void ShowDamagePopUp()
    {
        Instantiate(damagePopUp, transform.position, Quaternion.identity, transform);
        damagePopUp.GetComponent<TextMesh>().text = ((int)total).ToString();
    }
    public void ReduceHealth(float damage)
    {
        total = damage - armor;
        PushHit();

        //Damage Pop up is assigned
        if (damagePopUp && health >0)
        {
            ShowDamagePopUp();
        }       
        health = health - total;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Dead();         
        }
       
    }
    
    public virtual void Dead()
    {
        rb.gravityScale = 0f;
        EnemyAnim.SetBool("IsDead", true);      
        aivelocity = 0f;

        Destroy(this.gameObject, 2.5f);

    }
    //When dead animation is end, event will triggered by end of the animation and method will work.
    public void GainExp()
    {
        expValue.GainExp(EnemyExp);
    }
    private float damagePerSec = 1.4f;
    
    protected void Update()
    {
        Movement();     
    }
  
    public virtual void Movement()
    {
        float distance = Vector2.Distance(target.transform.position, transform.position);

        //Ai movement

        //If weapon's push force cause ai's velocity to zero, again ai velocity set first velocity.
        if (aivelocity <= 0 && health > 0)
        {
            aivelocity = constVelocity;
        }
        //Distance between ai and baricade bigger than 2 unit, ai moving
        if (distance > 2)
        {
            transform.Translate(Vector2.left * Time.deltaTime * aivelocity);
            EnemyAnim.SetBool("IsAttack", false);
            EnemyAnim.SetBool("IsMoving", true);
        }

        //Distance between ai and baricade less than or equal 2 unit, ai entering the barricade hitbox and and damage per second the barricade.
        else if (distance <= 2)
        {
            
            damagePerSec -= Time.smoothDeltaTime;
            transform.Translate(Vector2.zero);
            EnemyAnim.SetBool("IsMoving", false);
            EnemyAnim.SetBool("IsAttack", true);
            if (damagePerSec <= 0)
            {
                barricade.TakenDamage(EnemyDamage);
                damagePerSec = 1.4f;
            }

        }
    }


}
