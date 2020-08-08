using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3f;
    public float lifeDuration = 2f;

    private float lifeTimer;
    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //Make the bullet move
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
        //Check if the byllet should be destroyed
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
		{
            gameObject.SetActive(false);
		}
    }
}
