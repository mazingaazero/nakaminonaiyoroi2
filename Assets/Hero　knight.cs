using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Animator animator;
    float speed = 4f;   // 1秒で4マス移動(unit/s)
    SpriteRenderer spriteRenderer;

    Rigidbody2D rigid2D;//
    public float jumpForce = 500f;  // ジャンプ力

    public Collider2D attackCollider; // 攻撃用のコライダー

    bool isJumping; // ジャンプしているか true:ジャンプしている false:してない

    int hp = 3; // HP

    test test;//testコンポーネントにアクセスしたいから

    [SerializeField]
    test enemyTest;

    [SerializeField]
    GameObject enemy;

    int score = 0;
    //スコアを増やす関数
    public void addScore()
    {
        this.score++;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigid2D = GetComponent<Rigidbody2D>();
        //このスクリプトと同じオブジェクトに
        //アタッチされているコンポーネントは
        //GetComponentで習得できる
        this.test = GetComponent<test>();

        Debug.Log(this.test.GetA());
        this.test.SetA(500);
        Debug.Log(this.test.GetA());

        this.enemyTest.SetA(34);

        test etest = this.enemyTest.GetComponent<Test>();
        Debug.Log(etest.GetA());
    }


    // Update is called once per frame
    void Update()
    {
        // 死んでいるなら
        if (this.hp <= 0)
        {
            return; // この関数から抜ける
        }


        // 左右方向のキー入力受け取り(-1?0?1の値)
        float horizontal = Input.GetAxis("Horizontal");

        // 移動
        transform.Translate(horizontal * this.speed * Time.deltaTime, 0, 0);

        // アニメーターのパラメータを変更
        this.animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // 左右反転処理
        if (horizontal > 0)
        {
            this.spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            this.spriteRenderer.flipX = true;
        }

        // 攻撃
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.animator.SetTrigger("Attack");
        }

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.X) && this.isJumping == false)
        {
            this.isJumping = true;
            // 上方向にjumpForce分の力を加える
            this.rigid2D.AddForce(Vector2.up * this.jumpForce);
            this.animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面判定
        if (collision.gameObject.tag == "Ground")
        {
            this.isJumping = false;
        }
        // 敵の攻撃を受ける
        else if (collision.gameObject.tag == "EnemyAttack")
        {
            if (this.hp > 0)    // 生きているなら
            {
                this.hp--;      // HPを減らす
                if (this.hp > 0)// 減らした結果まだ生きているなら
                {
                    this.animator.SetTrigger("Damage");
                }
                else            // 死んだなら
                {
                    this.animator.SetTrigger("Death");

                    //スコアを保存
                    PlayerPrefs.SetInt("Score", this.Score);
                    //ストレージ
                    PlayerPrefs.Save();

                    //３秒後にシーンを切り替える
                    Invoke("GoToResukt", 3f);

                }
            }
        }
    }

    //結果シーンを切り替える関数
    void GotoResult()
    {
        //シーンを切り替える　因数にはシーンファイル名
        SceneManager.LoadScene(Result); a
    }


    void GotoResult()
    {
        //シーンを切り替える処理をここに作る
    }
    // コライダーを有効にする関数
    public void ColliderOn()
    {
        this.attackCollider.enabled = true;
    }

    // コライダーを無効にする関数
    public void ColliderOff()
    {
        this.attackCollider.enabled = false;
    }
}