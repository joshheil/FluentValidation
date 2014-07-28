using System;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Machine.Specifications;

namespace FluentValidationPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime DOB = new DateTime(2013,12,09);

            Person person = new Person(){DateofBirth = DOB,FirstName = "Josh", LastName = "Heil"};
            PersonValidator personValidator = new PersonValidator();
            ValidationResult validationResult = personValidator.Validate(person, ruleSet: "RequiredFields");
            if (validationResult.IsValid)
                Console.WriteLine("Looks Good");
            else 
                Console.WriteLine("Didn't Validate");

            Console.ReadLine();
        }
    }

    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateofBirth { get; set; }
    }


    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleSet("RequiredFields", RequiredFieldsValidatorForNurse);
        }

        private void RequiredFieldsValidatorForNurse()
        {
            RuleFor(person => person.LastName).NotEmpty();
            RuleFor(person => person.FirstName).NotEmpty();
        }
    }

    public class when_validating_an_empty_person
    {
        private static PersonValidator personValidator;
        private static ValidationResult validationResult;

        private Establish context = () =>
                                    {
                                        personValidator = new PersonValidator();
                                        validationResult = new ValidationResult();
                                    };

        Because of = () => validationResult = personValidator.Validate(new Person(), ruleSet: "RequiredFields");

        private It should_fail_validation = () => validationResult.IsValid.ShouldEqual(false);
    }


    public class when_validating_a_person_populated_with_only_first_name
    {
        private static PersonValidator personValidator;
        private static ValidationResult validationResult;

        private Establish context = () =>
        {
            personValidator = new PersonValidator();
            validationResult = new ValidationResult();
        };

        Because of = () => validationResult = personValidator.Validate(new Person(){DateofBirth = new DateTime(1928, 12,21), FirstName = "John", LastName = null}, ruleSet: "RequiredFields");

        It should_still_fail_validation = () => validationResult.IsValid.ShouldEqual(false);
    }

    public class when_validating_a_populated_person
    {
        private static PersonValidator personValidator;
        private static ValidationResult validationResult;

        private Establish context = () =>
        {
            personValidator = new PersonValidator();
            validationResult = new ValidationResult();
        };

        Because of = () => validationResult = personValidator.Validate(new Person() { DateofBirth = new DateTime(1928, 12, 21), FirstName = "John", LastName = "Doe" }, ruleSet: "RequiredFields");

        It should_pass_validation = () => validationResult.IsValid.ShouldEqual(true);
    }
 }
