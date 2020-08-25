using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlGenerator : MonoBehaviour
{
    public GameObject Grass;

    public GameObject GrassUpRamp;

    public GameObject GrassDownRamp;

    public GameObject player;

    public GameObject Background;

    public GameObject Spike;

    public GameObject Mace;

    public GameObject Particle;

    public GameObject floorIsLava;

    private float lastX; //Last x coordinate of Grass

    private float lastY; //Last y coordinate of Grass

    private float lastBG; //Last x coordinate of Background

    private float lastFloor;

    private float enemyLastX;

    private float variationPlatform;

    // Start is called before the first frame update
    void Start()
    {
        Regenerate();
    }

    // Update is called once per frame
    void Update()
    {
        //For regular Grass:
        // To line them up, next one needs to be 0.95 to the right. Changing levels abrubtly should usually be 2 up/down.
        //For Dirt:
        // Should be same x as grass above, -0.9 y to one above, and at z = -1
        //For GrassUp 2 and GrassDown 2:
        // To line them up, next one needs to be 0.8 to the right and 0.8 up/down
        //For GrassUp 2 to Grass:
        // Grass should be 0.8 to the right, and -1.1 down
        //For Background:
        // X should be +23, same Y, same Z
        //For Spike_Up:
        // +0.6 Y, same X
        //For Mace:
        // +0.9 Y, same X

        if (player.transform.position.x <= 50)
        {
            variationPlatform = 7;
        }
        else if (player.transform.position.x >= 50 && player.transform.position.x <= 150)
        {
            variationPlatform = 8;
        }
        else
        {
            variationPlatform = 10;
        }

        if (player.transform.position.x >= lastBG - 12)
        {
            Instantiate(Background, new Vector3(lastBG + 23f, 3f, 5), gameObject.transform.rotation);
            lastBG += 23f;
        }
        if (player.transform.position.x >= lastX - 18) //If the camera is about to move passed the last generated block
        {
            int randomDir = Random.Range(1, 9); //inclusive, exclusive
            int randomPlatformNumber = Random.Range(6, 16); 

            if (randomDir == 1 || randomDir == 2 && lastY + 1 < variationPlatform) //Up platform, no ramp
            {
                for (int i = 0; i < randomPlatformNumber; i++)
                {
                    Instantiate(Grass, new Vector2(lastX + 0.95f, lastY + 2), gameObject.transform.rotation);
                    lastX += 0.95f;
                }
                lastY += 2;
            }
            else if (randomDir == 3 || randomDir == 4 && lastY - 3 > 0) //Down platform, no ramp
            {
                for (int i = 0; i < randomPlatformNumber; i++)
                {
                    Instantiate(Grass, new Vector2(lastX + 0.95f, lastY - 2), gameObject.transform.rotation);
                    lastX += 0.95f;
                }
                lastY -= 2;
            }
            else if (randomDir == 5 && lastY + 1 < variationPlatform) //Up platform, with ramp
            {
                Instantiate(GrassUpRamp, new Vector2(lastX + 2.7f, lastY + 0.8f), gameObject.transform.rotation);
                lastX += 2.7f;
                lastY += 0.8f;
                for (int i = 0; i < randomPlatformNumber; i++)
                {
                    Instantiate(Grass, new Vector2(lastX + 0.95f, lastY), gameObject.transform.rotation);
                    lastX += 0.95f;
                }
            }
            else if (randomDir == 6 && lastY - 3 > 0) //Down platform, with ramp
            {
                Instantiate(GrassDownRamp, new Vector2(lastX + 0.95f, lastY), gameObject.transform.rotation);
                lastX += 2.7f;
                lastY -= 0.8f;
                for (int i = 0; i < randomPlatformNumber; i++)
                {
                    Instantiate(Grass, new Vector2(lastX + 0.95f, lastY), gameObject.transform.rotation);
                    lastX += 0.95f;
                }
            }
            else if (randomDir == 7 && lastY + 1 < variationPlatform) //1.5x up platform
            {
                for (int i = 0; i < randomPlatformNumber; i++)
                {
                    Instantiate(Grass, new Vector2(lastX + 0.95f, lastY + 3), gameObject.transform.rotation);
                    lastX += 0.95f;
                }
                lastY += 3;
            }
            else if (randomDir == 8 && lastY - 3 > 0) //1.5x down platform
            {
                for (int i = 0; i < randomPlatformNumber; i++)
                {
                    Instantiate(Grass, new Vector2(lastX + 0.95f, lastY - 3), gameObject.transform.rotation);
                    lastX += 0.95f;
                }
                lastY -= 3;
            }


            enemyLastX = lastX - (randomPlatformNumber * 0.95f); //Makes enemyLastX the first platform in each segment

            if (player.transform.position.x > 10 && player.transform.position.x <= 150)
            {
                int ifEnemy = Random.Range(0, 10);
                SpawnEnemy(ifEnemy, randomPlatformNumber);
            }
            if (player.transform.position.x > 150 && player.transform.position.x <= 300)
            {
                int ifEnemy = Random.Range(0, 8);
                SpawnEnemy(ifEnemy, randomPlatformNumber);
            }
            if (player.transform.position.x > 300)
            {
                int ifEnemy = Random.Range(0, 7);
                SpawnEnemy(ifEnemy, randomPlatformNumber);
            }
        }

        if (player.transform.position.x >= lastFloor + 40)
        {
            Instantiate(floorIsLava, new Vector3(lastFloor + 100, -7, 0), gameObject.transform.rotation);
            lastFloor += 100;
        }

    }

    public void Regenerate() //Initial Sequence
    {
        Instantiate(Background, new Vector3(2.65f, 3f, 5), gameObject.transform.rotation);
        Instantiate(Background, new Vector3(25.65f, 3f, 5), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(-6.45f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(-5.5f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(-4.55f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(-3.6f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(-2.65f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(-1.7f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(-0.75f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(0.2f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(1.15f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(2.1f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(3.05f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(4.0f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(4.95f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(5.9f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(6.85f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(7.8f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(8.75f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(9.7f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(10.65f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(11.6f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(12.55f, -0.2f), gameObject.transform.rotation);
        Instantiate(Grass, new Vector2(13.5f, -0.2f), gameObject.transform.rotation);
        lastX = 13.5f;
        lastY = -0.2f;
        lastBG = 25.65f;
        lastFloor = 0;
        enemyLastX = 13.5f;
    }

    public void SpawnEnemy(int ifEnemy, int randomPlatformNumber)
    {
        if (ifEnemy == 0)
        {
            int numSpike = Random.Range(1, 6);
            if (numSpike < randomPlatformNumber - 1)
            {
                for (int i = 0; i < numSpike; i++)
                {
                    Instantiate(Spike, new Vector3(enemyLastX + 0.95f, lastY + 0.6f, -1), gameObject.transform.rotation);
                    enemyLastX += 0.95f;
                }
            }
        }
        if (ifEnemy == 1 && randomPlatformNumber > 5)
        {
            float macePos = Random.Range(5, randomPlatformNumber);
            macePos *= 0.95f;
            Instantiate(Mace, new Vector3(enemyLastX + macePos, lastY + 0.9f, -1), gameObject.transform.rotation);
        }
        if (ifEnemy == 2 || ifEnemy == 3) 
        {
            float randParticle = Random.Range(4, randomPlatformNumber);
            randParticle *= 0.95f;
            Instantiate(Particle, new Vector2(enemyLastX + randParticle, lastY + 1), gameObject.transform.rotation);
        }
    }
}
