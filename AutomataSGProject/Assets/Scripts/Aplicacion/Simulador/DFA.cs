using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using static UnityEngine.GraphicsBuffer;

public static class DFA
{
    public static class Alphabet
    {
        public static HashSet<char> Data { get; private set; } = new HashSet<char>();
        public static bool Add(char input)
        {
            bool output = true;
            char[] exceptions = {'{','}',',','e',' ','-'};
            foreach (char iteration in exceptions)
            {
                if(input == iteration)
                {
                    output = false;
                }
            }
            if(output )
            {
                Data.Add(input);
                Debug.Log("The symbol " + input + " has been added to the current alphabet.");
            }
            return output;
        }
        public static bool Remove(char input)
        {
            bool output = true;
            if(!Data.Contains(input))
            {
                output = false;
                Debug.LogError("The symbol " + input + " is not declare in the current alphabet.");
            }
            if(output)
            {
                foreach(string iteration in States.Data)
                {
                    Transitions.Remove(new Transition() {
                        State = iteration, Symbol = input 
                    });
                }
                Data.Remove(input);
                Debug.Log("The symbol " + input + " has been remove from the current alphabet.");
            }
            return output;
        }
        public static void Clear()
        {
            Data.Clear();
            Debug.Log("The alphabet has been cleared.");
        }
    }
    public static class States
    {
        public static HashSet<string> Data { get; private set; } = new HashSet<string>();
        public static bool Add(string input)
        {
            bool output = true;
            if (Data.Contains(input.ToLower()))
            {
                output = false;
                Debug.LogError("The state " + Data + " is already part of the current States list.");
            }
            if (output)
            {
                Data.Add(input.ToLower());
                Debug.Log("The state "+input+" has been added to the current States list.");
            }
            return output;
        }
        public static bool Remove(string input)
        {
            bool output = true;
            if (!Data.Contains(input.ToLower()))
            {
                output = false;
                Debug.LogError("The state "+input+" is not part of the current States list.");
            }
            if (output)
            {
                foreach(char iteration in Alphabet.Data)
                {
                    Transitions.Remove(new Transition() { State=input, Symbol=iteration});
                }
                Data.Remove(input);
                Debug.Log("The state " + input + " has beeen removed from the current States list.");
            }
            return output;
        }
    }
    public static class Transitions
    {
        public static Dictionary<(string State, char Symbol), string> Data { get; private set; }
            = new Dictionary<(string state, char symbol), string>();
        public static bool Contain(Transition input)
        {
            bool output = true;
            if (!Data.ContainsKey((input.State, input.Symbol)))
            {
                output = false;
                //Debug.LogError("The transition (" + input.State + "," + input.Symbol + ") is not a valid transition ");
            }
            return output;
        }
        public static bool Add(Transition input)
        {
            bool output=true;
            if (Alphabet.Data.Count <= 0)
            {
                Debug.LogError("You do not be able to add a transition without set an alphabet before.");
                output = false;
            }
            if (States.Data.Count <= 0)
            {
                Debug.LogError("You do not be able to add a transition without set some states before.");
                output = false;
            }
            if (!States.Data.Contains(input.State))
            {
                Debug.LogError("The state " + input.State + " was not declareted in the states list.");
                output = false;
            }
            if (!States.Data.Contains(input.FinalState))
            {
                Debug.LogError("The state " + input.FinalState + " was not declareted in the states list.");
                output = false;
            }
            if (!Alphabet.Data.Contains(input.Symbol))
            {
                Debug.LogError("The symbol " + input.Symbol + " was not declareted in the alphabet.");
                output = false;
            }
            if (Data.ContainsKey((input.State, input.Symbol)))
            {
                Debug.LogError("The transition (" + input.State + "," + input.Symbol + ") it already exist, you do not be able to add the same transition twice.");
                output = false;
            }
            if (output)
            {
                Data.Add((input.State, input.Symbol), input.FinalState);
                Debug.Log("The transition (" + input.State + "," + input.Symbol + "):" + input.FinalState + " has been added.");
            }
            return output;
        }
        public static bool Remove(Transition input)
        {
            bool output = true;
            if (!States.Data.Contains(input.State))
            {
                Debug.LogError("The state " + input.State + " is not valid.");
                output = false;
            }
            if (!Alphabet.Data.Contains(input.Symbol))
            {
                Debug.LogError("The symbol " + input.Symbol + " is not a valid symbol.");
                output = false;
            }
            output = Contain(input);
            if (output)
            {
                Data.Remove((input.State, input.Symbol));
                Debug.Log("The transition (" + input.State + "," + input.Symbol + " has been removed.");
            }
            return output;
        }
        public static void Clear()
        {
            Data.Clear();
            Debug.Log("The transitions list has been cleared.");
        }
        public static List<Transition> Get() 
        {
            List<Transition> output = new List<Transition>();
            foreach(var iteration in Data)
            {
                output.Add(new Transition()
                {
                    State = iteration.Key.State, 
                    Symbol = iteration.Key.Symbol,
                    FinalState = iteration.Value
                });
            }
            return output;
        }
    }
    public static class FinalSates
    {
        public static HashSet<string> Data { get; private set; } = new HashSet<string>();
        public static bool Add(string input)
        {
            bool output = true;
            if (States.Data.Count <= 0)
            {
                Debug.LogError("You do not be able to add a final state without set some states before.");
                output = false;
            }
            if (!States.Data.Contains(input))
            {
                Debug.LogError("The state " + input + " was not declaret in the current state list.");
                output = false;
            }
            if (!States.Data.Contains(input))
            {
                output = false;
                Debug.LogError("The final state "+input+" is not contain in the current final states list.");
            }
            if (output)
            {
                Data.Add(input);
                Debug.Log("The state " + input + " has been added to the final states list.");
            }
            return output;
        }
        public static bool Remove(string input)
        {
            bool output = true;
            if (!States.Data.Contains(input))
            {
                Debug.LogError("The state " + input + " is not valid.");
                output = false;
            }
            if (!Data.Contains(input))
            {
                Debug.LogError("The state " + input + " is not a final state.");
                output = false;
            }
            if (output)
            {
                Data.Remove(input);
                foreach(char iteration in Alphabet.Data)
                {
                    Transitions.Remove(new Transition()
                    {
                        State = input,
                        Symbol = iteration
                    });
                }
                Debug.Log("The state " + input + " has been remove from final states list.");
            }
            return output;
        }
    }
    public static class InitialState
    {
        public static string Value { get; private set; } = string.Empty;
        public static bool Set(string input)
        {
            bool output = true;
            if (States.Data.Count <= 0)
            {
                Debug.LogError("You do not be able to set a initial state without add some states before.");
                output = false;
            }
            if (!States.Data.Contains(input))
            {
                Debug.LogError("The state " + input + " was not declarated in the current states list.");
                output = false;
            }
            if (output)
            {
                Value = input;
                Simulation.CurrentState = Value;
                Debug.Log("The state " + input + " has been setted to initial state");
            }
            return output;
        }
    }

    public static class Simulation
    {
        public static string CurrentState { get; internal set; }
        public static string PreviousState {  get; private set; }
        public static string StringToProcess { get; private set; }
        public static string StringOnProcess {  get; private set; }
        public static bool Finish { get; private set; }

        public static void Restart()
        {
            CurrentState = InitialState.Value;
            PreviousState = "-";
            StringToProcess = string.Empty;
            StringOnProcess = "-";
            Finish = false;
        }
        public static bool SetStringToProcess(string ProcessInput)
        {
            bool output = true;
            foreach (char symbol in ProcessInput)
            {
                if (!Alphabet.Data.Contains(symbol))
                {
                    Debug.LogError("The string " + ProcessInput + " contain invalid symbols");
                    output = false;
                    break;
                }
            }
            if (output)
            {
                string toSet = "-";
                Debug.Log("The string " + ProcessInput + " has been setted to be process");
                StringToProcess = ProcessInput;
                for (int i = ProcessInput.Length - 1; i >= 0; i--)
                {
                    toSet += ProcessInput[i];
                }
                StringOnProcess = toSet;
            }
            return output;
        }
        private static char GetLastSymbol()
        {
            return StringOnProcess[StringOnProcess.Length - 1];
        }
        public static bool NextStep()
        {
            bool output = true;
            if (GetLastSymbol() == '-')
            {
                if (FinalSates.Data.Contains(CurrentState))
                {
                    Debug.Log("The string " + StringToProcess + " is a valid input on this automata.");
                    output = false;
                }
                else
                {
                    Debug.LogError("The string " + StringToProcess + " is not a valid input on this automata.");
                    output = false;
                }
                return output;
            }
            if (Alphabet.Data.Count <= 0)
            {
                Debug.LogError("You do not be able to evaluate a string without set the alphabet before.");
                output = false;
            }
            if (States.Data.Count <= 0)
            {
                Debug.LogError("You do not be able to evaluate a string without add some states before.");
                output = false;
            }
            if (FinalSates.Data.Count <= 0)
            {
                Debug.LogError("You do not be able to evaluate a string without add some final states before.");
                output = false;
            }
            if (Transitions.Data.Count <= 0)
            {
                Debug.LogError("You do not be able to evaluate a string without add some transitions before.");
                output = false;
            }
            if (InitialState.Value == string.Empty)
            {
                Debug.LogError("You do not be able to evaluate a string wothout set the initial state before.");
                output = false;
            }
            if (!Transitions.Contain( new Transition() { State = CurrentState, Symbol = GetLastSymbol() } ))
            {
                Debug.LogError("The transition (" + CurrentState + "," + GetLastSymbol() + ") is not a valid transition.");
                output = false;
            }
            if (Finish)
            {
                PreviousState = CurrentState;
                CurrentState = Transitions.Data[(CurrentState, GetLastSymbol())];
                Debug.Log("Transition (" + PreviousState + "," + GetLastSymbol() + "):" + CurrentState);
                Debug.Log("String on Process: " + StringOnProcess);
                StringOnProcess = StringOnProcess.Remove(StringOnProcess.Length - 1);
            }
            return Finish = output;
        }
        public static bool FullProcess()
        {
            bool output = true;
            while (NextStep())
            {
                output = FinalSates.Data.Contains(CurrentState);
            }
            return output;
        }
    }
}