using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuChuTree : MonoBehaviour
{
    public int[] myTree;
    private Node root = null;

    void Start()
    {
        for (int i = 0; i < myTree.Length; i++)
        {
           Add(myTree[i]);
        }
        PrintTree(root);
    }

    void Add(int item)
    {
        Node node = new Node(item);
        if (root == null)
        {
            root = node;
        }
        else
        {
            Node temp = root;
            while (true)
            {
                if(temp==null)break;
                if (item >= temp.date)
                {
                    if (temp.RightChild == null)
                    {
                        temp.RightChild = node;
                        node.Parent     = temp;
                        break;
                    }
                    else
                    {
                        temp = temp.RightChild;
                    }
                }
                else
                {
                    if (temp.LeftChild == null)
                    {
                        temp.LeftChild = node;
                        node.Parent    = temp;
                        break;
                    }
                    else
                    {
                        temp = temp.LeftChild;
                    }
                }
            }
        }
    }

    void PrintTree(Node item)
    {
        if(item==null)return;
        PrintTree(item.LeftChild);
        print(item.date+" ");
        PrintTree(item.RightChild);
    }
}
