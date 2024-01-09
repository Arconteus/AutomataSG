using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        SetModel();
        SetSimulation();
    }
    public void SetModel()
    {
        //Definir el alfabeto
        DFA.Alphabet.Add('0');
        DFA.Alphabet.Add('1');
        //Definir los estados
        DFA.States.Add("q0");
        DFA.States.Add("q1");
        //Definir las transiciones
        DFA.Transitions.Add(new Transition()
        { State = "q0", Symbol = '0', FinalState = "q0" });
        DFA.Transitions.Add(new Transition()
        { State = "q0", Symbol = '1', FinalState = "q1" });
        DFA.Transitions.Add(new Transition()
        { State = "q1", Symbol = '0', FinalState = "q0" });
        DFA.Transitions.Add(new Transition()
        { State = "q1", Symbol = '1', FinalState = "q1" });
        //Definir el estado inicial
        DFA.InitialState.Set("q0");
        //Definit el estado final
        DFA.FinalSates.Add("q1");
    }
    public void SetSimulation()
    {
        DFA.Simulation.Restart();
        DFA.Simulation.SetStringToProcess("001101");
        DFA.Simulation.FullProcess();
    }
}
