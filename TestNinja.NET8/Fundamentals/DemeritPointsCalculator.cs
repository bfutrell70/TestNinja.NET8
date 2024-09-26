using System;

namespace TestNinja.NET8.Fundamentals
{
    public class DemeritPointsCalculator
    {
        private const int SpeedLimit = 65;
        private const int MaxSpeed = 300;
        
        public int CalculateDemeritPoints(int speed)
        {
            if (speed < 0 || speed > MaxSpeed) 
                throw new ArgumentOutOfRangeException($"Speed must be between 0 and {MaxSpeed}");
            
            if (speed <= SpeedLimit) return 0; 
            
            const int kmPerDemeritPoint = 5;
            var demeritPoints = (speed - SpeedLimit)/kmPerDemeritPoint;

            return demeritPoints;
        }        
    }
}