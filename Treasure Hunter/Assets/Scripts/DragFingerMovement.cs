using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFingerMovement : MonoBehaviour
{
    public Animator animator;

    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float moveSpeed = 10f;

    bool touchStart = false;


    // Start is called before the first frame update
    void Start()
    {
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

            if (GetComponent<CapsuleCollider2D>() == Physics2D.OverlapPoint(touchPosition) && touchStart)
            {
                animator.SetBool("SpeedNew", true);
            }

            touchPosition.z = 0;
            direction = (touchPosition - transform.position);

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

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
                animator.SetBool("SpeedNew", false);
            }
        }
    }
}
