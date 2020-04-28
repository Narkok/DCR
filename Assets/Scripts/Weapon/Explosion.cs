using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion: MonoBehaviour {
    
    public void Setup(Vector3 position, float explosionTime) {
        transform.position = position;
        StartCoroutine(SelfDestruction(explosionTime));
    }


    private IEnumerator SelfDestruction(float explosionTime) {
        yield return new WaitForSeconds(explosionTime);
        Destroy(gameObject);
    }
}
