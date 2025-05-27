public static class ObstacleBehaviorFactory
{
    public static IObstacleBehavior Create(MoveObstacleType type)
    {
        return type switch
        {
            MoveObstacleType.A => new UpDownObstacle(),
            MoveObstacleType.B => new LeftRightObstacle(),
            MoveObstacleType.C => new RotateObstacle(),
            MoveObstacleType.D => new BigJumpObstacle(),
            MoveObstacleType.J => new DeguldegulObstacle(),
            MoveObstacleType.M => new AccelObstacle(),
            MoveObstacleType.O => new AccelZObstacle(),
            _ => null
        };
    }
}