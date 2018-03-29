//
// CricleEffect.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using Xamarin.Forms;
namespace FoundIt.Effects
{
    public class CircleEffect : RoutingEffect
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        public double Radius { get; set; }

        public CircleEffect() : base("FoundIt.CircleEffect")
        {
        }
    }
}
