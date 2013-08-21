using System;
using System.Transactions;

namespace EventToolkit
{
    class TransactionNotification : IEnlistmentNotification
    {
        readonly Action eventSender;

        public TransactionNotification(Action eventSender)
        {
            this.eventSender = eventSender;
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }

        public void Commit(Enlistment enlistment)
        {
            eventSender();
            enlistment.Done();
        }

        public void Rollback(Enlistment enlistment)
        {
            enlistment.Done();
        }

        public void InDoubt(Enlistment enlistment)
        {
            enlistment.Done();
        }
    }
}