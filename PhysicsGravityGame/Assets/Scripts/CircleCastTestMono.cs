using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCastTestMono : MonoBehaviour {

    private static List<GizmoCircleData> gizmoCircleDatas = new List<GizmoCircleData>();

    private void OnDrawGizmos() {
        foreach(var data in gizmoCircleDatas) {
            Gizmos.DrawWireSphere(data.origin, data.radius);
        }

        gizmoCircleDatas = new List<GizmoCircleData>();
    }

    //private void Update() {
    //    PhysicsService.CircleCastAll(transform.position, 2f, Vector3.left + Vector3.up);
    //}

    public static void AddDebugCirclesToList(Vector3 origin, float radius) {
        gizmoCircleDatas.Add(new GizmoCircleData() { origin = origin, radius = radius });
    }

    private struct GizmoCircleData {
        public Vector3 origin;
        public float radius;
    }
}
