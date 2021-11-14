using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun__ : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private float _maxDistance;
    Rigidbody takenObject;
    enum Status { taking, taken }
    Status currentStatus;

    [SerializeField] Transform attachPosition;
    Vector3 initialPosition;
    Quaternion initialRotation;
    [SerializeField]
    float moveSpeed;
    

    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            
            AudioManager._instance.PlayAudio((int) Audios.LASER2);
            takenObject = gravityShoot(); //Raycast centro y choca rigidbody
        }
        if (takenObject != null)
        {
            if (Input.GetMouseButton(2) && takenObject != null)
            {
                takenObject.isKinematic = true;
                switch (currentStatus)
                {
                    case Status.taking:
                        updateTaking();
                        break;
                    case Status.taken:
                        updateTaken();
                        break;
                }
                if (Input.GetKeyDown(KeyCode.T))
                    detachObject(1000);

            }
            else
            {
                detachObject(0);
            }
        }
    }

    private Rigidbody gravityShoot()
    {
        if (Physics.Raycast(_cam.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out RaycastHit hit, 200.0f))
        {
            Rigidbody rb = hit.rigidbody;
            if (rb == null)
                return null;
            rb.isKinematic = true;
            initialPosition = rb.transform.position;
            initialRotation = rb.transform.rotation;
            currentStatus = Status.taking;
            return rb;
        }
        return null;
    }

    private void updateTaking()
    {
        takenObject.MovePosition(takenObject.position
            + (attachPosition.position - takenObject.position).normalized
            * moveSpeed * Time.deltaTime);

        takenObject.rotation = Quaternion.Lerp(initialRotation, attachPosition.rotation,
            (takenObject.position - initialPosition).magnitude
            / (attachPosition.position - initialPosition).magnitude);

        if ((attachPosition.position - takenObject.position).magnitude < 0.1f)
            currentStatus = Status.taken;
    }
    
    private void updateTaken()
    {
        takenObject.gameObject.layer = 3;
        foreach (Transform trans in takenObject.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 3;
        }
        takenObject.transform.position = attachPosition.position;
        takenObject.transform.rotation = attachPosition.rotation;
        if (!takenObject.CompareTag("BlockingCube"))
        {
            takenObject.detectCollisions = false;
        }
        
    }

    private void detachObject(float force)
    {
        takenObject.gameObject.layer = 0;
        foreach (Transform trans in takenObject.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 0;
        }
        takenObject.detectCollisions = true;
        takenObject.isKinematic = false;
        takenObject.AddForce(attachPosition.forward * force);
        takenObject = null;
    }
}