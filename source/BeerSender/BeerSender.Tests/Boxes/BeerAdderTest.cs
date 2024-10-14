using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.CommandHandlers;

namespace BeerSender.Tests.Boxes
{
    public class BeerAdderTest : BoxTest<AddBeerBottle>
    {
        [Fact]
        public void WhenAddedToEmptyBox_ShouldAddBottle()
        {
            Given(
                Box_added_with_capacity(24)
            );

            When(
                Add_beer_bottle_to_box(gouden_carolus)
            );

            Then(
                Beer_bottle_added(gouden_carolus)
            );
        }

        protected override CommandHandler<AddBeerBottle> Handler
            => new BeerAdder(eventStore);
    }
}