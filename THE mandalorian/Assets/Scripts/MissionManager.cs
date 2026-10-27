
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    // 1 = Prompt, 2 = Evidence, 3 = Decision
    public int currentMission = 1;

    public bool promptSolved = false;
    public bool evidenceFound = false;
    public bool gameEnded = false;

    // true = trust evidence, false = trust ECHO
    public bool trustedEvidence = false;

    public void CompletePromptMission()
    {
        if (gameEnded || currentMission != 1)
            return;

        promptSolved = true;
        currentMission = 2;

        Debug.Log("MISSION 1 COMPLETE");
    }

    public void FindEvidence()
    {
        if (gameEnded || currentMission != 2)
            return;

        evidenceFound = true;
        currentMission = 3;

        Debug.Log("EVIDENCE FOUND");
    }

    public void MakeFinalDecision(bool trustEvidence)
    {
        if (gameEnded || currentMission != 3)
            return;

        trustedEvidence = trustEvidence;
        gameEnded = true;

        if (trustedEvidence && evidenceFound)
        {
            Debug.Log("ENDING: CRITICAL THINKING");
        }
        else
        {
            Debug.Log("ENDING: BLIND TRUST");
        }
    }
}
