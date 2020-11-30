using System.Collections.Generic;
using System;
using System.Linq;

namespace AlanZucconi.AI.FSM
{
    [Serializable]
    public class FiniteStateMachine<T>
        where T : struct, IConvertible
    {
        [Serializable]
        public struct Transition
        {
            public T TargetState;
            public Func<bool> Condition;
            public Action Action; // Performed when the transition is triggered

            public Transition (T targetState, Func<bool> condition, Action action)
            {
                TargetState = targetState;
                Condition = condition;
                Action = action;
            }
        }

        private T CurrentState;

        private Dictionary<T, Action> Actions = new Dictionary<T, Action>();
        private Dictionary<T, List<Transition>> Transitions = new Dictionary<T, List<Transition>>();

        public FiniteStateMachine (T initialState)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Generic argument T must be an enum!");

            CurrentState = initialState;
        }

        public void AddAction(T state, Action action)
        {
            Actions.Add(state, action);
        }
        public void AddTransition (T from, T to, Func<bool> condition, Action action = null)
        {
            // Adds the new transition lists if not present
            List<Transition> transitions;
            if ( ! Transitions.TryGetValue(from, out transitions) )
            {
                transitions = new List<Transition>();
                Transitions.Add(from, transitions);
            }
            transitions.Add(new Transition (to, condition, action) );
        }



        // FSM[State.Value] = actionDelegate;
        public Action this[T state]
        {
            set
            {
                AddAction(state, value);
            }
        }
        // FSM[State.ValueFrom, State.ValueTo] = conditionDelegate;
        public Func<bool> this[T stateFrom, T stateTo]
        {
            set
            {
                AddTransition(stateFrom, stateTo, value);
            }
        }



        public void Update()
        {
            // Current state action
            Action action;
            if (Actions.TryGetValue(CurrentState, out action))
                action();

            // Transitions
            List<Transition> transitions;
            if (Transitions.TryGetValue(CurrentState, out transitions))
            {
                // Takes the first one
                Transition transition = transitions.FirstOrDefault(t => t.Condition());
                if (transition.Condition != null)
                {
                    if (transition.Action != null)
                        transition.Action();
                    CurrentState = transition.TargetState;
                }
            }
        }
    }
}