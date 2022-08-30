using shapesquiz_API.Enums;

namespace shapesquiz_API.Helpers
{
   public static class EnumHelpers
   {
      public static string NameByShapeProperty(ShapeProperty wProperty)
      {
         return (wProperty) switch
         {
            ShapeProperty.Unknown => string.Empty,
            ShapeProperty.Height => "height",
            ShapeProperty.Width => "width",
            ShapeProperty.Radius => "radius",
            ShapeProperty.Side => "side",
            _ => throw new NotImplementedException(),
         };
      }
   }
}
