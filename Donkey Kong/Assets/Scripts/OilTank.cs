using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTank : MonoBehaviour
{
    public float delay = 1;
    private float dTimer = 0;
    public bool burning = false;
    public float bInterval = 3;
    public Fireball fireballPrefab;
    public float bTimer = 0;
    public GameObject spawnPoint;
    private Animator animator;
    private bool extra = false;
    // Start is called before the first frame update
    void Start()
    {
        dTimer = delay;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dTimer -= Time.deltaTime;
        if (dTimer <=0 && !burning) {
            Burn();
            SpawnFire();
        }
        if (burning)
        {
            bTimer -= Time.deltaTime;
            if (bTimer <= 0)
            {
                bTimer = bInterval;

            }
        }
    }
    void Burn()
    {
        burning = true;
        bTimer = bInterval;
        animator.SetBool("Burning", true);
    }
    void SpawnFire() {
       Fireball fire = Instantiate<Fireball>(fireballPrefab);
       fire.transform.position = spawnPoint.transform.position;
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.gameObject.tag == "Barrel" && !extra) {
            Destroy(col.gameObject);
            SpawnFire();
            extra = true;
        }
    }
}
