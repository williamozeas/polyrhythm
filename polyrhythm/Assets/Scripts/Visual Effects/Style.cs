using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StyleColor { Left, Right, Bg, Fill, Extra }

public class Style
{
    public Color leftColor;
    public Color rightColor;
    public Color bgColor;
    public Color fillColor;
    public Color extraColor;

    
    public void OnHitLeft(int intensity) {
        OnHit(intensity);
    }
    public void OnHitRight(int intensity) {
        OnHit(intensity);
    }
    public void OnHit(int intensity) {

    }

    public Style() {
        ColorUtility.TryParseHtmlString("#FF008A", out rightColor);
        ColorUtility.TryParseHtmlString("#00FAFF", out leftColor);
        bgColor = Color.black;
        fillColor = Color.white;
        extraColor = fillColor;
    }

    public Style(Color left, Color right, Color bg, Color fill) {
        leftColor = left;
        rightColor = right;
        bgColor = bg;
        fillColor = fill;
        extraColor = fill;
    }

    public Style(Color left, Color right, Color bg, Color fill, Color extra) {
        leftColor = left;
        rightColor = right;
        bgColor = bg;
        fillColor = fill;
        extraColor = extra;
    }

    public Style(string left, string right, string bg, string fill) {
        ColorUtility.TryParseHtmlString(left, out leftColor);
        ColorUtility.TryParseHtmlString(right, out rightColor);
        ColorUtility.TryParseHtmlString(bg, out bgColor);
        ColorUtility.TryParseHtmlString(fill, out fillColor);
        extraColor = fillColor;
    }

    public Style(string left, string right, string bg, string fill, string extra) {
        ColorUtility.TryParseHtmlString(left, out leftColor);
        ColorUtility.TryParseHtmlString(right, out rightColor);
        ColorUtility.TryParseHtmlString(bg, out bgColor);
        ColorUtility.TryParseHtmlString(fill, out fillColor);
        ColorUtility.TryParseHtmlString(extra, out extraColor);

    }
}
