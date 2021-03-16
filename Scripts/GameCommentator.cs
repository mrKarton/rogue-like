using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCommentator : MonoBehaviour
{
    public Text goodSign;
    public Text badSign;
    public Text neutralSign;
    public Text statSign;

    public float speed;
    public float time;
    public float statTime;

    private Coroutine statCoroutine;

    public void SayGood(string text)
    {
        goodSign.text = text;
        StartCoroutine(Show(goodSign));
    }

    public void SayBad(string text)
    {
        badSign.text = text;
        StartCoroutine(Show(badSign));
    }

    public void Say(string text)
    {
        neutralSign.text = text;
        StartCoroutine(Show(neutralSign));
    }

    private void Start()
    {
        neutralSign.color -= new Color(0,0,0,1);
        badSign.color -= new Color(0, 0, 0, 1);
        goodSign.color -= new Color(0, 0, 0, 1);
        statSign.color -= new Color(0, 0, 0, 1);
        Say("FIGHT!");
    }

    public IEnumerator Show(Text sign)
    {
        Coroutine addition = StartCoroutine(SetAlphaFor(sign, 1));
        yield return new WaitForSeconds(time);
        StopCoroutine(addition);
        StartCoroutine(SetAlphaFor(sign, 0));
    }

    public IEnumerator SetAlphaFor(Text sign, float alpha)
    {
        while (sign.color.a != alpha)
        {
            Color reqColor = sign.color;
            reqColor.a = alpha;
            sign.color = Color.Lerp(sign.color, reqColor, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
    }

    public void SayStat(string text)
    {
        statSign.text += text + "\n";
        if(statCoroutine != null)
        {
            StopCoroutine(statCoroutine);
        }
        statCoroutine = StartCoroutine(Show(statSign));

    }

    public IEnumerator ShowStat()
    {
        StartCoroutine(Show(statSign));
        yield return new WaitForSeconds(statTime);
        statSign.text = "";
    }
}
