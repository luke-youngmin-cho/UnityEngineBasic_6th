using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff<T>
    where T : MonoBehaviour
{
    void OnActive(T target);
    void OnDuration(T target);
    void OnDeactive(T target);
}
