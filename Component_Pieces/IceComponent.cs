using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceComponent : ComponentPiece
{
    private void Start()
    {
        effect = this.AddComponent<EffectSlow>();
        effect.GetComponent<EffectSlow>().SetAmount(Amount);
    }
}
