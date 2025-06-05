
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
{
    private UI ui;

    [SerializeField] private int skillCost;
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color  lockedSkillColor;
    
    private Image skillImage;

    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;


    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
        
    }

    private void Start()
    {
        ui = GetComponentInParent<UI>();

        skillImage = GetComponentInChildren<Image>();

        skillImage.color = lockedSkillColor;

        if(unlocked)
        {
            skillImage.color = Color.white;  
        }


    }
    public void UnlockSkillSlot()
    {
        if (PlayerManager.instance.HaveEnoughExp(skillCost) == false)
            return;


        for (int i = 0; i < shouldBeUnlocked.Length; i++)
            {
               if (shouldBeUnlocked[i].unlocked == false)
                {
                   Debug.Log("Cannot unlock skill");
                   return;
                }
            }

        for (int i = 0; i < shouldBeLocked.Length; i++)
            {
                if (shouldBeLocked[i].unlocked == true)
                    {
                        Debug.Log("Cannot unlock skill");
                        return;
                    }
            }

        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (ui == null || ui.skillToolTip == null)
        {
            Debug.LogError("UI or skillToolTip is null");
            return;
        }
        ui.skillToolTip.ShowToolTip(skillDescription, skillName, skillCost);

       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ui == null || ui.skillToolTip == null)
        {
            return;
        }
        ui.skillToolTip.HideToolTip();
       
    }

    public void LoadData(GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;

       }

       //skillImage.color = unlocked ? Color.white : lockedSkillColor;


    }

    //public void LoadData(GameData _data)
    //{
    //    if (_data.skillTree == null)
    //    {
    //        Debug.LogError("skillTree is null in GameData!");
    //        return;
    //    }

    //    if (_data.skillTree.TryGetValue(skillName, out bool value))
    //    {
    //        unlocked = value;
    //    }

    //    skillImage.color = unlocked ? Color.white : lockedSkillColor;
    //}

    public void SaveData(ref GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }
        else
            _data.skillTree.Add(skillName, unlocked);
    }
}

