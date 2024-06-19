using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMove : MonoBehaviour
{
    Animator enemyAnimator;
    Rigidbody2D thisRB;

    public bool NearPlayer;

    EnemyHealth EH;

    public bool rangedEnemy;

    public bool inTheAir;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = this.GetComponent<Animator>();
        thisRB = this.GetComponent<Rigidbody2D>();
        EH = this.GetComponent<EnemyHealth>();
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.right * EH.turnRay, Color.red);
        Debug.DrawRay(transform.position, -Vector2.right * EH.turnRay, Color.green);

        RaycastHit2D forwardHit = Physics2D.Raycast(transform.position, Vector2.right, EH.turnRay, LayerMask.GetMask("PlayerLayer"));

        if(forwardHit.collider != null)
        {
            if(forwardHit.transform.tag == "Player" && forwardHit.distance <= EH.turnRay)
            {
                this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                enemyAnimator.SetBool("Walking", false);
                enemyAnimator.SetBool("Idle", true);
            }
            if(forwardHit.transform.tag == "Player" && forwardHit.distance <= (EH.turnRay - 5f) && NearPlayer == false)
            {
                thisRB.AddForce(transform.right * (75f * EH.enemySpeed), ForceMode2D.Force);
                enemyAnimator.SetBool("Walking", true);
                enemyAnimator.SetBool("Idle", false);
            }
        }

        RaycastHit2D backwardsHit = Physics2D.Raycast(transform.position, -Vector2.right, EH.turnRay, LayerMask.GetMask("PlayerLayer"));

        if(backwardsHit.collider != null)
        {
            if(backwardsHit.transform.tag == "Player" && backwardsHit.distance <= EH.turnRay)
            {
                this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                enemyAnimator.SetBool("Walking", false);
                enemyAnimator.SetBool("Idle", true);
            }
            if(backwardsHit.transform.tag == "Player" && backwardsHit.distance <= (EH.turnRay - 5f) && NearPlayer == false)
            {
                thisRB.AddForce(transform.right * (75f * EH.enemySpeed), ForceMode2D.Force);
                enemyAnimator.SetBool("Walking", true);
                enemyAnimator.SetBool("Idle", false);
            }
        }


        if(backwardsHit.collider == null && forwardHit.collider == null)
        {
            enemyAnimator.SetBool("Walking", false);
            enemyAnimator.SetBool("Idle", true);
        }

        if(inTheAir == true)
        {
            thisRB.AddForce(transform.up * -100f, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemyAnimator.SetBool("Attack", true);
            enemyAnimator.SetBool("Walking", false);
            enemyAnimator.SetBool("Idle", false);
            NearPlayer = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemyAnimator.SetBool("Attack", true);
            enemyAnimator.SetBool("Walking", false);
            enemyAnimator.SetBool("Idle", false);
            NearPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemyAnimator.SetBool("Attack", false);
            enemyAnimator.SetBool("Idle", true);
            NearPlayer = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            inTheAir = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            inTheAir = false;
        }
    }
}
