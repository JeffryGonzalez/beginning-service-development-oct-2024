# The Software Center

We are building an app for our companie's in-house software entitlement team. 

They want an API (and an app, for another class) that allows them to add software to the catalog of supported software at this "company".

We will do that part in later classes.


## The Help Desk

We want a way for employees to submit challenges they have with supported software (that they are entitled to) at our company.

They can provide a description of the issue they are having, and add other information which will help the help desk staff "triage" the concern.

Is it an inconveniennce?
Is it stopping them from doing their work?
Is it causing problems with the company beinng able to fulfill it's mission?

Operation: Add Issue
Operands:
    - "WHO" - "employees" - Authorization, bearer 
        - reference to data we don't own.
    - software - the id of something from our software catalog
    - description - "dang thing won't start"


GET /software  - every piece of software in our catalog

GET /user/software - the subset of /software that the calling user has access to.

POST /user/software/{idOfSoftware}/issues
Authorization: bearer 389389.3898983.989898
Content-Type: application/json

{

    "description": "Thing broke"
}

`service.AddIssue("x9999", "word-2023", "I want clippy back")` - POST /issues


Resources - "Important thingies" - nouns.
    - Collections (/employees)
    - Documents (/employees/bob-smith) (here bob smith is a subordinate resource of the employees collection)

- GET - retrieve a representation of this resource (safe, cacheable)
- POST - on a collection, please consider making this data a part of your collection, (on a document, it means "process this"), it's a utility drawer.
- PUT - PUT IS NOT UPDATE. Put is "replace the resource at this URL with this new representation" PUT /employees
- DELETE - if successful, this resource should no longer be exposed from the API. DELETE /employees/bob-smith


### Status

We also want to have a way for employees to check the "status" of the help desk as a whole.

What is the backlog for getting help with an issue?

Who can I contact if it is an emergency?


Response:

Status: Good | Degraded | Failing 

Backlog: {
    numberOfIssuesNotChecked: 99,
    numberofIssuesInProcess: 22,
}

emergencyContact: {
    name: "Jennifer",
    email: j@aol.com,
    phone: 555-1212
}