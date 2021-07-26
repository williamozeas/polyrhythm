using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public static MaskManager i;

    public StraightWipeLCM straightWipeLCM;
    public StraightWipeRCM straightWipeRCM;
    public CircleGrowTopR circleGrowTopR;
    public CircleGrowTopL circleGrowTopL;
    public CircleGrowBotR circleGrowBotR;
    public CircleGrowBotL circleGrowBotL;
    public CircleGrowCenter circleGrowCenter;
    public StraightWipeRCM drippyWipeR;
    public StraightWipeLCM drippyWipeL;
    public StraightWipeRCM wavyWipeR;
    public StraightWipeLCM wavyWipeL;
    public HorizontalDoorsCM horizontalDoors;
    public HorizontalDoorsCM verticalDoors;
     
    
    void Awake()
    {
        if(i != null)
            GameObject.Destroy(i);
        else
            i = this;

        DontDestroyOnLoad(this);
    }

    public void Transition(StyleTransition trans, StyleDirection dir, string[] args) {
        switch(trans) {
            case StyleTransition.StraightWipeL: {
                if(dir == StyleDirection.Add) {
                    straightWipeLCM.Add();
                } else {
                    straightWipeLCM.Remove();
                }
                break;
            }
            case StyleTransition.StraightWipeR: {
                if(dir == StyleDirection.Add) {
                    straightWipeRCM.Add();
                } else {
                    straightWipeRCM.Remove();
                }
                break;
            }
            case StyleTransition.CircleGrowTopR: {
                if(dir == StyleDirection.Add) {
                    circleGrowTopR.Add();
                } else {
                    circleGrowTopR.Remove();
                }
                break;
            }
            case StyleTransition.CircleGrowTopL: {
                if(dir == StyleDirection.Add) {
                    circleGrowTopL.Add();
                } else {
                    circleGrowTopL.Remove();
                }
                break;
            }
            case StyleTransition.CircleGrowBotR: {
                if(dir == StyleDirection.Add) {
                    circleGrowBotR.Add();
                } else {
                    circleGrowBotR.Remove();
                }
                break;
            }
            case StyleTransition.CircleGrowBotL: {
                if(dir == StyleDirection.Add) {
                    circleGrowBotL.Add();
                } else {
                    circleGrowBotL.Remove();
                }
                break;
            }
            case StyleTransition.CircleGrowCenter: {
                if(dir == StyleDirection.Add) {
                    circleGrowCenter.Add();
                } else {
                    circleGrowCenter.Remove();
                }
                break;
            }
            case StyleTransition.DrippyWipeL: {
                if(dir == StyleDirection.Add) {
                    drippyWipeL.Add();
                } else {
                    drippyWipeL.Remove();
                }
                break;
            }
            case StyleTransition.DrippyWipeR: {
                if(dir == StyleDirection.Add) {
                    drippyWipeR.Add();
                } else {
                    drippyWipeR.Remove();
                }
                break;
            }
            case StyleTransition.WavyWipeL: {
                if(dir == StyleDirection.Add) {
                    wavyWipeL.Add();
                } else {
                    wavyWipeL.Remove();
                }
                break;
            }
            case StyleTransition.WavyWipeR: {
                if(dir == StyleDirection.Add) {
                    wavyWipeR.Add();
                } else {
                    wavyWipeR.Remove();
                }
                break;
            }
            case StyleTransition.HorizontalDoors: {
                if(dir == StyleDirection.Add) {
                    horizontalDoors.Add();
                } else {
                    horizontalDoors.Remove();
                }
                break;
            }
            case StyleTransition.VerticalDoors: {
                if(dir == StyleDirection.Add) {
                    verticalDoors.Add();
                } else {
                    verticalDoors.Remove();
                }
                break;
            }
        }
    }
}
