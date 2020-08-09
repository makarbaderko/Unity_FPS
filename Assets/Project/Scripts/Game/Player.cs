using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera playerCamera;
    [Header("Gameplay")]
    public float knockBackForce = 100f;
    private bool isHurt;
    public float hurtDuration = 0.5f;
    public int initialHealth = 100;
    private int health;
    public int Health { get { return health; } }
    public int initialAmmo = 12;
    private int ammo;
    public int Ammo { get { return ammo; } }
    // Start is called before the first frame update
    void Start()
    {
        ammo = initialAmmo;
        health = initialHealth;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0)
            {
                ammo--;
                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet();
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }
    }

    //Check for collisions
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Collect ammo crates
        if (hit.collider.gameObject.GetComponent<AmmoCrate>() != null) {
            AmmoCrate ammoCrate = hit.collider.gameObject.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;
            Destroy(ammoCrate.gameObject);
        } else if (hit.collider.GetComponent<Enemy>() != null) {
            if (isHurt == false)
            {
                //Touching enemies
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                health -= enemy.damage;
                isHurt = true;
                //Perform knock back
                Vector3 hurtDirection = (transform.position - enemy.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<Rigidbody>().AddForce(knockbackDirection*knockBackForce);
                StartCoroutine(HurtRoutine());
            }
        }
	}

    IEnumerator HurtRoutine()
	{
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
	}
}
