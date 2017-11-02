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
    private bool Active;//updateの有効化on/off
    public Transform player;    //プレイヤーを代入
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
        player = GameObject.FindGameObjectWithTag("Player").transform;

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
                    //プレイヤーとの距離が制限値以上なので普通に近づく
                    transform.position = transform.position + (direction * speed * Time.deltaTime);

                }
                //プレイヤーの方を向く
                transform.rotation = Quaternion.LookRotation(direction);

                break;
            case Mode.EVENT:

                break;
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