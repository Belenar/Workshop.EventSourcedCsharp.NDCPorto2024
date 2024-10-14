using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.CommandHandlers;

namespace BeerSender.Tests.Boxes;

public class CreateBoxTest : BoxTest<CreateBox>
{
    [InlineData(0, 6)]
    [InlineData(5, 6)]
    [InlineData(6, 6)]
    [InlineData(7, 12)]
    [InlineData(11, 12)]
    [InlineData(12, 12)]
    [InlineData(13, 24)]
    [InlineData(23, 24)]
    [InlineData(24, 24)]
    [Theory]
    public void WhenCreatedWithValidCapacity_ShouldCreateBox(
        int desired_spots, int actual_spots)
    {
        Given(
        );

        When(
            Create_box_for_capacity(desired_spots)
        );

        Then(
            Box_created_with_capacity(actual_spots)
        );
    }

    [Fact]
    public void WhenCreatedWithInvalidCapacity_ShouldFail()
    {
        Given(
            Box_created_with_capacity(24)
        );

        When(
            Create_box_for_capacity(6)
        );

        Then(
            Box_was_already_created()
        );
    }

    [Fact]
    public void WhenUsingInvalidCapacity_ShouldFail()
    {
        Given(
        );

        When(
            Create_box_for_capacity(25)
        );

        Then(
            Invalid_desired_capacity()
        );
    }

    protected override CommandHandler<CreateBox> Handler
        => new BoxCreator(eventStore);
}