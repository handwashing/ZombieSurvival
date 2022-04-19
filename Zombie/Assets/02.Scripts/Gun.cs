using System.Collections;
using UnityEngine;

//���� ����
public class Gun : MonoBehaviour
{
    // ���� ���¸� ǥ���ϴ� �� ����� Ÿ���� ����
    public enum State //enum������/���� ������ ������� ����
    {
        Ready, //�߻� �غ� ��
        Empty, //Žâ�� ��
        Reloading //������ ��
    }

    public State state { get; private set; }//���� ���� ����

    public Transform fireTransform;//ź���� �߻�� ��ġ

    public ParticleSystem muzzleFlashEffect; //�ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffect; //ź�� ���� ȿ��

    private LineRenderer bulletLineRenderer; //ź�� ������ �׸��� ���� ������

    private AudioSource gunAudioPlayer; //�� �Ҹ� �����

    public GunData gunData; //���� ���� ������

    private float fireDistance = 50f; //�����Ÿ�

    public int ammoRemain = 100; //���� ��ü ź��
    public int magAmmo; //���� źâ�� ���� �ִ� ź��

    private float lastFireTime; //���� ���������� �߻��� ����

    private void Awake()
    {
        //����� ���۳�Ʈ�� ���� ��������
        bulletLineRenderer = GetComponent<LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        //����� ���� �� ���� ����
        bulletLineRenderer.positionCount = 2;
        //���� �������� ��Ȱ��ȭ
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable()
    {//�� ���� �ʱ�ȭ
        //��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = gunData.startAmoRemain;
        //���� źâ�� ���� ä���
        magAmmo = gunData.magCapacity;

        //���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        //���������� ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }

    //�߻� �õ�
    public void Fire()
    {
        //���� ���°� �߻� ������ ����
        //&&������ �� �߻� �������� gunData.timeBetFire �̻��� �ð��� ����
        if (state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)
        {
            //������ �� �߻� ���� ����
            lastFireTime = Time.time;
            //���� �߻� ó�� ����
            Shot();
        }
    }

    //���� �߻� ó��
    private void Shot()
    {
        //����ĳ��Ʈ�� ���� �浹 ������ �����ϴ� �����̳�
        RaycastHit hit;
        //ź���� ���� ���� ������ ����
        Vector3 hitPosition = Vector3.zero;

        //����ĳ��Ʈ(���� ����, ����, �浹 ���� �����̳�, �����Ÿ�)
        if(Physics.Raycast(fireTransform.position,fireTransform.forward,out hit, fireDistance))
        {
            //���̰� � ��ü�� �浹�� ���

            //�浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            //�������κ��� IDamageanle ������Ʈ�� �������µ� �����ߴٸ�
            if (target != null)
            {
                //������ OnDamageable �Լ��� ������� ���濡 ������ �ֱ�
                target.OnDamage(gunData.damage, hitPosition, hit.normal);
            }

            //���̰� �浹�� ��ġ ����
            hitPosition = hit.point;
        }
        else
        {
            //���̰� �ٸ� ��ü�� �浹���� �ʾҴٸ�
            //ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        //�߻� ����Ʈ ��� ����
        StartCoroutine(ShotEffect(hitPosition));

        //���� ź�� ���� -1
        magAmmo--;
        if (magAmmo <= 0)
        {
            //źâ�� ���� ź���� ���ٸ� ���� ���� ���¸� Empty�� ����
            state = State.Empty;
        }
    }

    //�߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        //�ѱ� ȭ�� ȿ�� ���
        muzzleFlashEffect.Play();
        //ź�� ���� ȿ�� ���
        shellEjectEffect.Play();
        //�Ѱ� �Ҹ� ���(OneShot�� �Ҹ� ��ø ����_)
        gunAudioPlayer.PlayOneShot(gunData.shotClip);

        //���� �������� �ѱ��� ��ġ
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        //���� ������ �Է����� ���� �浹 ��ġ
        bulletLineRenderer.SetPosition(1, hitPosition);
        //���� �������� Ȱ��ȭ�Ͽ� ź�� ������ �א�
        bulletLineRenderer.enabled = true;

        //0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);

        //���� �������� ��Ȱ��ȭ�Ͽ� ź�� ������ ����
        bulletLineRenderer.enabled = false;
    }

    //������ �õ�
    public bool Reload()
    {
        return false;
    }

    //���� ������ ó���� ����
    private IEnumerator ReloadRoutine()
    {
        //���� ���¸� ������ �� ���·� ��ȯ
        state = State.Reloading;

        //������ �ҿ� �ð���ŭ ó�� ����
        yield return new WaitForSeconds(gunData.reloadTime);

        //���� ���� ���¸� �߻� �غ�� ���·� ����
        state = State.Ready;
    }
}
