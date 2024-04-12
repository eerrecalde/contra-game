using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
  public float speed = 5.0f;
  public float jumpForce = 5.0f;
  private bool isJumping = false;
  private bool isCrouching = false;
  private float crouchAmount = 0.5f;
  private Rigidbody2D rb;
  private BoxCollider2D bc;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    bc = GetComponent<BoxCollider2D>();
  }

  void Update()
  {
    float moveHorizontal = Input.GetAxis("Horizontal");
    Vector3 movement = new Vector3(moveHorizontal, 0, 0);
    transform.position += movement * speed * Time.deltaTime;

    if (moveHorizontal > 0)
    {
      transform.localScale = new Vector3(1, 1, 1);
    }
    else if (moveHorizontal < 0)
    {
      transform.localScale = new Vector3(-1, 1, 1);
    }

    if (Input.GetKeyDown(KeyCode.W) && !isJumping)
    {
      Debug.Log("Jumping started");
      rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
      isJumping = true;
    }

    if (Input.GetKey(KeyCode.S))
    {
      if (!isCrouching)
      {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        bc.size = new Vector2(bc.size.x, bc.size.y / 2);
        bc.offset = new Vector2(bc.offset.x, bc.offset.y / 2);
        isCrouching = true;
      }
    }
    else if (isCrouching && transform.localScale.y < 1)
    {
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
      bc.size = new Vector2(bc.size.x, bc.size.y * 2);
      bc.offset = new Vector2(bc.offset.x, bc.offset.y * 2);
      isCrouching = false;
    }
  }
  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Square"))
    {
      Debug.Log("Jumping ended");
      isJumping = false;
    }
  }
}