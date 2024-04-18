using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Box_Script : MonoBehaviour
{
	public Rigidbody2D box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
	{
        //if Falling_Box touches Border_Box
        //{
            //teleport Falling_Box to top with random x value
        //}
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border_Box"))
        {
            transform.position = new Vector2(Random.Range(-2.75f,2.75f), Random.Range(5f,15f));
            box.drag = Random.Range(5f, 10f);
        }
    }
}
