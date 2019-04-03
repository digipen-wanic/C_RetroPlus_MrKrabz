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
        }
        if (burning)
        {
            bTimer -= Time.deltaTime;
            if (bTimer <= 0)
            {
                bTimer = bInterval;
                Fireball fire = Instantiate<Fireball>(fireballPrefab);
                fire.transform.position = spawnPoint.transform.position;
            }
        }
    }
    void Burn()
    {
        burning = true;
        bTimer = bInterval;
        animator.SetBool("Burning", true);
    }
}
