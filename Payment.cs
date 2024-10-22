using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore
{
    public abstract class Payment
    {
        public abstract void ProcessPayment(decimal amount);
    }

    public class PaypalPayment : Payment
    {
        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Paid {amount:C} using PayPal.");
        }
    }

    public class CreditCardPayment : Payment
    {
        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Paid {amount:C} using Credit Card.");
        }
    }

}
