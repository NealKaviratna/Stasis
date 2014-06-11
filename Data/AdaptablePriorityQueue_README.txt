

-----Below are the results of JUnit Tests that demonstrate functionality------	

	@Test
	public void emptyQueueTest()
	{
		apq = new AdaptablePriorityQueue<>();
		System.out.println("result: "+apq.isEmpty());
	}

result: true

	@Test
	public void removeTest()
	{
		apq = new AdaptablePriorityQueue<>();
		apq.add("Danger");
		
		System.out.println("peek: "+apq.peek().getValue());

		Bucket<String> rem = apq.remove();

		System.out.println("remove: "+rem);
		System.out.println("isEmpty: "+apq.isEmpty());
		System.out.println("error: "apq.peek()");
	}

peek: Danger
remove: Danger
isEmpty: True
<< Causes a Null Pointer Exception>> (remember to check if the queue is empty)

	@Test
	public void Queue0()
	{
		apq = new AdaptablePriorityQueue<>();
		apq.add("Danger");
		
		System.out.println("peek: "+apq.peek().getValue());

		Bucket<String> rem = apq.remove();

		System.out.println("remove: "+rem);
		System.out.println("isEmpty: "+apq.isEmpty());
		System.out.println("error: "apq.peek()");

		apq.add(rem.getData(), rem.getValue());
		System.out.println("peek again: "+apq.peek().getValue());
	}

peek: Danger
remove: Danger
isEmpty: True
peek again: Danger

	@Test
	public void Queue2()
	{
		apq = new AdaptablePriorityQueue<>();
		apq.add(-1,"A");

		System.out.println("negative priorities: "apq.peek().getValue());
		apq.add(-1,"B");
		apq.add("B");
		apq.add("C");

		System.out.println("default priority is 0: "+apq.peek().getValue());
		apq.add("B");
		apq.add(3,"A");
		
		System.out.println("Priorities can be incremented or decremented: "+apq.remove().getValue());
	}

negative priorities: A
default priority is 0: C
Priorities can be incremented or decremented: A


	@Test
	public void Queue3()
	{
		apq = new AdaptablePriorityQueue<>();
		apq.add("A");
		apq.add("B");
		apq.add("C");
		apq.add("D");
		apq.add("E");
		apq.add("F");
		apq.add("G");
		apq.add("H");
		apq.add("I");
		apq.add("J");
		apq.add("K");
		apq.add("L");
		apq.add("M");
		apq.add("N");
		apq.add("O");
		apq.add("P");
		System.out.println("Regrowable, arbitrarily picks FIFO during equivalent top priorites: " +apq.remove().getValue());
	}

Regrowable, arbitrarily picks FIFO during equivalent top priorities: A