namespace Data
{
    public class BaseShipStats
    {
        public float Health;
        public float Mobility;
        public float MoveSpeed;
        public float ShootSpeed;
        public string Type;

        public override string ToString()
        {
            return Type;
        }
    }
}