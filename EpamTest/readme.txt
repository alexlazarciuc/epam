 The chosen approach is listed below:
 I decided to split the tests in Unit and Component testing given the UI is not available at this poomt sp I will focus only on the mocked api side and unit test.
 Unit Test will cover the core domain logic inside the StudyGroup entity (validation, user management, creation timestamp).
 Component itself will validate that the controller enforces business rules via mocked repositories (uniqueness, filtering, joining, leaving).

 High-level Context for unit tests:
 Assumptions:
 - The StudyGroup constructor is responsible for enforcing validation: name length, valid subject, and timestamp.
 - Users are added or removed via AddUser() / RemoveUser() methods.
 - Subject is an enum, only allowing Math, Chemistry, and Physics.
 - The CreateDate should reflect the time of creation accurately.

 Input Highlights:
 - Name inputs vary in length to test 1a.
 - Subject input includes both valid enum values and an invalid integer cast.
 - Users are passed as User objects with IDs and names.
 - CreateDate is tested using DateTime.Now.

 High-level Context for component tests:
 Assumptions:
 - Controller methods are responsible for enforcing business rules using repository data (e.g., uniqueness per subject).
 - Repository is mocked, so we're validating controller behavior, not database logic.
 - Responses are returned as IActionResult and should be interpreted accordingly.
 - GetStudyGroups can be used to test sorted and unsorted return values.

 Input Highlights:
 - Valid StudyGroup objects with varying names, subjects, and creation dates.
 - Mocked repository returns empty or pre-populated lists to simulate scenarios. For this I used Moq library to mock the repository behavior.
 - UserId and StudyGroupId are passed directly to simulate join/leave behavior.

 The solution is structured to ensure that both unit and component tests are isolated from external dependencies, focusing on the logic within the StudyGroup entity and the controller's interaction with the repository.
 The sql query is added under the TestApp/Sql folder as sqlQuery.txt for reference, which can be used to validate the SQL logic against the expected results in the database.