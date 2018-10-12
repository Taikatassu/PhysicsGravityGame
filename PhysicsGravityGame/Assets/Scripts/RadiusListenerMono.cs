using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RadiusListenerMono : MonoBehaviour, IEventListener, IRadiusListener {

    public bool controlScale;
    public bool controlTrailWidth;
    public float trailWidthMultiplier;

    private Transform t;
    private TrailRenderer trail;

    public void RegisterListeners(IEntity entity) {
        var gameEntity = (GameEntity)entity;
        if(gameEntity != null) gameEntity.AddRadiusListener(this);
    }

    public void OnRadius(GameEntity entity, float value) {
        if(controlScale && ValidateTransformReference()) t.localScale = Vector3.one * value;

        if(controlTrailWidth && ValidateTrailRendererReference()) trail.startWidth = value * trailWidthMultiplier;
    }

    private bool ValidateTransformReference() {
        if(t == null) t = transform;

        return t != null;
    }

    private bool ValidateTrailRendererReference() {
        if(trail == null) trail = gameObject.GetComponentInChildren<TrailRenderer>();

        return trail != null;
    }
}
