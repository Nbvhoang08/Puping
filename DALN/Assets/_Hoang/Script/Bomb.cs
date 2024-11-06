using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public GameObject zone;
    public int maxExplosionRadius;
    public LayerMask obstacleLayer; // Layer của các vật cản
    public LayerMask wallLayer;



    private void Awake()
    {
        StartCoroutine(Explore());
    }


    public void ExpandZone()
    {
        Vector2 bombPosition = transform.position;

        int rightRadius = CalculateExplosionRadius(Vector2.right);
        int leftRadius = CalculateExplosionRadius(Vector2.left);
        int upRadius = CalculateExplosionRadius(Vector2.up);
        int downRadius = CalculateExplosionRadius(Vector2.down);
        for (int i = 0; i <= rightRadius; i+=3)
            Instantiate(zone, new Vector2(bombPosition.x + i, bombPosition.y), Quaternion.identity);
        for (int i = 3; i <= leftRadius; i+=3)
            Instantiate(zone, new Vector2(bombPosition.x - i, bombPosition.y), Quaternion.identity);
        for (int i = 3; i <= upRadius; i+=3)
            Instantiate(zone, new Vector2(bombPosition.x, bombPosition.y + i), Quaternion.identity);
        for (int i = 3; i <= downRadius; i+=3)
            Instantiate(zone, new Vector2(bombPosition.x, bombPosition.y - i), Quaternion.identity);


        Debug.Log($"Explosion Radii - Right: {rightRadius}, Left: {leftRadius}, Up: {upRadius}, Down: {downRadius}");
    }

    private int CalculateExplosionRadius(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxExplosionRadius, wallLayer | obstacleLayer);

        if (hit.collider != null)
        {
            if ((1 << hit.collider.gameObject.layer & wallLayer) != 0)
            {
                // Nếu chạm vào tường
                return Mathf.Max(0, Mathf.FloorToInt(hit.distance) - 1);
            }
            else if ((1 << hit.collider.gameObject.layer & obstacleLayer) != 0)
            {
                // Nếu chạm vào obstacle
                return Mathf.FloorToInt(hit.distance);
            }
        }

        // Nếu không chạm vào gì
        return maxExplosionRadius;
    }

    private IEnumerator Explore()
    {
        Debug.Log("Explore Coroutine Started");

        yield return new WaitForSeconds(0.2f);
        ExpandZone();

        yield return new WaitForSeconds(3.0f);

        // Instantiate explosions
        Vector2 bombPosition = transform.position;
        int rightRadius = CalculateExplosionRadius(Vector2.right);
        int leftRadius = CalculateExplosionRadius(Vector2.left);
        int upRadius = CalculateExplosionRadius(Vector2.up);
        int downRadius = CalculateExplosionRadius(Vector2.down);

        for (int i = 0; i <= rightRadius; i+=3)
            Instantiate(explosion, new Vector2(bombPosition.x + i, bombPosition.y), Quaternion.identity);
        for (int i = 3; i <= leftRadius; i+=3)
            Instantiate(explosion, new Vector2(bombPosition.x - i, bombPosition.y), Quaternion.identity);
        for (int i = 3; i <= upRadius; i+=3)
            Instantiate(explosion, new Vector2(bombPosition.x, bombPosition.y + i), Quaternion.identity);
        for (int i = 3; i <= downRadius; i+=3)
            Instantiate(explosion, new Vector2(bombPosition.x, bombPosition.y - i), Quaternion.identity);
        Debug.Log("Explore Coroutine Finished");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);

       
    }

}
