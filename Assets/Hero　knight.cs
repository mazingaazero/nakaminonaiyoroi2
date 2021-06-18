using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Animator animator;
    float speed = 4f;   // 1�b��4�}�X�ړ�(unit/s)
    SpriteRenderer spriteRenderer;

    Rigidbody2D rigid2D;//
    public float jumpForce = 500f;  // �W�����v��

    public Collider2D attackCollider; // �U���p�̃R���C�_�[

    bool isJumping; // �W�����v���Ă��邩 true:�W�����v���Ă��� false:���ĂȂ�

    int hp = 3; // HP

    test test;//test�R���|�[�l���g�ɃA�N�Z�X����������

    [SerializeField]
    test enemyTest;

    [SerializeField]
    GameObject enemy;

    int score = 0;
    //�X�R�A�𑝂₷�֐�
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
        //���̃X�N���v�g�Ɠ����I�u�W�F�N�g��
        //�A�^�b�`����Ă���R���|�[�l���g��
        //GetComponent�ŏK���ł���
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
        // ����ł���Ȃ�
        if (this.hp <= 0)
        {
            return; // ���̊֐����甲����
        }


        // ���E�����̃L�[���͎󂯎��(-1?0?1�̒l)
        float horizontal = Input.GetAxis("Horizontal");

        // �ړ�
        transform.Translate(horizontal * this.speed * Time.deltaTime, 0, 0);

        // �A�j���[�^�[�̃p�����[�^��ύX
        this.animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // ���E���]����
        if (horizontal > 0)
        {
            this.spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            this.spriteRenderer.flipX = true;
        }

        // �U��
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.animator.SetTrigger("Attack");
        }

        // �W�����v
        if (Input.GetKeyDown(KeyCode.X) && this.isJumping == false)
        {
            this.isJumping = true;
            // �������jumpForce���̗͂�������
            this.rigid2D.AddForce(Vector2.up * this.jumpForce);
            this.animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �n�ʔ���
        if (collision.gameObject.tag == "Ground")
        {
            this.isJumping = false;
        }
        // �G�̍U�����󂯂�
        else if (collision.gameObject.tag == "EnemyAttack")
        {
            if (this.hp > 0)    // �����Ă���Ȃ�
            {
                this.hp--;      // HP�����炷
                if (this.hp > 0)// ���炵�����ʂ܂������Ă���Ȃ�
                {
                    this.animator.SetTrigger("Damage");
                }
                else            // ���񂾂Ȃ�
                {
                    this.animator.SetTrigger("Death");

                    //�X�R�A��ۑ�
                    PlayerPrefs.SetInt("Score", this.Score);
                    //�X�g���[�W
                    PlayerPrefs.Save();

                    //�R�b��ɃV�[����؂�ւ���
                    Invoke("GoToResukt", 3f);

                }
            }
        }
    }

    //���ʃV�[����؂�ւ���֐�
    void GotoResult()
    {
        //�V�[����؂�ւ���@�����ɂ̓V�[���t�@�C����
        SceneManager.LoadScene(Result); a
    }


    void GotoResult()
    {
        //�V�[����؂�ւ��鏈���������ɍ��
    }
    // �R���C�_�[��L���ɂ���֐�
    public void ColliderOn()
    {
        this.attackCollider.enabled = true;
    }

    // �R���C�_�[�𖳌��ɂ���֐�
    public void ColliderOff()
    {
        this.attackCollider.enabled = false;
    }
}