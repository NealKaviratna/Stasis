// AdaptablePriorityQueue implement by Chris Tansey

using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
	public class AdaptablePriorityQueue<V> {
		
		/**
				 * The backing array of this heap
				 */
		private List<Bucket<V>> dataArray;
		
		/**
				 * The number of elements that have been added to this heap
				 */
		private int size;
		
		/**
				 * Default constructor,initializes default length backing array (11)
				 */
		public AdaptablePriorityQueue() {
			// TODO Implement this.
			dataArray = new List<Bucket<V>>(11);
			size = 0;
		}
		
		/**
				 * Default add method that adds input without a priority weight to 
				 * priority Queue in realtime.
				 * for adding a response value with a default weight of 1
				 * 
				 * @param value - the response value 
				 */
		public void add(V value)
		{
			dataArray.Add(new Bucket<V>(0, value));
			size++;
			
		}
		
		/**
				 * Specific add method that adds input to the priority queue in realtime 
				 * for response values with manual priority weights
				 * 
				 * @param data sets the input manual priority weight
				 * @param value sets the response value 
				 */
		
		public void add(int data,V value) {
			// TODO Implement this.
			
			for(int i=1;i<size+1;i++)
			{
				if(dataArray[i].getValue().Equals(value)){
					dataArray[i].setData(dataArray[i].getData()+data);
					heapifyUp(i);
					
					if(size>(dataArray.Capacity/2))
						resize();
					return;
				}
			}
			
			dataArray[size+1] = new Bucket<V>(data, value);
			size++;
			heapifyUp(size);
			
			if(size>(dataArray.Capacity/2))
				resize();
		}
		
		/**
				 * resize method for to handle overflow in realtime while receiving new input
				 * resizes the backing array once the size is greater than half the size than half its length
				 * increasing its size by double its length
				 */
		
		public void resize(){
			List<Bucket<V>> resizeArray = new List<Bucket<V>>(dataArray.Capacity*2);
			
			for(int i = 0; i<dataArray.Capacity;i++)
			{
				resizeArray[i]= dataArray[i];
			}
			dataArray= resizeArray;
		}
		
		/**
				 * checks if the Priority Queue is handling any data
				 * 
				 * @return true if the Queue is empty and false if it is not
				 */
		
		public bool isEmpty() {
			// TODO Implement this.
			if(size==0)
				return true;
			return false;
		}
		
		/**
				 * method for checking the response value without resetting its priority 
				 * @return the most important response value available
				 */
		
		public Bucket<V> peek() {
			// TODO Implement this.
			if(size>0)
				return dataArray[1];
			return null;
		}
		
		/**
				 * method for returning the response value with the highest priority, discarding its priority
				 * 
				 * @return the response value to be removed from the Queue     
				 */
		
		public Bucket<V> remove() {
			// TODO Implement this.
			if(size>0)
			{
				Bucket<V> temp = dataArray[1];
				
				if(size>1){
					
					swap(1,size);
					dataArray[size] = null; 
					size--;
					heapifyDown(1);
					return temp;
				}
				
				dataArray[size]= null;
				size--;
				return temp;
			}
			return null;
		}
		
		/**
				 * returns the number of elements inside the Priority Queue
				 * @return the current size
				 */
		public int getSize() {
			// TODO Implement this.
			return size;
		}
		
		/**
				 * bubbles up the Priority Queue, reorganizing them with the root being the highest priority
				 * and the children are the lower priority than its parent
				 * 
				 * @param arrayIndex the index that is being compared to its parent's value
				 */
		
		private void heapifyUp(int arrayIndex)
		{
			if(arrayIndex==1) return;
			
			if(dataArray[arrayIndex].getData()>(dataArray[arrayIndex/2].getData()))
			{
				swap(arrayIndex,arrayIndex/2);
				heapifyUp(arrayIndex/2);
			}
		}
		
		/**
				 * Bubbles down the Priority Queue in order to replace a parent
				 * with a child of greater priority
				 * 
				 * @param arrayIndex the index of the parent being compared to its children
				 */
		
		private void heapifyDown(int arrayIndex)
		{
			if(arrayIndex>=(size)) return;
			
			
			if(((arrayIndex*2+1)>size)||dataArray[arrayIndex*2].getData()>(dataArray[(arrayIndex*2)+1].getData())) //check if left is less than right
			{
				if(dataArray[arrayIndex].getData()<(dataArray[arrayIndex*2].getData()))
				{
					swap(arrayIndex,arrayIndex*2);
					heapifyDown(arrayIndex*2);
				}
			}else //right is less than left
			{
				if(dataArray[arrayIndex].getData()<(dataArray[(arrayIndex*2)+1].getData()))
				{
					swap(arrayIndex,(arrayIndex*2)+1);
					heapifyDown((arrayIndex*2)+1);
				}
			}
		}
		
		/**
				 * Swaps two different response values in the Priority Queue
				 * 
				 * @param putOn - response1 being swapped
				 * @param pulledOff - response2 being swapped
				 */
		
		private void swap(int putOn, int pulledOff)
		{
			Bucket<V> temp = dataArray[pulledOff];
			dataArray[pulledOff] = dataArray[putOn];
			dataArray[putOn] = temp;
		}
		
	}
}