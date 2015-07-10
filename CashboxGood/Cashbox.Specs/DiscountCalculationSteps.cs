using System;
using System.Linq;
using Cashbox.DataAccess;
using Cashbox.Models;
using Cashbox.Services;
using TechTalk.SpecFlow;
using NUnit.Framework;
using FakeItEasy;

namespace Cashbox.Specs
{
    [Binding]
    public class DiscountCalculationSteps
    {
        private Account _account;
        private Order _order;
        private decimal _result;

        [Given(@"I have an account with order history of total value of (.*)")]
        public void GivenIHaveAnAccountWithOrderHistoryOfTotalValueOf(int total)
        {
            _account = new Account { Id = 1 };
            _order = new Order { Id = 1, AccountId = 1, Total = total};
        }
        
        [Given(@"I make a purchase which is worth (.*)")]
        public void GivenIMakeAPurchaseWhichIsWorth(int value)
        {
            var orderRepository = A.Fake<IRepository<Order>>();
            A.CallTo(() => orderRepository.Query()).Returns(new[] { _order }.AsQueryable());

            var accountRepository = A.Fake<IRepository<Account>>();
            A.CallTo(() => accountRepository.Query()).Returns(new[] { _account }.AsQueryable());

            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.Repository<Account>()).Returns(accountRepository);
            A.CallTo(() => unitOfWork.Repository<Order>()).Returns(orderRepository);

            var unitOfWorkFactory = A.Fake<IUnitOfWorkFactory>();
            A.CallTo(() => unitOfWorkFactory.Create()).Returns(unitOfWork);

            var service = new PurchaseService(unitOfWorkFactory);

            _result = service.GetDiscount(_account.Id, value);
        }
        
        [Then(@"I get a (.*)% discount")]
        public void ThenIGetADiscount(int discountInPercent)
        {
            Assert.That(_result, Is.EqualTo((decimal)discountInPercent/100));
        }
    }
}
