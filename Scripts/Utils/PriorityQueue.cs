using System;

public class PriorityQueue<T> where T: IComparable<T>, IEquatable<T>{
    private T[] q;
    private int n;

    public PriorityQueue() {
        q = new T[1];
        n = 0;
    }

    public bool isEmpty(){
        return n == 0;
    }

    public bool contains(T el) {
        for (int i = 1; i < n; i++) {
            if (q[i].Equals(el))
                return true;
        }
        return false;
    }


    private void resize(int l) {
        T[] tmp = new T[l];
        for (int i = 1; i <= n ; i++)
            tmp[i] = q[i];
        q = tmp;
    }

    public T peek() {
        if (this.isEmpty())
            throw new System.Exception();
        else
            return q[1];
    }

    public T poll() {
        if (this.isEmpty())
            throw new System.Exception();
        else {
            T min = q[1];
            swap(1, n--);
            sink(1);
            if ((n > 0) && (n == (q.Length - 1) / 4))
                resize(q.Length / 2);
            return min;
        }
            
    }

    
    public void add(T el) {
        if (n >= q.Length - 1)
            resize(2 * q.Length);

        q[++n] = el;
        swim(n);
    }

    private void swim(int k) {
        if (k == 1) return;
        while (k > 1 &&  q[k/2].CompareTo(q[k]) > 0 ) {
            swap(k, k / 2);
            k = k / 2;
            if (k == 1) return;
        }
    }

    private void sink(int k){
        while (2 * k <= n) {
            int j = 2 * k;
            if (j < n && q[j].CompareTo(q[j+1]) > 0)
                j++;
            if ( !(q[k].CompareTo(q[j]) > 0 ))
                break;
            swap(k, j);
            k = j;
        }
    }

    public void keepHeap(){
        for (int i = n/2; i < 0 ; i--)
            sink(i);
    }


    private void swap(int v1, int v2) {
        T a = q[v1];
        q[v1] = q[v2];
        q[v2] = a;
    }

}
