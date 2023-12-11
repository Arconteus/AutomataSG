using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Animations;
using UnityEngine;

public class AutomataSimulator : MonoBehaviour
{
    //=============================================
    // Quintupla del automata
    //=============================================
    //<==[Variables publicas]==>
    public HashSet<char> alphabet;
    public HashSet<string> states;
    //<==[Variables Privadas]==>
    private Dictionary<(string state, char symbol), string> transitions;
    private HashSet<string> finalStates;
    private string initialState;
    //=============================================
    // Variables de procesamiento
    //=============================================
    private string stringToProcess;
    private string stringOnProcess;
    private string currentState;
    private string previousState;
    private bool finish;
    //=============================================
    // Funcion de inicializacion de la simulacion
    //=============================================

    public AutomataSimulator()
    {
        //Se inicializan todos los tipos de lista de la quintupla
        this.alphabet = new HashSet<char>();
        this.states = new HashSet<string>();
        this.transitions = new Dictionary<(string, char), string>();
        this.finalStates = new HashSet<string>();
        this.SetStringToProcess("");
        this.finish = false;
        this.Clear();
    }

    //=============================================
    // Funcion de inicializacion de la simulacion
    //=============================================

    public bool AddTransition(string initialState, char symbol, string finalState)
    {
        if (!this.alphabet.Contains(symbol))
        {
            Debug.LogError("(X) The symbol " + symbol + " does not exist in the alphabet.");
            return false;
        }
        if (!this.states.Contains(initialState))
        {
            Debug.LogError("(X) The initial state does not exist in the states list.");
            return false;
        }
        if (!this.states.Contains(finalState))
        {
            Debug.LogError("(X) The final state does not exist in the states list.");
            return false;
        }
        if (this.transitions.ContainsKey((initialState, symbol)))
        {
            Debug.LogError("(X) The transition (" + initialState + "," + symbol + ") already exist, you can't add the same transition with other final state.");
            return false;
        }
        this.transitions.Add((initialState, symbol), finalState);
        Debug.Log("(O) The transition (" + initialState + "," + symbol + "):" + finalState + " has been added.");
        return true;
    }
    public bool AddFinalState(string finalStateToAdd)
    {
        if (this.states.Contains(finalStateToAdd))
        {
            this.finalStates.Add(finalStateToAdd);
            Debug.Log("(X) The state " + finalStateToAdd + " has been added to the final states list.");
            return true;
        }
        else
        {
            Debug.LogError("(X) The state " + finalStateToAdd + " does not exist, the final state needs exist.");
            return false;
        }
    }
    public bool SetInitialState(string initialStateToSet)
    {
        if (!this.states.Contains(initialStateToSet))
        {
            Debug.LogError("(X) The final state does not exist on the states list.");
            return true;
        }
        this.initialState = initialStateToSet;
        this.currentState = initialStateToSet;
        this.previousState = "-";
        Debug.Log("(O) The initial state has been setted.");
        return true;
    }
    public bool SetStringToProcess(string input)
    {
        bool output = true;
        string toProcess = string.Empty;
        foreach (char symbol in input)
        {
            if (!this.alphabet.Contains(symbol))
            {
                output = false;
            }
        }
        if (output)
        {
            this.stringToProcess = input;
            for(int i = input.Length - 1; i >= 0; i--)
            {
                toProcess += input[i];
            }
            this.stringOnProcess = "-" + toProcess;
            this.Restart();
        }
        if (input == "")
        {
            this.stringToProcess = string.Empty;
            this.stringOnProcess = "-";
            this.Restart();
        }
        return output;
    }

    //=============================================
    // Funcion de mantenimiento
    //=============================================
    public void Clear()
    {
        //Se elimina todo rastro de cada variable de quintuplas
        this.alphabet.Clear();
        this.states.Clear();
        this.transitions.Clear();
        this.finalStates.Clear();
        this.initialState = string.Empty;
        this.stringToProcess = string.Empty;
        this.stringOnProcess = string.Empty;
        this.finish = false;
        //Se elimina la logica de procesamiento
        Debug.Log("(O) The automata model has been cleaned.");
    }

    public void Restart()
    {
        this.currentState = this.initialState;
        this.previousState = "-";
        this.finish = false;
    }

    //=============================================
    // Funciones de solicitud de informacion
    //=============================================

    public string GetStringToProcess()
    {
        string output = this.stringToProcess;
        return output;
    }
    public string GetStringOnProcess()
    {
        string output = this.stringOnProcess;
        return output;
    }
    public string GetCurrentState()
    {
        string output = this.currentState;
        return output;
    }
    public string GetPreviousState()
    {
        string output = this.previousState;
        return output;
    }
    public char GetLastSymbol()
    {
        char output = this.stringOnProcess[this.stringOnProcess.Length - 1];
        return output;
    }
    public List<(string, char, string)> GetTransitionsList()
    {
        List<(string, char, string)> output = new List<(string, char, string)>();
        foreach (var transition in this.transitions)
        {
            string state = transition.Key.state;
            char symbol = transition.Key.symbol;
            string value = transition.Value;
            output.Add(new(state,symbol,value));
        }
        return output;
    }
    public List<string> GetFinalStatesList() {
        List<string> output = this.finalStates.ToList<string>();
        return output;
    }
    public List<char> GetAlphabetList()
    {
        List<char> output = this.alphabet.ToList<char>();
        return output;
    }
    public List<string> GetStatesList()
    {
        List<string> output = this.states.ToList<string>();
        return output;
    }

    //=============================================
    // Funciones de procesamiento
    //=============================================

    public bool nextStep()
    {
        if(this.stringToProcess == string.Empty)
        {
            Debug.LogError("(X) The string to process was not declared.");
            return false;
        }
        if (this.GetLastSymbol() == '-')
        {
            this.finish = true;
        }
        if (this.finish)
        {
            Debug.Log("(O) The automata was finished.");
            if (this.finalStates.Contains(this.currentState))
            {
                Debug.Log("(O) The string "+this.stringToProcess+" is accepted.");
                return false;
            }
            else
            {
                Debug.LogError("(X) The string " + this.stringToProcess + " is not accepted.");
                return false;
            }
        }
        if (!this.transitions.ContainsKey((this.currentState, this.GetLastSymbol())))
        {
            Debug.LogError("(X) The string contains a invalid transitions");
            Debug.LogError("(X) The string " + this.stringOnProcess + " is not aceptes.");
            this.finish = true;
            return false;
        }
        this.previousState = this.currentState;
        this.currentState = this.transitions[(this.currentState,this.GetLastSymbol())];
        Debug.Log("(O) [Transition] (" + this.previousState + "," + this.GetLastSymbol()+"):"+this.currentState);
        this.stringOnProcess = this.stringOnProcess.Remove(this.stringOnProcess.Length-1);
        Debug.Log("(O) String on process: " + this.stringOnProcess);
        return true;
    }

    public bool FullProcess()
    {
        bool output = false;

        while ( output = this.nextStep())
        {

        }
        return output;
    }

    //=============================================
    // Funciones de procesamiento
    //=============================================

    public void Start()
    {
        this.alphabet.Add('0');
        this.alphabet.Add('1');
        this.states.Add("1");
        this.states.Add("2");
        this.SetInitialState("w");
        this.AddFinalState("x");
        this.AddTransition("a", '0', "2");
        this.AddTransition("1", 'b', "3");
        this.AddTransition("2", '1', "c");
        this.SetStringToProcess("01a1");
        this.FullProcess();
    }
}
