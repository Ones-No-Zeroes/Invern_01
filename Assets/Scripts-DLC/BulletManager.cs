using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public GameObject bullet;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("MagicBullet", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            MagicBullet();
        }
    }

    void MagicBullet()
    {
        Instantiate(bullet, new Vector3(54f, 28.465f, 0), bullet.transform.rotation);
    }
}
