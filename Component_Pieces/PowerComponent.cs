using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerComponent : ComponentPiece
{
    private void Start()
    {
        effect = this.AddComponent<EffectDamage>();
        effect.GetComponent<EffectDamage>().SetAmount(Amount);
    }
}
