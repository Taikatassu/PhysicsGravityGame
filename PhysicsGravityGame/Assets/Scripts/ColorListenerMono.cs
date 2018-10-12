using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ColorListenerMono : MonoBehaviour, IEventListener, IColorListener {

    public bool controlSpriteColor;
    public bool controlTrailColor;

    private SpriteRenderer sr;
    private TrailRenderer trail;

    public void RegisterListeners(IEntity entity) {
        var gameEntity = (GameEntity)entity;
        if(gameEntity != null) gameEntity.AddColorListener(this);
    }

    public void OnColor(GameEntity entity, Color value) {
        if(controlSpriteColor && ValidateSpriteRendererReference()) sr.color = value;

        if(controlTrailColor && ValidateTrailRendererReference()) trail.material.color = value;
    }

    private bool ValidateSpriteRendererReference() {
        if(sr == null) sr = gameObject.GetComponentInChildren<SpriteRenderer>();

        return sr != null;
    }

    private bool ValidateTrailRendererReference() {
        if(trail == null) trail = gameObject.GetComponentInChildren<TrailRenderer>();

        return trail != null;
    }
}
