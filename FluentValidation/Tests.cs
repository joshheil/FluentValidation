using System;
using FluentValidation.Results;
using FluentValidation;
using Machine.Specifications;

namespace FluentValidationPOC
{
    public class with_the_person_validator
    {
        protected static PersonValidator personValidator;
        protected static ValidationResult validationResult;

        private Establish context = () =>
        {
            personValidator = new PersonValidator();
            validationResult = new ValidationResult();
        };
    }

    public class when_validating_an_empty_person : with_the_person_validator
    {
        Because of = () => validationResult = personValidator.Validate(new Person(), ruleSet: "RequiredFields");

        It should_fail_validation = () => validationResult.IsValid.ShouldEqual(false);
    }


    public class when_validating_a_person_populated_with_only_first_name : with_the_person_validator
    {
        Because of = () => validationResult = personValidator.Validate(new Person() { DateofBirth = new DateTime(1928, 12, 21), FirstName = "John", LastName = null }, ruleSet: "RequiredFields");

        It should_still_fail_validation = () => validationResult.IsValid.ShouldEqual(false);
    }

    public class when_validating_a_populated_person : with_the_person_validator
    {
        Because of = () => validationResult = personValidator.Validate(new Person() { DateofBirth = new DateTime(1928, 12, 21), FirstName = "John", LastName = "Doe" }, ruleSet: "RequiredFields");

        It should_pass_validation = () => validationResult.IsValid.ShouldEqual(true);
    }
}
