// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;


/// <summary>The parameters that control a <see cref="Character"/>.</summary>
/// 

[Serializable]
public class CharacterParameters
{
    /************************************************************************************************************************/

    [SerializeField]
    private Vector3 _MovementDirection;
    public Vector3 MovementDirection
    {
        get => _MovementDirection;
        set => _MovementDirection = Vector3.ClampMagnitude(value, 1);
    }

    /************************************************************************************************************************/

    public float ForwardSpeed { get; set; }
    public float DesiredForwardSpeed { get; set; }
    public float VerticalSpeed { get; set; }

    /************************************************************************************************************************/
}

