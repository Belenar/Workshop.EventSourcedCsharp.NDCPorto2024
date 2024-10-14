using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.CommandHandlers;

namespace BeerSender.Tests.Boxes;

public class AddShippingLabelTest : BoxTest<AddShippingLabel>
{
    [Theory]
    [InlineData(Carrier.Ups, "ABC123")]
    [InlineData(Carrier.Ups, "ABC999")]
    [InlineData(Carrier.FedEx, "DEF123")]
    [InlineData(Carrier.FedEx, "DEF999")]
    [InlineData(Carrier.Dhl, "GHI123")]
    [InlineData(Carrier.Dhl, "GHI999")]
    public void WhenUsingValidLabel_ShouldAddLabel(
        Carrier carrier, string tracking_code)
    {
        Given(
            Box_created_with_capacity(24)
        );

        When(
            Add_label_with_carrier_and_code(carrier, tracking_code)
        );

        Then(
            Shipping_label_added_with_carrier_and_code(carrier, tracking_code)
        );
    }

    [Theory]
    [InlineData(Carrier.Ups, "AB123")]
    [InlineData(Carrier.Ups, "ZZZ999")]
    [InlineData(Carrier.FedEx, "DE123")]
    [InlineData(Carrier.FedEx, "ZZZ999")]
    [InlineData(Carrier.Dhl, "GH123")]
    [InlineData(Carrier.Dhl, "ZZZ999")]
    public void WhenUsingInvalidLabel_ShouldFail(
        Carrier carrier, string tracking_code)
    {
        Given(
            Box_created_with_capacity(24)
        );

        When(
            Add_label_with_carrier_and_code(carrier, tracking_code)
        );

        Then(
            Label_was_invalid()
        );
    }

    protected override CommandHandler<AddShippingLabel> Handler
        => new ShippingLabelAdder(eventStore);
}