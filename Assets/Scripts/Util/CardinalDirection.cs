
namespace RectangleTrainer.Compass
{
    public class CardinalDirections
    {
        public static Direction[] directions = {
            new() {Name = "ั" , Degrees =   0},
            new() {Name = "ัย", Degrees =  45},
            new() {Name = "ย" , Degrees =  90},
            new() {Name = "Þย", Degrees = 135},
            new() {Name = "Þ" , Degrees = 180},
            new() {Name = "Þว", Degrees = 225},
            new() {Name = "ว" , Degrees = 270},
            new() {Name = "ัว", Degrees = 315}
        };
            
        public struct Direction
        {
            public float Degrees;
            public string Name;
        }
    }
}