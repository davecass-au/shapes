using Microsoft.AspNetCore.Mvc;
using shapesquiz_API.Models;
using shapesquiz_API.Enums;
using shapesquiz_API.Helpers;

namespace shapesquiz_API.Controllers
{
   [ApiController]
   [Route("api/shape")]
   public class ShapeController : ControllerBase
   {
      [HttpGet(Name = "GetShape")]
      public Shape Get(string description)
      {
         // Split the description into a list of words 
         List<string> wordList = SplitDescription(description);

         // To be slightly dynamic, loop through the list looking for a match.
         foreach (string curWord in wordList)
         {
            switch (curWord)
            {
               case "circle":
                  return BuildCircle(wordList);
               case "oval":
                  return BuildOval(wordList);
               case "rectangle":
                  return BuildRect(wordList);
               case "triangle":
                  return BuildPolygon(wordList, 3);
               case "square":
                  return BuildPolygon(wordList, 4);
               case "pentagon":
                  return BuildPolygon(wordList, 5);
               case "hexagon":
                  return BuildPolygon(wordList, 6);
               case "heptagon":
                  return BuildPolygon(wordList, 7);
               case "octagon":
                  return BuildPolygon(wordList, 8);
            }
         }

         // If we get here then return unknown
         return BuildUnknown();
      }

      private static List<string> SplitDescription(string pDescription)
      {
         return pDescription.Split(' ').ToList().ConvertAll(x => x.ToLower());
      }

      private static Shape BuildCircle(List<string> pWords)
      {
         DescriptionProcessor curProcessor = new(pWords, new List<ShapeProperty> { ShapeProperty.Radius });

         int radius = curProcessor.GetValueByProperty(ShapeProperty.Radius);
         
         return new Circle
         {
            Type = DrawType.Ellipse,
            X = radius + 10,
            Y = radius + 10,
            RadiusX = radius,
            RadiusY = radius
         };
      }

      private static Shape BuildOval(List<string> pWords)
      {
         DescriptionProcessor curProcessor = new(pWords, new List<ShapeProperty> { ShapeProperty.Width, ShapeProperty.Height });

         int radiusX = curProcessor.GetValueByProperty(ShapeProperty.Width) / 2;
         int radiusY = curProcessor.GetValueByProperty(ShapeProperty.Height) / 2;

         return new Circle
         {
            Type = DrawType.Ellipse,
            X = radiusX + 10,
            Y = radiusY + 10,
            RadiusX = radiusX,
            RadiusY = radiusY
         };
      }

      private static Shape BuildRect(List<string> pWords)
      {
         DescriptionProcessor curProcessor = new(pWords, new List<ShapeProperty> {ShapeProperty.Width, ShapeProperty.Height});

         int widthValue = curProcessor.GetValueByProperty(ShapeProperty.Width);
         int heightValue = curProcessor.GetValueByProperty(ShapeProperty.Height);

         return new Rectangle
         {
            Type = DrawType.Rect,
            X = 10,
            Y = 10,
            Height = heightValue,
            Width = widthValue
         };
      }

      private static Shape BuildPolygon(List<string> pWords, int pNumberOfSides)
      {
         DescriptionProcessor curProcessor = new(pWords, new List<ShapeProperty> { ShapeProperty.Side });

         int sideLength = curProcessor.GetValueByProperty(ShapeProperty.Side);
         
         return new Polygon
         {
            Type = DrawType.LineTo,
            X = sideLength + 10,
            Y = sideLength + 10,
            SideLength = sideLength,
            NumberOfSides = pNumberOfSides
         };
      }

      private static Shape BuildUnknown()
      {
         return new Unknown { Type = DrawType.Unknown };
      }
   }
}