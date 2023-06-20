using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IWeaponable weaponable = null;
        if(other.gameObject.TryGetComponent(out weaponable))
        {
            weaponable.GetWeapon();
            Destroy(this.gameObject);
        }
    }
}
