using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentPiece : PlaceableObject
{
    [field: Header("Component Properties")]
    [field: SerializeField] public float Amount { get; private set; }
    public Effect effect { get; protected set; }
}
