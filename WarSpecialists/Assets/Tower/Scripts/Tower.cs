using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 防御塔控制
/// TODO 防御塔逻辑问题
/// 为防止重复发送请求，只执行己方防御塔逻辑，这个处理问题很大，只适合1v1的情况
/// 当己方有5人时将会执行5次防御塔的逻辑，听谁的这个问题会很麻烦
/// 防御塔逻辑由服务器来做，能很容易解决这个问题，但服务器没有引擎的api，做起来工作量也大，先不做了
/// </summary>
public class TowerCtrl : AIBaseCtrl, IResourceListener
{
    /// <summary>
    /// 攻击点
    /// </summary>
    [SerializeField]
    public Transform m_AttackPos;

    /// <summary>
    /// 是否是己方队伍
    /// </summary>
    public bool m_IsFriend;

    public int Target;

    /// <summary>
    /// 攻击间隔计时
    /// </summary>
    private float m_Timer;

    public void AttackResponse()
    {
        // 生成一个攻击特效
        GameObject go = null;
        if (Team == 1)
            go = Instance.GetObject("TowerBulletOne");
        else
            go = Instance.GetObject("TowerBulletTwo");

    }

    public override void DeathResponse()
    {
        base.DeathResponse();

        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!m_IsFriend)
            return;

        // 寻找目标
        if (Target == null || Target.Model.CurHp <= 0)
        {
            Target = m_Rader.FindEnemy();
            if (Target == null)
                return;
        }

        // 检测攻击距离
        float distance = Vector3.Distance(transform.position, Target.transform.position);
        if (distance > AttackDistance)
        {
            Target = null;
            return;
        }
        // 开始攻击
        m_Timer += Time.deltaTime;
        if (m_Timer >= AttackInterval)
        {
            m_Timer = 0;
 
        }
    }

    public void OnLoaded(string assetName, object asset)
    {
        switch (assetName)
        {

        }
    }
}