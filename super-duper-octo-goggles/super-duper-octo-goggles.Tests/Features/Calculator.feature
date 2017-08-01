Feature: Calculator
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I have entered <first> into the calculator
	When I have entered <second> into the calculator
	Then the result should be 120 on the screen

@Calculator 
Scenario Outline: Add two numbers
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I have entered <first> into the calculator
	When I have entered <second> into the calculator
	Then the result should be <result> on the screen
	
	Examples: 
	| first | second | result |
	| 50    | 70     | 120    |
