// An interface that defines a method to check the rewind condition
public interface IRewindable
{
	//
	// Summary:
	//     When rewind marker is hit in the timeline, it calls this function.
	//
	// Returns:
	//     If the timeline can continue.
	bool OnRewind();
}

