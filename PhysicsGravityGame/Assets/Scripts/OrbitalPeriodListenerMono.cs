using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class OrbitalPeriodListenerMono : MonoBehaviour, IEventListener, IOrbitalPeriodListener {

    public bool controlTrailTime;

    private TrailRenderer trail;

    public void RegisterListeners(IEntity entity) {
        var gameEntity = (GameEntity)entity;
        if(gameEntity != null) gameEntity.AddOrbitalPeriodListener(this);
    }

    public void OnOrbitalPeriod(GameEntity entity, float value) {
        if(controlTrailTime && ValidateTrailRendererReference()) trail.time = value;
    }

    private bool ValidateTrailRendererReference() {
        if(trail == null) trail = gameObject.GetComponentInChildren<TrailRenderer>();

        return trail != null;
    }
}
