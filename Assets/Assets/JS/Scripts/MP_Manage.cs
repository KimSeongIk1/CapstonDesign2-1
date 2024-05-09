using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_Manage : MonoBehaviour
{
    public Sprite[] img;
    Image thisImg;
    public Image[] skill_dummyImg;
    public GameObject playerObj;
    public GameObject skill_dummyObj;
    public int mpPoint = 0;
    public bool skillOn = false;
    public int skillCost = 100;
    private void Start()
    {
        thisImg = GetComponent<Image>();
        skill_dummyObj.GetComponent<Image>();
    }
    private void FixedUpdate()
    {
        if(mpPoint <= 5)
        {
            ChangeMPimg(1);

        }
        else if (mpPoint <= 15)
        {
            ChangeMPimg(2);
        }
        else if(mpPoint <= 20)
        {
            ChangeMPimg(3);
        }
        else if (mpPoint <= 40)
        {
            ChangeMPimg(4);
        }
        else if (mpPoint <= 60)
        {
            ChangeMPimg(5);
        }
        else if (mpPoint <= 80)
        {
            ChangeMPimg(6);
        }
        else if (mpPoint >= 100)
        {
            ChangeMPimg(7);
            skillOn = true;
           // skill_dummyObj
        }
    }
    public int MpValue
    {
        get
        {
            return mpPoint;
        }
        set
        {
            mpPoint = value;
        }
    }
    public void MpCharge()
    {
        Debug.Log("MP ÃæÀü");
        MpValue += 20;
    }
    public int SkillCost
    {
        get
        {
            return mpPoint;
        }
        set
        {
            mpPoint = value;
           // MpValue -= skillCost;
        }
    }
    void ChangeMPimg(int index)
    {
        thisImg.sprite = img[index];
    }
}
