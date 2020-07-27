using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DragFingerMovement : MonoBehaviour
{
    public Animator animator;

    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float moveSpeed = 2f;

    bool touchStart = false;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            touchStart = true;
            //this is where animation run play when touch start
            if (GetComponent<CapsuleCollider2D>() == Physics2D.OverlapPoint(touchPosition) && touchStart)
            {
                animator.SetBool("SpeedNew", true);
            }

            touchPosition.z = 0;
            direction = (touchPosition - transform.position);

            //this is to flip the character from left to righ when draged by finger
            Vector3 characterScale = transform.localScale;
            if (touchPosition.x <= 0)
            {
                characterScale.x = -15;
            }
            if (touchPosition.x >= 0)
            {
                characterScale.x = 15;
            }
            transform.localScale = characterScale;

            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

            //this is the end state of the touch so it will be back to the idle animation
            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
                animator.SetBool("SpeedNew", false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Troll_One"))
        {
            StartCoroutine("ReloadScene");
            Time.timeScale = 0f;
        }
        else if (collision.CompareTag("Box"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Troll_One"));
        }

        if (collision.CompareTag("Troll"))
        {
            StartCoroutine("ReloadScene");
            Time.timeScale = 0f;
        }
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("_Main");
    }
}
