using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehavior 를 상속받은 컴포넌트들은 생성자를통해서 생성하면 안됨.
public class Movement : MonoBehaviour
{
    // Serialize : 직렬화, 개체를 데이터(text / byte 등)로 변환하는 과정
    // [ ] 를 멤버 필드/프로퍼티 / 클래스 / 구조체 등의 앞에 사용하면 Attribute 를 달 수 있게된다.
    // Attribute : 앞에있는 필드/프로퍼티/클래스/구조체 등의 메타데이터 생성용 클래스
    // 메타데이터 : 데이터의 데이터 (ex. "private Transform _transform" 이라는 필드가 추가적으로 Serialize 하는 필드라는것을 명시)
    // SerializeFeild : 내 필드/프로퍼티를 외부에 공개하고싶지 않으면서 인스펙터창에서는 노출시키고싶을때 사용하는 Attribute
    [SerializeField] private Transform _transform;

    private float _h;
    private float _v;
    private float _r;

    /// <summary>
    /// 스크립트 인스턴스가 처음 로드될 때 호출
    /// 해당 스크립트인스턴스를 컴포넌트로 포함하는 게임오브젝트가 활성화 되어있어야 호출
    /// (게임오브젝트가 비활성화 된 채로 실행되면 해당 게임오브젝트는 인스턴스화 되지 않고, 따라서 스크립트 인스턴스도 인스턴스화 되지않는다)
    /// 스크립트인스턴스를 비활성화하겠다고 해 놓아도 
    /// 게임오브젝트가 인스턴스화 될때 해당 스크립트 인스턴스도 로드되기 때문에 Awake는 호출된다.
    /// -> 생성자 대용으로 사용함 (생성자와 같다는것은 아님. 멤버변수들의 초기화를 해야할때 주로 Awake에서 함)
    /// </summary>
    private void Awake()
    {
        Debug.Log("Awake");
    }

    /// <summary>
    /// 이 컴포넌트를 가지는 GameObject 가 활성화 되거나 
    /// 이 스크립트인스턴스가 활성화 될 때마다 호출됨.
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    /// <summary>
    /// 에디터에서만 호출. 멤버변수들을 모두 default 로 설정
    /// </summary>
    private void Reset()
    {
        Debug.Log("Reset");
    }

    /// <summary>
    /// Awake 에서 초기화가 완료된 다른 참조들을 사용해서 멤버 또는 다른 객체 생성 등을 한번 수행 해야한다고 할 때 주로 사용
    /// </summary>
    void Start()
    {
        Debug.Log("Start");
    }

    /// <summary>
    /// 고정 프레임 속도로 매프레임 호출됨
    /// 물리연산관련 로직은 이 이벤트에서 수행해야함.
    /// </summary>
    private void FixedUpdate()
    {
        // Vector : 크기와 방향을 가진 원소 <-> Scalar : 크기만 가진 원소 ( ex, 속도 vs 속력 )
        // Vector3 : x, y, z 축별 크기를 가진 벡터원소 구조체
        // fixedDeltaTime : FixedUpdate 프레임간격 (프레임당 시간변화량)
        //_transform.position += new Vector3(_h, 0.0f, _v) * Time.fixedDeltaTime;
        _transform.Translate(new Vector3(_h, 0.0f, _v) * Time.fixedDeltaTime, Space.Self);
        _transform.Rotate(Vector3.up * _r * 360.0f * Time.fixedDeltaTime);
    }

    /// <summary>
    /// 다른 collider 가 trigger 인 collider 와 겹치는 순간에 호출됨.
    /// 이 스크립트를 컴포넌트로가지는 GameObject 가 Trigger를 가지고 있던지, other 의 GameObject 가 Trigger를 가지고 있던지 
    /// 둘중에 하나라도 RigidBody를 가지고 있고, 둘중에 하나라도 Trigger 를 가지고 있어야한다.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Movement] : {other.name} 가 트리거됨");
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    /// <summary>
    /// Collider - RigidBody 간의 상호작용. 접촉시 호출
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[Movement] : {collision.gameObject.name} 가 충돌됨");
    }

    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }

    private void OnMouseDown()
    {
        Debug.Log($"[Movement] : {gameObject.name} 에 마우스 눌림");
    }

    private void OnMouseEnter()
    {
        Debug.Log($"[Movement] : {gameObject.name} 에 마우스 진입");
    }

    private void OnMouseDrag()
    {
        Debug.Log($"[Movement] : {gameObject.name} 에 마우스 끌기");
    }

    private void OnMouseExit()
    {
        Debug.Log($"[Movement] : {gameObject.name} 에 마우스 탈출");
    }

    private void OnMouseOver()
    {
        Debug.Log($"[Movement] : {gameObject.name} 에 마우스 겹침");
    }

    private void OnMouseUp()
    {
        Debug.Log($"[Movement] : {gameObject.name} 에 마우스 떨어짐");
    }

    /// <summary>
    /// 게임 로직의 기본 단위 
    /// 매프레임마다 호출됨 (프레임 간격은 연산량에 따라 계속 다름)
    /// </summary>
    void Update()
    {
        //Debug.Log("Update");
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");
        Debug.Log($"DeltaMove : ({_h}, {0}, {_v})");
        _r = Input.GetAxis("Mouse X");
    }

    /// <summary>
    /// 매프레임의 마지막에 호출됨
    /// 게임 로직과 직접적인 연관이 없고 그냥 프레임마다 호출이 되기만 하면 될때 주로씀 (Camera)
    /// </summary>
    private void LateUpdate()
    {

    }

    /// <summary>
    /// Gizmos : 화면에 존재하는 모든 그래픽적인 요소 (아이콘 등)
    /// Gizmos를 그릴때 내것도 그리고싶으면 이 이벤트함수에 구현하면 됨.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Vector3.zero, 2.0f);
    }

    /// <summary>
    /// Graphic User Interface 를 렌더링할때 호출되는 이벤트함수.
    /// 에디터를 커스터마이징 하거나 드로우콜 등에대한 성능 체크를 할때 등에 사용함.
    /// </summary>
    private void OnGUI()
    {
        Vector2 mousePos = Event.current.mousePosition;
    }

    /// <summary>
    /// 앱이 일시정지/ 해제 되었을때 호출
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        
    }

    /// <summary>
    /// 앱이 종료될때 호출.
    /// 앱이 종료되려는데 오브젝트를 생성한다거나 하는것을 막아야할때 등에 응용할수 있음.
    /// </summary>
    private void OnApplicationQuit()
    {
        
    }

    /// <summary>
    /// 이 스크립트인스턴스를 컴포넌트로가지는 GameObject 가 비활성화되거나
    /// 이 스크립트인스턴스가 비활성화될 때 호출
    /// </summary>
    private void OnDisable()
    {
        
    }

    /// <summary>
    /// 이 스크립트인스턴스를 컴포넌트로가지는 GameObject 가 파괴되거나 
    /// 이 스크립트인스턴스가 파괴될 때 호출
    /// OnDestroy() 내에서는 GameObject 를 생성하는 함수를 호출하면 안됨.
    /// </summary>
    private void OnDestroy()
    {
        
    }
}
