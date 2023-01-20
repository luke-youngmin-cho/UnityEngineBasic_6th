using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehavior �� ��ӹ��� ������Ʈ���� �����ڸ����ؼ� �����ϸ� �ȵ�.
public class Movement : MonoBehaviour
{
    /// <summary>
    /// ��ũ��Ʈ �ν��Ͻ��� ó�� �ε�� �� ȣ��
    /// �ش� ��ũ��Ʈ�ν��Ͻ��� ������Ʈ�� �����ϴ� ���ӿ�����Ʈ�� Ȱ��ȭ �Ǿ��־�� ȣ��
    /// (���ӿ�����Ʈ�� ��Ȱ��ȭ �� ä�� ����Ǹ� �ش� ���ӿ�����Ʈ�� �ν��Ͻ�ȭ ���� �ʰ�, ���� ��ũ��Ʈ �ν��Ͻ��� �ν��Ͻ�ȭ �����ʴ´�)
    /// ��ũ��Ʈ�ν��Ͻ��� ��Ȱ��ȭ�ϰڴٰ� �� ���Ƶ� 
    /// ���ӿ�����Ʈ�� �ν��Ͻ�ȭ �ɶ� �ش� ��ũ��Ʈ �ν��Ͻ��� �ε�Ǳ� ������ Awake�� ȣ��ȴ�.
    /// -> ������ ������� ����� (�����ڿ� ���ٴ°��� �ƴ�. ����������� �ʱ�ȭ�� �ؾ��Ҷ� �ַ� Awake���� ��)
    /// </summary>
    private void Awake()
    {
        Debug.Log("Awake");
    }

    /// <summary>
    /// �� ������Ʈ�� ������ GameObject �� Ȱ��ȭ �ǰų� 
    /// �� ��ũ��Ʈ�ν��Ͻ��� Ȱ��ȭ �� ������ ȣ���.
    /// </summary>
    private void OnEnable()
    {        
        Debug.Log("OnEnable");        
    }

    /// <summary>
    /// �����Ϳ����� ȣ��. ����������� ��� default �� ����
    /// </summary>
    private void Reset()
    {
        Debug.Log("Reset");
    }

    /// <summary>
    /// Awake ���� �ʱ�ȭ�� �Ϸ�� �ٸ� �������� ����ؼ� ��� �Ǵ� �ٸ� ��ü ���� ���� �ѹ� ���� �ؾ��Ѵٰ� �� �� �ַ� ���
    /// </summary>
    void Start()
    {
        Debug.Log("Start");
    }

    /// <summary>
    /// ���� ������ �ӵ��� �������� ȣ���
    /// ����������� ������ �� �̺�Ʈ���� �����ؾ���.
    /// </summary>
    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// �ٸ� collider �� trigger �� collider �� ��ġ�� ������ ȣ���.
    /// �� ��ũ��Ʈ�� ������Ʈ�ΰ����� GameObject �� Trigger�� ������ �ִ���, other �� GameObject �� Trigger�� ������ �ִ��� 
    /// ���߿� �ϳ��� RigidBody�� ������ �ְ�, ���߿� �ϳ��� Trigger �� ������ �־���Ѵ�.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Movement] : {other.name} �� Ʈ���ŵ�");
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    /// <summary>
    /// Collider - RigidBody ���� ��ȣ�ۿ�. ���˽� ȣ��
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[Movement] : {collision.gameObject.name} �� �浹��");
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log($"[Movement] : {gameObject.name} �� ���콺 ����");
    }

    private void OnMouseEnter()
    {
        Debug.Log($"[Movement] : {gameObject.name} �� ���콺 ����");
    }

    private void OnMouseDrag()
    {
        Debug.Log($"[Movement] : {gameObject.name} �� ���콺 ����");
    }

    private void OnMouseExit()
    {
        Debug.Log($"[Movement] : {gameObject.name} �� ���콺 Ż��");
    }

    private void OnMouseOver()
    {
        Debug.Log($"[Movement] : {gameObject.name} �� ���콺 ��ħ");
    }

    private void OnMouseUp()
    {
        Debug.Log($"[Movement] : {gameObject.name} �� ���콺 ������");
    }

    /// <summary>
    /// ���� ������ �⺻ ���� 
    /// �������Ӹ��� ȣ��� (������ ������ ���귮�� ���� ��� �ٸ�)
    /// </summary>
    void Update()
    {
        //Debug.Log("Update");
    }

    /// <summary>
    /// ���������� �������� ȣ���
    /// ���� ������ �������� ������ ���� �׳� �����Ӹ��� ȣ���� �Ǳ⸸ �ϸ� �ɶ� �ַξ� (Camera)
    /// </summary>
    private void LateUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Vector3.zero, 2.0f);
    }
}
