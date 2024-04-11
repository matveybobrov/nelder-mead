namespace HELPERS
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class Result
    {
        public List<Point[]> Steps { get; set; }
        public Point Solution { get; set; }

        public Result(List<Point[]> steps, Point solution)
        {
            Steps = steps;
            Solution = solution;
        }
    }
}
