package crc646c02556064c59ab6;


public class TakenLijstListViewAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ZAPP.TakenLijstListViewAdapterViewHolder, ZAPP", TakenLijstListViewAdapterViewHolder.class, __md_methods);
	}


	public TakenLijstListViewAdapterViewHolder ()
	{
		super ();
		if (getClass () == TakenLijstListViewAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("ZAPP.TakenLijstListViewAdapterViewHolder, ZAPP", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
