using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public Transform sun;
    private Transform planet;

    const float G = 0.000667f;
    Vector2 starPosition = new Vector2(0f, 0f);
    Vector2 planetPosition = new Vector2(20f, 0f);
    Vector2 planetVelocity;
    //float sunX = 0;
    //float sunY = 0;
    //float earthX = 20;
    //float earthY = 0;
    //float vX;
    //float vY;
    //float aY;
    //float aX;
    float M_e = 0.5974f;
    float M_s = 198900f * 100f;
    float angle_radians;
    float initialOrbitalVelocityMultiplier = 1f; //0.5f - 1.2f range is safe

    private void Start() {
        Initialize();
    }

    private void Update() {
        CalculatePositions();
        UpdatePositions();
    }

    private void Initialize() {
        planet = transform;
        planetPosition.x = planet.position.x;
        var trail = planet.gameObject.GetComponent<TrailRenderer>();
        trail.startWidth = planet.localScale.x * 2f;
        trail.time = (planetPosition.x - 12f) * 0.75f;
        planetVelocity = Vector2.up * Mathf.Sqrt((G * (M_s + M_e)) / (starPosition - planetPosition).magnitude) * initialOrbitalVelocityMultiplier;
        Debug.Log("Intial orbital velocity: " + planetVelocity.magnitude + ", " + gameObject.name);

        Time.timeScale = 1f;
    }

    private void UpdatePositions() {
        sun.position = starPosition;
        planet.position = planetPosition;
    }

    private void CalculatePositions() {

        float dx = starPosition.x - planetPosition.x;
        float dy = starPosition.y - planetPosition.y;
        float distanceBetweenBodies = Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));

        angle_radians = Mathf.Atan2(dy, dx);

        float accelerationMagnitude = (G * M_s) / Mathf.Pow(distanceBetweenBodies, 2);
        Vector2 accelerationVector = new Vector2(accelerationMagnitude * Mathf.Cos(angle_radians), accelerationMagnitude * Mathf.Sin(angle_radians));
        planetVelocity += accelerationVector * Time.deltaTime;
        planetPosition += planetVelocity * Time.deltaTime;
    }



    /*
    //This works perfectly:
    public Transform sun;
    public Transform earth;

    const float G = 0.000667f;
    float sunX = 0;
    float sunY = 0;
    float earthX = 20;
    float earthY = 0;
    float vX;
    float vY;
    float aY;
    float aX;
    float M_e = 0.5974f;
    float M_s = 198900f * 50;
    float angle_radians;
    float initialOrbitalVelocityMultiplier = 1f; //0.5f - 1.2f range is safe

    private void Start() {
        Initialize();
    }

    private void Update() {
        CalculatePositions();
        UpdatePositions();
    }

    private void Initialize() {

        vX = 0f;
        vY = Mathf.Sqrt((G * (M_s + M_e)) / new Vector2(sunX - earthX, sunY - earthY).magnitude) * initialOrbitalVelocityMultiplier; //0.6f;
        Debug.Log("Intial orbital velocity: " + vY);

        Time.timeScale = 1f;
    }

    private void UpdatePositions() {
        sun.position = new Vector2(sunX, sunY);
        earth.position = new Vector2(earthX, earthY);
    }

    private void CalculatePositions() {
        float dx, dy;
        float D;
        float A;

        dx = sunX - earthX;
        dy = sunY - earthY;
        D = Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));

        angle_radians = Mathf.Atan2(dy, dx);

        A = (G * M_s) / Mathf.Pow(D, 2);
        aX = A * Mathf.Cos(angle_radians);
        aY = A * Mathf.Sin(angle_radians);
        vX += aX * Time.deltaTime;
        vY += aY * Time.deltaTime;
        earthX += vX * Time.deltaTime;
        earthY += vY * Time.deltaTime;
    }
    */
}
