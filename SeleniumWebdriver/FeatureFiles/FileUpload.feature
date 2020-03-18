#@MsTest:DeploymentItem:Resources
Feature: File Upload
	File upload using MSTest deployment attribute


Scenario: File Upload with deployment attribute
	Given I nagivate to the bugzila web page
	And I login in the bugzila application
	Then I click on "Testng" link
	And I click on the add attachment button and upload the file
	And I logout from the application
