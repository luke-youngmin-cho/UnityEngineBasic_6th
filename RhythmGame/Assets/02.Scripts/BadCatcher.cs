using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCatcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameStatus.IncreaseBadCount();
        HitAlertsManager.instance.PopUp(NoteHitter.HitJudge.Bad);
        Destroy(collision.gameObject);
    }
}
