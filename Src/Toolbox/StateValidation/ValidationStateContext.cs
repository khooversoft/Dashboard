using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Extensions;
using Toolbox.Tools;

namespace Toolbox.StateValidation
{
    public class ValidationStateContext
    {
        private readonly ConcurrentDictionary<string, Element> _elements = new ConcurrentDictionary<string, Element>(StringComparer.OrdinalIgnoreCase);
        private readonly Action _stateHasChange;
        private readonly Func<IReadOnlyList<ValidationMessage>> _validate;

        public ValidationStateContext(Func<IReadOnlyList<ValidationMessage>> validate, Action stateHasChange)
        {
            validate.VerifyNotNull(nameof(validate));
            stateHasChange.VerifyNotNull(nameof(stateHasChange));

            _validate = validate;
            _stateHasChange = stateHasChange;
        }

        public (ValidationState State, string? ErrorMsg) Get(string id)
        {
            Element element = GetElement(id);
            return (element.State, element.ErrorMsg);
        }

        public ValidationStateContext Reset() => this.Action(_ => _elements.Values.ForEach(x => x.State = ValidationState.Reset));

        public ValidationStateContext Set(string id, Action<object> setAction)
        {
            ValidateId(id);
            setAction.VerifyNotNull(nameof(setAction));

            var element = new Element
            {
                Id = id,
                SetAction = setAction,
            };

            _elements[id] = element;
            return this;
        }

        public ValidationStateContext SetState(string id, ValidationState state, string? errorMsg)
        {
            Element element = GetElement(id);

            element!.State = state;
            element.ErrorMsg = errorMsg;

            return this;
        }

        public ValidationStateContext Submit(string id, object value)
        {
            value.VerifyNotNull(nameof(value));

            Element element = GetElement(id);

            element!.State = ValidationState.Fail;
            element.SetAction(value);

            IReadOnlyList<ValidationMessage> validationResult = _validate()
                .VerifyNotNull(nameof(validationResult));

            _elements.Values
                .ForEach(x => x.State = x.State switch
                {
                    ValidationState.Reset => x.State,

                    _ => ValidationState.Pass
                });

            foreach (var item in validationResult)
            {
                SetState(item.Key, ValidationState.Fail, item.ErrorMsg);
            }

            _stateHasChange();
            return this;
        }

        private static void ValidateId(string id) => id
            .VerifyNotEmpty(nameof(id))
            .VerifyAssert(x => !x.Any(y => char.IsLetterOrDigit(y) || y == '.'), "Invalid");

        private Element GetElement(string id)
        {
            ValidateId(id);

            _elements.TryGetValue(id, out Element? element)
               .VerifyAssert(x => x == true, "Element not found");

            return element!;
        }


        private class Element
        {
            public string? ErrorMsg { get; set; }
            public string Id { get; init; } = null!;
            public Action<object> SetAction { get; init; } = null!;
            public ValidationState State { get; set; } = ValidationState.Reset;
        }
    }
}
