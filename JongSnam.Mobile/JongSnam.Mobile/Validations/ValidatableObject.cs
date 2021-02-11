using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JongSnam.Mobile.Validations
{
    public class ValidatableObject<T> : BindableObject
    {
        private List<string> _errors;
        private T _value;
        private bool _isValid;

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            Validations = new List<IValidationRule<T>>();
        }

        public List<string> Errors
        {
            get => _errors;
            set
            {
                _errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public List<IValidationRule<T>> Validations { get; }

        public bool Validate()
        {
            Errors.Clear();

            var errors = Validations.Where(v => !v.Check(Value)).Select(v => v.ValidationMessage);

            Errors = errors.ToList();

            IsValid = !Errors.Any();

            return IsValid;
        }

        public void Reset()
        {
            Value = default(T);
            Errors.Clear();
            IsValid = true;
        }
    }
}
