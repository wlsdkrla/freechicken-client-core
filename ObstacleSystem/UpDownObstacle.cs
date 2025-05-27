using UnityEngine;

public class UpDownObstacle : IObstacleBehavior
{
    private MovingObstacle context;

    public void Initialize(MovingObstacle context)
    {
        this.context = context;
        context.initPositionY = context.transform.position.y;
        context.turningPoint = context.initPositionY - context.distance;
    }

    public void UpdateBehavior()
    {
        float currentY = context.transform.position.y;
        if (currentY >= context.initPositionY) context.turnSwitch = false;
        else if (currentY <= context.turningPoint) context.turnSwitch = true;

        float direction = context.turnSwitch ? 1 : -1;
        Vector3 move = Vector3.up * direction * context.moveSpeed * Time.deltaTime;
        context.transform.Translate(move);
        if (context.isPlayerFollow) context.player.transform.Translate(move);

    }
}