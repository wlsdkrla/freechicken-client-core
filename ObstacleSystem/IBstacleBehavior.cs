using UnityEngine;

public interface IObstacleBehavior
{
    void Initialize(MovingObstacle context);  // context를 통해 공용 변수 접근
    void UpdateBehavior();                   // 매 프레임 호출
}
