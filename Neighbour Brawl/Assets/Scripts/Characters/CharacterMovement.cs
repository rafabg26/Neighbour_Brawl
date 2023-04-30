using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 4.5f;
    private Rigidbody2D _body;
    private Animator _anim;
    public float jumpForce = 10.0f;
    // Start is called before the first frame update
    private BoxCollider2D _box;
    private bool isPunching = false;

    private (Vector2, Vector2) getGroundCheckCorners() {
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        return (corner1, corner2);
    }

    public Collider2D getGroundObject{
        get {
            var (corner1, corner2) = getGroundCheckCorners();
            Collider2D platformCollider = Physics2D.OverlapArea(corner1, corner2, LayerMask.GetMask("Ground"));
            if (platformCollider != null) {
                return platformCollider;
            }
            else {
                return null;
            }
        }
    }
    public bool grounded {
        get {
            return (getGroundObject != null && getGroundObject.CompareTag("ground"));
        }
    }

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (!isPunching) {
        float deltaX = Input.GetAxis("Horizontal")  * speed;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
        _body.gravityScale = (grounded && Mathf.Approximately(Mathf.Abs(deltaX), 0f)) ? 0 : 3;
        if (grounded && 
            Input.GetButtonDown("Jump") && getGroundObject.gameObject.CompareTag("ground")) {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Vector3 pScale = Vector3.one;
        _anim.SetFloat("speed", Mathf.Abs(deltaX));
        _anim.SetBool("jumping", !grounded);
        //Esto invierte al sprite y hace que gire !!!!
        //Aproximately calcula si es mas o menos es similar a un valor
        if (!Mathf.Approximately(deltaX, 0f)) {
            transform.localScale = new Vector3(-(Mathf.Sign(deltaX)/pScale.x), 1f/pScale.y, 1f);
        } 

        //Pegar puñetazo
        if(Input.GetKeyDown(KeyCode.X)){
            if(grounded){
                
            _anim.Play("Punch");
            isPunching = true;
            }
        }
        

        }else {
        // Si el personaje está golpeando, no permite más movimientos
            _body.velocity = new Vector2(0f, _body.velocity.y);
            _body.gravityScale = 0;
        }
        AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0);
        if ((stateInfo.IsName("Punch") || stateInfo.IsName("airKick"))&& stateInfo.normalizedTime >= 1.0f) {
            _anim.SetBool("Punch", false);
            isPunching = false;
            FinishPunchAnimation();
        }  
    }
    public void FinishPunchAnimation() {
        isPunching = false;
    }

}
