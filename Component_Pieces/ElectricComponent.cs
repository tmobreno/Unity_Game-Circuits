using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectricComponent : ComponentPiece
{
    private void Start()
    {
        effect = this.AddComponent<EffectChain>();
    }
}
