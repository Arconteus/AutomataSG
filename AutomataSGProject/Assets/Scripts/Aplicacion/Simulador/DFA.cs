using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DFA
{
    //========================================================================
    // Variables Quintupla
    //========================================================================
    // <==[Public]==>
    public static HashSet<char> Alphabet = new HashSet<char>();
    public static HashSet<string> States = new HashSet<string>();
    // <==[Private]==>
    private static Dictionary<(string state, char symbol), string> Transitions
        = new Dictionary<(string state, char symbol), string>();
    private static HashSet<string> FinalStates = new HashSet<string>();
    private static string InitialState = string.Empty;
    //========================================================================
    // Variables de control
    //========================================================================
    private static string CurrentState = string.Empty;
    private static string PreviousState = "-";
    private static string StringToProcess = string.Empty;
    private static string StringOnProcess = "-";
    private static bool Finished = false;
    //========================================================================
    // Funciones de mantenimiento
    //========================================================================
    public static void Clear()
    {
        // quintupla clear logic
        FinalStates.Clear();
        Transitions.Clear();
        Alphabet.Clear();
        States.Clear();
        InitialState = string.Empty;
        // control clear logic
        CurrentState = string.Empty;
        PreviousState = "-";
        StringToProcess = string.Empty;
        StringOnProcess = "-";
        Finished = false;
        Debug.Log("The deterministic finite automata have been eliminated.");
    }
    public static void Restart()
    {
        CurrentState = InitialState;
        PreviousState = "-";
        StringToProcess = string.Empty;
        StringOnProcess = "-";
        Finished = false;
    }
    //========================================================================
    // Funciones de configuracion
    //========================================================================
    public static bool AddTransition(string State, char Symbol, string Target)
    {
        bool output = true;
        if (Alphabet.Count <= 0)
        {
            Debug.LogError("You do not be able to add a transition without set an alphabet before.");
            output = false;
        }
        if (States.Count <= 0)
        {
            Debug.LogError("You do not be able to add a transition without set some states before.");
            output = false;
        }
        if (!States.Contains(State))
        {
            Debug.LogError("The state " + State + " was not declareted in the states list.");
            output = false;
        }
        if (!States.Contains(Target))
        {
            Debug.LogError("The state " + Target + " was not declareted in the states list.");
            output = false;
        }
        if (!Alphabet.Contains(Symbol))
        {
            Debug.LogError("The symbol " + Symbol + " was not declareted in the alphabet.");
            output = false;
        }
        if (Transitions.ContainsKey((State, Symbol)))
        {
            Debug.LogError("The transition (" + State + "," + Symbol + ") it already exist, you do not be able to add the same transition twice.");
            output = false;
        }
        if (output)
        {
            Transitions.Add((State, Symbol), Target);
            Debug.Log("The transition (" + State + "," + Symbol + "):" + Target + " has been added.");
        }
        return output;
    }
    public static bool AddFinalState(string StateInput)
    {
        bool output = true;
        if (States.Count <= 0)
        {
            Debug.LogError("You do not be able to add a final state without set some states before.");
            output = false;
        }
        if (!States.Contains(StateInput))
        {
            Debug.LogError("The state " + StateInput + " was not declaret in the state list before.");
            output = false;
        }
        if (FinalStates.Contains(StateInput))
        {
            Debug.LogError("The state " + StateInput + " is already exist on the final state list, you do not be able to add the same state twice.");
            output = false;
        }
        if (output)
        {
            FinalStates.Add(StateInput);
        }
        return output;
    }
    public static bool SetInitialState(string StateInput)
    {
        bool output = true;
        if (States.Count <= 0)
        {
            Debug.LogError("You do not be able to set a initial state without add some states before.");
            output = false;
        }
        if (!States.Contains(StateInput))
        {
            Debug.LogError("The state " + StateInput + " was not declarated in the states list.");
            output = false;
        }
        if (output)
        {
            InitialState = StateInput;
            CurrentState = InitialState;
            Debug.Log("The state " + StateInput + " has been setted to initial state");
        }
        return output;
    }
    public static bool SetStringToProcess(string ProcessInput)
    {
        bool output = true;
        foreach (char symbol in ProcessInput)
        {
            if (!Alphabet.Contains(symbol))
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

    //========================================================================
    // Funciones de eliminacion
    //========================================================================
    public static bool RemoveTransition(string state, char symbol)
    {
        bool output = true;
        if (!States.Contains(state))
        {
            Debug.LogError("The state " + state + " is not valid.");
            output = false;
        }
        if (!Alphabet.Contains(symbol))
        {
            Debug.LogError("The symbol " + symbol + " is not a valid symbol.");
            output = false;
        }
        if (!Transitions.ContainsKey((state, symbol)))
        {
            Debug.LogError("The transition (" + state + "," + symbol + ") is not a valid transition");
            output = false;
        }
        if (output)
        {
            Transitions.Remove((state, symbol));
            Debug.Log("The transition (" + state + "," + symbol + " has been removed.");
        }
        return output;
    }
    public static bool RemoveFinalState(string state)
    {
        bool output = true;

        if (!States.Contains(state))
        {
            Debug.LogError("The state " + state + " is not valid.");
            output = false;
        }
        if (!FinalStates.Contains(state))
        {
            Debug.LogError("The state " + state + " is not a final state.");
            output = false;
        }
        if (output)
        {
            FinalStates.Remove(state);
            Debug.Log("The state " + state + " has been remove from final states list.");
        }
        return output;
    }
    public static bool FullRemoveState(string state)
    {
        bool output = true;
        if (!States.Contains(state))
        {
            Debug.LogError("The state " + state + " is not valid.");
            output = false;
        }
        foreach (char symbol in Alphabet)
        {
            Debug.Log("Trying to remove (" + state + "," + symbol + ").");
            RemoveTransition(state, symbol);
        }
        return output;
    }
    //========================================================================
    // Funciones de solicitud de informacion
    //========================================================================
    public static List<(string, char, string)> GetTransitionsList()
    {
        List<(string, char, string)> output = new List<(string, char, string)>();
        foreach (var item in Transitions)
        {
            output.Add(new(item.Key.state, item.Key.symbol, item.Value));
        }
        return output;
    }
    public static List<string> GetFinalStatesList()
    {
        List<string> output = FinalStates.ToList<string>();
        return output;
    }
    public static string GetInitialState()
    {
        return InitialState;
    }
    public static string GetCurrentState()
    {
        return CurrentState;
    }
    public static string GetPreviousState()
    {
        return PreviousState;
    }
    public static string GetStringToProcess()
    {
        return StringToProcess;
    }
    public static string GetStringOnProcess()
    {
        return StringOnProcess;
    }
    public static char GetLastSymbol()
    {
        return StringOnProcess[StringOnProcess.Length - 1];
    }
    public static bool IsFinished()
    {
        return Finished;
    }
    public static bool IsFinalState()
    {
        return FinalStates.Contains(CurrentState);
    }
    public static bool CheckFinalState(string input)
    {
        return FinalStates.Contains(input);
    }
    //========================================================================
    // Funciones de procesamiento
    //========================================================================
    public static bool NextStep()
    {
        bool output = true;
        if (GetLastSymbol() == '-')
        {
            if (IsFinalState())
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
        if (Alphabet.Count <= 0)
        {
            Debug.LogError("You do not be able to evaluate a string without set the alphabet before.");
            output = false;
        }
        if (States.Count <= 0)
        {
            Debug.LogError("You do not be able to evaluate a string without add some states before.");
            output = false;
        }
        if (FinalStates.Count <= 0)
        {
            Debug.LogError("You do not be able to evaluate a string without add some final states before.");
            output = false;
        }
        if (Transitions.Count <= 0)
        {
            Debug.LogError("You do not be able to evaluate a string without add some transitions before.");
            output = false;
        }
        if (InitialState == string.Empty)
        {
            Debug.LogError("You do not be able to evaluate a string wothout set the initial state before.");
            output = false;
        }
        if (!Transitions.ContainsKey((CurrentState, GetLastSymbol())))
        {
            Debug.LogError("The transition (" + CurrentState + "," + GetLastSymbol() + ") is not a valid transition.");
            output = false;
        }
        if (Finished)
        {
            PreviousState = CurrentState;
            CurrentState = Transitions[(CurrentState, GetLastSymbol())];
            Debug.Log("Transition (" + PreviousState + "," + GetLastSymbol() + "):" + CurrentState);
            Debug.Log("String on Process: " + StringOnProcess);
            StringOnProcess = StringOnProcess.Remove(StringOnProcess.Length - 1);
        }
        return Finished = output;
    }
    public static bool FullProcess()
    {
        bool output = true;
        while (NextStep())
        {
            output = IsFinalState();
        }
        return output;
    }
}