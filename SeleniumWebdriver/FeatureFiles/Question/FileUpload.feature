Feature: FileUpload
	To test the file upload feature


Scenario: Upload the file while creating bug
	Given I navigate to the bug zila application "http://localhost/"
	And I click on the element with locator "enter_bug"
	And I login into the application with  usernam "rahul@bugzila.com" and password "welcome"
	And I press on Login button
	And I click on "Testng" link 
	Then I click on button "attachment_false"
	And Wait for the attachment button "data" for "30" seconds
	And I upload the file "ExcelData.xlsx" present in Resources
	Then logout from the application
