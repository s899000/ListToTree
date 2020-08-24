using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListToTree
{
    public static class ListExtention
    {
        public static List<TResult> ListToTree<T, TResult>(
               this List<T> list,
               Func<T, bool> InitWhere,
               Func<TResult,T,bool> NodeWhere,
               Action<TResult, T> mapping,
               Action<TResult, TResult> action,
               List<TResult> ptlist = null,
               TResult pt = default(TResult)
           ) where TResult : new()
        {

            if (ptlist == null)
            {
                ptlist = new List<TResult>();
            }
            TResult o_PowerTree;

            if (pt == null)
            {
                var parentNodes = list.Where(p => InitWhere(p)).ToList();
                foreach (var item in parentNodes)
                {
                    o_PowerTree = new TResult();
                    mapping(o_PowerTree, item);
                    ListToTree(list, null, NodeWhere, mapping, action, ptlist, o_PowerTree);
                    ptlist.Add(o_PowerTree);
                }
            }
            else
            {
                var Nodes = list.Where(p => NodeWhere(pt,p)).ToList();
                foreach (var item in Nodes)
                {
                    o_PowerTree = new TResult();
                    mapping(o_PowerTree, item);
                    ListToTree(list, null, NodeWhere, mapping, action, ptlist, o_PowerTree);
                    action(pt, o_PowerTree);
                }
            }
            return ptlist;
        }
    }
}
