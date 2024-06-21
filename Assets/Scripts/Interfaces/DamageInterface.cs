using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageInterface
{
    void Damage(float damage);
    void Damage(float damage, Vector3 dir);
}
