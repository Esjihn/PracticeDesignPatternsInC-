using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Strategies
{
    /**
     * Consider the quadratic equation and its canonical solution.
     *      Formula) ax^2 + bx + c = 0
     *
     *                    -b +- sqrt(b^2 - 4ac)
     *      Solution) x = ----------------------
     *                            2a
     *
     * The part b^2 - 4*a*c is called the discriminant. Suppose we want to provide an API with two
     * different strategies for calculating the discriminant:
     * 1) In OrdinaryDiscriminateStrategy, If the discriminate is negative, we return it as-is. This
     *      is OK, since our main API returns Complex.
     * 2) In RealDiscriminatingStrategy, if the discriminant is negative, the return value is NaN
     *      (not a number). NaN propagates throughout the calculation, so the equation solver gives
     *      two NaN values.
     *
     * Please implement both of these strategies as well as the equation solver itself. Regarding plus-minus
     * in the formula, please return the + result as the first element and - as the second.
     */

    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var result = (b * b) - (4 * a * c);

            if (result < 0)
                return result;

            return Double.NaN;
        }
    }

    public class memberDTO
    {
        public string name;
        public int? value;
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo (return NaN on negative discriminant!)
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var result = (b * b) - (4 * a * c);

            if (result > 0)
                return result;

            return Double.NaN;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy _strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this._strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var result = (b * b) - (4 * a * c);
            var answer1 = ((-1) * b + Math.Sqrt(result)) / (2 * a);
            var answer2 = ((-1) * b - Math.Sqrt(result)) / (2 * a);

            return new Tuple<Complex, Complex>(answer1,answer2);
        }
    }

    public class MathematicalStrategyTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            // Static Strategy
            IDiscriminantStrategy ods = new OrdinaryDiscriminantStrategy();
            IDiscriminantStrategy rds = new RealDiscriminantStrategy();

            Console.WriteLine($"Ordinary Discriminate Strategy: {ods.CalculateDiscriminant(10, 30, 0)}");
            Console.WriteLine($"Real Discriminate Strategy: {rds.CalculateDiscriminant(10, 30, 0)}");

            var iods = new QuadraticEquationSolver(ods);
            var irds = new QuadraticEquationSolver(rds);

            Console.WriteLine($"Solve Ordinary: {iods.Solve(10, 30, 0)}");
            Console.WriteLine($"Solve Real: {irds.Solve(10,30,0)}");
        }
    }
}
