using UnityEngine;
using TMPro;

public class RandomEquationGenerator : MonoBehaviour
{
    private TextMeshPro equationText;
    public int solution;
    void Start()
    {
        equationText = GetComponentInChildren<TextMeshPro>();
        if(equationText == null)
        {
            Debug.LogError("TextMeshPro component not found in children.");
            return;
        }
    }

    string GenerateRandomEquation()
    {
        int x; 
        int a; 
        int b; 
        int c; 

        x = Random.Range(0, 6);
        Debug.Log("x: " + x);
        solution = x;

        a = Random.Range(1, 6);

        b = Random.Range(-5, 6);

        c = a * x + b;

        string equation = "";

        if (a == 1)
            equation += "x";
        else
            equation += a.ToString() + "x";

        if (b > 0)
            equation += "+" + b.ToString();
        else if (b < 0)
            equation += "-" + Mathf.Abs(b).ToString();

        equation += "=" + c.ToString();

        return equation;
    }

    public int InitializePaper()
    {
        if (equationText == null)
            equationText = GetComponentInChildren<TextMeshPro>();

        if (equationText == null)
        {
            Debug.LogError("TextMeshPro component not found in children.");
            return -1;
        }
        equationText.text = GenerateRandomEquation();
        return solution;
    }
}
