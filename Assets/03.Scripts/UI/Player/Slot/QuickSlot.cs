using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class QuickSlot : MonoBehaviour
{
    private IUsable usable;
    [field: SerializeField] private Image icon;
    [field: SerializeField] private TMP_Text keyBinding;
    [field: SerializeField] private TMP_Text quantity;
    [field: SerializeField] private Image cooltimeImg;
    [field: SerializeField] private InputActionReference inputActionReference;
    public int slotID;
    private float coolTime;
    private float fillAmount;

    private void Awake()
    {
        UIManager.Instance.quickSlots[slotID] = this;
        SetSlot(null);
    }

    private void Start()
    {
        keyBinding.text = InputManager.GetBindingName(inputActionReference.action.name);
        inputActionReference.action.Enable();
        inputActionReference.action.started += Use;
    }

    private void FixedUpdate()
    {
        if (fillAmount < 0)
            fillAmount = 0;

        if (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime;
            cooltimeImg.fillAmount = fillAmount / coolTime;
        }
    }

    public void SetSlot(IUsable usable)
    {
        if (this.usable is AttackSkill)
        {
            AttackSkill skill = (AttackSkill)usable;
            skill.ApplyCoolTime -= SetCooltime;
        }

        this.usable = usable;

        if (usable == null)
        {
            SetAlpha(0);
            return;
        }

        if (usable is IStackable)
        {
            IStackable stackable = (IStackable)usable;
            quantity.text = stackable.Get().ToString();
        }

        if (usable is UseItem)
        {
            UseItem useItem = (UseItem)usable;
            useItem.OnCountChanged += SetQuantity;
            icon.sprite = Sprite.Create(useItem.texture, new Rect(0, 0, useItem.texture.width, useItem.texture.height), Vector2.one * 0.5f);

            useItem.ApplyCoolTime += SetCooltime;
        }

        if (usable is AttackSkill)
        {
            AttackSkill skill = (AttackSkill)usable;
            icon.sprite = skill.icon;
            quantity.text = string.Empty;

            skill.ApplyCoolTime += SetCooltime;
        }


        SetAlpha(1);
    }

    private void SetAlpha(float f)
    {
        Color color = icon.color;
        color.a = f;
        icon.color = color;
    }

    public IUsable Get()
    {
        return usable;
    }

    private void SetQuantity(int count)
    {
        quantity.text = count.ToString();
    }

    private void Use(InputAction.CallbackContext context)
    {
        if (usable == null)
            return;

        if (fillAmount != 0)
            return;

        if (usable is IStackable)
        {
            IStackable stackable = (IStackable)usable;

            if (stackable.Get() == 0)
                return;
        }

        if (usable is Skill)
        {
            if (GameManager.Instance.player.IsAttack())
                return;
        }

        usable.Use();
    }

    private void SetCooltime(float f)
    {
        coolTime = f;
        fillAmount = f;
    }

    private void SetCooltime()
    {
        SetCooltime(5f);
    }
}
