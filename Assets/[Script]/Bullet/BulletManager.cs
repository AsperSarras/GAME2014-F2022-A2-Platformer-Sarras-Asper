using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletManager
{
    /*********************** SINGLETON SECTION ***************************/

    // Step 1. Private Static Instance
    private static BulletManager instance;

    // Step 2. Make the Constructor Function Private
    // Constructor
    private BulletManager()
    {
        Initialize();
    }

    // Step 3. Public Static Creational Method -- Instance (Gateway to the class)
    public static BulletManager Instance()
    {
        return instance ??= new BulletManager();
    }

    /*********************************************************************/

    private Queue<GameObject> bulletPool;
    private int bulletNumber;
    private GameObject bulletPrefab;
    private Transform bulletParent;
    GameObject bullet;

    private void Initialize()
    {
        Debug.Log("Initializing");
        bulletNumber = 30;
        bulletPool = new Queue<GameObject>();
        bulletPrefab = Resources.Load<GameObject>("Prefab/Bullet");
    }

    public void BuildBulletPool()
    {
        Debug.Log("Building Pool");
        bulletParent = GameObject.Find("[BULLETS]").transform;
        for (int i = 0; i < bulletNumber; i++)
        {
            bulletPool.Enqueue(CreateBullet());
        }
    }

    private GameObject CreateBullet()
    {
        var tempBullet = MonoBehaviour.Instantiate(bulletPrefab, bulletParent);
        tempBullet.SetActive(false);
        return tempBullet;
    }

    public GameObject GetBullet(Vector2 position)
    {
        Debug.Log("Check1");
        bullet = null;

        if (bulletPool.Count < 0)
        {
            bulletPool.Enqueue(CreateBullet());
        }
        Debug.Log("Check2");
        bullet = bulletPool.Dequeue();
        Debug.Log("Check3");
        bullet.SetActive(true);
        Debug.Log("Check4");
        bullet.transform.position = position;
        Debug.Log("Check5");
        bullet.GetComponent<BulletController>().Activate();
        Debug.Log("Check6");
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        var bulletController = bullet.GetComponent<BulletController>();
        bulletController.ResetAllPhysics();
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    public void DestroyPool()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            var tempBullet = bulletPool.Dequeue();
            MonoBehaviour.Destroy(tempBullet);
        }
        bulletPool.Clear();
    }

}
