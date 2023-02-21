using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAlertsManager : MonoBehaviour
{
    public static HitAlertsManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private PopUpText _bad;
    [SerializeField] private PopUpText _miss;
    [SerializeField] private PopUpText _good;
    [SerializeField] private PopUpText _great;
    [SerializeField] private PopUpText _cool;
    [SerializeField] private PopUpText _combo;


    public void PopUp(NoteHitter.HitJudge hitJudge)
    {
        if (_bad.gameObject.activeSelf) _bad.transform.Translate(Vector3.forward);
        if (_miss.gameObject.activeSelf) _miss.transform.Translate(Vector3.forward);
        if (_good.gameObject.activeSelf) _good.transform.Translate(Vector3.forward);
        if (_great.gameObject.activeSelf) _great.transform.Translate(Vector3.forward);
        if (_cool.gameObject.activeSelf) _cool.transform.Translate(Vector3.forward);

        switch (hitJudge)
        {
            case NoteHitter.HitJudge.None:
                break;
            case NoteHitter.HitJudge.Bad:
                {
                    _bad.PopUp();
                    _bad.transform.Translate(Vector3.back);
                }
                break;
            case NoteHitter.HitJudge.Miss:
                {
                    _miss.PopUp();
                    _miss.transform.Translate(Vector3.back);
                }
                break;
            case NoteHitter.HitJudge.Good:
                {
                    _good.PopUp();
                    _good.transform.Translate(Vector3.back);
                }
                break;
            case NoteHitter.HitJudge.Great:
                {
                    _great.PopUp();
                    _great.transform.Translate(Vector3.back);
                }
                break;
            case NoteHitter.HitJudge.Cool:
                {
                    _cool.PopUp();
                    _cool.transform.Translate(Vector3.back);
                }
                break;
            default:
                break;
        }

        if (GameStatus.currentCombo > 1)
        {
            _combo.PopUp(GameStatus.currentCombo.ToString());
        }
    }
}
