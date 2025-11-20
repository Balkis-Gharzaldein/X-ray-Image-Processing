namespace WindowsFormsApp1
{
    public class DrawnShape
    {
        public string ShapeType { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public DrawnShape(string shapeType, Point startPoint, Point endPoint)
        {
            ShapeType = shapeType;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}