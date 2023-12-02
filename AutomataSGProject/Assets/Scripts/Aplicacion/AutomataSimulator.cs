using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AutomataSimulator : MonoBehaviour
{
    //=============================================
    // Quintupla del automata
    //=============================================
    public HashSet<char> alphabet;
    public HashSet<string> states;
    private Dictionary<(string, char), string> transitions;
    private HashSet<string> finalStates;
    private string initialState;
    //=============================================
    // Variables de procesamiento
    //=============================================
    private string actualState;
    private string stringPrime;
    private string stringOnProcess;
    //=============================================
    //Funcion de inicializacion de la simulacion
    //=============================================
    public void Start()
    {
        this.clear();
    }
    /**
     * Esta funcion limpia toda la memoria del modelo del automata.
     **/
    public void clear()
    {
        //Se inicializan todos los tipos de lista de la quintupla
        this.alphabet = new HashSet<char>();
        this.states = new HashSet<string>();
        this.transitions = new Dictionary<(string, char), string>();
        this.finalStates = new HashSet<string>();
        //Se elimina todo rastro de cada variable de quintuplas
        this.alphabet.Clear();
        this.states.Clear();
        this.transitions.Clear();
        this.finalStates.Clear();
        this.initialState = string.Empty;
        //Se elimina la logica de procesamiento
        this.actualState = string.Empty;
        this.stringPrime = string.Empty;
        this.stringOnProcess = string.Empty;
        Debug.Log("(O) The automata model has been cleaned.");
    }
    //=============================================
    // Funcion de transiciones
    //=============================================
    /**
     * Esta funcion requiere de 3 argumentos, añade una transicion a la lista de transiciones
     * Sintaxis: addTransitions([initialState],[symbol],[finalState]);
     * Argumentos:
     * [initialsState] Estado inicial de la transicion
     * [symbol] Simbolo necesario para la transicion
     * [finalState] Estado resultante de la transicion
     * Condiciones:
     * [Symbol] debe pertenecer a [alphabet]
     * [initialState] y [finalState] deben pertenecer a [states]
     **/
    public void addTransition(string initialState, char symbol, string finalState)
    {
        if (!this.alphabet.Contains(symbol))
        {
            Debug.LogError("(X) The symbol " + symbol + " does not exist in the alphabet.");
            return;
        }
        if (this.states.Contains(initialState))
        {
            Debug.LogError("(X) The initial state does not exist in the states list.");
            return;
        }
        if (this.states.Contains(finalState))
        {
            Debug.LogError("(X) The final state does not exist in the states list.");
            return;
        }
        if (this.transitions.ContainsKey((initialState, symbol)))
        {
            Debug.LogError("(X) The transition (" + initialState + "," + symbol + ") already exist, you can't add the same transition with other final state.");
            return;
        }
        this.transitions.Add((initialState, symbol), finalState);
        Debug.LogAssertion("(O) The transition (" + initialState + "," + symbol + "):" + finalState + " has been added.");
        return;
    }
    //=============================================
    // Funciones de estados
    //=============================================
    /**
     * Esta funcion añade a la lista de estados finales estados ya existentes.
     * Sintaxis: addFinalState(finalStateToAdd);
     * Argumentos:
     * [finalStateToAdd] estado que sera considerado como estado final
     * Condiciones:
     * [finalStatetoAdd] debe pertenecer a [states]
     **/
    public void addFinalState(string finalStateToAdd)
    {
        if (this.states.Contains(finalStateToAdd))
        {
            this.finalStates.Add(finalStateToAdd);
            Debug.LogError("(X) The state " + finalStateToAdd + " has been added to the final states list.");
            return;
        }
        else
        {
            Debug.LogError("(X) The state " + finalStateToAdd + " does not exist, the final state needs exist.");
            return;
        }
    }
    /**
     * Esta funcion establece el estado principal, al cambiar el estado inicial el modelo de autoamta es reiniciado.
     * Sintaxis: setInitialState(initialSate)
     * Argumentos:
     * [initialState] Es el estado inicial por el cual comienza la simulacion
     * Condiciones:
     * [initialState] debe pertenecer a [states]
     **/
    public void setInitialState(string initialState)
    {
        if (this.states.Contains(initialState))
        {
            Debug.LogAssertion("(O) The initial state has been setted.");
            return;
        }
        Debug.LogError("(X) The final state does not exist on the states list.");
        return;
    }
    //=============================================
    // Procesamiento
    //=============================================
    public void setStringToProcess(string stringToSet)
    {
        this.stringPrime = stringToSet;
        Debug.LogAssertion("(O) The string " + stringToSet + " has been setted.");
    }
    public void restart()
    {
        this.stringOnProcess = this.stringPrime;
        this.actualState = this.initialState;
    }
}
