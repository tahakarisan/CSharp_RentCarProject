using FluentValidation;
namespace CoreLayer.CrossCuttingConcerns
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var result = validator.Validate(new ValidationContext<object>(entity));
            if (!result.IsValid)
            {
                throw  new ValidationException("İşlem başarısız");
            }
        }
    }
}
