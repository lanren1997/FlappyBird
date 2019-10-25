using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBrid;

    public float force=10f;

    public Animator ani_playerr;

    public bool death = false;

    public delegate void  DeathNotify();

    public event DeathNotify OnDeath;

    private Vector3 initPos;

    public UnityAction<int> OnScore;

    // Start is called before the first frame update
    void Start()
    {
        this.ani_playerr = this.GetComponent<Animator>();
        Idle();
        initPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.death)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            rigidbodyBrid.velocity = Vector2.zero;
            rigidbodyBrid.AddForce(new Vector2(0, force),ForceMode2D.Impulse);
        }
    }
    public void Init()
    {
        this.transform.position = initPos;
        this.Idle();
        this.death = false;
    }

    public void Idle()
    {
        this.rigidbodyBrid.Sleep();
        this.rigidbodyBrid.simulated = false;
        this.ani_playerr.SetTrigger("Idle");
    }
    public void Fly()
    {
        this.rigidbodyBrid.WakeUp();
        this.rigidbodyBrid.simulated = true;
        this.ani_playerr.SetTrigger("Fly");
    }

    public void Die()
    {
        this.death = true;
        if (this.OnDeath!=null)
        {
            this.OnDeath();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D:" + collision.gameObject.name + ":" + gameObject.name + ":" + Time.deltaTime);

        if (collision.gameObject.name.Equals("ScoreArea"))
        {
            if (this.OnScore!=null)
            {
                this.OnScore(1);
            }
        }
        else
        {
            this.Die();
        }
        
    }

}
