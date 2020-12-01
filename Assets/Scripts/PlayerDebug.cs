using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    public Shader lineShader;
    private Rigidbody rb;
    private LineRenderer velocityLine;
    private LineRenderer trackLine;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        velocityLine = DrawLine(rb.position, rb.position + rb.velocity, Color.white);
        trackLine = DrawLine(rb.position, rb.position + rb.velocity, Color.red);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateLine(velocityLine, rb.position, rb.position + rb.velocity);

        PlayerCollision pc = this.GetComponent<PlayerCollision>();
        if(pc.isOnTrack)
            UpdateLine(trackLine, rb.position, pc.trackPlane.ClosestPointOnPlane(rb.position));
    }

    // Both start and end seem to be relative to the global origin.
    LineRenderer DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(lineShader);
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        return lr;
    }

    void UpdateLine(LineRenderer line, Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }
}
