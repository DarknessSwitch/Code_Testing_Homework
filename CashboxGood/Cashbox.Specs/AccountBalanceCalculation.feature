Feature: AccountBalanceCalculation

Scenario: Calculate balance of an account after making a purchase
Given I have an account with 500 on balance
And  I buy two products which are worth 200 and 150
When I ask how much is the balance now
Then Then I'm given "150" as an answer
