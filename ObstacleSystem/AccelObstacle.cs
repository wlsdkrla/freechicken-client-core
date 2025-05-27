using UnityEngine;

public class AccelObstacle : IObstacleBehavior
{
    private MovingObstacle context;

    public void Initialize(MovingObstacle context)
    {
        this.context = context;
    }

    public void UpdateBehavior()
    {
        if (context.isMove && !context.removeObj)
        {
            context.rigid.AddForce(Vector3.right * context.moveSpeed, ForceMode.Acceleration);
            context.isMove = false;
        }
        else if (context.removeObj)
        {
            context.gameObject.SetActive(false);
            context.isMove = false;
        }
    }
}
