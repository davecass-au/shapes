using shapesquiz_API.Enums;

namespace shapesquiz_API.Models
{
   public abstract class Shape
   {
      public DrawType Type { get; set; }

      public int X { get; set; }
      public int Y { get; set; }

   }

   public class Circle : Shape
   {
      public int RadiusX { get; set; }
      public int RadiusY { get; set; }
   }

   public class Rectangle : Shape
   {
      public int Height { get; set; }
      public int Width { get; set; }
   }

   public class Polygon : Shape
   {
      public int SideLength { get; set; }
      public int NumberOfSides { get; set; }
   }

   public class Unknown : Shape
   {

   }
}
