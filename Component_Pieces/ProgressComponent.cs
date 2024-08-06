using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressComponent : ComponentPiece
{
    private void Start()
    {
        effect = this.AddComponent<EffectXP>();
        effect.GetComponent<EffectXP>().SetAmount(Amount);
    }
}
