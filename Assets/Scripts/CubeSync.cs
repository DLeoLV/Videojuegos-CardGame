using Photon.Pun;
using UnityEngine;

public class CubeSync : MonoBehaviourPun, IPunObservable
{
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    void Start()
    {
        networkPosition = transform.position;
        networkRotation = transform.rotation;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
