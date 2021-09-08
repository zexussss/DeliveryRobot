using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DeliveryPoints[] area = DeliveryPoints.GetDeliveryPoints();
            DeliveryRobot robot = new DeliveryRobot();
            robot.Delivery(area);
        }
    }

    class DeliveryPoints
    {
        public int XPositionDeliveryPoint;
        public int YPositionDeliveryPoint;
        public DeliveryPoints()
        {
            XPositionDeliveryPoint = 0;
            YPositionDeliveryPoint = 0;
        }
        public static DeliveryPoints[] GetDeliveryPoints()
        {
            string defaultData = Console.ReadLine();
            if (defaultData != null)
            {
                Regex deliveryPointsPattern = new Regex(@"(\d,\s*\d)"); //pattern to select all delivery points
                MatchCollection deliveryPointsCount = deliveryPointsPattern.Matches(defaultData); //getting all coords of delivery points
                DeliveryPoints[] tempArea = new DeliveryPoints[deliveryPointsCount.Count]; //creating array of coords
                try
                {
                    string tempString = Regex.Replace(defaultData, @"[^0-9]+", ""); //removing all symbols from string except numbers
                    int counter = 2;
                    for (int i = 0; i < tempArea.Length; i++) //filling array of coords
                    {
                        tempArea[i] = new DeliveryPoints();                                                                  
                        tempArea[i].XPositionDeliveryPoint = Convert.ToInt32(Convert.ToString(tempString[counter]));
                        tempArea[i].YPositionDeliveryPoint = Convert.ToInt32(Convert.ToString(tempString[counter + 1]));        
                        counter += 2;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return tempArea;
            }
            else return null;
        }
    }
    class DeliveryRobot
    {
        public bool IsDelivered;
        public int XPosition;
        public int YPosition;

        public DeliveryRobot()
        {
            XPosition = 0;
            YPosition = 0;
            IsDelivered = false;
        }
        
        public void MoveNorth() //function of moving up
        {
            YPosition++;
            Console.Write("N");
        }
        public void MoveSouth() //function of moving down
        {
            YPosition--;
            Console.Write("S");
        }
        public void MoveEast() //function of moving right
        {
            XPosition++;
            Console.Write("E");
        }
        public void MoveWest() //function of moving left
        {
            XPosition--;
            Console.Write("W");
        }
        public void DropPizza() //function of dropping pizza
        {
            IsDelivered = true;
            Console.Write("D");
        }

        public void Delivery(DeliveryPoints[] area) //function to deliver pizza
        {
            for (int i = 0; i < area.Length; i++) //cycle of delivering pizza
            {
                while (!IsDelivered)
                {
                    if (XPosition == area[i].XPositionDeliveryPoint &&
                        YPosition == area[i].YPositionDeliveryPoint)
                    {
                        DropPizza();
                    }
                    if (XPosition > area[i].XPositionDeliveryPoint)
                    {
                        MoveWest();
                    }
                    if (XPosition < area[i].XPositionDeliveryPoint)
                    {
                        MoveEast();
                    }
                    if (YPosition > area[i].YPositionDeliveryPoint)
                    {
                        MoveSouth();   
                    }
                    if (YPosition < area[i].YPositionDeliveryPoint)
                    {
                        MoveNorth();
                    }
                }
                IsDelivered = false;
            }
        }
    }
}