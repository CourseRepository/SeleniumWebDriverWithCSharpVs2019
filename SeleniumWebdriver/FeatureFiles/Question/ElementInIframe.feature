Feature: Element In Iframe
	TO locate the element which is in Iframe


Scenario: Locate the element in IFrmae
	Given I open the web page with url "https://manage.develop.camilyo.net/app/login/sign-in"
	And I wait for page to loaded completely
	Then I switch to iframe with id "frame"
	And Locate the element with id "ember10"
	Then Entre the information "abc@gmail.com"
