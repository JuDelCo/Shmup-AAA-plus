using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public EnergyComponent energy { get { return (EnergyComponent)GetComponent(ComponentIds.Energy); } }

        public bool hasEnergy { get { return HasComponent(ComponentIds.Energy); } }

        static readonly Stack<EnergyComponent> _energyComponentPool = new Stack<EnergyComponent>();

        public static void ClearEnergyComponentPool() {
            _energyComponentPool.Clear();
        }

        public Entity AddEnergy(float newLevel, float newDepletedUntil) {
            var component = _energyComponentPool.Count > 0 ? _energyComponentPool.Pop() : new EnergyComponent();
            component.level = newLevel;
            component.depletedUntil = newDepletedUntil;
            return AddComponent(ComponentIds.Energy, component);
        }

        public Entity ReplaceEnergy(float newLevel, float newDepletedUntil) {
            var previousComponent = hasEnergy ? energy : null;
            var component = _energyComponentPool.Count > 0 ? _energyComponentPool.Pop() : new EnergyComponent();
            component.level = newLevel;
            component.depletedUntil = newDepletedUntil;
            ReplaceComponent(ComponentIds.Energy, component);
            if (previousComponent != null) {
                _energyComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveEnergy() {
            var component = energy;
            RemoveComponent(ComponentIds.Energy);
            _energyComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherEnergy;

        public static AllOfMatcher Energy {
            get {
                if (_matcherEnergy == null) {
                    _matcherEnergy = new Matcher(ComponentIds.Energy);
                }

                return _matcherEnergy;
            }
        }
    }
}
