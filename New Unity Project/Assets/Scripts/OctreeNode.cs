using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctreeNode 
{
    public BoundingBox BBox;
    private OctreeNode _Parent;
    
    public OctreeNode Parent
    {
        set { Debug.Assert(_Parent == null, "Node already has a parent!"); _Parent = value; }
        get { return _Parent; }
    }

    private List<OctreeNode> Children;
    private List<GameObject> Objects;
    private List<GameObject> delist;


    //constuctor 
    public OctreeNode(Vec3 origin, Vec3 extends, OctreeNode parent = null)
    {
        BBox = new BoundingBox(origin, extends);
        _Parent = parent;
        Children = null;
        Objects = null;
    }

    public void MakeChildren()
    {
        Debug.Assert(Children == null, "Children already present on this OctreeNode");
        Children = new List<OctreeNode>();
        Vec3 extends = BBox.Extents * 0.5f;
        Vec3 origin = BBox.Position;
       

        Children.Add(new OctreeNode(new Vec3(origin.x - extends.x, origin.y - extends.y, origin.z - extends.z), extends, Parent));
        Children.Add(new OctreeNode(new Vec3(origin.x - extends.x, origin.y - extends.y, origin.z + extends.z), extends, Parent));
        Children.Add(new OctreeNode(new Vec3(origin.x - extends.x, origin.y + extends.y, origin.z - extends.z), extends, Parent));
        Children.Add(new OctreeNode(new Vec3(origin.x - extends.x, origin.y + extends.y, origin.z + extends.z), extends, Parent));
        Children.Add(new OctreeNode(new Vec3(origin.x + extends.x, origin.y - extends.y, origin.z - extends.z), extends, Parent));
        Children.Add(new OctreeNode(new Vec3(origin.x + extends.x, origin.y - extends.y, origin.z + extends.z), extends, Parent));
        Children.Add(new OctreeNode(new Vec3(origin.x + extends.x, origin.y + extends.y, origin.z - extends.z), extends, Parent));
        Children.Add(new OctreeNode(new Vec3(origin.x + extends.x, origin.y + extends.y, origin.z + extends.z), extends, Parent));

        foreach(OctreeNode child in Children)
        {
            child.Parent = this;
        }
        
        
    }
    void SendObjectsToChildren()
    {
        List<GameObject> delist = new List<GameObject>();
        foreach(GameObject go in Objects)
        {
            Bounds b = go.GetComponent<Renderer>().bounds;
            Vec3 goCentre = new Vec3(b.center);
            Vec3 goExtends = new Vec3(b.extents);
            foreach(OctreeNode child in Children)
            {
                if(child.BBox.containsObject(goCentre,goExtends))
                {
                    if (child.AddObject(go))
                    {
                        delist.Add(go);
                        break;

                    }
                                                             
                }
            }
        }
        foreach (GameObject go in delist)
        {
            Objects.Remove(go);
        }
    }
    public bool AddObject(GameObject a_object)
    {
        bool objectAdded = false;
        if(Children != null && Children.Count > 0)
        {
            foreach( OctreeNode child in Children)
            {
                objectAdded = child.AddObject(a_object);
                if (objectAdded)
                {
                    break;
                }
                
            }
        }
        if(objectAdded != true)
        {
            if (Objects == null)
            {
                Objects = new List<GameObject>();
            }
            Bounds b = a_object.GetComponent<Renderer>().bounds;
            Vec3 goCentre = new Vec3(b.center);
            Vec3 goExtends = new Vec3(b.extents);
            if (BBox.containsObject(goCentre, goExtends))
            {
                Objects.Add(a_object);
                objectAdded = true;
                if(Objects.Count >=2 && Children == null)
                {
                    MakeChildren();
                    SendObjectsToChildren();
                }
            }
        }
        return objectAdded;
    }

    
    private bool RemoveObject(GameObject a_object)
    {
        if (Objects != null)
        {
            if (Objects.Remove(a_object))
            {
                return true;
            }
        }
        if (Children != null)
        {
            foreach(OctreeNode child in Children)
            {
                if (child.RemoveObject(a_object))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void Draw()
    {
        BBox.Draw();
        if (Children != null)
        {
            foreach(OctreeNode child in Children)
            {
                child.Draw();
            }
        }
    }

    public void PerformCollisionTest()
    {
        //step through the children
        if(Children !=null && Children.Count > 0)
        {
            foreach(OctreeNode child in Children)
            {
                child.PerformCollisionTest();
            }
        }
        if(Objects != null && Objects.Count >1)
        {
            List<GameObject> delist = new List<GameObject>();
            for (int i = 0; i < Objects.Count; i++)
            {
                //test the bounds against next object in list 
                Bounds o1 = Objects[i].GetComponent<Renderer>().bounds;
                Vec3 o1center = new Vec3(o1.center);
                Vec3 o1Extends = new Vec3(o1.extents);

                for(int j = 1; j <Objects.Count; j ++)
                {
                    Bounds o2 = Objects[j].GetComponent<Renderer>().bounds;
                    Vec3 o2center = new Vec3(o2.center);
                    Vec3 o2Extends = new Vec3(o2.extents);
                    //test for collision 
                    if (o1center.x - o1Extends.x < o2center.x + o2Extends.x &&
                        o1center.x + o1Extends.x > o2center.x - o2Extends.x &&
                        o1center.y - o1Extends.y < o2center.y + o2Extends.y &&
                        o1center.y + o1Extends.y > o2center.y - o2Extends.y &&
                        o1center.z - o1Extends.z < o2center.z + o2Extends.z &&
                        o1center.z + o1Extends.z > o2center.z - o2Extends.z )
                    {
                        //object in collission with another object 
                        delist.Add(Objects[i]);
                        delist.Add(Objects[j]);
                    }
                }
            }
        }
    }





}
