using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOT : MonoBehaviour
{
    // Start is called before the first frame update
    public bool notEvent;
    void Awake()
    {
        if (notEvent)
        {
            StartCoroutine(DesSpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private IEnumerator DesSpawn()
    {
        
        yield return new WaitForSeconds(3f);
        DestroySelf();


    }

}
