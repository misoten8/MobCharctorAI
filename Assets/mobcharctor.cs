using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class mobcharctor : MonoBehaviour
{
    //=========================================
    //構造体
    //=========================================
    public enum Mode
    {
        UROURO,//町をうろうろしている
        FOLLOW,//プレイヤーのファンになって追っかけている
        WAIT,  //静止状態
        EVENT, //イベントによって異な
        END
    };
    //=========================================
    //グローバル変数
    //=========================================
    public Mode m_Mode;
    private Animator animator;
    private bool Active;//updateの有効化on/off
    public Transform player;    //プレイヤーを代入
    public Transform goal0;
    public Transform goal1;
    public Transform goal2;
    public float speed = 1; //移動速度
    public float limitDistance = 0; //敵キャラクターがどの程度近づいてくるか設定(この値以下には近づかない）



    float changetime;   //動きを更新する時刻
    //=========================================
    //関数名:Start
    //戻り値:なし
    //引き数:なし
    //=========================================
    void Start()
    {

        m_Mode = Mode.UROURO;
        Active = true;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        goal0  = GameObject.FindGameObjectWithTag("Goal0").transform;
        goal1  = GameObject.FindGameObjectWithTag("Goal1").transform;
        goal2  = GameObject.FindGameObjectWithTag("Goal2").transform;

        limitDistance = 10f;




    }
    //=========================================
    //関数名:Update
    //戻り値:なし
    //引き数:なし
    //=========================================
    void Update()
    {
        switch (m_Mode)
        {
            case Mode.UROURO:

                if (changetime < Time.time)
                {
                    ////更新時刻が来た
                    //animator.SetBool("isrunning", false);
                    //animator.SetBool("iswalking", true);
                    //int goal = UnityEngine.Random.Range(0, 2);    //ランダムで目的地を変える
                    //GameObject goalobj = GameObject.Find("Goal" + goal);
                    //GameObject obj = GameObject.Find("Mob1");
                    //NavMeshAgent nma = obj.GetComponent<NavMeshAgent>();
                    //nma.SetDestination(goalobj.transform.position);
                    //changetime = Time.time + UnityEngine.Random.Range(5f, 10f);  //次の更新時刻を決める
                    ////Debug.Log("歩行中");
                }

                else
                {
                    animator.SetBool("isrunning", false);
                    animator.SetBool("iswalking", false);
                }

                break;
            case Mode.FOLLOW:

                Vector3 playerPos = player.position;                 //プレイヤーの位置
                Vector3 direction = playerPos - transform.position; //方向と距離を求める。
                float distance = direction.sqrMagnitude;            //directionから距離要素だけを取り出す。
                direction = direction.normalized;                   //単位化（距離要素を取り除く）
                direction.y = 0f;                                   //後に敵の回転制御に使うためY軸情報を消去。これにより敵上下を向かなくなる。
                //プレイヤーの距離が一定以上でなければ、敵キャラクターはプレイヤーへ近寄ろうとしない
                if (distance >= limitDistance)
                {
                    animator.SetBool("iswalking", false);
                    animator.SetBool("isrunning", true);
                    //プレイヤーとの距離が制限値以上なので普通に近づく
                    transform.position = transform.position + (direction * speed * Time.deltaTime);

                }
                else if (distance < limitDistance)
                {
                    animator.SetBool("iswalking", false);
                    animator.SetBool("isrunning", false);
                }
                //プレイヤーの方を向く
                transform.rotation = Quaternion.LookRotation(direction);

                break;
            case Mode.EVENT:

                break;
            
        }
        Debug.Log(m_Mode);
        //=======================================
        //モブの状態操作
        //=======================================
        if (Input.GetKeyDown("1"))
        {
            m_Mode = Mode.UROURO;
        }
        else if (Input.GetKeyDown("2"))
        {
            m_Mode = Mode.FOLLOW;
        }
        else if (Input.GetKeyDown("3"))
        {
            m_Mode = Mode.WAIT;
        }

    }
    //=========================================
    //関数名:SetMode
    //戻り値:なし
    //引き数:モブキャラに設定したい値
    //=========================================
    public void SetMode(Mode mode)
    {
        m_Mode = mode;

    }
}