using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonuseMultiplication : Scaler
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out AIUnit aIUnit))
        {
            _boxCollider.enabled = false;
            int count = _pool.childCount * (_scale - 1);
            StartCoroutine(CreateUnits(count,collision.gameObject));
            _activeSwitcher.DisableGroupBounce();
        }
    }
}
