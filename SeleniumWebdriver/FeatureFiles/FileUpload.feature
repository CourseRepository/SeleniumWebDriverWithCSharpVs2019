@MsTest:DeploymentItem:Resources
Feature: File Upload
	File upload using MSTest deployment attribute


Scenario: File Upload with deployment attribute
	Given I nagivate to google webpage
	And I deploy the item present in Resource folder
	Then I verify that the file is present in required direcotry
