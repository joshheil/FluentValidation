using System;
using FluentValidation;
using FluentValidation.Results;

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
 }
