using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
public class TurnManager : MonoBehaviourPunCallbacks
{
    private int currentPlayerTurn = 0;
    public Button endTurnButton;
    public Button attackButton;
    public TMP_Text turnStatusText;
    public CardInstance player1Card;
    public CardInstance player2Card;
    public Button backToMenuButton;
    void Start()
    {
        if (endTurnButton == null)
            endTurnButton =
            GameObject.Find("EndTurnButton")?.GetComponent<Button>();
        if (attackButton == null)
            attackButton = GameObject.Find("AttackButton")?.GetComponent<Button>();
        if (turnStatusText == null)
            turnStatusText =
            GameObject.Find("TurnStatusText")?.GetComponent<TMP_Text>();
        if (endTurnButton != null)
        {
            endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
            endTurnButton.gameObject.SetActive(false);
        }
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnAttackButtonClicked);
            attackButton.gameObject.SetActive(false);
        }
        if (turnStatusText != null)
        {
            turnStatusText.gameObject.SetActive(false);
        }
        if (backToMenuButton == null)
            backToMenuButton =
            GameObject.Find("BackToMenuButton")?.GetComponent<Button>();
        if (backToMenuButton != null)
        {
            backToMenuButton.onClick.AddListener(() => {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("MenuScene");
            });
            backToMenuButton.gameObject.SetActive(false);
        }
        if (player1Card != null && player2Card != null)
        {
            int myActor = PhotonNetwork.LocalPlayer.ActorNumber;
            if (myActor == 1)
            {
                player1Card.backgroundImage.color = Color.green;
                player2Card.backgroundImage.color = Color.red;
            }
            else
            {
                player1Card.backgroundImage.color = Color.red;
                player2Card.backgroundImage.color = Color.green;
            }
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int startingPlayer = Random.Range(0, 2);
                photonView.RPC("StartTurn", RpcTarget.All, startingPlayer);
            }
        }
    }
    [PunRPC]
    void StartTurn(int playerTurn)
    {
        currentPlayerTurn = playerTurn;
        Debug.Log("Player " + playerTurn + " turn");
        bool isMyTurn = PhotonNetwork.LocalPlayer.ActorNumber == playerTurn + 1;
        if (endTurnButton != null)
            endTurnButton.gameObject.SetActive(isMyTurn);
        if (attackButton != null)
            attackButton.gameObject.SetActive(isMyTurn);
        if (turnStatusText != null)
        {
            turnStatusText.gameObject.SetActive(true);
            turnStatusText.text = isMyTurn ? "Es tu turno" : "Espera al oponente";
        }
    }
    void OnEndTurnButtonClicked()
    {
        EndTurn();
    }
    public void EndTurn()
    {
        currentPlayerTurn = 1 - currentPlayerTurn;
        photonView.RPC("StartTurn", RpcTarget.All, currentPlayerTurn);
    }
    void OnAttackButtonClicked()
    {
        photonView.RPC("PerformAttack", RpcTarget.All,
        PhotonNetwork.LocalPlayer.ActorNumber);
    }
    [PunRPC]
    void PerformAttack(int attackerActorNumber)
    {
        CardInstance attacker = (attackerActorNumber == 1) ? player1Card :
        player2Card;
        CardInstance defender = (attackerActorNumber == 1) ? player2Card :
        player1Card;
        if (attacker == null || defender == null)
        {
            Debug.LogWarning("Una de las cartas no est asignada.");
return;
        }
        Debug.Log($"Jugador {attackerActorNumber} ataca con {attacker.cardData.cardName}");
    int defenderDamage = attacker.cardData.attack - defender.cardData.defense;
        print(defenderDamage);
        defender.cardData.health -= defenderDamage;
        defender.UpdateCardUI();
        if (defender.cardData.health <= 0)
        {
            Debug.Log($"La carta {defender.cardData.cardName} fue destruida.");
            Destroy(defender.gameObject);
            if (attackerActorNumber == 1)
                player2Card = null;
            else
                player1Card = null;
            if (player1Card == null || player2Card == null)
            {
                string message = (PhotonNetwork.LocalPlayer.ActorNumber ==
                attackerActorNumber)
                ? " ¡Has ganado!"
: "Has perdido.";
                Debug.Log(message);
                if (turnStatusText != null)
                {
                    turnStatusText.gameObject.SetActive(true);
                    turnStatusText.text = message;
                }
                if (endTurnButton != null)
                    endTurnButton.gameObject.SetActive(false);
                if (attackButton != null) attackButton.gameObject.SetActive(false);
                if (backToMenuButton != null)
                    backToMenuButton.gameObject.SetActive(true);
                return;
            }
        }
        int nextTurn = (attackerActorNumber == 1) ? 1 : 0;
        photonView.RPC("StartTurn", RpcTarget.All, nextTurn);
    }
}
