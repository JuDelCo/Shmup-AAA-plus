using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputStringComponent inputString { get { return (InputStringComponent)GetComponent(ComponentIds.InputString); } }

        public bool hasInputString { get { return HasComponent(ComponentIds.InputString); } }

        static readonly Stack<InputStringComponent> _inputStringComponentPool = new Stack<InputStringComponent>();

        public static void ClearInputStringComponentPool() {
            _inputStringComponentPool.Clear();
        }

        public Entity AddInputString(string newValue) {
            var component = _inputStringComponentPool.Count > 0 ? _inputStringComponentPool.Pop() : new InputStringComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.InputString, component);
        }

        public Entity ReplaceInputString(string newValue) {
            var previousComponent = hasInputString ? inputString : null;
            var component = _inputStringComponentPool.Count > 0 ? _inputStringComponentPool.Pop() : new InputStringComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.InputString, component);
            if (previousComponent != null) {
                _inputStringComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputString() {
            var component = inputString;
            RemoveComponent(ComponentIds.InputString);
            _inputStringComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherInputString;

        public static AllOfMatcher InputString {
            get {
                if (_matcherInputString == null) {
                    _matcherInputString = new Matcher(ComponentIds.InputString);
                }

                return _matcherInputString;
            }
        }
    }
}
