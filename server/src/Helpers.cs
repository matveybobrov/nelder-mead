using System;
using System.Collections.Generic;

namespace HELPERS {
    public class Point : ICloneable{
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y) {
            X = x;
            Y = y;
        }

        public double f() {
            return X * X + X * Y + Y * Y - 6 * X - 9 * Y;
        }

        public static Point operator -(Point p1, Point p2) {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator +(Point p1, Point p2) {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator /(Point p1, int a) {
            return new Point(p1.X / a, p1.Y / a);
        }

        public static Point operator *(Point p1, int a) {
            return new Point(p1.X * a, p1.Y * a);
        }

        public object Clone() {
            return new Point(X, Y);
        }

        public override string ToString() {
            string text = X.ToString() + ", " + Y.ToString();
            return text;
        }

    }

    public class Result {
        public List<Simplex> Steps { get; set; }
        public Point Solution { get; set; }

        public Result(List<Simplex> steps, Point solution) {
            Steps = steps;
            Solution = solution;
        }
    }

    public class Simplex : ICloneable {
        public Point Best { get; set; }
        public Point Good { get; set; }
        public Point Worst { get; set; }


        public void Sort() {
            Point temp;

            if (Best.f() > Good.f()) {
                temp = Best;
                Best = Good;
                Good = temp;
            }
            if (Good.f() > Worst.f()) {
                temp = Worst;
                Worst = Good;
                Good = temp;
            }
            if (Best.f() > Good.f()) {
                temp = Best;
                Best = Good;
                Good = temp;
            }
        }

        public Point Reflection() {
            Point mid = (Best + Good) / 2;
            Point x_r = mid + (mid - Worst);
            return x_r;
        }

        public Point Expansion() {
            Point mid = (Best + Good) / 2;
            Point x_r = mid + (mid - Worst);
            Point x_e = mid + (x_r - mid) * 2;
            return x_e;
        }

        public Point Contract() {
            Point mid = (Best + Good) / 2;
            Point x_c = mid + (Worst - mid) / 2;
            return x_c;
        }

        public void Shrink() {
            Good = Best + (Good - Best) / 2;
            Worst = Best + (Worst - Best) / 2;
        }

        public Simplex(Point best, Point good, Point worst) {
            Best = best;
            Good = good;
            Worst = worst;
            Sort();
        }

        public object Clone() {
            return new Simplex(Best, Good, Worst);
        }

        public override string ToString() {
            string text = "Best: " + Best.ToString() + ", Good: " + Good.ToString() + ", Worst: " + Worst.ToString();
            return text;
        }
    }
}