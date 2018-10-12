using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnterTestMono : MonoBehaviour {
    public bool collisionDetectionEnabled;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!collisionDetectionEnabled) return;

        Debug.Log(gameObject.name + " colliding with " + collision.gameObject.name);
        var effect1 = Instantiate(Resources.Load("Prefabs/CollisionEffectTest") as GameObject);
        effect1.transform.position = collision.transform.position;
        var effect2 = Instantiate(Resources.Load("Prefabs/CollisionEffect2Test") as GameObject);
        effect2.transform.position = transform.position;
    }
}
