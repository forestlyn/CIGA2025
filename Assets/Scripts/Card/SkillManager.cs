using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager _instance;
    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new System.Exception("SkillManager is not initialized. Please ensure it is attached to a GameObject in the scene.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of SkillManager detected. Destroying the new instance.");
        }
    }


    public List<GameObject> skills;

    public List<Skill> skillList = new List<Skill>();

    public void ActiveSkillGo(int skillid)
    {
        foreach (var skill in skills)
        {
            if (skill.GetComponent<Skill>().SkillID == skillid)
            {
                skill.gameObject.SetActive(true);
                skillList.Add(skill.GetComponent<Skill>());
                return;
            }
        }
    }

}
