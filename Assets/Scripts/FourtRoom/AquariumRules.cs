using System.Linq;
using UnityEngine;

public class AquariumRules : MonoBehaviour
{
    private bool isCorrect = false;

    //public string serial;

    public FloatingController[] aquariums;
    public AquariumsManager manager;
    private int[] cuts;

    int[] correctWires;

    private static Color[] allColors = { Color.red, Color.blue, Color.green };

    public ChestController chestController;

    private void Start()
    {
        cuts = manager.cuts;
        correctWires = new int[aquariums.Length];

        for (int i = 0; i < aquariums.Length; i++)
        {
            correctWires[i] = DetermineCorrectWire(aquariums[i].serial, aquariums[i].orderedColors);
            Debug.Log("Aquarium " + (i + 1) + " correct wire: " + correctWires[i]);
        }
    }

    void Update()
    {
        bool ok = true;

        for (int i = 0; i < aquariums.Length; i++)
        {
            //int reversedIndex = aquariums[i].orderedColors.Length - 1 - cuts[i];
            if (cuts[i] != correctWires[i]) 
            {
                ok = false;
                break;
            }
        }

        isCorrect = ok;
        if (isCorrect)
        {
            Debug.Log("Correct config");
            chestController.ToggleChest();
        }
            
    }


    int DetermineCorrectWire(string serial, Color[] wires)
    {
        int redCount = wires.Count(c => c == Color.red);
        int blueCount = wires.Count(c => c == Color.blue);
        int greenCount = wires.Count(c => c == Color.green);

        // RULE 1: If serial contains 'Z', cut wire at position equal to red wire count (0-based)
        if (serial.Contains('Z'))
        {
            int index = wires.Length - redCount;
            if (index >= 0 && index < wires.Length)
                return index;
        }

        // RULE 2: If there are more green wires than red wires, cut the first green wire
        if (greenCount > redCount)
        {
            int index = 0;
            for (int i = 0; i < wires.Length; i++)
            {
                if (wires[i] == Color.green)
                    index = i;
            }
            return index;
        }

        // RULE 3: If serial contains 2 or more 'B's, cut the first red wire
        if (serial.Count(c => c == 'B') >= 2)
        {
            return System.Array.IndexOf(wires, Color.red);
        }

        // RULE 4: If green and blue counts are equal, cut wire 2 (0 -based)
        if (greenCount == blueCount)
        {
            return wires.Length - 3;
        }

        // RULE 5: If there are exactly 2 red wires, cut the blue wire
        if (redCount == 2)
        {
            for (int i = wires.Length - 1; i >= 0; i--)
            {
                if (wires[i] == Color.blue)
                    return i;
            }
        }

        // RULE 6: If serial ends in a digit, cut the second wire
        if (char.IsDigit(serial.Last()))
        {
            return wires.Length - 2;
        }

        // DEFAULT RULE: Cut the last wire
        return 0;
    }

}
