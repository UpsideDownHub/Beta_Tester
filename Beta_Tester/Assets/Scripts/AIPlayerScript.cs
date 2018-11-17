using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Jump;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class LastPossibility
{
    public Vector3? PlayerPosition { get; set; }
    public Vector3? GroundPosition { get; set; }
    public bool Jumped { get; set; }
    public bool Direction { get; set; } // true = right | false = left

    public LastPossibility()
    {
        PlayerPosition = null;
        GroundPosition = null;
    }
}

public enum PossibleWaysToGoStraight
{
    Straight,
    StraightJump,
    UpJump,
    Cant
}

public class AIPlayerScript : MonoBehaviour
{
    List<Vector3> walked = new List<Vector3>();
    List<Jump> jumpPossibilities = new List<Jump>();
    Rigidbody Rigidbody;
    Animator animator;

    CapsuleCollider CapsuleCollider;
    Transform circleT;
    [SerializeField] int XDistanceToJump = 2;
    [SerializeField] int XValidationDistance = 4;
    [SerializeField] int MaxYToJump = 3;
    [SerializeField] int MaxXToJump = 3;
    public float speed = 1.5f;
    [SerializeField] Tilemap tileM;
    bool direction = true;

    [Range(0, 100)]
    [SerializeField] int JumpProblability = 50;
    List<List<string>> data;

    Vector3 position = new Vector3(0, 0, 0);
    LastPossibility lastPossibility = new LastPossibility();
    bool ignore = false;
    bool _backing = false;
    float jumpSpeed;
    bool forceToJump = false;
    bool backing = false;
    bool isLevelInBeginning = true;
    bool isLevelCompleted = false;
    bool flip = false;
    int flipX = 0;
    float circleTLocalScaleX;
    float circleTLocalScaleY;
    void Start()
    {
        circleT = GameObject.Find("Circle").transform;
        jumpSpeed = speed;
        this.data = PhaseCreationManager.data; // OurPhases.data;
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider>();

        circleTLocalScaleX = circleT.localScale.x;
        circleTLocalScaleY = circleT.localScale.x;
    }

    void Update()
    {
        //if (transform.position.x >= data.Count - 8)
        //    isLevelCompleted = true;

        if (isLevelInBeginning)
        {
            if (circleT.localScale.x >= 0 && circleT.localScale.x <= 70)
            {
                Time.timeScale = 0f;
                circleTLocalScaleX += 0.8f;
                circleTLocalScaleY += 0.8f;
                circleT.localScale = new Vector3(circleTLocalScaleX, circleTLocalScaleY, circleT.localScale.z);
            }
            else
            {
                Time.timeScale = 1f;
                circleT.localScale = new Vector3(70, 70, circleT.localScale.z);
                isLevelInBeginning = false;
            }
        }
        if (isLevelCompleted)
        {
            if (circleT.localScale.x > 0)
            {
                circleTLocalScaleX -= 24 * Time.deltaTime;
                circleTLocalScaleY -= 24 * Time.deltaTime;
                circleT.localScale = new Vector3(circleTLocalScaleX, circleTLocalScaleY, circleT.localScale.z);
            }
            else
            {
                GameObject.Find("CutSceneJump").GetComponent<cutSceneJump>().JumpCutScene = true;
                DontDestroyOnLoad(GameObject.Find("CutSceneJump"));
                circleT.localScale = new Vector3(0, 0, 0);
                SceneManager.LoadScene("levelCleber");
                isLevelCompleted = false;
            }
        }

        //if (Rigidbody.velocity.x > 0)
        //{
        //    animator.SetBool("moving", true);
        //}
        //else if(Rigidbody.velocity.x == 0) 
        //{
        //    animator.SetBool("moving", false);
        //}
        //else if (Rigidbody.velocity.y > 0 || Rigidbody.velocity.y < 0)
        //{
        //    animator.SetBool("jump", true);
        //}
        //else if (Rigidbody.velocity.y == 0)
        //{
        //    animator.SetBool("jump", false);
        //}

        if (_backing && !ignore)
        {
            direction = walked.Last().x > (int)transform.position.x;
            var x = (int)transform.position.x + (direction ? 0 : 1); // + (direction ? 0 : 1)
            var y = (int)transform.position.y;
            if (x != lastPossibility.PlayerPosition.Value.x && y != lastPossibility.PlayerPosition.Value.y)
            {
                var newPos = walked.Last();
                direction = newPos.x > (int)transform.position.x;

                print(" x: " + x + " |  y :" + y);
                print("_x: " + newPos.x + " | _y :" + newPos.y);
                while (newPos.x == x && newPos.y == y)
                {
                    walked.Remove(walked.Last());
                    newPos = walked.Last();
                    direction = newPos.x < x;
                }


                if (newPos.y == y)
                    speed = newPos.x < x ? -Mathf.Abs(speed) : Mathf.Abs(speed);

                else if (newPos.y > y)
                {
                    var _i = 0;
                    for (var i = walked.Count - 1; i >= 0; i--)
                        if (walked[i].y == walked[i - 1].y)
                        {
                            _i = i;
                            break;
                        }

                    var height = Mathf.Abs(walked[_i].y - y) + 1;
                    direction = walked[_i].x > x;
                    if (height > MaxYToJump + 1)
                        print("não é possivel retornar");
                    else
                    {
                        Rigidbody.AddForce(new Vector3(0, 300 + (50 * (height - 1))));
                        jumpSpeed = XDistanceToJump;
                    }
                }
            }
            else
            {
                direction = lastPossibility.Direction;
                position = new Vector3();
                _backing = false;
            }
        }
        else
        {
            if (direction ? (position.x + 1) <= (int)transform.position.x : (position.x - 1) >= (int)transform.position.x)
            {
                position = new Vector3((int)transform.position.x, (int)transform.position.y);
                walked.Add(position);
                if (!ignore)
                    VerifyNearPositions();

                //if (data[NextXElement((int)position.x, 1)][Mathf.Abs((int)position.y - 10)] != 0)
                //    jumpSpeed = 0;
            }
        }
        //if (position.y > (int)transform.position.y)
        //{
        //    //Debug.DrawLine(new Vector2(NextXElement((int)position.x, 1), (int)position.y), new Vector2(NextXElement((int)position.x, 1) + 0.5f, (int)position.y + 0.5f), Color.red, 0.3f);
        //    if (data[NextXElement((int)position.x, 1)][Mathf.Abs((int)position.y - 10)] != 0)
        //    {
        //        //jumpSpeed = 0;
        //        //Rigidbody.velocity = new Vector3(0, Rigidbody.velocity.y, Rigidbody.velocity.z);
        //    }
        //}
    }

    void VerifyNearPositions()
    {
        jumpPossibilities = new List<Jump>();
        //isStucked = false;
        bool nextX = false;
        var _x = (int)position.x + (direction ? 0 : 1);
        var _y = (int)position.y;
        for (int x = _x; direction ? (x <= _x + XValidationDistance) : (x >= _x - XValidationDistance); x = direction ? (x + 1) : (x - 1))
        {
            for (int y = data[x].Count - 1; y >= 0; y--)
            {
                //Debug.DrawLine(new Vector2(x, Mathf.Abs(y - 10)), new Vector2(x + 0.5f, Mathf.Abs(y - 10) + 0.5f), Color.red, 0.3f);
                var realY = Mathf.Abs(y - 10);

                if (lastPossibility.PlayerPosition.HasValue && backing && lastPossibility.PlayerPosition.Value.x > _x)
                {
                    flip = true;
                    flipX = x;
                    backing = false;
                }
                //Pulo para frente.
                //if (!GroundInOrBelow(Mathf.Abs(_y - 10), NextXElement(_x, 1)) && GetValue(NextXElement(_x, 1), Mathf.Abs(_y - 10) - 1) == 0)

                //verificar se o valor esta a frente e abaixo do personagem e não bloco acima dele
                if (GetValue(x, y) == 0 && x == NextXElement(_x, 1) && y == Mathf.Abs(_y - 10) && !GroundInOrBelow(y, x, y - 2, false) && GetValue(PrevXElement(x, 1), y) != 0) //GetValue(NextXElement(_x, 1), Mathf.Abs(_y - 10) - 1) == 0
                {
                    //Debug.DrawLine(new Vector2(x, Mathf.Abs(y - 10)), new Vector2(x + 0.5f, Mathf.Abs(y - 10) + 0.5f), Color.red, 0.3f);
                    var XdistanceToJump = 0;
                    for (var __x = 1; __x <= MaxXToJump + 1; __x++)
                    {
                        if (!GroundInOrBelow(Mathf.Abs(_y - 10), NextXElement(_x, __x), Mathf.Abs(_y - 10 - (_y > 2 ? 2 : 0))) && (GetValue(NextXElement(_x, __x), Mathf.Abs(_y - 10) - 1) == 0)) XdistanceToJump++;
                        else break;
                    }

                    //altura mais longe do que o personagem consegue pular
                    if (XdistanceToJump != 0)
                    {
                        if (XdistanceToJump > MaxXToJump)
                        {
                            if (!CanGoStraightInBelow(_x, Mathf.Abs(_y - 10)) && !GroundInOrBelow(Mathf.Abs(_y - 10), x))
                            {
                                flip = true;
                                flipX = x;
                            }
                        }
                        //caso ele consiga pular verificar se há um bloco acima
                        else if (GetValue(NextXElement(_x, XdistanceToJump + 1), Mathf.Abs(_y - 10) - 1) == 0)
                        {
                            //if (!GroundInOrBelow(Mathf.Abs(_y - 10), NextXElement(_x, 1)) && GetValue(NextXElement(_x, 1), Mathf.Abs(_y - 10) - 1) == 0)
                            //{
                            //    lastPossibility.PlayerPosition = new Vector3(_x, Mathf.Abs(_y - 10), 0);
                            //    lastPossibility.GroundPosition = new Vector3(x, y, 0);
                            //    lastPossibility.Jumped = false;

                            //    if (Random.Range(0, 100) <= JumpProblability)
                            //    {
                            //        lastPossibility.Jumped = true;
                            //        jumpPossibilities.Set(new Vector3(x, Mathf.Abs(y - 10)), new Vector3(0, 250, 0), XdistanceToJump * 1.5f);
                            //    }
                            //}
                            //else
                            jumpPossibilities.Set(new Vector3(x, realY), new Vector3(0, 250, 0), XdistanceToJump * 1.5f);
                        }
                        //caso não consiga pular, porem é pouco espaço
                        else if (!CanGoStraightInBelow(_x, Mathf.Abs(_y - 10)) && !GroundInOrBelow(Mathf.Abs(_y - 10), x))
                            flip = true;
                    }
                }

                if (GetValue(x, y) != 0 && GetValue(x, y) != 2 && (GetValue(PrevXElement(x, 1), y) == 0 || GetValue(PrevXElement(x, 1), y) == 2))
                {
                    //bloqueio a frente
                    if (realY == _y + 1)
                    {
                        var canPassBelow = CanPassBelow(_x, y, x);
                        if (BlockedGrounds(null, x, y) > MaxYToJump && !canPassBelow)
                        {
                            flip = true;
                            flipX = x;
                        }
                        else if (lastPossibility.PlayerPosition.HasValue && lastPossibility.PlayerPosition.Value.x == _x)
                        {
                            //lastPossibility.Jumped = true;
                            int height = (int)lastPossibility.GroundPosition.Value.y - _y;
                            jumpPossibilities.Set(new Vector3(x, Mathf.Abs(y - 10)), new Vector3(0, 300 + (50 * (height - 1)), 0), XDistanceToJump, true);
                            //lastPossibility = new LastPossibility();
                        }
                        else if (!canPassBelow)
                            forceToJump = true;
                    }
                    //Pulo para blocos acima.
                    //tem bloco acima e a frente e até a distancia determinada
                    if (x == _x + XDistanceToJump && realY > _y && (realY - _y <= MaxYToJump))
                    {
                        //tem bloco acima do que será pulado
                        if (GetValue(x, y - 1) != 0) continue;

                        //bloco acima bloquando pulo
                        for (var i = 0; i < XDistanceToJump; i++)
                            if (GroundInOrBelow(Mathf.Abs(_y - 8), _x + i, (y - 1 < 0 ? 0 : y - 1), false))
                            {
                                nextX = true;
                                break;
                            }
                        if (nextX)
                        {
                            nextX = false;
                            break;
                        }


                        int height = Mathf.Abs(y - 10) - _y;
                        jumpPossibilities.Set(new Vector3(x, Mathf.Abs(y - 10)), new Vector3(0, 300 + (50 * (height - 1)), 0), XDistanceToJump, true);

                        //if (forceToJump.HasValue)
                        //{
                        //    lastPossibility = new LastPossibility();
                        //    if (forceToJump.Value)
                        //    {
                        //        int height = Mathf.Abs(y - 10) - _y;
                        //        jumpPossibilities.Set(new Vector3(x, Mathf.Abs(y - 10)), new Vector3(0, 300 + (50 * (height - 1)), 0), XDistanceToJump, true);
                        //    }
                        //    forceToJump = null;
                        //}
                        //else
                        //{
                        //    lastPossibility.PlayerPosition = new Vector3(_x, Mathf.Abs(_y - 10), 0);
                        //    lastPossibility.GroundPosition = new Vector3(x, y, 0);
                        //    lastPossibility.Jumped = false;

                        //    if (Random.Range(0, 100) <= JumpProblability || flip)
                        //    {
                        //        lastPossibility.Jumped = true;
                        //        int height = Mathf.Abs(y - 10) - _y;
                        //        jumpPossibilities.Set(new Vector3(x, Mathf.Abs(y - 10)), new Vector3(0, 300 + (50 * (height - 1)), 0), XDistanceToJump, true);
                        //    }
                        //}
                    }
                }
            }
        }

        var canGoStraight = CanGoStraight(_x, Mathf.Abs(_y - 10));
        if (flip)
            jumpPossibilities = jumpPossibilities.Where(x => x.Up).ToList();

        if (canGoStraight == PossibleWaysToGoStraight.Straight && !flip)
        {
            jumpPossibilities = jumpPossibilities.Where(x => !walked.Contains(new Vector3(x.Position.x, x.Position.y))).ToList();
            backing = false;
        }

        if (jumpPossibilities.Count > 0)
        {
            if (canGoStraight != PossibleWaysToGoStraight.Cant && !flip && !forceToJump)
            {
                lastPossibility.PlayerPosition = new Vector3(_x, _y, 0);
                lastPossibility.GroundPosition = new Vector3(jumpPossibilities.First().Position.x, jumpPossibilities.First().Position.y, 0);
                lastPossibility.Jumped = false;
                lastPossibility.Direction = direction;
            }
            if (Random.Range(0, 100) <= JumpProblability || flip || forceToJump || canGoStraight == PossibleWaysToGoStraight.Cant)
            {
                lastPossibility.Jumped = true;
                var i = Random.Range(0, jumpPossibilities.Count);
                Rigidbody.AddForce(jumpPossibilities[i].Force);
                jumpSpeed = jumpPossibilities[i].JumpSpeed;
                animator.SetBool("moving", false);
                animator.SetBool("jump", true);
            }
            else if (canGoStraight == PossibleWaysToGoStraight.StraightJump)
            {
                jumpPossibilities = jumpPossibilities.Where(x => !x.Up).ToList();
                var i = Random.Range(0, jumpPossibilities.Count);
                if (jumpPossibilities.Count > 0)
                {
                    Rigidbody.AddForce(jumpPossibilities[i].Force);
                    jumpSpeed = jumpPossibilities[i].JumpSpeed;
                    animator.SetBool("moving", false);
                    animator.SetBool("jump", true);
                }
            }
            forceToJump = false;
            backing = false;
        }
        else if (flip)
        {
            if (backing)
                _backing = true;
            //StartCoroutine(BackToLastPossibility());
            //BackToLastPossibility();

            Flip();
            forceToJump = lastPossibility.Jumped ? false : true;
        }
        //isStucked = false;
        flip = false;
    }

    //é usado o indices do data [] e não a posição real
    bool GroundInOrBelow(int currentY, int x, int? untilY = null, bool down = true)
    {
        //if (down)
        //    Debug.DrawLine(new Vector2(x, Mathf.Abs(currentY - 10)), new Vector2(x + 0.5f, Mathf.Abs(currentY - 10) + 0.5f), Color.red, 0.3f);

        for (int y = currentY; (untilY.HasValue ? ((down ? (y <= untilY) : (y >= untilY))) : (down ? (y < 10) : (y >= 0))); y = down ? y + 1 : y - 1)
            if (GetValue(x, y) != 0 && GetValue(x, y) != 2)
                return true;
        return false;
    }

    int BlockedGrounds(int? blockedGrounds, int x, int y)
    {
        if (!blockedGrounds.HasValue) blockedGrounds = 1;

        var tempBlockedGrounds = blockedGrounds;

        if (BlockedGroundsAbove(x, y - 1))
            return MaxYToJump + 1;

        if (GetValue(x, y - 1) != 2 && GetValue(x, y - 1) != 0)
        {
            blockedGrounds++;
            return BlockedGrounds(blockedGrounds, x, y - 1);
        }

        if (GetValue(PrevXElement(x, 1), y - 1) != 2 && GetValue(PrevXElement(x, 1), y - 1) != 0)
        {
            blockedGrounds++;
            return BlockedGrounds(blockedGrounds, PrevXElement(x, 1), y - 1);
        }

        return blockedGrounds.Value;
    }

    bool BlockedGroundsAbove(int x, int y)
    {
        var i = 1;
        while (true)
        {
            if (GetValue(PrevXElement(x, i), y) != 2 && GetValue(PrevXElement(x, i), y) != 0)
            {
                if (x - i <= (int)transform.position.x)
                    return true;
            }
            else
                break;
            i++;
        }
        return false;
    }

    bool CanPassBelow(int x, int y, int untilX)
    {
        var i = 1;
        int? _x = null;
        while (direction ? untilX >= NextXElement(x, i) : untilX <= NextXElement(x, i))
        {
            Debug.DrawLine(new Vector2(NextXElement(x, i), Mathf.Abs(y - 10 + 1)), new Vector2(NextXElement(x, i) + 0.5f, Mathf.Abs(y - 10 + 1) + 0.5f), Color.red, 0.3f);
            if (GetValue(NextXElement(x, i), y + 1) == 0)
            {
                _x = NextXElement(x, i);
                break;
            }
            i++;
        }

        if (_x == null) return false;

        return GroundInOrBelow(y + 1, _x.Value) || CanGoStraightInBelow(_x.Value, y + 1);
    }

    int NextXElement(int elem, int qtd)
    {
        return direction ? (elem + qtd) : (elem - qtd);
    }
    int PrevXElement(int elem, int qtd)
    {
        return direction ? (elem - qtd) : (elem + qtd);
    }

    int GetValue(int x, int y)
    {
        if (y > 9 || y < 0) return 0;

        if (data[x][y].IndexOf("-") != -1)
            return int.Parse(data[x][y].Split('-')[0]);
        
        return int.Parse(data[x][y]);
    }

    void Flip()
    {
        XDistanceToJump = -XDistanceToJump;
        speed = -speed;
        direction = (direction ? false : true);
        backing = true;
    }

    bool CanGoStraightInBelow(int x, int y)
    {
        int i = 1;
        while (true)
        {
            if ((NextXElement(x, i) <= data.Count || NextXElement(x, i) < 0) && i + y <= data[x + i].Count)
            {
                var val = GetValue(NextXElement(x, i), y + i);
                //Debug.DrawLine(new Vector2(NextXElement(x, i), Mathf.Abs(y - 10 + i)), new Vector2(NextXElement(x, i) + 0.5f, Mathf.Abs(y - 10 + i) + 0.5f), Color.red, 0.3f);
                if (val != 2 && val != 0 && GetValue(NextXElement(x, i), y + i - 1) == 0)
                    return true;
            }
            else break;

            i++;
        }
        return false;
    }

    PossibleWaysToGoStraight CanGoStraight(int x, int y)
    {
        var i = 1;
        int? _x = null;
        while (direction ? NextXElement(x, i) <= x + XValidationDistance : NextXElement(x, i) >= x - XValidationDistance)
        {
            //Debug.DrawLine(new Vector2(NextXElement(x, i), Mathf.Abs(y - 10)), new Vector2(NextXElement(x, i) + 0.5f, Mathf.Abs(y - 10) + 0.5f), Color.red, 0.3f);
            if (GetValue(NextXElement(x, i), y) == 0)
            {
                _x = NextXElement(x, i);
                break;
            }
            i++;
        }

        if (!_x.HasValue) return PossibleWaysToGoStraight.Straight;

        var XdistanceToJump = 0;
        for (var __x = 0; __x <= MaxXToJump + 1; __x++)
        {
            if (!GroundInOrBelow(y, NextXElement(_x.Value, __x)) && (GetValue(NextXElement(_x.Value, __x), y - 1) == 0)) XdistanceToJump++;
            else break;
        }

        if (XdistanceToJump <= MaxXToJump && XdistanceToJump != 0) return PossibleWaysToGoStraight.StraightJump;

        return GroundInOrBelow(y, _x.Value) || CanGoStraightInBelow(_x.Value, y) ? PossibleWaysToGoStraight.Straight : PossibleWaysToGoStraight.Cant;
    }

    //IEnumerator BackToLastPossibility()
    //{
    //    _backing = true;

    //    while ((int)transform.position.x != lastPossibility.PlayerPosition.Value.x && (int)transform.position.y != lastPossibility.PlayerPosition.Value.y)
    //    {
    //        var x = (int)transform.position.x;
    //        var y = (int)transform.position.y;
    //        var newPos = walked.Last();

    //        while(newPos.x == x && newPos.y == y)
    //        {
    //            walked.Remove(walked.Last());
    //            newPos = walked.Last();
    //        }

    //        if (newPos.y == y)
    //        {
    //            speed = newPos.x > x ? Mathf.Abs(speed) : -Mathf.Abs(speed);
    //            Rigidbody.velocity = new Vector3(speed, Rigidbody.velocity.y, Rigidbody.velocity.z);
    //        }
    //        else if (newPos.y > y)
    //        {
    //            var _i = 0;
    //            for (var i = walked.Count - 1; i >= 0; i--)
    //                if (walked[i].y == walked[i - 1].y)
    //                {
    //                    _i = i;
    //                    break;
    //                }

    //            var height = Mathf.Abs(walked[_i].y - y);
    //            if (height > MaxYToJump)
    //                print("não é possivel retornar");
    //            else
    //            {
    //                Rigidbody.AddForce(new Vector3(0, 300 + (50 * (height - 1))));
    //                jumpSpeed = XDistanceToJump;
    //            }
    //        }
    //    }
    //    direction = lastPossibility.Direction;
    //    position = new Vector3();
    //    _backing = false;

    //    yield return 0;
    //}

    private void OnCollisionStay(Collision collision)
    {
        ignore = false;
        Rigidbody.AddForce(new Vector3(0, 0, 0));
        Rigidbody.velocity = new Vector3(speed, Rigidbody.velocity.y, Rigidbody.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("moving", true);
        animator.SetBool("jump", false);
        //position = direction? new Vector3() : new Vector3(data.Count,0);
        if (_backing)
            return;
        if ((int)transform.position.x > 1)
        {
            position = new Vector3((int)transform.position.x, (int)transform.position.y);
            VerifyNearPositions();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody.velocity = new Vector3(Mathf.Abs(jumpSpeed) * (direction ? 1 : -1), Rigidbody.velocity.y, Rigidbody.velocity.z);
        ignore = true;
    }
}



//Observação:
//Data[]        Posição Y
// 0                10
// 1                9
// 2                8
// 3                7
// 4                6
// 5                5
// 6                4
// 7                3
// 8                2
// 9                1