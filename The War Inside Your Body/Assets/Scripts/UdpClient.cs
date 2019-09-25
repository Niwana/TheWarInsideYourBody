using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TUIO;

public class UdpClient : MonoBehaviour , TuioListener
{

    private TuioClient client;
    private Dictionary<long, TuioObject> objectList;
    private Dictionary<long, TuioCursor> cursorList;
    private Dictionary<long, TuioBlob> blobList;
    private object cursorSync = new object();
    private object objectSync = new object();
    private object blobSync = new object();

    private Dictionary<long, GameObject> gameObjectList;

    public GameObject protein_1;
    public GameObject protein_2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script start");
        objectList = new Dictionary<long, TuioObject>(128);
        cursorList = new Dictionary<long, TuioCursor>(128);

        client = new TuioClient(3333);
        client.addTuioListener(this);

        client.connect();

        gameObjectList = new Dictionary<long, GameObject>(128);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (long key in objectList.Keys)
        {
            TuioObject tuioObject = objectList[key];

            if(!gameObjectList.ContainsKey(tuioObject.SymbolID))
            {
                InstantiateProtein(tuioObject);
            }
        }
        // Update movement
        foreach(long key in objectList.Keys)
        {
            TuioObject tuioObject = objectList[key];
            GameObject protein = gameObjectList[tuioObject.SymbolID];
            protein.transform.position = new Vector3(tuioObject.X * 10, -tuioObject.Y * 10, 0f);
            protein.transform.eulerAngles = new Vector3(protein.transform.eulerAngles.x, protein.transform.eulerAngles.y, -tuioObject.Angle*360f/6.28f);

            // LOOK AT THIS CODE
            Debug.Log(protein_1.name + " collides with " + protein_1.GetComponent<Protein>());
        }
    }


    void InstantiateProtein(TuioObject tuioObject)
    {
        GameObject protein = new GameObject();

        if (tuioObject.SymbolID == 108)
                protein = Instantiate(protein_1);

        if (tuioObject.SymbolID == 109)
            protein = Instantiate(protein_2);


        gameObjectList.Add(tuioObject.SymbolID, protein);
    }



    // TUIO Functions
    public void addTuioObject(TuioObject o)
    {
        lock (objectSync)
        {
            objectList.Add(o.SessionID, o);
        }
        //Debug.Log("add obj " + o.SymbolID + " (" + o.SessionID + ") " + o.X + " " + o.Y + " " + o.Angle);
    }

    public void updateTuioObject(TuioObject o)
    {
        lock (objectSync)
        {
            objectList[o.SessionID].update(o);
        }
        //Debug.Log("set obj " + o.SymbolID + " " + o.SessionID + " " + o.X + " " + o.Y + " " + o.Angle + " " + o.MotionSpeed + " " + o.RotationSpeed + " " + o.MotionAccel + " " + o.RotationAccel);
    }

    public void removeTuioObject(TuioObject o)
    {
        lock (objectSync)
        {
            objectList.Remove(o.SessionID);
            gameObjectList.Remove(o.SessionID);
        }
        //Debug.Log("del obj " + o.SymbolID + " (" + o.SessionID + ")");
    }

    public void addTuioCursor(TuioCursor c)
    {
        lock (cursorSync)
        {
            cursorList.Add(c.SessionID, c);
        }
        //Debug.Log("add cur " + c.CursorID + " (" + c.SessionID + ") " + c.X + " " + c.Y);
    }

    public void updateTuioCursor(TuioCursor c)
    {
        //Debug.Log("set cur " + c.CursorID + " (" + c.SessionID + ") " + c.X + " " + c.Y + " " + c.MotionSpeed + " " + c.MotionAccel);
    }

    public void removeTuioCursor(TuioCursor c)
    {
        lock (cursorSync)
        {
            cursorList.Remove(c.SessionID);
        }
       // Debug.Log("del cur " + c.CursorID + " (" + c.SessionID + ")");
    }

    public void addTuioBlob(TuioBlob b)
    {
        lock (blobSync)
        {
            blobList.Add(b.SessionID, b);
        }
        //Debug.Log("add blb " + b.BlobID + " (" + b.SessionID + ") " + b.X + " " + b.Y + " " + b.Angle + " " + b.Width + " " + b.Height + " " + b.Area);
    }

    public void updateTuioBlob(TuioBlob b)
    {
        //Debug.Log("set blb " + b.BlobID + " (" + b.SessionID + ") " + b.X + " " + b.Y + " " + b.Angle + " " + b.Width + " " + b.Height + " " + b.Area + " " + b.MotionSpeed + " " + b.RotationSpeed + " " + b.MotionAccel + " " + b.RotationAccel);
    }

    public void removeTuioBlob(TuioBlob b)
    {
        lock (blobSync)
        {
            blobList.Remove(b.SessionID);
        }
        //Debug.Log("del blb " + b.BlobID + " (" + b.SessionID + ")");
    }

    public void refresh(TuioTime frameTime)
    {
        
    }
}
