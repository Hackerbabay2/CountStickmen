using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonuseScaler : Scaler
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out AIUnit aIUnit))
        {
            _boxCollider.enabled = false;
            StartCoroutine(CreateUnits(_scale,collision.gameObject));
            _activeSwitcher.DisableGroupBounce();
        }
    }
}
