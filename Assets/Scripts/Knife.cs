using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private Vector2 direction;

    private bool isActive = true;

    private Rigidbody2D rb;
    private BoxCollider2D knifeCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            knifeCollider.enabled = true;
            rb.AddForce(direction, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            GameController.Instance.gameUI.KnifeUsed();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wood")
        {
            if (!isActive)
            {
                return;
            }

            isActive = false;

            GameController.Instance.knifes.Add(rb);
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.transform);

            //необходимо отодвинуть от коллайдера дерева, чтобы вращение оставалось прежним;
            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 0.85f);

            Vibration.Vibrate(100); //вибро при попадании в дерево

            GameController.Instance.SuccessSpawn();
        }

        else if (collision.transform.tag == "Knife" && isActive)
        {
            rb.velocity = new Vector2(rb.velocity.x, -3);
            knifeCollider.enabled = false;
            isActive = false;

            Vibration.Vibrate(200); //вибро при попадании в нож

            GameController.Instance.Result(false);
        }
    }
}
