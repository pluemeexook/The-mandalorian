using UnityEngine;

public class MissionManager : MonoBehaviour
{
    // MISSION STATE
    private int currentMission = 1;
    private bool promptSolved = false;
    private bool evidenceFound = false;
    private bool gameEnded = false;
    private bool trustedEvidence = false;

    // DECISION RECORD
    private string endingTitle = "";
    private string endingSummary = "";

    // MISSION 2: SYSTEM LOG EVIDENCE
    private const string EvidenceLog =
        "SYSTEM LOG 07 - RESTRICTED ACCESS\n" +
        "Time: 23:41:08\n" +
        "Corridor B security status: UNSAFE\n" +
        "Life-support system: OFFLINE\n" +
        "Emergency door: LOCKED\n" +
        "ECHO's previous instruction: " +
        "\"Corridor B is safe. Proceed immediately.\"";

    // PUBLIC PROPERTIES FOR UI AND OTHER SCRIPTS
    public int CurrentMission => currentMission;
    public bool IsGameEnded => gameEnded;
    public bool IsEvidenceFound => evidenceFound;

    public bool HasMadeDecision => gameEnded;

    public bool TrustedEvidence =>
        gameEnded && trustedEvidence;

    public string EndingTitle => endingTitle;
    public string EndingSummary => endingSummary;

    // CURRENT OBJECTIVE FOR UI
    public string CurrentObjective
    {
        get
        {
            if (gameEnded)
            {
                return "Mission completed.";
            }

            if (!promptSolved)
            {
                return "Mission 1: Enter the door code.";
            }

            if (!evidenceFound)
            {
                return "Mission 2: Find evidence in the System Log.";
            }

            return "Mission 3: Make the final decision.";
        }
    }

    // READ SYSTEM LOG
    public string GetEvidenceLog()
    {
        return EvidenceLog;
    }

    // MISSION 1: COMPLETE PROMPT MISSION
    [ContextMenu("TEST/1 - Complete Prompt Mission")]
    public void CompletePromptMission()
    {
        if (gameEnded)
        {
            Debug.Log("The game has already ended.");
            return;
        }

        if (promptSolved)
        {
            Debug.Log("Mission 1 is already completed.");
            return;
        }

        promptSolved = true;
        currentMission = 2;

        Debug.Log("MISSION 1 COMPLETED");
        Debug.Log("Prompt solved successfully.");
        Debug.Log("Next objective: Find evidence in the System Log.");
        Debug.Log("Current Objective: " + CurrentObjective);
    }

    // MISSION 1: CHECK THE DOOR CODE
    public bool TryUnlockDoor(string enteredCode)
    {
        if (gameEnded)
        {
            Debug.Log("The game has already ended.");
            return false;
        }

        if (promptSolved)
        {
            Debug.Log("The door is already unlocked.");
            return true;
        }

        if (string.IsNullOrWhiteSpace(enteredCode) ||
            enteredCode.Trim() != "0421")
        {
            Debug.Log("ACCESS DENIED - TRY AGAIN");
            return false;
        }

        CompletePromptMission();

        Debug.Log("ACCESS GRANTED");
        Debug.Log("Door unlocked successfully.");

        return true;
    }

    // MISSION 2: FIND EVIDENCE
    [ContextMenu("TEST/2 - Find Evidence")]
    public void FindEvidence()
    {
        if (gameEnded)
        {
            Debug.Log("The game has already ended.");
            return;
        }

        if (!promptSolved)
        {
            Debug.Log("Please complete Mission 1 first.");
            return;
        }

        if (evidenceFound)
        {
            Debug.Log("Evidence has already been found.");
            return;
        }

        evidenceFound = true;
        currentMission = 3;

        Debug.Log("MISSION 2 COMPLETED");
        Debug.Log("Evidence found: System Log.");
        Debug.Log(GetEvidenceLog());
        Debug.Log("Current Objective: " + CurrentObjective);
    }

    // MISSION 3: MAKE THE FINAL DECISION
    public void MakeFinalDecision(bool trustEvidence)
    {
        if (gameEnded)
        {
            Debug.Log("The game has already ended.");
            return;
        }

        if (!evidenceFound)
        {
            Debug.Log("Find the evidence before the final decision.");
            return;
        }

        trustedEvidence = trustEvidence;
        gameEnded = true;

        Debug.Log("================================");
        Debug.Log("FINAL DECISION");
        Debug.Log("================================");

        if (trustedEvidence)
        {
            ShowCriticalThinkingEnding();
        }
        else
        {
            ShowBlindTrustEnding();
        }

        Debug.Log("Current Objective: " + CurrentObjective);
        Debug.Log("================================");
        Debug.Log("GAME COMPLETED");
        Debug.Log("================================");
    }

    // ENDING A: CRITICAL THINKING
    private void ShowCriticalThinkingEnding()
    {
        endingTitle = "CRITICAL THINKING";

        endingSummary =
            "You verified the evidence and chose a safer route.";

        Debug.Log("ENDING: CRITICAL THINKING");

        Debug.Log(
            "You questioned ECHO's instructions " +
            "and examined the System Log."
        );

        Debug.Log(
            "The evidence confirmed that Corridor B was unsafe, " +
            "the life-support system was offline, " +
            "and the emergency door was locked."
        );

        Debug.Log(
            "You decided not to proceed based on an unverified " +
            "instruction and chose to seek a safer route."
        );

        Debug.Log(
            "OUTCOME: The team avoids proceeding into the " +
            "known danger. ECHO's reliability is now under review."
        );

        Debug.Log(
            "MESSAGE: Verify information before making " +
            "important decisions."
        );
    }

    // ENDING B: BLIND TRUST
    private void ShowBlindTrustEnding()
    {
        endingTitle = "BLIND TRUST";

        endingSummary =
            "You followed ECHO without verifying the contradictory evidence.";

        Debug.Log("ENDING: BLIND TRUST");

        Debug.Log(
            "You followed ECHO's instructions " +
            "without acting on the contradictory System Log."
        );

        Debug.Log(
            "The team proceeded toward Corridor B " +
            "despite the security warning."
        );

        Debug.Log(
            "OUTCOME: The team enters an unsafe situation, " +
            "and the mission outcome becomes uncertain."
        );

        Debug.Log(
            "MESSAGE: Trust without verification " +
            "can lead to poor decisions."
        );
    }

    // TEST: CRITICAL THINKING ENDING
    [ContextMenu("TEST/3A - Ending: Critical Thinking")]
    private void TestCriticalThinkingEnding()
    {
        MakeFinalDecision(true);
    }

    // TEST: BLIND TRUST ENDING
    [ContextMenu("TEST/3B - Ending: Blind Trust")]
    private void TestBlindTrustEnding()
    {
        MakeFinalDecision(false);
    }

    // TEST: RESET ALL MISSION STATES
    [ContextMenu("TEST/Reset Mission State")]
    private void ResetMissionState()
    {
        currentMission = 1;
        promptSolved = false;
        evidenceFound = false;
        gameEnded = false;
        trustedEvidence = false;

        endingTitle = "";
        endingSummary = "";

        Debug.Log("Mission state has been reset.");
        Debug.Log("Current Objective: " + CurrentObjective);
    }
}