using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int date;
    public Node LeftChild;
    public Node RightChild;
    public Node Parent;

    public  Node(int item)
    {
        date = item;
    }
}
