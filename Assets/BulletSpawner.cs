using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public float minRotation;
    public float maxRotation;
    public int numOfBullets;
    public bool isRandom;

    public float cooldown;
    float timer;
    public float bulletSpeed;
    public Vector2 bulletVelocity;

    float[] rotations;

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
        rotations = new float[numOfBullets];
        if (!isRandom)
        {
            DistributedRotations();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            SpawnBullets();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }

    public float[] RandomRotations()
    {
        for (int i = 0; i < numOfBullets; i++)
        {
            rotations[i] = Random.Range(minRotation, maxRotation);
        }
        return rotations;
    }


    public float[] DistributedRotations()
    {
        for (int i = 0; i < numOfBullets; i++)
        {
            var fraction = (float)i / ((float)numOfBullets - 1);
            var difference = maxRotation - minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + minRotation;
        }
        foreach (var r in rotations) print(r);
        return rotations;
    }

    public GameObject[] SpawnBullets()
    {
        if (isRandom)
        {
            RandomRotations();
        }

        GameObject[] spawnedBullets = new GameObject[numOfBullets];
        for (int i = 0; i < numOfBullets; i++)
        {
            spawnedBullets[i] = Instantiate(bullet, transform);
            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = rotations[i];
            b.speed = bulletSpeed;
            b.velocity = bulletVelocity;


        }
        return spawnedBullets;
    }


}
