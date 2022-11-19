using FluentValidation;

namespace Contact.Application.Person
{
    public class PersonSaveDtoValidator : AbstractValidator<PersonSaveDto>
    {
        public PersonSaveDtoValidator()
        {
            RuleFor(q => q.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Ad alanı 50 karakterden fazla olamaz");

            RuleFor(q => q.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Soyad alanı 50 karakterden fazla olamaz");

            RuleFor(q => q.Company)
                .NotEmpty().WithMessage("Şirket alanı boş olamaz")
                .MaximumLength(50).WithMessage("Şirket alanı 50 karakterden fazla olamaz");
        }
    }
}
