using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Data
{
    public class Bucket<V>
    {

        private int data;
        private V value;


        public Bucket(int data, V value)
        {
            this.data = data;
            this.value = value;
        }


        public int getData()
        {
            return data;
        }

        public void setData(int data)
        {
            this.data = data;
        }


        public V getValue()
        {
            return value;
        }


    }
}
