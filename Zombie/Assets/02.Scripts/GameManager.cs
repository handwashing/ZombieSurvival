using UnityEngine;

//점수와 게임오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour
{   //싱글턴 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            //만약 싱글턴 변수에 아직 오브젝트가 할당되지 않았다면...
            if (m_instance == null)
            {
                //씬에서 GameManager 오브젝트를 찾아서 할당
                
            }
        }
    }

    public bool isGameover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
