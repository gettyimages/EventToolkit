# EventToolkit

Succinct, dependency-free, ioc-friendly utilities for publishing / subscribing to events within the application domain.

## Events

Events aid decoupling and separation of concerns.

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

Moving non-essential code such as logging into event handlers can isolate your core domain from infrastructure concerns.

    public class CustomerMakesPurchaseHandler : Handles<OrderPaid> {
        readonly ILogger logger;

        public CustomerMakesPurchaseHandler(ILogger logger) {
            this.logger = logger;
        }

        protected override void Handle(OrderPaid message) {
            logger.Log(message);
        }
    }

## Transactions

Events raised within a *TransactionScope* will be raised once the transaction is completed, or lost if the transaction is cancelled.

        EventBus.Subscribe<FeePaidOff>(e => Console.Log("Fee paid off!"));

        using (var trx = new TransactionScope()) {
            EventBus.Publish<FeePaidOff>(new FeePaidOff { Fee = this });
            Console.Log("Committing transaction...");
            trx.Complete();
        }

        // prints:
        // Comitting transaction...
        // Fee paid off!

## Thanks

* [Udi Dahan](http://www.udidahan.com) who introduced me to [Domain Events](http://www.udidahan.com/2009/06/14/domain-events-salvation/)
* [Jimmy Bogard](http://lostechies.com/jimmybogard) for a tactical [introduction to Domain Events](http://lostechies.com/jimmybogard/2010/04/08/strengthening-your-domain-domain-events/)
* [sapiens](http://github.com/sapiens) for [DomainEventsToolkit](http://github.com/sapiens/DomainEventsToolkit),
  the inspiration for EventToolkit.

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Added some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create new Pull Request