Feature: DiscountCalculation

Scenario: Calculate discount after purchasing enough products for the discount and having enough value purchased in previous orders
Given I have an account with order history of total value of 500
And I make a purchase which is worth 200
Then I get a 15% discount 