using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Creaturi;
using UnityEditor;
using UnityEngine;
using System.Linq;

public static class Pathfind {

	public static float cebDist(float x1, float y1, float x2, float y2){
        return Mathf.Max(Mathf.Abs(x2 - x1), Mathf.Abs(y2 - y1));
    }

    public class Node : System.IComparable<Node>, IEquatable<Node>{
        public float x, y, f, g;
        public Node(float x, float y){
            this.x = x;
            this.y = y;
        }


        public bool Equals(Node other) {
            return (this.x == other.x) && (this.y == other.y);
        }

        int IComparable<Node>.CompareTo(Node o) {
            if (this.f < o.f) return -1;
            else if (this.f > o.f) return +1;
            else return 0;
        }
    }

    private static Creature creatura;
    private static HashSet<Node> closed;
    private static PriorityQueue<Node> open;
    private static Dictionary<Node, Node> parr;

    public static List<Vector2> pathJPS(Creature creat, float ex, float ey){
        Pathfind.creatura= creat;
        Node start= new Node((int)creatura.pos.x, (int)creatura.pos.y),
        end= new Node(ex,ey);
        List<Vector2> pt= new List<Vector2>();

        closed= new HashSet<Node>();
        open= new PriorityQueue<Node>();
        parr= new Dictionary<Node, Node>();
        start.g= 0;
        start.f= cebDist(start.x,start.y, end.x, end.y);
        open.add(start);

        while (!open.isEmpty()){
            Node curr= open.poll();
            if (curr.Equals(end)) {
                pt= makePath(parr,curr);
                return pt;
            }
            closed.Add(curr);
            Node parrent;
            parr.TryGetValue(curr, out parrent);
            foreach (Node n in succ2(curr, parrent, end)) {
                if (closed.Contains(n)) continue;
                if ( creatura.invalid(n.x, n.y)  ) continue;

                float gscore= curr.g+ 1 ;

                if (!open.contains(n) || n.g> gscore){
                    n.g= gscore;
                    n.f= gscore+ + cebDist(n.x, n.y, end.x, end.y);
                    parr.Add(n, curr);
                    if (open.contains(n))
                        open.keepHeap();
                    else
                        open.add(n);
//                    if (open.contains(n)) open.remove(n);
//                    open.add(n);
                }
            }
        }
        return pt;
    }

    private static List<Vector2> makePath(Dictionary<Node, Node> parr, Node curr) {
        List<Vector2> path= new List<Vector2>();
        path.Add(new Vector2(curr.x, curr.y));
        while (parr.ContainsKey(curr)){
            path.Add(new Vector2(parr[curr].x, parr[curr].y));
            curr= parr[curr];
        }
        path.Reverse();
        return path;
    }

    /**  jump point search*/
    private static List<Node> succ2(Node x, Node parr, Node end){
        if (parr==null)  //start node, ret all neighbors
            return new List<Node>(neighbors(x));

        int dx= (int) Mathf.Clamp(x.x-parr.x,-1,+1),
        dy= (int) Mathf.Clamp(x.y - parr.y, -1, +1);
        List<Node> pruned= prune(x, dx, dy);
        List<Node> res= new List<Node>();

        foreach (Node pr in pruned){
            dx= (int) Mathf.Clamp(pr.x-x.x,-1,+1);
            dy= (int) Mathf.Clamp(pr.y - x.y, -1, +1);
            Node n= jump(x.x, x.y, dx,dy, end.x, end.y);
            if (n!=null) res.Add(n);
        }
        return res;
    }

    /** a star succesors */
    private static Node[] neighbors(Node n){
        return new Node[]{new Node(n.x-1,n.y),  new Node(n.x+1, n.y),
            new Node(n.x-1,n.y+1), new Node(n.x, n.y+1), new Node(n.x+1, n.y+1),
            new Node(n.x-1, n.y-1), new Node(n.x, n.y-1), new Node(n.x+1, n.y-1)
        };
    }

    /** d- dir from parent*/
    private static List<Node> prune(Node n, int dx, int dy) {
        List<Node> res= new List<Node>();
        if (dx==0 || dy==0){ //line dir
            res.Add(new Node(n.x+dx, n.y+dy));

            if (creatura.invalid(n.x + dy, n.y + dx))
                res.Add(new Node(n.x+dx+dy, n.y+dx+dy));
            if (creatura.invalid(n.x - dy, n.y - dx))
                res.Add(new Node(n.x+dx-dy, n.y-dx+dy));
        } else { //diag dir
            res.Add(new Node(n.x+dx, n.y+dy));
            res.Add(new Node(n.x+dx, n.y));
            res.Add(new Node(n.x, n.y+dy));

            if (creatura.invalid(n.x, n.y - dy))
                res.Add(new Node(n.x+dx, n.y-dy));
            if (creatura.invalid(n.x - dx, n.y))
                res.Add(new Node(n.x-dx, n.y+dy));
        }
        return res;
    }

    private static Node jump(float x1, float y1, int dx, int dy, float x2, float y2) {
        float nx = x1 + dx,  ny = y1 + dy;

        if (dx == 0 || dy == 0) {     // +
            return jumpStraight(nx, ny, dx, dy, x2, y2);
        } else {                 //  x                   !!!!   Nu sare decat pe oriz/vert
            while (true) {
                if ((nx == x2) && (ny == y2))
                    return new Node(nx, ny);
                if (creatura.invalid(nx, ny))
                    return null;
                if (creatura.invalid(nx, ny - dy) || creatura.invalid(nx - dx, ny))
                    return new Node(nx, ny); //forced
                if (jumpStraight(nx, ny, dx, 0, x2, y2) != null)
                    return new Node(nx, ny);
                if (jumpStraight(nx, ny, 0, dy, x2, y2) != null)
                    return new Node(nx, ny);
                nx += dx;
                ny += dy;
            }
        }
    }

    private static Node jumpStraight(float nx, float ny, int dx, int dy, float x2, float y2) {
        while (true) {
            if ((nx == x2) && (ny == y2))
                return new Node(nx, ny);
            if (creatura.invalid(nx, ny))
                return null;
            if (creatura.invalid(nx + dy, ny + dx) || creatura.invalid(nx - dy, ny - dx))
                return new Node(nx, ny); ; //forced
            nx += dx;
            ny += dy;
        }
    }



}
