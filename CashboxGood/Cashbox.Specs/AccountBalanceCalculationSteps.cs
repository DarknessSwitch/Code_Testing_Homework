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
    public class AccountBalanceCalculationSteps
    {
        private Account _account;
        private Product _product1;
        private Product _product2;
        private decimal _result;

        [Given(@"I have an account with (.*) on balance")]
        public void GivenIHaveAnAccountWithOnBalance(decimal balance)
        {
            _account = new Account { Id = 1, Balance = balance };
        }
        
        [Given(@"I buy two products which are worth (.*) and (.*)")]
        public void GivenIBuyTwoProductsWhichAreWorthAnd(decimal price1, decimal price2)
        {
            _product1 = new Product { Id = 1, Price = price1, Amount = 1 };
            _product2 = new Product { Id = 2, Price = price2, Amount = 1 };

        }
        
        [When(@"I ask how much is the balance now")]
        public void WhenIAskHowMuchIsTheBalanceNow()
        {
            var accountRepository = A.Fake<IRepository<Account>>();
            A.CallTo(() => accountRepository.Get(A<int>._)).Returns(_account);

            var productRepository = A.Fake<IRepository<Product>>();
            A.CallTo(() => productRepository.Query()).Returns(new[] { _product1, _product2 }.AsQueryable());

            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.Repository<Account>()).Returns(accountRepository);
            A.CallTo(() => unitOfWork.Repository<Product>()).Returns(productRepository);

            var unitOfWorkFactory = A.Fake<IUnitOfWorkFactory>();
            A.CallTo(() => unitOfWorkFactory.Create()).Returns(unitOfWork);

            var service = new PurchaseService(unitOfWorkFactory);

            service.Purchase(_account.Id, new[] { _product1.Id, _product2.Id }, _product1.Price+_product2.Price);
            _result = _account.Balance;
        }
        
        [Then(@"Then I'm given ""(.*)"" as an answer")]
        public void ThenThenIMGivenAsAnAnswer(decimal expected)
        {
            Assert.That(_result, Is.EqualTo(expected));
        }
    }
}
