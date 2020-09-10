﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MariaAbility : MonoBehaviour
{
    public Image cooldownImg;
    public float cd1 = 5;
    bool isCooldown = false;
    public KeyCode ability1;

    // Ability 1
    [SerializeField]
    private AnimationClip ability_1;
    Vector3 position;
    public Canvas ability1Canvas;
    public Image skillshot;
    public Image rangeCircle;
    public Transform player;
    private PlayerMovement movement;
    private bool abilityCasting;
    private float _abilityCastingTime;
    private float _abilityCastingTimer;
    private float damageDelayTime = 1f;
    private float damageDelayTimer = 0;
    public float abilityRange = 50;

    public bool AbilityCasting { get => abilityCasting; set => abilityCasting = value; }

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<PlayerMovement>();
        cooldownImg.fillAmount = 0;
        skillshot.GetComponent<Image>().enabled = false;
        rangeCircle.GetComponent<Image>().enabled = false;

        _abilityCastingTime = ability_1.length;
        _abilityCastingTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ability 1
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(-90f, transRot.eulerAngles.y, transRot.eulerAngles.z);
        ability1Canvas.gameObject.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);

    }

    void Ability1()
    {
        if(Input.GetKey(ability1) && isCooldown == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
            rangeCircle.GetComponent<Image>().enabled = true;
        }

        if(skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            isCooldown = true;
            AbilityCasting = true;
            Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
            player.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);
            cooldownImg.fillAmount = 1;
            movement.Agent.speed = 0;
            movement.Agent.acceleration = 0;
        }

        if(isCooldown)
        {
            cooldownImg.fillAmount -= 1 / cd1 * Time.deltaTime;
            skillshot.GetComponent<Image>().enabled = false;
            rangeCircle.GetComponent<Image>().enabled = false;

            if (cooldownImg.fillAmount <= 0)
            {
                cooldownImg.fillAmount = 0;
                isCooldown = false;
            }

            if (AbilityCasting && _abilityCastingTimer <= 0)
            {
                _abilityCastingTimer = _abilityCastingTime;
                damageDelayTimer = 0;
            }

            if (AbilityCasting && _abilityCastingTimer > 0)
            {
                _abilityCastingTimer -= Time.deltaTime;
                damageDelayTimer -= Time.deltaTime;
                if (damageDelayTimer <= 0)
                {
                    GameObject[] enemies;
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");

                    foreach (var enemy in enemies)
                    {
                        Vector3 directionToTarget = transform.position - enemy.transform.position;
                        float angel = Vector3.Angle(transform.forward, directionToTarget);
                        if (Mathf.Abs(angel) > 90 && Mathf.Abs(angel) < 270 && directionToTarget.magnitude <= abilityRange)
                            Debug.Log("Ability1 to enemy!");
                    }

                    damageDelayTimer = damageDelayTime;
                }
            }
        }
    }
}
