using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRecord  {

	public Node node;
	public Conection conection;
	public int costSoFar;
	/*
	public bool IsIn(List<NodeRecord> nodeRecordList)
	{
		foreach (NodeRecord nodeRecord in nodeRecordList) {
			if (this == nodeRecord) {
				return true;
			}
		}
		return false;
	}

	public NodeRecord Find(List<NodeRecord> nodeRecordList)
	{
		foreach (NodeRecord nodeRecord in nodeRecordList) {
			if (this == nodeRecord) {
				return nodeRecord;
			}
		}
		return;
	}

	*/
}
