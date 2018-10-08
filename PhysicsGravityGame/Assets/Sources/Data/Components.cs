using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent {
    public Vector2 value;
}

[Game]
public class VelocityComponent : IComponent {
    public Vector2 value;
}

[Game]
public class MassComponent : IComponent {
    public float value;
}

[Game]
public class ForceComponent : IComponent {
    public Vector2 force;
}

[Meta, Unique]
public class CelestialBodySettingsComponent : IComponent {
    public CelestialBodySettings value;
}