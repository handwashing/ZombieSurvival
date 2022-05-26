using System.Collections;
using UnityEngine;
using UnityEngine.AI; //AI, 내비게이션 시스템 관련 코드 가져오기

//좀비 AI 구현
public class Zombie : LivingEntity
{
  public LayerMask whatIsTarget; //추적 대상 레이어

  public LivingEntity targetEntity; //추적 대상
  public NavMeshAgent navMeshAgent; //경로 계산 Ai 에이전트

  public ParticleSystem hitEffect; //피격 시 재생할 파티클 효과
  public AudioClip deathSound; //사망 시 재생할 소리
  public AudioClip hitSound; //피격 시 재생할 소리

  private Animator zombieAnimator; //Animator Component
  private AudioSource zombieAudioPlayer; //Audio Soure Component
  private Renderer zombieRenderer; //Renderer Component

  public float damage = 20f; //공격력
  public float timeBetAttack = 0.5f; //공격 간격
  public float lastAttackTime; //마지막 공격 시점

  //추적할 대상이 존재하는지 알려주는 프로퍼티
  private bool hasTarget{
      get{
          //추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
      }
  }
}
