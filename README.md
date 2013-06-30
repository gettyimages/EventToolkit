
# DomainToolkit

Succinct, dependency-free, ioc-friendly utilities for working with a domain-centric design paradigm.

## Domain Events

Domain Events aid decoupling and separation of concerns. 

    public class Fee {
        public RecordPayment(amountPaid) {
          if (amountPaid >= amountOwed)
            EventBus.Publish(new FeePaidOff { Fee = this });
        }
    }

    [Test]
    public void Should_notify_when_the_balance_is_paid_off() {
        Fee paidOffFee = null;
        EventBus.Subscribe<FeePaidOff>(e => paidOffFee = e.Fee);

        var customer = new Customer();
        var fee = customer.ChargeFee(100m);
        fee.RecordPayment(100m);

        paidOffFee.ShouldEqual(fee);
    }

## Repositories / Unit of Work

*Incomplete...*

## Thanks

* [Udi Dihan](http://www.udidahan.com) who introduced me to [Domain Events](http://www.udidahan.com/2009/06/14/domain-events-salvation/)
* [Jimmy Bogard](http://lostechies.com/jimmybogard) for a tactical [introduction to Domain Events](http://lostechies.com/jimmybogard/2010/04/08/strengthening-your-domain-domain-events/)
* [sapiens](http://github.com/sapiens) for [DomainEventsToolkit](http://github.com/sapiens/DomainEventsToolkit),
  the inspiration for DomainToolkit.

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Added some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create new Pull Request
