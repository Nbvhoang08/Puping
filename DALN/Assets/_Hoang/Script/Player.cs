using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BombPrefab;
    public bool CanSet;
    public int maxRadius;
    void Start()
    {
        CanSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        setBomb();
    }
    void setBomb()
    {
        if (BombPrefab != null && Input.GetKeyDown(KeyCode.Space) && CanSet)
        {
            // Tìm quả bom chưa được kích hoạt
            GameObject bomb  =  Instantiate(BombPrefab,transform.position ,Quaternion.identity);
            bomb.GetComponent<Bomb>().maxExplosionRadius = maxRadius ;
            StartCoroutine(cooldown());
        } 
    }
    IEnumerator cooldown()
    {
        // Đợi 3 giây
        yield return new WaitForSeconds(1.0f);

        // Cho phép đặt bom trở lại
        CanSet = true;
    }
}
