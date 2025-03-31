Feature: Create Account
	Create IMDB account

Scenario: Accept cookies
	Given I am on the IMDB homepage
	And I see the cookie prompt
	When I click on Accept
	Then the cookie prompt is gone
	

	