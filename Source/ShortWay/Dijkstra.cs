using System;
using System.Collections;
using System.Windows.Forms;
namespace Devgis.ShortWay
{
    /// <summary>
    ///��������Dijkstra�㷨�ĺ��ģ�����Hashtable��Ϊͼ���ڽӱ�洢
    ///����SortedList��Ϊ�洢����㵽�����������Զ�����ע����Ҫ��
    ///key��length�ߵ��Ž�SortedList����ΪSortedList�ǰ�������ģ�
    ///������Щ��ʽ��������ڴ��ʹ�ú�������Ч��
    /// </summary>
    public class Dijkstra
    {
        //���浽���������̾���,ͬʱ��������Ѿ���õ��յ�
        private double[] distance;
        //�洢��ǰ�Ѿ���õ����·���յ�TNODE��length
        private SortedList sortedNodeList;
        //�洢Դ�㵽�����յ���������ĸ������·��
        private ArrayList[] paths;
        //ͼ���ڽӱ�ı�ʾ
        private Hashtable hashGraph;
        //��ʶ����ID
        private int fnode;
        //��ʶͼ�Ľڵ����
        private int nodeCount;
        public Dijkstra(Hashtable graph, int fnode, int count)
        {
            this.hashGraph = graph;
            this.fnode = fnode;
            this.nodeCount = count;
        }

        private void Initial()
        {
            //       source = new int[nodeCount];
            distance = new double[nodeCount];
            sortedNodeList = new SortedList();
            paths = new ArrayList[nodeCount];
            Hashtable hashAdj = (Hashtable)hashGraph[fnode];
            if (hashAdj == null)
            {
                throw new Exception("�㲻����!");
            }
            IDictionaryEnumerator iEnum = hashAdj.GetEnumerator();
            while (iEnum.MoveNext())
            {
                int tnode = (int)iEnum.Key;
                double length = (double)iEnum.Value;
                sortedNodeList.Add(length, tnode);
            }
            for (int i = 0; i < nodeCount; i++)
            {
                paths[i] = new ArrayList();
                paths[i].Add(fnode);
                paths[i].Add(i);
                if (i == fnode)
                    distance[i] = 0;
                else
                    distance[i] = -1;
            }
        }

        public void GenerateMinPath()
        {
            Initial();
            for (int i = 0; i < nodeCount - 2; i++)
            {
                if (sortedNodeList.Count != 0)
                {
                    double minLen = (double)sortedNodeList.GetKey(0);
                    int destination = (int)sortedNodeList.GetByIndex(0);
                    sortedNodeList.RemoveAt(0);
                    distance[destination] = minLen;
                    Hashtable hashAdj = (Hashtable)hashGraph[destination];
                    if (hashAdj == null)
                        continue;
                    IDictionaryEnumerator iEnum = hashAdj.GetEnumerator();
                    while (iEnum.MoveNext())
                    {
                        int adjNode = (int)iEnum.Key;
                        double len = (double)iEnum.Value;
                        if (distance[adjNode] == -1 && !sortedNodeList.ContainsValue(adjNode))
                        {
                            double dis = minLen + len;
                            sortedNodeList.Add(dis, adjNode);
                            paths[adjNode].RemoveRange(1, paths[adjNode].Count - 2);
                            paths[adjNode].InsertRange(1, paths[destination].GetRange(1, paths[destination].Count - 1));
                        }
                        else if (distance[adjNode] == -1 && sortedNodeList.ContainsValue(adjNode))
                        {
                            int index = sortedNodeList.IndexOfValue(adjNode);
                            double currLen = (double)sortedNodeList.GetKey(index);
                            double dis = minLen + len;
                            if (dis < currLen)
                            {
                                sortedNodeList.RemoveAt(index);
                                sortedNodeList.Add(dis, adjNode);
                                paths[adjNode].RemoveRange(1, paths[adjNode].Count - 2);
                                paths[adjNode].InsertRange(1, paths[destination].GetRange(1, paths[destination].Count - 1));
                            }
                        }
                        else
                            continue;
                    }
                }
            }
        }

        //��ô�Դ�㵽�������������̵ľ���
        public double[] GetMinDistance()
        {
            return distance;
        }

        //��ȡ��Դ�㵽�������������̾����·��
        public ArrayList[] GetAllMinPath()
        {
            return paths;
        }

        //��ȡ��Դ�㵽ָ�������̾����·��
        public ArrayList GetMinPath(int v0)
        {
            if (v0 < 0 || v0 >= nodeCount)
            {
                throw (new Exception("�ڵ㲻��ȷ"));
            }
            return paths[v0];
        }

    }
}