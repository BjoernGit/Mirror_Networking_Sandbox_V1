using UnityEngine;
using Mirror;
using Mirror.Examples.NetworkRoom;

namespace Mirror.Examples.Tanks
{
    public class Projectile : NetworkBehaviour
    {
        public float destroyAfter = 5;
        public Rigidbody rigidBody;
        public float force = 1000;

        [SyncVar]
        public GameObject shooterGO;
        [SyncVar]
        public uint shooterID;

        public override void OnStartServer()
        {
            Invoke(nameof(DestroySelf), destroyAfter);
        }

        // set velocity for server and client. this way we don't have to sync the
        // position, because both the server and the client simulate it.
        void Start()
        {
            rigidBody.AddForce(transform.forward * force);
        }

        // destroy for everyone on the server
        [Server]
        void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }

        // ServerCallback because we don't want a warning if OnTriggerEnter is
        // called on the client
        [ServerCallback]
        void OnTriggerEnter(Collider co)
        {
            Debug.Log(shooterID);
            RpcClaimPrize();
            NetworkServer.Destroy(gameObject);
        }

        //[Server]
        [ClientRpc]
        void RpcClaimPrize()
        {
            // Null check is required, otherwise close timing of multiple claims could throw a null ref.
            //hitObject.GetComponent<Reward>().ClaimPrize(gameObject);
            Debug.Log(shooterID + "RPC");
            if (shooterGO != null)
            {
                Debug.Log("fuck");
                //CmdCallThis();

                //try something from discord
                NetworkIdentity.spawned[shooterID].gameObject.GetComponent<PlayerScore>().CmdIncreaseScore();

            }
        }

        [Command]
        private void CmdCallThis()
        {
            shooterGO.GetComponent<PlayerScore>().CmdIncreaseScore();
        }
    }
}
