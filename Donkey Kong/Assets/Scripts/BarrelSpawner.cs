using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public float spawnInterval = 3;
    public float waitInterval = 3;
    public float throwTime = 1;
    public Barrel barrelPrefab;
    public float spawnTimer = 0;
    public float waitTimer = 0;
    public float throwTimer = 0;
    private bool waited = false;
    private bool throwing = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        waitTimer = waitInterval;
    }

    // Update is called once per frame
    void Update()
    {

        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0 && !waited)
        {
            waited = true;
            spawnTimer = spawnInterval;
        }
        if (throwing)
        {
            throwTimer -= Time.deltaTime;
            if (throwTimer <= 0)
            {
                throwing = false;
   
                Barrel barrel = Instantiate<Barrel>(barrelPrefab);
                barrel.transform.position = new Vector2(-1.27f, 1.154f);
            }
        }
        if (waited)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                print("throw");
                spawnTimer = spawnInterval;
                animator.SetTrigger("ThrowBarrel");
                throwing = true;
                throwTimer = throwTime;
            }
        }
    }
}
