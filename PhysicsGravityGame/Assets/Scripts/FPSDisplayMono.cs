using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplayMono : MonoBehaviour {

    private TMP_Text text;
    private float updateInterval = 0.3f;
    private float lastUpdateTime;
    private List<float> frameTimes;

    void Start() {
        text = GetComponent<TMP_Text>();
        lastUpdateTime = Time.time;
        frameTimes = new List<float>();
    }

    void Update() {
        frameTimes.Add(Time.deltaTime);

        if (Time.time - lastUpdateTime >= updateInterval) {
            lastUpdateTime = Time.time;
            var totalFrameTime = 0f;
            foreach (var ft in frameTimes) {
                totalFrameTime += ft;
            }

            var intervalAverageFPS = 1 / (totalFrameTime / frameTimes.Count);
            text.text = "FPS: " + intervalAverageFPS.ToString("F0");
            frameTimes = new List<float>();
        }
    }
}
