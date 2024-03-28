using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Revit.Entity.Entity
{
    public class TreeNode<T> where T : class
    {
        public T Value { get; set; }

        public List<TreeNode<T>> Childs { get; set; }=new List<TreeNode<T>>();

        public string Name { get; set; }
    }
}