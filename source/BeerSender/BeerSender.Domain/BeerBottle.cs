using System.Text;

namespace BeerSender.Domain
{
    public class BeerBottle
    {
        public string Name { get; set; }
        public double AlcoholPercentage { get; set; }
        public BeerType BeerType { get; set; }
        public string Brewery { get; set; }
    }
}