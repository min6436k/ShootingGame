using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class CoolDownText
{
    public EnumTypes.PlayerSkill Skill;
    public TextMeshProUGUI Text;
}

public class PlayerUI : MonoBehaviour
{
    public Image[] HealthImages;
    public Image[] AddOnImages;

    public Slider FuelSlider;

    public List<CoolDownText> SkillCooldownTexts = new List<CoolDownText>();
    public TextMeshProUGUI NoticeText;
    private Coroutine _noticeCoroutine;

    private void Start()
    {
    }

    private void Update()
    {
        UpdateHealthAndAddOn();
        UpdateSkills();
        UpdateFuel();
    }

    private void UpdateHealthAndAddOn()
    {
        for(int i = 0; i < 3; i++)
        {
            HealthImages[i].gameObject.SetActive(i < GameInstance.Instance.PlayerHP);
        }

        for (int i = 0; i < 2; i++)
        {
            AddOnImages[i].gameObject.SetActive(i < GameInstance.Instance.AddOnCount);
        }
    }

    private void UpdateSkills()
    {
        foreach (CoolDownText i in SkillCooldownTexts)
        {
            i.Text.gameObject.SetActive(GameManager.Instance.PlayerCharacter.Skills[i.Skill].bIsCoolTime);

            i.Text.text = Mathf.RoundToInt(GameManager.Instance.PlayerCharacter.Skills[i.Skill].CurrentCoolTime).ToString();
        }
    }

    private void UpdateFuel()
    {
        FuelSlider.value = GameInstance.Instance.PlayerFuel / 100;
    }

    public void CoolNoticeCoolTime(EnumTypes.PlayerSkill playerSkill)
    {
        if (_noticeCoroutine != null) StopCoroutine(_noticeCoroutine);
        
        _noticeCoroutine = StartCoroutine(NoticeCoolTime(playerSkill));
    }


    IEnumerator NoticeCoolTime(EnumTypes.PlayerSkill playerSkill)
    {
        NoticeText.color = Color.white;

        NoticeText.gameObject.SetActive(true);

        NoticeText.text = playerSkill.ToString() +"Skill is CoolDown";

        yield return new WaitForSeconds(1);

        float fadeTime = 0;
        while (fadeTime < 0.4f)
        {
            NoticeText.color = Color.Lerp(Color.white, Color.clear, fadeTime);
            fadeTime += Time.deltaTime * 2.5f;
            yield return null;
        }

        NoticeText.gameObject.SetActive(false);

    }
}
