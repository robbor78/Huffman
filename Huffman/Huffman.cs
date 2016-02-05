using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
  class Huffman
  {
    public String Decode(String archive, String[] dictionary)
    {
      if (String.IsNullOrEmpty(archive)) { return ""; }
      if (dictionary == null || dictionary.Length == 0)
      {
        throw new ArgumentNullException("Dictionary may not be null.");
      }

      Node treeRoot = BuildHuffmanTree(dictionary);

      Node work = treeRoot;
      StringBuilder result = new StringBuilder();
      int length = archive.Length;
      for (int i = 0; i < length; i++)
      {
        if (work.IsLeaf)
        {
          result.Append((char)(ASCII_A + work.Value));
          work = treeRoot;
        }

        char bit = archive[i];
        if (bit == '0')
        {
          work = work.Left0;
        }
        else if (bit == '1')
        {
          work = work.Right1;
        }
        else
        {
          //this should never happen
          System.Diagnostics.Debug.Assert(false, "Invalid bit in archive. 0 or 1 expected. Actual value: " + bit);
        }
      }
      result.Append((char)(ASCII_A + work.Value));

      return result.ToString();
    }

    private Node BuildHuffmanTree(string[] dictionary)
    {
      Node root = new Node();

      int length = dictionary.Length;
      for (int i = 0; i < length; i++)
      {
        string element = dictionary[i];
        Node work = root;
        foreach (char bit in element)
        {
          if (bit == '0')
          {
            if (work.Left0 == null)
            {
              work.Left0 = new Node();
            }
            work = work.Left0;
          }
          else if (bit == '1')
          {
            if (work.Right1 == null)
            {
              work.Right1 = new Node();
            }
            work = work.Right1;
          }
          else
          {
            //this should never happen
            System.Diagnostics.Debug.Assert(false, "Invalid bit in dictionary. 0 or 1 expected. Actual value: " + bit);
          }
        }
        work.Value = i;

      }

      return root;
    }

    private readonly int ASCII_A = (int)'A';

    /// <summary>
    /// Data structure to store a left-right tree.
    /// </summary>
    class Node
    {
      internal Node()
      {
        Left0 = null;
        Right1 = null;
        Value = 0;
      }

      public Node Left0 { get; set; }
      public Node Right1 { get; set; }
      public int Value { get; set; }

      public bool IsLeaf { get { return Left0 == null && Right1 == null; } }
    }


  }
}
